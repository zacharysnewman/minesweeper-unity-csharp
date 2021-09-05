using System;
using System.Collections.Generic;
using System.Linq;

public struct Map : IEquatable<Map>
{
    public readonly MapInformation MapInfo;
    public readonly Dictionary<Coords, Tile> Tiles;

    public Map(MapInformation mapInfo, Dictionary<Coords, Tile> tiles)
    {
        this.MapInfo = mapInfo;
        this.Tiles = tiles;
    }

    public bool Equals(Map other) => this.Tiles == other.Tiles;
    public override bool Equals(Object other) => this.Equals((Map)other);
    public override int GetHashCode() => base.GetHashCode();
    public static bool operator ==(Map a, Map b) => a.Equals(b);
    public static bool operator !=(Map a, Map b) => !a.Equals(b);

    public static Map GenerateNewMap(MapInformation mapInfo) => GenerateNewMap(mapInfo, (false, Coords.Zero));

    private static Map GenerateNewMap(MapInformation mapInfo, (bool, Coords) excludedTileInfo)
    {
        var (doExcludeTile, excludedTileCoords) = excludedTileInfo;

        List<Tile> allTiles = new List<Tile>();

        for (int x = 0; x < mapInfo.Width; x++)
        {
            for (int y = 0; y < mapInfo.Height; y++)
            {
                var newTileCoords = new Coords(x, y);
                if (doExcludeTile && excludedTileCoords == newTileCoords)
                {
                    continue;
                }
                allTiles.Add(new Tile(newTileCoords));
            }
        }
        var shuffledTiles = Shuffler.Shuffle(allTiles);
        var bombsToAdd = mapInfo.Bombs;
        var tilesWithBombs = shuffledTiles.Select(x => bombsToAdd-- > 0 ? x.With(isBomb: true) : x).ToList();

        if (doExcludeTile)
        {
            tilesWithBombs.Add(new Tile(excludedTileCoords));
        }

        return new Map(mapInfo, tilesWithBombs.ToDictionary((x) => x.coords));
    }

    public bool CanActivateTile(Coords coords) => this.Tiles.ContainsKey(coords);
    public Map WithActivatedTile(Coords coords, bool flagMode)
    {
        if (this.Tiles.Select(x => x.Value).All(x => x.tileState == TileState.hidden))
        {
            var newMap = Map.GenerateNewMap(this.MapInfo, (true, coords));
            var newTiles = new Dictionary<Coords, Tile>(newMap.Tiles);
            newTiles = Activate(newTiles, coords, flagMode, primaryActivation: true);
            return new Map(newMap.MapInfo, newTiles);
        }
        else
        {
            var newTiles = new Dictionary<Coords, Tile>(this.Tiles);
            newTiles = Activate(newTiles, coords, flagMode, primaryActivation: true);

            return new Map(this.MapInfo, newTiles);
        }
    }

    public static Dictionary<Coords, Tile> Activate(Dictionary<Coords, Tile> tiles, Coords coords, bool flagMode, bool primaryActivation = false)
    {
        var newTiles = new Dictionary<Coords, Tile>(tiles);
        var tile = newTiles[coords];
        var nearbyActivateableTiles = GetNearbyTiles(newTiles, tile.coords)
            .Where((x) => x.tileState == TileState.hidden);

        switch (tile.tileState)
        {
            case TileState.hidden:
                newTiles[coords] = tile.With(flagMode ? TileState.flagged : TileState.revealed);

                if (!flagMode && IsNotNearAnyBombs(tiles, coords) && !tile.isBomb)
                {
                    newTiles = ActivateAll(newTiles, nearbyActivateableTiles);
                }
                break;
            case TileState.flagged:
                newTiles[coords] = flagMode ? tile.With(TileState.hidden) : newTiles[coords];
                break;
            case TileState.revealed:
                if ((GetNearbyFlagCount(tiles, coords) >= GetNearbyBombCount(tiles, coords)) &&
                    (flagMode || IsNotNearAnyBombs(tiles, coords) || primaryActivation))
                {
                    newTiles = ActivateAll(newTiles, nearbyActivateableTiles);
                }
                break;
        }
        return newTiles;
    }

    public static Dictionary<Coords, Tile> ActivateAll(Dictionary<Coords, Tile> tiles, IEnumerable<Tile> nearbyTiles)
    {
        var newTiles = new Dictionary<Coords, Tile>(tiles);
        foreach (var t in nearbyTiles)
        {
            newTiles = Activate(newTiles, t.coords, false);
        }
        return newTiles;
    }

    public static bool IsNotNearAnyBombs(Dictionary<Coords, Tile> tiles, Coords coords) => GetNearbyBombCount(tiles, coords) == 0;
    public static int GetNearbyBombCount(Dictionary<Coords, Tile> tiles, Coords coords) =>
        GetNearbyTiles(tiles, coords).Where(x => x.isBomb).Count();
    public static int GetNearbyFlagCount(Dictionary<Coords, Tile> tiles, Coords coords) =>
        GetNearbyTiles(tiles, coords).Where(x => x.tileState == TileState.flagged).Count();


    private static IEnumerable<Tile> GetNearbyTiles(Dictionary<Coords, Tile> tiles, Coords coords)
    {
        var col = coords.x;
        var row = coords.y;

        return new Tile?[]{
                TryGetTile(tiles, new Coords(col-1,row-1)),
                TryGetTile(tiles, new Coords(col-1,row)),
                TryGetTile(tiles, new Coords(col-1,row+1)),
                TryGetTile(tiles, new Coords(col,row-1)),
				//TryGetTile(tiles, new Coords(col,row)),
				TryGetTile(tiles, new Coords(col,row+1)),
                TryGetTile(tiles, new Coords(col+1,row-1)),
                TryGetTile(tiles, new Coords(col+1,row)),
                TryGetTile(tiles, new Coords(col+1,row+1))
        }
        .Where(x => x != null)
        .Select(x => (Tile)x);
    }

    private static Tile? TryGetTile(Dictionary<Coords, Tile> tiles, Coords coords)
    {
        try
        {
            return tiles[coords];
        }
        catch
        {
            return null;
        }
    }
}
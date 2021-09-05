using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Events.StateEvents;


public class RendererBehaviour : MonoBehaviour
{
    public GameObject tilePrefab;
    public Tileset tileset;

    public Camera sceneCamera;
    public GameObject winDisplayUI;
    public GameObject loseDisplayUI;
    public Text bombCountText;

    public Transform tileParent;
    public GameObject[] tileObjects;

    private void Start()
    {
        EventAggregator.Get<StateChangedEvent>().Subscribe(OnStateChanged);
    }

    public void OnStateChanged(State newState)
    {
        foreach (GameObject tileObject in this.tileObjects)
        {
            Destroy(tileObject);
        }

        var (mapInfo, tilesDict) = newState;
        var tiles = tilesDict.Select(tileKeyValuePair => tileKeyValuePair.Value);
        this.bombCountText.text = (mapInfo.Bombs - tiles.Where(x => x.tileState == TileState.flagged).Count()).ToString();

        var winLoseStatus = WinOrLoseCheck(tiles);
        switch (winLoseStatus)
        {
            case WinLoseStatus.none:
                break;
            case WinLoseStatus.win:
                this.winDisplayUI.SetActive(true);
                break;
            case WinLoseStatus.lose:
                this.loseDisplayUI.SetActive(true);
                break;
        }

        this.tileObjects = tiles
            .Select((tile) => CreateTile(tilesDict, tile, winLoseStatus))
            .ToArray();

        if (tiles.All(x => x.tileState == TileState.hidden)) // First
        {
            this.sceneCamera.transform.position = new Vector3((float)mapInfo.Width / 2f, (float)mapInfo.Height / 2f - 0.5f, this.sceneCamera.transform.position.z);
        }
    }

    public enum WinLoseStatus { none, win, lose }
    private WinLoseStatus WinOrLoseCheck(IEnumerable<Tile> tiles)
    {
        var isBombRevealed = tiles.Any(x => x.isBomb && x.tileState == TileState.revealed);
        if (isBombRevealed)
        {
            return WinLoseStatus.lose;
        }
        var allTilesAreRevealed = tiles.All(x => x.isBomb || (!x.isBomb && x.tileState == TileState.revealed));
        if (allTilesAreRevealed)
        {
            return WinLoseStatus.win;
        }

        return WinLoseStatus.none;
    }

    public GameObject CreateTile(Dictionary<Coords, Tile> tiles, Tile tile, WinLoseStatus winLoseStatus)
    {
        var tileObject = GameObject.Instantiate(this.tilePrefab, tile.coords, Quaternion.identity, this.tileParent);
        tileObject.GetComponent<SpriteRenderer>().sprite = GetSpriteForTile(tiles, tile, winLoseStatus);
        return tileObject;
    }

    public Sprite GetSpriteForTile(Dictionary<Coords, Tile> tiles, Tile tile, WinLoseStatus winLoseStatus)
    {
        Sprite sprite = this.tileset.hidden;
        switch (tile.tileState)
        {
            case TileState.hidden:
                if (tile.isBomb && winLoseStatus == WinLoseStatus.lose)
                {
                    sprite = this.tileset.bombRevealed;
                }
                else
                {
                    sprite = this.tileset.hidden;
                }
                break;
            case TileState.revealed:
                if (tile.isBomb)
                {
                    sprite = this.tileset.bombDetonated;
                }
                else
                {
                    var bombCount = Map.GetNearbyBombCount(tiles, tile.coords);
                    sprite = this.tileset.nearbyBombs[bombCount];
                }
                break;
            case TileState.flagged:
                if (!tile.isBomb && winLoseStatus == WinLoseStatus.lose)
                {
                    sprite = this.tileset.bombIncorrect;
                }
                else
                {
                    sprite = this.tileset.flagged;
                }
                break;
        }
        return sprite;
    }
}

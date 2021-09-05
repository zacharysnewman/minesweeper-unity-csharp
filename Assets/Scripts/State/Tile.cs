using System.Collections;
using System.Collections.Generic;

public struct Tile
{
    public readonly Coords coords;
    public readonly TileState tileState;
    public readonly bool isBomb;

    public Tile(Coords coords)
    {
        this.coords = coords;
        this.tileState = TileState.hidden;
        this.isBomb = false;
    }

    public Tile(Coords coords, TileState tileState, bool isBomb)
    {
        this.coords = coords;
        this.tileState = tileState;
        this.isBomb = isBomb;
    }

    public Tile With(TileState newTileState)
    {
        return new Tile(this.coords, newTileState, this.isBomb);
    }
    public Tile With(bool isBomb)
    {
        return new Tile(this.coords, this.tileState, isBomb);
    }

    public override string ToString() => $"{{ ({this.coords.x},{this.coords.y}), {this.isBomb} }}";
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Events.StateEvents;

public struct State
{
    public readonly Map map;

    private State(Map map)
    {
        this.map = map;
        EventAggregator.Get<StateChangedEvent>().Publish(this);
    }

    public State WithMap(Map newMap) => this.map == newMap ? this : new State(newMap);
    public State WithActivatedTile(Coords coords, bool flagMode) =>
        this.map.CanActivateTile(coords) ? this.WithMap(this.map.WithActivatedTile(coords, flagMode)) : this;

    public void Deconstruct(out MapInformation mapInfo, out Dictionary<Coords, Tile> tiles)
    {
        mapInfo = this.map.MapInfo;
        tiles = this.map.Tiles;
    }
}

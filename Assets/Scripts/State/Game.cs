using System;
using System.Collections;
using System.Collections.Generic;
using Events.UnityEvents;
using Events.InputEvents;

public static class Game
{
    public static State state = new State();

    public static void Init()
    {
        EventAggregator.Get<GenerateMapEvent>().Subscribe(OnGenerateMap);
        EventAggregator.Get<ActivateTileEvent>().Subscribe(OnActivateTile);
    }

    private static void OnGenerateMap(MapInformation mapInfo) =>
        Game.state = Game.state.WithMap(Map.GenerateNewMap(mapInfo));

    private static void OnActivateTile(Coords coords, bool flagMode) =>
        Game.state = Game.state.WithActivatedTile(coords, flagMode);
}

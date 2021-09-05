using System.Collections;
using UnityEngine;
using Events.UnityEvents;
using Events.InputEvents;

// UI Input > EntryBehaviour > Game > RenderBehaviour > UI Display

public class EntryBehaviour : MonoBehaviour
{
    private void Start() => Game.Init();

    private Vector3 GetWorldPosition(Vector3 UIPosition) => Camera.main.ScreenToWorldPoint(UIPosition);
    private Coords GetTileCoords(Vector3 worldPosition) => new Coords(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y));

    public void OnLeftClick() => EventAggregator.Get<ActivateTileEvent>()
        .Publish(GetTileCoords(GetWorldPosition(Input.mousePosition)), false);
    public void OnRightClick() => EventAggregator.Get<ActivateTileEvent>()
    .Publish(GetTileCoords(GetWorldPosition(Input.mousePosition)), true);

    private int mapWidth = 10, mapHeight = 10, bombs = 10;
    public void OnMapWidthChanged(float rawMapWidth) => this.mapWidth = (int)rawMapWidth;
    public void OnMapHeightChanged(float rawMapHeight) => this.mapHeight = (int)rawMapHeight;
    public void OnBombCountChanged(float rawBombCount) => this.bombs = (int)rawBombCount;
    public void OnGenerateMapClick() => EventAggregator.Get<GenerateMapEvent>()
        .Publish(new MapInformation(this.mapWidth, this.mapHeight, this.bombs));
}

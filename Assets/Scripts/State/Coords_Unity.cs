using UnityEngine;

public partial struct Coords
{
    public static implicit operator Vector2(Coords coords) => new Vector2(coords.x, coords.y);
    public static implicit operator Vector3(Coords coords) => new Vector3(coords.x, coords.y);
}

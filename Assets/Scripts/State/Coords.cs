using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Coords : IEquatable<Coords>
{
    public readonly int x, y;

    public Coords(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public HashSet<Coords> Neighbors
    {
        get
        {
            var (x, y) = this;
            return new HashSet<Coords>()
            {
                (x-1,y-1),(x-1,y),(x-1,y+1),
                (x,y-1),/*	(x,y),*/(x,y+1),
                (x+1,y-1),(x+1,y),(x+1,y+1)
            };
        }
    }

    public int LivingNeighborCount(HashSet<Coords> livingCells) =>
    this.Neighbors
        .Where((x) => livingCells.Contains(x))
        .Count();

    public override string ToString() => $"({x},{y})";

    public static Coords Zero = new Coords(0, 0);

    public void Deconstruct(out int x, out int y)
    {
        x = this.x;
        y = this.y;
    }

    public bool Equals(Coords other) => this.x == other.x && this.y == other.y;
    public override bool Equals(object obj) => this.Equals((Coords)obj);
    public override int GetHashCode() => base.GetHashCode();

    public static bool operator ==(Coords a, Coords b) => a.Equals(b);
    public static bool operator !=(Coords a, Coords b) => !a.Equals(b);

    public static implicit operator Coords((int, int) t) => new Coords(t.Item1, t.Item2);

    public static implicit operator Vector2(Coords coords) => new Vector2(coords.x, coords.y);
    public static implicit operator Vector3(Coords coords) => new Vector3(coords.x, coords.y);
}
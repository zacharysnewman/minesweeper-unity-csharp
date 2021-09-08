using System;
using System.Collections.Generic;
using System.Linq;

public partial struct Coords : IEquatable<Coords>
{
    public readonly int x, y;

    public Coords(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

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
}
public struct MapInformation
{
    public readonly int Width;
    public readonly int Height;
    public readonly int Bombs;

    public MapInformation(int width, int height, int bombs)
    {
        this.Width = width;
        this.Height = height;
        this.Bombs = bombs;
    }

    public override string ToString() => $"{{ Width: {this.Width}, Height: {this.Height}, Bombs: {this.Bombs} }}";
}

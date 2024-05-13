using Godot;

public partial class Globals : Node
{
    public float ScrollSpeed { get; set; } = 2.0f;
    public float InitialDistanceBetweenPipes { get; } = 120.0f;
    public float InitialGapBetweenPipes { get; } = 48.0f;
}
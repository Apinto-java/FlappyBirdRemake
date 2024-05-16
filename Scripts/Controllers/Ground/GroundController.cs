using Godot;

public partial class GroundController : Node
{
    public Ground Ground { get; set; }
    public GroundScrollCommand ScrollCommand { get; set; } = new();

    public GroundController(Ground ground)
    {
        Ground = ground;
    }
}
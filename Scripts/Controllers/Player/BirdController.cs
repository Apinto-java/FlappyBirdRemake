using Godot;

public partial class BirdController : Node
{
    public Player Player { get; set; }
    public BirdJumpCommand JumpCommand { get; set; } = new();
    public BirdRotateCommand RotateCommand { get; set; } = new();

    public BirdController(Player player)
    {
        Player = player;
    }
}
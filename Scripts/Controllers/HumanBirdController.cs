using Godot;

public partial class HumanBirdController : BirdController
{
    public HumanBirdController(Player player) : base(player)
    {
    }

    public override void _PhysicsProcess(double delta)
    {
        if(Input.IsActionJustPressed("jump"))
            Player.Jump();
    }
}
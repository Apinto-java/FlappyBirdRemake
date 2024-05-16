using Godot;

public partial class PhysicsGroundController : GroundController
{
    private Globals _globals;

    public PhysicsGroundController(Ground ground) : base(ground)
    {
    }

    public override void _Ready()
    {
        _globals = GetNode<Globals>("/root/Globals");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 newScroll = Ground.ScrollOffset;
		newScroll.X -= _globals.ScrollSpeed;
        ScrollCommand.Execute(Ground, new GroundScrollParams { ScrollOffset = newScroll });
    }
}
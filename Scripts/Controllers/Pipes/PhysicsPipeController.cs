using Godot;

public partial class PhysicsPipeController : PipeController
{
    private Globals _globals;

    public PhysicsPipeController(Pipes pipes) : base(pipes)
    {
    }

    public override void _Ready()
    {
        _globals = GetNode<Globals>("/root/Globals");
    }

    public override void _PhysicsProcess(double delta)
    {
        var newPosition = Pipes.GlobalPosition;
        newPosition.X -= _globals.ScrollSpeed;
        ScrollCommand.Execute(Pipes, new PipeScrollParams { Position = newPosition});
    }
}
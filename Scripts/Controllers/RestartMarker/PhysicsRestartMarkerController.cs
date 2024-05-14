using Godot;

public partial class PhysicsRestartMarkerController : RestartMarkerController
{
    private Globals _globals;

    public PhysicsRestartMarkerController(RestartMarker restartMarker) : base(restartMarker)
    {
    }

    public override void _Ready()
    {
        _globals = GetNode<Globals>("/root/Globals");
    }

    public override void _PhysicsProcess(double delta)
    {
        var newPosition = RestartMarker.GlobalPosition;
		newPosition.X -= _globals.ScrollSpeed;
        MoveCommand.Execute(RestartMarker, new RestartMarkerMoveCommandParams() { Position = newPosition} );
    }
}
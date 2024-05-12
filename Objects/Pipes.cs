using Godot;

public partial class Pipes : Node2D
{
    private Globals _globals { get; set; }

    /// <summary>
	///	Called when the node enters the scene tree for the first time. 
	///	Initializes all necessary variables and sets Physics Processing to false.
	/// </summary>
    public override void _Ready()
    {
        // The physics process is enabled afterwards
        SetPhysicsProcess(false);
        _globals = GetNode<Globals>("/root/Globals");
    }

    /// <summary>
	/// Called every frame, 'delta' is the time elapsed since the last frame.
	/// Moves the pipe in the x axis by <c>- Globals.ScrollSpeed </c>
	/// </summary>
	/// <param name="delta">Time elapsed since the last frame.</param>
    public override void _PhysicsProcess(double delta)
    {
        var newPosition = this.GlobalPosition;
        newPosition.X -= _globals.ScrollSpeed;
        this.GlobalPosition = newPosition;
    }
}
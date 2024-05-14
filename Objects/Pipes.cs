using System.Drawing;
using Godot;

public partial class Pipes : Node2D
{
    private Globals _globals { get; set; }
    private Pipe UpperPipe { get; set; }
    private Pipe LowerPipe { get; set; }
    private CollisionShape2D _scoreAreaCollisionShape { get; set; }

    /// <summary>
	///	Called when the node enters the scene tree for the first time. 
	///	Initializes all necessary variables and sets Physics Processing to false.
	/// </summary>
    public override void _Ready()
    {
        // The physics process is enabled afterwards
        SetPhysicsProcess(false);
        _globals = GetNode<Globals>("/root/Globals");
        UpperPipe = GetNode<Pipe>("UpperPipe");
        LowerPipe = GetNode<Pipe>("LowerPipe");
        _scoreAreaCollisionShape = GetNode<CollisionShape2D>("ScoreArea/ScoreCollisionShape");

        SetGap();
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

    public void SetGap()
    {
        GD.Print($"Current gap: {_globals.CurrentGapBetweenPipesInPixels}");
        UpperPipe.Position = new Vector2(UpperPipe.Position.X, UpperPipe.Position.Y - _globals.CurrentGapBetweenPipesInPixels);
        LowerPipe.Position = new Vector2(LowerPipe.Position.X, LowerPipe.Position.Y + _globals.CurrentGapBetweenPipesInPixels);
        var rectShape = _scoreAreaCollisionShape.Shape as RectangleShape2D;
        rectShape.Size = new Vector2(UpperPipe.GetPipeWidth(), _globals.CurrentGapBetweenPipesInPixels * 2);
    }
}
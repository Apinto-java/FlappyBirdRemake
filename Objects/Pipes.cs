using System.Drawing;
using Godot;

public partial class Pipes : Node2D
{
    private Globals _globals { get; set; }
    private Pipe UpperPipe { get; set; }
    private Pipe LowerPipe { get; set; }
    private CollisionShape2D _scoreAreaCollisionShape { get; set; }
    private VisibleOnScreenNotifier2D _visibilityNotifier;

    private Node _controllerContainer;
    public PipeController Controller {get; private set;}

    /// <summary>
	///	Called when the node enters the scene tree for the first time. 
	///	Initializes all necessary variables and sets Physics Processing to false.
	/// </summary>
    public override void _Ready()
    {
        _controllerContainer = GetNode<Node>("ControllerContainer");
        _globals = GetNode<Globals>("/root/Globals");
        UpperPipe = GetNode<Pipe>("UpperPipe");
        LowerPipe = GetNode<Pipe>("LowerPipe");
        _scoreAreaCollisionShape = GetNode<CollisionShape2D>("ScoreArea/ScoreCollisionShape");
        _visibilityNotifier = GetNode<VisibleOnScreenNotifier2D>("VisibilityNotifier");
        _visibilityNotifier.ScreenExited += () => OnPipesExitedScreen();

        SetGap();
    }

    public void SetController(PipeController pipeController)
    {
		foreach(var child in _controllerContainer.GetChildren())
		{
			child.QueueFree();
		}

		if(pipeController == null)
			return;

		Controller = pipeController;
		_controllerContainer.AddChild(Controller);
    }

    public void MoveToPosition(Vector2 position)
    {
        this.GlobalPosition = position;
    }

    public void SetGap()
    {
        GD.Print($"Current gap: {_globals.CurrentGapBetweenPipesInPixels}");
        UpperPipe.GlobalPosition = new Vector2(UpperPipe.GlobalPosition.X, UpperPipe.GlobalPosition.Y - _globals.CurrentGapBetweenPipesInPixels);
        LowerPipe.GlobalPosition = new Vector2(LowerPipe.GlobalPosition.X, LowerPipe.GlobalPosition.Y + _globals.CurrentGapBetweenPipesInPixels);
        var rectShape = _scoreAreaCollisionShape.Shape as RectangleShape2D;
        rectShape.Size = new Vector2(UpperPipe.GetPipeWidth(), _globals.CurrentGapBetweenPipesInPixels * 2);
    }

    private void OnPipesExitedScreen()
    {
        GD.Print("Pipes exited screen");
        this.SetController(null);
        this.QueueFree();
    }
}
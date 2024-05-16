using Godot;
using System;

public partial class MainLevel : Node2D
{
	private Player _player { get; set; }
	private Ground _ground { get; set; }
	private RestartMarker _restartMarker { get; set; }
	private Area2D _restartArea { get; set; }
	private Area2D _groundArea;
	private Node _pipesContainer;
	private Marker2D _restartMarkerPosition;

	[Export] public PackedScene PipesScene { get; set; }

	private Globals _globals;

	private int _score;
	private bool _gameOver = false;
	private bool GameOver 
	{
		get => _gameOver;
		set => _gameOver = value;
	}

	/// <summary>
	///	Called when the node enters the scene tree for the first time. 
	///	Initializes all necessary variables.
	/// </summary>
	public override void _Ready()
	{
		_globals = GetNode<Globals>("/root/Globals");
		_score = 0;

		_player = GetNode<Player>("Player");
		_player.SetController(new HumanBirdController(_player));
		_player.PlayerHitWall += () => PlayerHitWall();

		_ground = GetNode<Ground>("Ground");
		_ground.SetController(new PhysicsGroundController(_ground));
		_groundArea = GetNode<Area2D>("Areas/GroundArea");
		_groundArea.BodyEntered += (body) => PlayerTouchedGround(body);
		
		_restartMarkerPosition = GetNode<Marker2D>("RestartMarkerPosition");
		_restartArea = GetNode<Area2D>("Areas/RestartArea");
		_restartArea.BodyEntered += (body) => OnRestartMarkerHit(body);

		_restartMarker = GetNode<RestartMarker>("RestartMarker");

		_pipesContainer = GetNode<Node>("PipesContainer");

		ChangeRestartMarkerPosition();
	}

    /// <summary>
    /// Called every frame, 'delta' is the time elapsed since the last frame.
    /// Checks whether the jump input has been pressed, if that's the case, the physics process
    /// for the player and for the pipes is enabled.
    /// </summary>
    /// <param name="delta">Time elapsed since the last frame.</param>
    public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("restart"))
			GetTree().ReloadCurrentScene();

		if(Input.IsActionJustPressed("jump") && !GameOver)
		{
			// _player.SetController(new HumanBirdController(_player));
			var pipes = GetTree().GetNodesInGroup("pipes");
			foreach(var pipe in pipes)
			{
				pipe.SetPhysicsProcess(true);
			}
			_restartMarker.SetController(new PhysicsRestartMarkerController(_restartMarker));
		}
	}

	/// <summary>
	/// Executed when the player touched the ground.
	/// It stops physics processing for the Player, the Pipes and the Ground.
	/// </summary>
    private void PlayerTouchedGround(Node2D body)
	{
		if(body is not Player)
			return;

		DisablePhysicsProcess();
	}

	/// <summary>
	/// Executed when the player collides with a wall.
	/// It stops physics processing for the Player, the Pipes and the Ground.
	/// </summary>
	private void PlayerHitWall()
	{
		DisablePhysicsProcess();
	}

	/// <summary>
	/// Disables the Physics Process for the player, the Pipes and the Ground.
	/// It also stops the player animation.
	/// </summary>
	private void DisablePhysicsProcess()
	{
		GameOver = true;
		_player.SetController(null);
		_player.StopAnimation();
		var pipes = GetTree().GetNodesInGroup("pipes");
		foreach(var pipe in pipes)
		{
			if(pipe is not Pipes pipeObj)
				continue;

			pipeObj.SetController(null);
		}
		_restartMarker.SetController(null);
		_ground.SetController(null);
	}

	private void OnRestartMarkerHit(Node2D node)
	{
		GD.Print($"Node {node} collided with restart area");
		if (node is not RestartMarker)
			return;

		_restartMarker.GlobalPosition = _restartMarkerPosition.GlobalPosition;

		var pipe = PipesScene.Instantiate<Pipes>();
		var pipePosition = new Vector2(_restartMarker.GlobalPosition.X, GD.RandRange(60, 120));
		pipe.GlobalPosition = pipePosition;
		CallDeferred(MainLevel.MethodName.AddChild, pipe);
		pipe.CallDeferred(Pipes.MethodName.SetController, new PhysicsPipeController(pipe));
	}

	private void ChangeRestartMarkerPosition()
	{
		var viewportSize = GetViewportRect().Size;
		_restartMarkerPosition.GlobalPosition = new Vector2(
				viewportSize.X + _globals.InitialDistanceBetweenPipes, 
				_restartMarkerPosition.GlobalPosition.Y
			);
		_restartMarker.GlobalPosition = _restartMarkerPosition.GlobalPosition;
	}
}

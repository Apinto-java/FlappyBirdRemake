using Godot;
using System;

public partial class MainLevel : Node2D
{
	private Player _player { get; set; }
	private Ground _ground { get; set; }
	private RestartMarker _restartMarker { get; set; }
	private Area2D _restartArea { get; set; }

	private int _score;

	/// <summary>
	///	Called when the node enters the scene tree for the first time. 
	///	Initializes all necessary variables.
	/// </summary>
	public override void _Ready()
	{
		_score = 0;

		_player = GetNode<Player>("Player");
		_player.SetController(new HumanBirdController(_player));
		_player.PlayerHitWall += () => PlayerHitWall();

		_ground = GetNode<Ground>("Ground");
		_ground.OnPlayerTouchedGound += () => PlayerTouchedGround();

		_restartArea = GetNode<Area2D>("RestartArea");
		_restartArea.BodyEntered += (body) => OnPipeEnteredRestartArea(body);
		_restartMarker = GetNode<RestartMarker>("RestartMarker");
	}

	/// <summary>
	/// Called every frame, 'delta' is the time elapsed since the last frame.
	/// Checks whether the jump input has been pressed, if that's the case, the physics process
	/// for the player and for the pipes is enabled.
	/// </summary>
	/// <param name="delta">Time elapsed since the last frame.</param>
	public override void _Process(double delta)
	{
		if(!_player.IsPhysicsProcessing() && Input.IsActionJustPressed("jump"))
		{
			_player.SetPhysicsProcess(true);
			var pipes = GetTree().GetNodesInGroup("pipes");
			foreach(var pipe in pipes)
			{
				pipe.SetPhysicsProcess(true);
			}
			_restartMarker.SetPhysicsProcess(true);
		}
	}

	/// <summary>
	/// Executed when the player touched the ground.
	/// It stops physics processing for the Player, the Pipes and the Ground.
	/// </summary>
    private void PlayerTouchedGround()
	{
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
		_player.SetPhysicsProcess(false);
		_player.StopAnimation();
		var pipes = GetTree().GetNodesInGroup("pipes");
		foreach(var pipe in pipes)
		{
			pipe.SetPhysicsProcess(false);
		}
		_restartMarker.SetPhysicsProcess(false);
		_ground.SetPhysicsProcess(false);
	}

	private void OnPipeEnteredRestartArea(Node2D node)
	{
		GD.Print("Body entered restart zone");
		if (node is not RestartMarker)
			return;

		GD.Print("Restart marker passed");
	}
}

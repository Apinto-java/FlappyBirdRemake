using Godot;
using System;

public partial class MainLevel : Node2D
{
	private Player _player { get; set; }
	private Ground _background { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = GetNode<Player>("Player");
		_player.SetController(new HumanBirdController(_player));

		_background = GetNode<Ground>("Ground");
		_background.OnPlayerTouchedGound += () => PlayerTouchedGround();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void PlayerTouchedGround()
	{
		GD.Print("Level: The player touched the ground");
	}
}

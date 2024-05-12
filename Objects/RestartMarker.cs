using Godot;
using System;

public partial class RestartMarker : CharacterBody2D
{

	private Globals _globals;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetPhysicsProcess(false);
		_globals = GetNode<Globals>("/root/Globals");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		var newPosition = this.GlobalPosition;
		newPosition.X -= _globals.ScrollSpeed;
		this.GlobalPosition = newPosition;
	}
}

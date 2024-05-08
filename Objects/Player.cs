using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float Gravity = 20.0f;
	[Export] public float MaxFallingSpeed = 30.0f;
	[Export] public float AccelerationPerFrame = 1.0f;
	[Export] public float JumpImpulse = -10.0f;

	private BirdController _controller;
	private Node _controllerContainer;

	public override void _Ready()
	{
		this.Velocity = new Vector2(0.0f, Gravity);

		_controllerContainer = GetNode<Node>("ControllerContainer");
	}

	public override void _PhysicsProcess(double delta)
	{
		var newFallingSpeed = Math.Min(Velocity.Y + AccelerationPerFrame, MaxFallingSpeed);
		Vector2 newVelocity = Velocity;
		newVelocity.Y = newFallingSpeed;
		Velocity = newVelocity;
		GD.Print($"Velocity length: {this.Velocity.Length()}");
		MoveAndSlide();
	}

	public void Jump()
	{
		Velocity = new Vector2(Velocity.X, JumpImpulse);
	}

	public void SetController(BirdController birdController)
	{
		foreach(var child in _controllerContainer.GetChildren())
		{
			child.QueueFree();
		}

		_controller = birdController;
		_controllerContainer.AddChild(_controller);
	}
}

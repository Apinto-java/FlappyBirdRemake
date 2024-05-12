using Godot;
using System;

public partial class Player : CharacterBody2D
{
	/// <summary>
	/// Gravity to be applied each frame
	/// </summary>
	[Export] public float Gravity = 10.0f;
	/// <summary>
	/// Max falling speed of the player
	/// </summary>
	[Export] public float MaxFallingSpeed = 200.0f;
	/// <summary>
	/// Vertical force to be applied when the player hits the "jump" button.
	/// </summary>
	[Export] public float JumpImpulse = -200.0f;

	private const float MaxRotationInDegrees = 75.0f;
	private const float MinRotationInDegrees = -75.0f;

	private float _positiveAngleProportion;
	private float _negativeAngleProportion;

	private BirdController _controller;
	private Node _controllerContainer;
	private AnimatedSprite2D _animatedSprite;

	[Signal]
	public delegate void PlayerHitWallEventHandler();

	/// <summary>
	///	Called when the node enters the scene tree for the first time. 
	///	Initializes all necessary variables and sets Physics Processing to false.
	///	Starts the Player "flying" animation.
	/// </summary>
	public override void _Ready()
	{
		// Physics Process is disabled by default and re-enabled by the level when an input has been pressed
		SetPhysicsProcess(false);
		_controllerContainer = GetNode<Node>("ControllerContainer");

		_animatedSprite = GetNode<AnimatedSprite2D>("Sprite");
		_animatedSprite.Play("flying");
		
		_positiveAngleProportion =  Mathf.Abs( MinRotationInDegrees / MaxFallingSpeed);
		_negativeAngleProportion = Mathf.Abs( MaxRotationInDegrees / JumpImpulse );
		GD.Print($"Positive angle proportion: {_positiveAngleProportion}, Negative angle proportion: {_negativeAngleProportion}");
	}

	/// <summary>
	/// Called every frame, 'delta' is the time elapsed since the last frame.
	///	Applies gravity to the player on every physics frame. 
	///	Rotates the player sprite according to its current
	///	vertical velocity.
	///	If the player collided with a pipe, <c>PlayerHitWall</c> is emmited.
	/// </summary>
	/// <param name="delta">Time elapsed since the last frame.</param>
	public override void _PhysicsProcess(double delta)
	{

		var newFallingSpeed = Math.Min(Velocity.Y + Gravity, MaxFallingSpeed);
		Vector2 newVelocity = Velocity;
		newVelocity.Y = newFallingSpeed;
		Velocity = newVelocity;
		_animatedSprite.RotationDegrees = GetRotationByVerticalVelocity(Velocity.Y);
		MoveAndSlide();
		for(int i = 0; i < GetSlideCollisionCount(); i++)
		{
			GD.Print($"Collision # {i}");
			var collision = GetSlideCollision(i);
			if(collision != null && collision.GetCollider() is Pipe)
			{
				EmitSignal(SignalName.PlayerHitWall);
			}
		}
	}

	/// <summary>
	/// Given the current vertical velocity of the bird, returns its corresponding rotation in degrees.
	/// </summary>
	/// <param name="verticalVelocity">Vertical velocity of the Bird</param>
	/// <returns>A float representing rotation in degrees that the bird should have given its vertical velocity</returns>
	private float GetRotationByVerticalVelocity(float verticalVelocity)
	{
		//	If the bird is jumping
		if(verticalVelocity < 0)
		{
			return _negativeAngleProportion * verticalVelocity;
		}

		//	If the bird is falling
		if(verticalVelocity > 0)
		{
			var result = _positiveAngleProportion * verticalVelocity;
			return result;
		}

		return 0.0f;
	}

	/// <summary>
	/// Applies a vertical Impulse to the Player based on <c>JumpImpulse</c>
	/// </summary>
	public void Jump()
	{
		Velocity = new Vector2(Velocity.X, JumpImpulse);
	}

	/// <summary>
	/// Stops all animations being played by the Player's <c>AnimatedSprite2D</c>
	/// </summary>
	public void StopAnimation()
	{
		_animatedSprite.Stop();
	}

	/// <summary>
	/// Sets the Controller for the Player and removes the previous one.
	/// </summary>
	/// <param name="birdController">BirdController to be assigned to this Player.</param>
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

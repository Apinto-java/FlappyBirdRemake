using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float Gravity = 10.0f;
	[Export] public float MaxFallingSpeed = 200.0f;
	[Export] public float JumpImpulse = -200.0f;

	private const float MaxRotationInDegrees = 75.0f;
	private const float MinRotationInDegrees = -75.0f;

	private float _positiveAngleProportion;
	private float _negativeAngleProportion;

	private BirdController _controller;
	private Node _controllerContainer;
	private AnimatedSprite2D _animatedSprite;

	public override void _Ready()
	{

		_controllerContainer = GetNode<Node>("ControllerContainer");

		_animatedSprite = GetNode<AnimatedSprite2D>("Sprite");
		_animatedSprite.Play("flying");
		
		_positiveAngleProportion =  Mathf.Abs( MinRotationInDegrees / MaxFallingSpeed);
		_negativeAngleProportion = Mathf.Abs( MaxRotationInDegrees / JumpImpulse );
		GD.Print($"Positive angle proportion: {_positiveAngleProportion}, Negative angle proportion: {_negativeAngleProportion}");
	}

	public override void _PhysicsProcess(double delta)
	{
		var newFallingSpeed = Math.Min(Velocity.Y + Gravity, MaxFallingSpeed);
		Vector2 newVelocity = Velocity;
		newVelocity.Y = newFallingSpeed;
		Velocity = newVelocity;
		_animatedSprite.RotationDegrees = GetRotationByVerticalVelocity(Velocity.Y);
		MoveAndSlide();
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
			GD.Print(result);
			return result;
		}

		return 0.0f;
	}

	 // Función de mapeo lineal
    private float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
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

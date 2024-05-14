using Godot;
using System;

public partial class HumanBirdController : BirdController
{
    /// <summary>
    /// Force applied when the jump key is pressed
    /// </summary>
    private float JumpImpulse = -200.0f;
    /// <summary>
	/// Gravity to be applied each frame
	/// </summary>
	private float Gravity = 10.0f;
	/// <summary>
	/// Max falling speed of the player
	/// </summary>
	private float MaxFallingSpeed = 200.0f;

    private const float MaxRotationInDegrees = 75.0f;
	private const float MinRotationInDegrees = -75.0f;

    private float _positiveAngleProportion = 0;
    private float _negativeAngleProportion = 0;

    public HumanBirdController(Player player) : base(player)
    {
    }

    public override void _Ready()
    {
        _positiveAngleProportion =  Mathf.Abs( MinRotationInDegrees / MaxFallingSpeed);
		_negativeAngleProportion = Mathf.Abs( MaxRotationInDegrees / JumpImpulse );
    }

    public override void _PhysicsProcess(double delta)
    {
        //  Calculate current bird's velocity
        var newFallingSpeed = Math.Min(Player.Velocity.Y + Gravity, MaxFallingSpeed);
        Vector2 newVelocity = Player.Velocity;
		newVelocity.Y = newFallingSpeed;
		Player.Velocity = newVelocity; 
        Player.MoveAndSlide();

        for(int i = 0; i < Player.GetSlideCollisionCount(); i++)
		{
			var collision = Player.GetSlideCollision(i);
			if(collision != null && collision.GetCollider() is Pipe)
			{
				Player.EmitSignal(Player.SignalName.PlayerHitWall);
			}
		}

        // Make the bird jump if the "jump" key is pressed.
        if(Input.IsActionJustPressed("jump"))
            JumpCommand.Execute(Player, new BirdJumpParams() { Impulse = JumpImpulse });

        //  Rotate the Bird according to its velocity,.
        RotateCommand.Execute(Player, new BirdRotateParams() { RotationDegrees = GetRotationByVerticalVelocity(Player.Velocity.Y)});
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
}
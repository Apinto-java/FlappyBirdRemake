using FlappyBirdRemake.Controllers.PlayerControllers;
using Godot;

namespace FlappyBirdRemake.Objects
{
	public partial class Player : CharacterBody2D
	{
		public BirdController Controller { get; private set; }
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
			_controllerContainer = GetNode<Node>("ControllerContainer");

			_animatedSprite = GetNode<AnimatedSprite2D>("Sprite");
			_animatedSprite.Play("flying");
		}


		/// <summary>
		/// Applies a vertical Impulse to the Player based on <c>JumpImpulse</c>
		/// </summary>
		public void Jump(float impulse)
		{
			Velocity = new Vector2(Velocity.X, impulse);
		}

		public void SetRotation(float rotationDegrees)
		{
			this.RotationDegrees = rotationDegrees;
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

			if(birdController == null)
				return;

			Controller = birdController;
			_controllerContainer.AddChild(Controller);
		}

		public override void _ExitTree()
		{
			Dispose();
		}
	}
}

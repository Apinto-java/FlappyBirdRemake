using FlappyBirdRemake.Objects;
using Godot;
using System;

namespace FlappyBirdRemake.Controllers.PlayerControllers
{
	public partial class HumanBirdController : BirdController
	{
		public HumanBirdController(Player player) : base(player)
		{
		}

		public override void _PhysicsProcess(double delta)
		{
			ApplyGravity();
			
			for(int i = 0; i < Player.GetSlideCollisionCount(); i++)
			{
				var collision = Player.GetSlideCollision(i);
				if(collision != null && collision.GetCollider() is Pipe)
				{
					Player.EmitSignal(Player.SignalName.PlayerHitWall);
				}
				collision.Dispose();
			}

			ApplyRotationBasedOnVelocity();
			
		}

		public override void _UnhandledInput(InputEvent @event)
		{
			if(!@event.IsActionPressed("jump"))
			{
				@event.Dispose();
				return;
			}

			@event.Dispose();
			JumpCommand.Execute(Player, _jumpParams);
		}

		
	}
}
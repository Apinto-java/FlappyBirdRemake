using FlappyBirdRemake.Objects;
using Godot;

namespace FlappyBirdRemake.Controllers.PlayerControllers
{
    public partial class DeathAnimationController : BirdController
    {
        public DeathAnimationController(Player player) : base(player)
        {
        }

        public override void _Ready()
        {
            base._Ready();

            //  Change collision mask and layer
            Player.CollisionLayer = 2;
            Player.CollisionMask = 2;

            //  Increment Z-Index
            Player.ZIndex = 2;

            //  Jump
            JumpCommand.Execute(Player, _jumpParams);
        }

        public override void _PhysicsProcess(double delta)
        {
            //  Fall to the ground which is on Collision mask and layer 2
            ApplyGravity();
            ApplyRotationBasedOnVelocity();
        }

        public override void _ExitTree()
        {
            Dispose();
        }
    }
}
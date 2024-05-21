using FlappyBirdRemake.Commands.PlayerCommands;
using FlappyBirdRemake.Objects;
using Godot;
using System;

namespace FlappyBirdRemake.Controllers.PlayerControllers {
    public partial class BirdController : Node
    {
        public Player Player { get; set; }
        public BirdJumpCommand JumpCommand { get; set; } = new();
        public BirdRotateCommand RotateCommand { get; set; } = new();

        protected float _positiveAngleProportion = 0;
        protected float _negativeAngleProportion = 0;
        protected const float MaxRotationInDegrees = 75.0f;
        protected const float MinRotationInDegrees = -75.0f;
        protected BirdRotateParams _rotationParams = new();
        protected BirdJumpParams _jumpParams = new();

        /// <summary>
        /// Force applied when the jump key is pressed
        /// </summary>
        protected float JumpImpulse = -150.0f;
        /// <summary>
        /// Gravity to be applied each frame
        /// </summary>
        protected float Gravity = 10.0f;
        /// <summary>
        /// Max falling speed of the player
        /// </summary>
        protected float MaxFallingSpeed = 200.0f;

        public BirdController(Player player)
        {
            Player = player;
        }

        public override void _Ready()
        {
            _positiveAngleProportion =  Mathf.Abs( MinRotationInDegrees / MaxFallingSpeed);
            _negativeAngleProportion = Mathf.Abs( MaxRotationInDegrees / JumpImpulse );

            _jumpParams.Impulse = JumpImpulse;
        }
        
        protected void ApplyRotationBasedOnVelocity()
        {
            _rotationParams.RotationDegrees = GetRotationByVerticalVelocity(Player.Velocity.Y);
            //  Rotate the Bird according to its velocity,.
            RotateCommand.Execute(Player, _rotationParams);
        }

        protected void ApplyGravity()
        {
            var newFallingSpeed = Math.Min(Player.Velocity.Y + Gravity, MaxFallingSpeed);
            Vector2 newVelocity = Player.Velocity;
            newVelocity.Y = newFallingSpeed;
            Player.Velocity = newVelocity; 
            Player.MoveAndSlide();
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

        public override void _ExitTree()
        {
            JumpCommand.Dispose();
            RotateCommand.Dispose();
            Dispose();
        }
    }
}
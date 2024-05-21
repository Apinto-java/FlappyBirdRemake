using Godot;
using FlappyBirdRemake.Objects;
using FlappyBirdRemake.Commands.GroundCommands;

namespace FlappyBirdRemake.Controllers.GroundControllers
{
    public partial class PhysicsGroundController : GroundController
    {
        private float _scrollSpeed;
        private GroundScrollParams _scrollParams = new();

        public PhysicsGroundController(Ground ground) : base(ground)
        {
        }

        public override void _Ready()
        {
            _scrollSpeed = GetNode<Globals>("/root/Globals").ScrollSpeed;
        }

        public override void _PhysicsProcess(double delta)
        {
            Vector2 newScroll = Ground.ScrollOffset;
            newScroll.X -= _scrollSpeed;
            _scrollParams.ScrollOffset = newScroll;
            ScrollCommand.Execute(Ground, _scrollParams);
        }
    }
}

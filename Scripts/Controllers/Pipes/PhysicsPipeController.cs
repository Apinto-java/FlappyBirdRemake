using FlappyBirdRemake.Commands.PipesCommands;
using FlappyBirdRemake.Objects;
using Godot;

namespace FlappyBirdRemake.Controllers.PipesControllers
{
    public partial class PhysicsPipeController : PipeController
    {
        private float _scrollSpeed;
        private PipeScrollParams _scrollParams = new();

        public PhysicsPipeController(Pipes pipes) : base(pipes)
        {
        }

        public override void _Ready()
        {
            _scrollSpeed = GetNode<Globals>("/root/Globals").ScrollSpeed;
        }

        public override void _PhysicsProcess(double delta)
        {
            var newPosition = Pipes.GlobalPosition;
            newPosition.X -= _scrollSpeed;
            _scrollParams.Position = newPosition;
            ScrollCommand.Execute(Pipes, _scrollParams);
        }
    }
}
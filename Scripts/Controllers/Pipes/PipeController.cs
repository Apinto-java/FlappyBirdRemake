using FlappyBirdRemake.Commands.PipesCommands;
using FlappyBirdRemake.Objects;
using Godot;

namespace FlappyBirdRemake.Controllers.PipesControllers
{
    public partial class PipeController : Node 
    {
        public Pipes Pipes { get; set; }
        public PipeScrollCommand ScrollCommand { get; set; } = new();

        public PipeController(Pipes pipes)
        {
            Pipes = pipes;
        }

        public override void _ExitTree()
        {
            ScrollCommand.Dispose();
            Dispose();
        }
    }
}
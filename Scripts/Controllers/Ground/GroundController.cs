using FlappyBirdRemake.Commands.GroundCommands;
using FlappyBirdRemake.Objects;
using Godot;

namespace FlappyBirdRemake.Controllers.GroundControllers
{
    public partial class GroundController : Node
    {
        public Ground Ground { get; set; }
        public GroundScrollCommand ScrollCommand { get; set; } = new();

        public GroundController(Ground ground)
        {
            Ground = ground;
        }

        public override void _ExitTree()
        {
            ScrollCommand.Dispose();
            Dispose();
        }
    }
}

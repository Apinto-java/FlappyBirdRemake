using FlappyBirdRemake.Commands.RestartMarkerCommands;
using FlappyBirdRemake.Objects;
using Godot;

namespace FlappyBirdRemake.Controllers.RestartMarkerControllers
{
    public partial class RestartMarkerController : Node
    {
        public RestartMarker RestartMarker { get; set; }
        public RestartMarkerMoveCommand MoveCommand { get; set; } = new();

        public RestartMarkerController(RestartMarker restartMarker)
        {
            RestartMarker = restartMarker;
        }

        public override void _ExitTree()
        {
            MoveCommand.Dispose();
            Dispose();
        }
    }
}
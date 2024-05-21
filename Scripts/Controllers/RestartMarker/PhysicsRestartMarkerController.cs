using FlappyBirdRemake.Commands.RestartMarkerCommands;
using FlappyBirdRemake.Objects;
using Godot;

namespace FlappyBirdRemake.Controllers.RestartMarkerControllers
{
    public partial class PhysicsRestartMarkerController : RestartMarkerController
    {
        private float _scrollSpeed;
        private RestartMarkerMoveCommandParams _moveParams = new();

        public PhysicsRestartMarkerController(RestartMarker restartMarker) : base(restartMarker)
        {
        }

        public override void _Ready()
        {
            _scrollSpeed = GetNode<Globals>("/root/Globals").ScrollSpeed;
        }

        public override void _PhysicsProcess(double delta)
        {
            var newPosition = RestartMarker.GlobalPosition;
            newPosition.X -= _scrollSpeed;
            _moveParams.Position = newPosition;
            MoveCommand.Execute(RestartMarker, _moveParams );
        }
    }
}
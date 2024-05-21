using System;
using FlappyBirdRemake.Objects;
using Godot;

namespace FlappyBirdRemake.Commands.RestartMarkerCommands
{
    public partial class RestartMarkerMoveCommand : RestartMarkerCommand
    {
        public override void Execute(RestartMarker marker, GodotObject data)
        {
            ArgumentNullException.ThrowIfNull(marker);

            if(data is not RestartMarkerMoveCommandParams param)
                return;

            marker.Move(param.Position);
        }
    }

    public partial class RestartMarkerMoveCommandParams : GodotObject
    {
        public Vector2 Position { get; set; }   
    }
}
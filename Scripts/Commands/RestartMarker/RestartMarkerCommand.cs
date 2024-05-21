using FlappyBirdRemake.Objects;
using Godot;

namespace FlappyBirdRemake.Commands.RestartMarkerCommands
{
    public abstract partial class RestartMarkerCommand : GodotObject
    {
        public abstract void Execute(RestartMarker marker, GodotObject data);
    }
}
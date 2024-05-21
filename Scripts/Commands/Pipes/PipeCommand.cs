using FlappyBirdRemake.Objects;
using Godot;

namespace FlappyBirdRemake.Commands.PipesCommands
{
    public abstract partial class PipeCommand : GodotObject
    {
        public abstract void Execute(Pipes pipes, GodotObject data);
    }
}
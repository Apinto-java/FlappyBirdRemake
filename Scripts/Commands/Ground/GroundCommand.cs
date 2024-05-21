using FlappyBirdRemake.Objects;
using Godot;

namespace FlappyBirdRemake.Commands.GroundCommands
{
    public abstract partial class GroundCommand : GodotObject 
    {
        public abstract void Execute(Ground ground, GodotObject data);
    }
}
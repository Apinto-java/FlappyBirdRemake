using FlappyBirdRemake.Objects;
using Godot;

namespace FlappyBirdRemake.Commands.PlayerCommands
{
    public abstract partial class BirdCommand : GodotObject
    {
        public abstract void Execute(Player player, GodotObject data);
    }
}
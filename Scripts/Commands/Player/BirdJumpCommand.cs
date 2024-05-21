using FlappyBirdRemake.Objects;
using Godot;

namespace FlappyBirdRemake.Commands.PlayerCommands
{
    public partial class BirdJumpCommand : BirdCommand
    {
        public override void Execute(Player player, GodotObject data)
        {
            if(data is not BirdJumpParams jumpParams)
                return;

            player.Jump(jumpParams.Impulse);
        }
    }

    public partial class BirdJumpParams : GodotObject
    {
        public float Impulse { get; set; }
    }
}
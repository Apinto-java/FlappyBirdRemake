using FlappyBirdRemake.Objects;
using Godot;
using System;

namespace FlappyBirdRemake.Commands.PlayerCommands
{
    public partial class BirdRotateCommand : BirdCommand
    {
        public override void Execute(Player player, GodotObject data)
        {
            ArgumentNullException.ThrowIfNull(player);

            if(data is not BirdRotateParams rotateParams)
                return;

            player.SetRotation(rotateParams.RotationDegrees);
        }
    }

    public partial class BirdRotateParams : GodotObject
    {
        public float RotationDegrees { get; set; }
    }
}
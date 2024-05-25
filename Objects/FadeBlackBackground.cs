using Godot;
using System;

namespace FlappyBirdRemake.Objects.UI
{
	public partial class FadeBlackBackground : CanvasLayer
	{
		private AnimationPlayer _animationPlayer;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_animationPlayer = GetNode<AnimationPlayer>("FadeToBlackBacground/FadeToBlackAnimationPlayer");
		}

		public void FadeIn()
		{
			_animationPlayer.Play("fade_in");
		}

		public void FadeOut()
		{
			_animationPlayer.Play("fade_out");
		}


        public override void _ExitTree()
        {
            Dispose();
        }
    }
}

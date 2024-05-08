using Godot;
using System;

public partial class Ground : ParallaxBackground
{

    public override void _Process(double delta)
    {
		Vector2 newScroll = this.ScrollOffset;
		newScroll.X -= 2.0f;
        this.ScrollOffset = newScroll;
    }



}

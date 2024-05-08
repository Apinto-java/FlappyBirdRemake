using Godot;
using System;

public partial class background : ParallaxBackground
{
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ScrollOffset = new Vector2(ScrollOffset.X - 40.0f * (float)delta, 0.0f);
	}
}

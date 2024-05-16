using Godot;
using System;

public partial class GroundScrollCommand : GroundCommand
{
    public override void Execute(Ground ground, GodotObject data)
    {
        ArgumentNullException.ThrowIfNull(ground);

        if(data is not GroundScrollParams scrollParams)
            return;

        ground.Scroll(scrollParams.ScrollOffset);
    }
}

public partial class GroundScrollParams : GodotObject 
{
    public Vector2 ScrollOffset { get; set; }
}
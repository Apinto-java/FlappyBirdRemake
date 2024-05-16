using Godot;
using System;

public partial class PipeScrollCommand : PipeCommand
{
    public override void Execute(Pipes pipes, GodotObject data)
    {
        ArgumentNullException.ThrowIfNull(pipes);

        if(data is not PipeScrollParams scrollParams)
            return;

        pipes.MoveToPosition(scrollParams.Position);
    }
}

public partial class PipeScrollParams : GodotObject 
{
    public Vector2 Position { get; set; }
}
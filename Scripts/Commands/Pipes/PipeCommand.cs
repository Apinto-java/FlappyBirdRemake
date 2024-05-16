using Godot;

public abstract partial class PipeCommand : GodotObject
{
    public abstract void Execute(Pipes pipes, GodotObject data);
}
using Godot;

public abstract partial class GroundCommand : GodotObject 
{
    public abstract void Execute(Ground ground, GodotObject data);
}
using Godot;

public abstract partial class BirdCommand : GodotObject
{
    public abstract void Execute(Player player, GodotObject data);
}
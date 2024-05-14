using Godot;

public abstract partial class RestartMarkerCommand : GodotObject
{
    public abstract void Execute(RestartMarker marker, GodotObject data);
}
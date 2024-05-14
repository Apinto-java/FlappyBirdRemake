using Godot;

public partial class RestartMarkerController : Node
{
    public RestartMarker RestartMarker { get; set; }
    public RestartMarkerMoveCommand MoveCommand { get; set; } = new();

    public RestartMarkerController(RestartMarker restartMarker)
    {
        RestartMarker = restartMarker;
    }
}
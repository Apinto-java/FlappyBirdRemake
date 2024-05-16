using Godot;

public partial class PipeController : Node 
{
    public Pipes Pipes { get; set; }
    public PipeScrollCommand ScrollCommand { get; set; } = new();

    public PipeController(Pipes pipes)
    {
        Pipes = pipes;
    }
}
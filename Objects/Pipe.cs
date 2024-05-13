using Godot;

public partial class Pipe : StaticBody2D
{
    private Sprite2D Sprite { get; set; }

    public override void _Ready()
    {
        Sprite = GetNode<Sprite2D>("Sprite");
    }

    public int GetPipeWidth() 
    {
        return Sprite.Texture.GetWidth();
    }
}
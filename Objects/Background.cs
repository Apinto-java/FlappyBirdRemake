using Godot;
using System;

public partial class Background : ParallaxBackground
{

    private Area2D _groundCollider { get; set; }
    [Signal] public delegate void OnPlayerTouchedGoundEventHandler();

    public override void _Ready()
    {
        _groundCollider = GetNode<Area2D>("Area2D");
        _groundCollider.BodyEntered += OnBodyEntered;
    }

    public override void _Process(double delta)
    {
		Vector2 newScroll = this.ScrollOffset;
		newScroll.X -= 2.0f;
        this.ScrollOffset = newScroll;
    }

    private void OnBodyEntered(Node2D body)
    {
        GD.Print("Body touched ground");
        EmitSignal(SignalName.OnPlayerTouchedGound);
    }

}

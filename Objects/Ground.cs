using Godot;
using System;

public partial class Ground : ParallaxBackground
{

    private Area2D _groundCollider { get; set; }
    [Signal] public delegate void OnPlayerTouchedGoundEventHandler();

    private Globals _globals;

    public override void _Ready()
    {
        _groundCollider = GetNode<Area2D>("Area2D");
        _groundCollider.BodyEntered += OnBodyEntered;
        _globals = GetNode<Globals>("/root/Globals");
    }

    public override void _PhysicsProcess(double delta)
    {
		Vector2 newScroll = this.ScrollOffset;
		newScroll.X -= _globals.ScrollSpeed;
        this.ScrollOffset = newScroll;
    }

    private void OnBodyEntered(Node2D body)
    {

        if(body is Player)
            EmitSignal(SignalName.OnPlayerTouchedGound);
    }

}

using Godot;
using System;

public partial class RestartMarker : CharacterBody2D
{
	private Node _controllerContainer;
	public RestartMarkerController Controller { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_controllerContainer = GetNode<Node>("ControllerContainer");
		SetPhysicsProcess(false);
	}

	public void SetController(RestartMarkerController restartMarkerController)
	{
		foreach(var child in _controllerContainer.GetChildren())
		{
			child.QueueFree();
		}

		if(restartMarkerController == null)
			return;
			
		Controller = restartMarkerController;
		_controllerContainer.AddChild(Controller);
	}

	/// <summary>
	/// Moves this Restart Marker to the specified position
	/// </summary>
	/// <param name="position"><c>Vector2</c> representing the new position of the RestartMarker</param>
	public void Move(Vector2 position)
	{
		this.GlobalPosition = position;
	}
}

using FlappyBirdRemake.Controllers.GroundControllers;
using Godot;
using System;

namespace FlappyBirdRemake.Objects
{
    public partial class Ground : ParallaxBackground
    {
        [Signal] public delegate void OnPlayerTouchedGoundEventHandler();
        private Node _controllerContainer;
        public GroundController Controller { get; private set; }

        public override void _Ready()
        {
            _controllerContainer = GetNode<Node>("ControllerContainer");
        }

        public void SetController(GroundController groundController)
        {
            foreach(var child in _controllerContainer.GetChildren())
            {
                child.QueueFree();
            }

            if(groundController == null)
                return;

            Controller = groundController;
            _controllerContainer.AddChild(Controller);
        }

        public void Scroll(Vector2 scrollOffset)
        {
            this.ScrollOffset = scrollOffset;
        }

        public override void _ExitTree()
        {
            Dispose();
        }

    }
}

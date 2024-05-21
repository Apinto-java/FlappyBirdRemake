using System;
using System.Drawing;
using FlappyBirdRemake.Controllers.PipesControllers;
using Godot;

namespace FlappyBirdRemake.Objects
{
    public partial class Pipes : Node2D
    {
        private Pipe UpperPipe { get; set; }
        private Pipe LowerPipe { get; set; }
        private float _currentGapInPixels;
        private Area2D _scoreArea;
        [Signal] public delegate void PlayerHitScoreAreaEventHandler();
        private CollisionShape2D _scoreAreaCollisionShape { get; set; }
        private VisibleOnScreenNotifier2D _visibilityNotifier;

        private Node _controllerContainer;
        public PipeController Controller {get; private set;}

        /// <summary>
        ///	Called when the node enters the scene tree for the first time. 
        ///	Initializes all necessary variables and sets Physics Processing to false.
        /// </summary>
        public override void _Ready()
        {
            _controllerContainer = GetNode<Node>("ControllerContainer");
            UpperPipe = GetNode<Pipe>("UpperPipe");
            LowerPipe = GetNode<Pipe>("LowerPipe");
            _scoreArea = GetNode<Area2D>("ScoreArea");
            _scoreArea.BodyEntered += (body) => OnPlayerHitScoreArea(body);

            _scoreAreaCollisionShape = GetNode<CollisionShape2D>("ScoreArea/ScoreCollisionShape");
            _visibilityNotifier = GetNode<VisibleOnScreenNotifier2D>("VisibilityNotifier");
            _visibilityNotifier.ScreenExited += () => OnPipesExitedScreen();

            _currentGapInPixels = GetNode<Globals>("/root/Globals").CurrentGapBetweenPipesInPixels;
            SetGap();
        }

        public void SetController(PipeController pipeController)
        {
            foreach(var child in _controllerContainer.GetChildren())
            {
                child.QueueFree();
            }

            if(pipeController == null)
                return;

            Controller = pipeController;
            _controllerContainer.AddChild(Controller);
        }

        public void MoveToPosition(Vector2 position)
        {
            this.GlobalPosition = position;
        }

        public void SetGap()
        {
            UpperPipe.GlobalPosition = new Vector2(UpperPipe.GlobalPosition.X, UpperPipe.GlobalPosition.Y - _currentGapInPixels);
            LowerPipe.GlobalPosition = new Vector2(LowerPipe.GlobalPosition.X, LowerPipe.GlobalPosition.Y + _currentGapInPixels);
            var rectShape = _scoreAreaCollisionShape.Shape as RectangleShape2D;
            rectShape.Size = new Vector2(UpperPipe.GetPipeWidth(), _currentGapInPixels * 2);
        }

        private void OnPipesExitedScreen()
        {
            this.SetController(null);
            this.QueueFree();
        }

        public override void _ExitTree()
        {
            Dispose();
        }

        private void OnPlayerHitScoreArea(Node2D body)
        {
            if(body is not Player)
                return;

            EmitSignal(SignalName.PlayerHitScoreArea);
            _scoreAreaCollisionShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        }
    }
}
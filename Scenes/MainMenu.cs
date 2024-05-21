using FlappyBirdRemake.Controllers.GroundControllers;
using FlappyBirdRemake.Objects;
using Godot;
using System;

namespace FlappyBirdRemake.Scenes
{
	public partial class MainMenu : CanvasLayer
	{
		private Ground _ground;
		private AnimatedSprite2D _bird;
		private AnimationPlayer _titleAnimationPlayer;

		private Button _startButton;

		[Export(PropertyHint.File)] public string MainLevelScenePath;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_ground = GetNode<Ground>("Ground");
			_ground.SetController(new PhysicsGroundController(_ground));

			_bird = GetNode<AnimatedSprite2D>("TitleContainer/Bird");
			_bird.Play("flying");

			_titleAnimationPlayer = GetNode<AnimationPlayer>("TitleContainer/TitleAnimationPlayer");
			_titleAnimationPlayer.Play("title_animation");

			_startButton = GetNode<Button>("StartButton");
			_startButton.Pressed += OnStartButtonPressed;
		}

		private void OnStartButtonPressed()
		{
			GetTree().ChangeSceneToFile(MainLevelScenePath);
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}

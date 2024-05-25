using FlappyBirdRemake.Controllers.GroundControllers;
using FlappyBirdRemake.Objects;
using FlappyBirdRemake.Objects.Sound;
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

		private AudioPlayer _audioPlayer;
		private Timer _startGameTimer;

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

			_startGameTimer = GetNode<Timer>("StartGameTimer");
			_startGameTimer.Timeout += OnStartGameTimerTimeout;

			_audioPlayer = GetNode<AudioPlayer>("AudioPlayer");
		}

		private void OnStartGameTimerTimeout()
		{
			
			GetTree().ChangeSceneToFile(MainLevelScenePath);
		}

		private void OnStartButtonPressed()
		{
			_audioPlayer.PlayStartSound();
			_startGameTimer.Start();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}

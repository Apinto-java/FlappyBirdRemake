using System;
using System.Collections.Generic;
using System.Linq;
using FlappyBirdRemake.Controllers.GroundControllers;
using FlappyBirdRemake.Controllers.PipesControllers;
using FlappyBirdRemake.Controllers.PlayerControllers;
using FlappyBirdRemake.Controllers.RestartMarkerControllers;
using FlappyBirdRemake.Objects;
using FlappyBirdRemake.Objects.Sound;
using FlappyBirdRemake.Objects.UI;
using FlappyBirdRemake.Utils;
using Godot;
using static Godot.Area2D;

namespace FlappyBirdRemake.Scenes
{
	public partial class MainLevel : Node2D
	{
		private Player _player { get; set; }
		private Ground _ground { get; set; }
		private RestartMarker _restartMarker { get; set; }
		private Area2D _restartArea { get; set; }
		private Area2D _groundArea;
		private Area2D _upperBoundaryArea;
		private Marker2D _restartMarkerPosition;
		private HUD _hud;

		[Export] public PackedScene PipesScene { get; set; }

		private float _distanceBetweenPipes;

		private int _score;
		private int Score 
		{
			get => _score;
			set 
			{
				_score = value;
				_hud?.SetScore(value);
			}
		}
		private bool _gameOver = false;
		private bool _gameHasStarted = false;
		private bool GameOver 
		{
			get => _gameOver;
			set => _gameOver = value;
		}

		private readonly ScoreFileManager _scoreFileManager = new();

		private AudioPlayer _audioPlayer;
		private Timer _gameOverSoundTimer;

		/// <summary>
		///	Called when the node enters the scene tree for the first time. 
		///	Initializes all necessary variables.
		/// </summary>
		public override void _Ready()
		{
			
			_distanceBetweenPipes = GetNode<Globals>("/root/Globals").CurrentDistanceBetweenPipes;
			Score = 0;

			_player = GetNode<Player>("Player");
			_player.PlayerHitWall += PlayerHitWall;
			_player.PlayerJump += PlayerJumped;

			_ground = GetNode<Ground>("Ground");
			_ground.SetController(new PhysicsGroundController(_ground));
			_groundArea = GetNode<Area2D>("Areas/GroundArea");
			_groundArea.BodyEntered += PlayerTouchedGround;

			_restartMarkerPosition = GetNode<Marker2D>("RestartMarkerPosition");
			_restartArea = GetNode<Area2D>("Areas/RestartArea");
			_restartArea.BodyEntered += OnRestartMarkerHit;

			_upperBoundaryArea = GetNode<Area2D>("Areas/UpperBoundaryArea");
			_upperBoundaryArea.BodyEntered += OnPlayerHitUpperBoundary;

			_restartMarker = GetNode<RestartMarker>("RestartMarker");
			_hud = GetNode<HUD>("HUD");
			_hud.SetGetReadyVisibility(true);
			_audioPlayer = GetNode<AudioPlayer>("AudioPlayer");

			_gameOverSoundTimer = GetNode<Timer>("GameOverSoundTimer");
			_gameOverSoundTimer.Timeout += OnGameOverSoundTimer;
			ChangeRestartMarkerPosition();
		}

		private void PlayerJumped()
		{
			_audioPlayer.PlayFlapSound();
		}

		public override void _Input(InputEvent @event)
		{

			if(@event.IsActionPressed("restart"))
			{
				ReloadScene();
			}

			if(@event.IsActionPressed("jump") && !_gameHasStarted)
			{
				_gameHasStarted = true;
				// Jump action
				_player.SetController(new HumanBirdController(_player));
				_hud.SetGetReadyVisibility(false);
				_restartMarker.SetController(new PhysicsRestartMarkerController(_restartMarker));
			}

			// Dispose of the event once it has been used
			@event.Dispose();
		}

		private void ReloadScene()
		{
			_groundArea.BodyEntered -= PlayerTouchedGround;
			_restartArea.BodyEntered -= OnRestartMarkerHit;
			var nodeChildren = this.GetChildren();
			foreach(var child in nodeChildren)
			{
				child.QueueFree();
				child.Dispose();
			}
			
			GetTree().ReloadCurrentScene();
		}

		/// <summary>
		/// Executed when the player touched the ground.
		/// It stops physics processing for the Player, the Pipes and the Ground.
		/// </summary>
		private void PlayerTouchedGround(Node2D body)
		{
			if(body is not Player)
				return;

			OnGameOver();
		}

		/// <summary>
		/// Executed when the player collides with a wall.
		/// It stops physics processing for the Player, the Pipes and the Ground.
		/// </summary>
		private void PlayerHitWall()
		{
			OnGameOver();
		}

		private void OnPlayerHitUpperBoundary(Node2D body)
		{
			if(body is not Player)
				return;

			OnGameOver();
		}

		private void OnGameOver()
		{
			_audioPlayer.PlayHitSound();
			_gameOverSoundTimer.Start();
			GameOver = true;
			DisablePhysicsProcess();
			GameOverUI();
		}

		private void OnGameOverSoundTimer()
		{
			_audioPlayer.PlayDieSound();
		}

		private void GameOverUI()
		{
			int maxScore =_scoreFileManager.Load();
			if(Score > maxScore)
			{
				_scoreFileManager.Save(Score);
				maxScore = Score;
			}
			_hud.ShowGameOverMessage(Score, maxScore);
		}

		/// <summary>
		/// Disables the Physics Process for the player, the Pipes and the Ground.
		/// It also stops the player animation.
		/// </summary>
		private void DisablePhysicsProcess()
		{
			_player.SetController(new DeathAnimationController(_player));
			_player.StopAnimation();
			var pipes = GetTree().GetNodesInGroup("pipes");
			foreach(var pipe in pipes)
			{
				if(pipe is not Pipes pipeObj)
					continue;

				pipeObj.PlayerHitScoreArea -= OnPlayerHitScoreArea;
				pipeObj.SetController(null);
			}
			_restartMarker.SetController(null);
			_ground.SetController(null);
		}

		private void OnRestartMarkerHit(Node2D node)
		{
			if (node is not RestartMarker)
				return;

			_restartMarker.GlobalPosition = _restartMarkerPosition.GlobalPosition;

			var pipe = PipesScene.Instantiate<Pipes>();
			var pipePosition = new Vector2(_restartMarker.GlobalPosition.X, GD.RandRange(60, 120));
			pipe.GlobalPosition = pipePosition;
			CallDeferred(MainLevel.MethodName.AddChild, pipe);
			pipe.CallDeferred(Pipes.MethodName.SetController, new PhysicsPipeController(pipe));
			pipe.Connect(Pipes.SignalName.PlayerHitScoreArea, Callable.From(OnPlayerHitScoreArea));
		}

		private void OnPlayerHitScoreArea()
		{
			_audioPlayer.PlayScoreSound();
			Score += 1;
		}

		private void ChangeRestartMarkerPosition()
		{
			var viewportSize = GetViewportRect().Size;
			_restartMarkerPosition.GlobalPosition = new Vector2(
					viewportSize.X + _distanceBetweenPipes, 
					_restartMarkerPosition.GlobalPosition.Y
				);
			_restartMarker.GlobalPosition = _restartMarkerPosition.GlobalPosition;
		}
	}
}
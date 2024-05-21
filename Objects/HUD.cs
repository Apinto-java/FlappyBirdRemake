using System.Collections.Generic;
using Godot;
using System.Linq;

namespace FlappyBirdRemake.Objects.UI
{
	public partial class HUD : CanvasLayer
	{

		private Label _scoreLabel;
		private Label _gameOverLabel;
		private Label _gameOverScoreLabel;
		private Label _gameOverBestScoreLabel;
		private AnimationPlayer _splashAnimationPlayer;
		private AnimationPlayer _gameOverAnimationPlayer;
		private AnimationPlayer _getReadyAnimationPlayer;
		private AnimationPlayer _gameOverScoreAnimationPlayer;
		
		private MarginContainer _getReadyContainer;
		private Control _gameOverContainer;

		private TextureRect _medalImage;
		private TextureRect _newBestScoreImage;
		private Dictionary<int, string> _medalTextures = new();

		private Button _gameOverOkButton;
		[Export(PropertyHint.File)] public string MainMenuScenePath;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_scoreLabel = GetNode<Label>("ScoreLabel");
			_gameOverLabel = GetNode<Label>("GameOverContainer/GameOverLabel");
			_gameOverScoreLabel = GetNode<Label>("GameOverContainer/GameOverScore/CurrentScoreLabel");
			_gameOverBestScoreLabel = GetNode<Label>("GameOverContainer/GameOverScore/BestScoreLabel");
			_splashAnimationPlayer = GetNode<AnimationPlayer>("DeathFlash/SplashAnimationPlayer");
			_gameOverAnimationPlayer = GetNode<AnimationPlayer>("GameOverContainer/GameOverLabel/GameOverAnimationPlayer");
			_getReadyAnimationPlayer = GetNode<AnimationPlayer>("GetReadyContainer/GetReadyAnimationPlayer");
			_gameOverScoreAnimationPlayer = GetNode<AnimationPlayer>("GameOverContainer/GameOverScore/GameOverScoreAnimationPlayer");
			_getReadyContainer = GetNode<MarginContainer>("GetReadyContainer");
			_gameOverContainer = GetNode<Control>("GameOverContainer");
			_medalImage = GetNode<TextureRect>("GameOverContainer/GameOverScore/MedalImage");
			_newBestScoreImage = GetNode<TextureRect>("GameOverContainer/GameOverScore/NewBestScoreImage");

			_gameOverOkButton = GetNode<Button>("GameOverContainer/GameOverOkButton");
			_gameOverOkButton.Pressed += OnOkButtonPressed;

			InitializeMedalTextures();
			_gameOverContainer.Visible = false;
			_newBestScoreImage.Visible = false;
		}

		private void OnOkButtonPressed()
		{
			var mainMenuScenePath = "res://Scenes/MainMenu.tscn";

			GetTree().ChangeSceneToFile(mainMenuScenePath);
		}

		private void InitializeMedalTextures()
		{
			_medalTextures.Add(0, null);
			_medalTextures.Add(10, "res://Art/bronze_medal.tres");
			_medalTextures.Add(20, "res://Art/silver_medal.tres");
			_medalTextures.Add(30, "res://Art/gold_medal.tres");
			_medalTextures.Add(40, "res://Art/platinum_medal.tres");
		}

		public void SetGetReadyVisibility(bool visible)
		{
			_getReadyContainer.Visible = visible;

			if(!visible)
				_getReadyAnimationPlayer.Play("fade_out");
		}

		public void SetScore(int score)
		{
			_scoreLabel.Text = score.ToString();
		}

		public void ShowGameOverMessage(int score, int bestScore)
		{
			_gameOverContainer.Visible = true;
			PlayGameOverAnimation();
			SetGameOverScore(score, bestScore);
			PlayGameOverScoreAnimation();
			PlayDeathAnimation();
		}

		private void PlayGameOverAnimation()
		{
			_gameOverAnimationPlayer.Play("game_over_animation");
		}

		private void SetGameOverScore(int score, int bestScore)
		{
			_gameOverScoreLabel.Text = score.ToString();
			_gameOverBestScoreLabel.Text = bestScore.ToString();

			var medalImagePath = _medalTextures.Last(x => bestScore >= x.Key).Value;

			if(medalImagePath != null)
				_medalImage.Texture = GD.Load<AtlasTexture>(medalImagePath);

			if(score == bestScore)
				_newBestScoreImage.Visible = true;
		}

		private void PlayGameOverScoreAnimation()
		{
			_gameOverScoreAnimationPlayer.Play("slide_in");
		}

		private void PlayDeathAnimation()
		{
			_splashAnimationPlayer.Play("death_animation");
		}

		public override void _ExitTree()
		{
			Dispose();
		}
	}
}

using Godot;
using System;

namespace FlappyBirdRemake.Objects.Sound
{
	public partial class AudioPlayer : AudioStreamPlayer
	{
		public static AudioStream FlapSound { get; set; } = ResourceLoader.Load<AudioStreamWav>("res://Sounds/sfx_wing.wav", cacheMode: ResourceLoader.CacheMode.Reuse);
		public static AudioStream HitSound { get; set; } = ResourceLoader.Load<AudioStreamWav>("res://Sounds/sfx_hit.wav", cacheMode: ResourceLoader.CacheMode.Reuse);
		public static AudioStream DieSound { get; set; } = ResourceLoader.Load<AudioStreamWav>("res://Sounds/sfx_die.wav", cacheMode: ResourceLoader.CacheMode.Reuse);
		public static AudioStream ScoreSound { get; set; } = ResourceLoader.Load<AudioStreamWav>("res://Sounds/sfx_point.wav", cacheMode: ResourceLoader.CacheMode.Reuse);
		public static AudioStream StartSound { get; set; } = ResourceLoader.Load<AudioStreamWav>("res://Sounds/sfx_swooshing.wav", cacheMode: ResourceLoader.CacheMode.Reuse);

        public override void _Ready()
        {
            MaxPolyphony = 10;
        }

        public void PlayFlapSound()
		{
			if(!Playing) Play();
			var playback = GetAudioStreamPlaybackPolyphonic();
			playback.PlayStream(FlapSound);
		}
		
		public void PlayHitSound()
		{
			if(!Playing) Play();
			var playback = GetAudioStreamPlaybackPolyphonic();
			playback.PlayStream(HitSound);
		}

		public void PlayDieSound()
		{
			if(!Playing) Play();
			var playback = GetAudioStreamPlaybackPolyphonic();
			playback.PlayStream(DieSound);
		}

		public void PlayScoreSound()
		{
			if(!Playing) Play();
			var playback = GetAudioStreamPlaybackPolyphonic();
			playback.PlayStream(ScoreSound);
		}

		public void PlayStartSound()
		{
			if(!Playing) Play();
			var playback = GetAudioStreamPlaybackPolyphonic();
			playback.PlayStream(StartSound);
		}

		private AudioStreamPlaybackPolyphonic GetAudioStreamPlaybackPolyphonic()
		{
			return (AudioStreamPlaybackPolyphonic)GetStreamPlayback();
		}

	}
}

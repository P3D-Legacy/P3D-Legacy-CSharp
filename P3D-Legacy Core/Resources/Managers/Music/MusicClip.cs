using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

using P3D.Legacy.Core.Storage.Files.ContentFiles;

namespace P3D.Legacy.Core.Resources.Managers.Music
{
    /// <summary>
    /// Wrapper for MusicFile.
    /// </summary>
    public class MusicClip
    {
        public enum PlayEffect { Normal, FadeIn, FadeOut }
        public PlayEffect CurrentEffect { get; private set; }

        private float FadeVolume { get; set; } = 1.0f;
        private TimeSpan FadeDuration { get; set; } = new TimeSpan(0, 0, 0, 1);
        private float FadeTick => (float) (FadeDuration.TotalSeconds / 60d); // -- Not even sure what i did

        private DateTime? PauseUntil { get; set; }

        private MusicFile MusicFile { get; }
        public bool IsStandard => MusicFile.IsStandard;
        public TimeSpan Duration => MusicFile.Duration;

        public float Volume { get { return MediaPlayer.Volume; } set { MediaPlayer.Volume = value; } }

        public bool IsPlaying => State != MediaState.Stopped;
        private MediaState State => MediaPlayer.State;
        
        public MusicClip(MusicFile musicFile) { MusicFile = musicFile; }

        public void PlayFadeIn(float volume, TimeSpan fadeDuration, bool loopPlayback)
        {
            if (State == MediaState.Stopped)
            {
                Play(0.0f, loopPlayback);
                Volume = volume; //overwrite the volume
                FadeIn(fadeDuration);
            }
            else if (State != MediaState.Playing)
            {
                Stop();
                Play(0.0f, loopPlayback);
                Volume = volume; //overwrite the volume
                FadeIn(fadeDuration);
            }
        }
        public void PlayFadeOut(float volume, TimeSpan fadeDuration, bool loopPlayback)
        {
            if (State == MediaState.Stopped)
            {
                Play(0.0f, loopPlayback);
                Volume = volume; //overwrite the volume
                FadeOut(fadeDuration);
            }
            else if (State != MediaState.Playing)
            {
                Stop();
                Play(0.0f, loopPlayback);
                Volume = volume; //overwrite the volume
                FadeOut(fadeDuration);
            }
        }
        public void FadeIn(TimeSpan fadeDuration)
        {
            if (CurrentEffect != PlayEffect.FadeIn)
            {
                CurrentEffect = PlayEffect.FadeIn;
                FadeDuration = fadeDuration;
            }
        }
        public void FadeOut(TimeSpan fadeDuration)
        {
            if (CurrentEffect != PlayEffect.FadeOut)
            {
                CurrentEffect = PlayEffect.FadeOut;
                FadeDuration = fadeDuration;
            }
        }

        public void Play(float volume, bool loopPlayback)
        {
            Volume = volume;

            if (!IsPlaying)
            {
                MediaPlayer.Volume = volume;
                MediaPlayer.IsRepeating = loopPlayback;
                MediaPlayer.Play(MusicFile);
            }
        }
        public void Resume() => MediaPlayer.Resume();
        public void Pause() => MediaPlayer.Pause();
        public void Pause(TimeSpan duration)
        {
            PauseUntil = DateTime.Now + duration;
            Pause();
        }
        public void Stop() => MediaPlayer.Stop();

        public void Update(GameTime gameTime)
        {
            if (!IsPlaying)
                return;

            if (PauseUntil != null)
            {
                if (DateTime.Now < PauseUntil)
                    return;
                else
                {
                    PauseUntil = null;
                    Resume();
                }
            }

            var delta = (float) gameTime.ElapsedGameTime.TotalSeconds;
            switch (CurrentEffect)
            {
                case PlayEffect.Normal:
                    FadeVolume = 1.0f;
                    break;

                case PlayEffect.FadeIn:
                    if (FadeVolume >= 1.0f)
                    {
                        FadeVolume = 1.0f;
                        CurrentEffect = PlayEffect.Normal;
                    }
                    else
                        Volume += FadeTick * delta;
                    break;

                case PlayEffect.FadeOut:
                    if (FadeVolume < 0.0f)
                    {
                        FadeVolume = 0.0f;
                        Volume = FadeVolume;
                    }
                    else
                        Volume -= FadeTick * delta;
                    break;
            }
        }
    }
}
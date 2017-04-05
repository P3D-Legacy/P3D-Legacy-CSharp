using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

using P3D.Legacy.Core.Storage.Files.ContentFiles;

namespace P3D.Legacy.Core.Resources.Managers.Sound
{
    public class SoundEffectClip
    {
        public enum PlayEffect { Normal, FadeIn, FadeOut  }
        public PlayEffect CurrentEffect { get; private set; }

        private float FadeVolume { get; set; } = 1.0f;
        private TimeSpan FadeDuration { get; set; } = new TimeSpan(0, 0, 0, 1);
        private float FadeTick => (float) (FadeDuration.TotalSeconds / 60d); // -- Not even sure what i did

        private SoundEffectFile SoundEffectFile { get; }
        public bool IsStandard => SoundEffectFile.IsStandard;
        public TimeSpan Duration => SoundEffectFile.Duration;

        private SoundEffectInstance _currentClipInstance;

        private float _volume = 0.5f;
        public float Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                if (_currentClipInstance != null)
                    _currentClipInstance.Volume = value;
            }
        }

        public bool IsPlaying => State == SoundState.Playing;
        private SoundState State => _currentClipInstance?.State ?? SoundState.Stopped;

        public SoundEffectClip(SoundEffectFile soundEffectFile) { SoundEffectFile = soundEffectFile; }

        public void PlayFadeIn(float volume, TimeSpan fadeDuration, bool loopPlayback)
        {
            if (_currentClipInstance == null)
            {
                Play(0.0f, loopPlayback);
                _volume = volume; //overwrite the volume
                FadeIn(fadeDuration);
            }
            else if (!IsPlaying)
            {
                Stop();
                Play(0.0f, loopPlayback);
                _volume = volume; //overwrite the volume
                FadeIn(fadeDuration);
            }
        }
        public void PlayFadeOut(float volume, TimeSpan fadeDuration, bool loopPlayback)
        {
            if (_currentClipInstance == null)
            {
                Play(0.0f, loopPlayback);
                _volume = volume; //overwrite the volume
                FadeOut(fadeDuration);
            }
            else if (!IsPlaying)
            {
                Stop();
                Play(0.0f, loopPlayback);
                _volume = volume; //overwrite the volume
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

        public void Play(float volume, bool loopTrack) => Play(0f, 0f, volume, loopTrack);
        public void Play(float pitch, float pan, float volume, bool loopTrack)
        {
            _volume = volume;

            if (!IsPlaying)
            {
                _currentClipInstance = SoundEffectFile.CreateInstance();  
                _currentClipInstance.Pitch = pitch;
                _currentClipInstance.Pan = pan;
                _currentClipInstance.Volume = volume;
                _currentClipInstance.IsLooped = loopTrack;
                _currentClipInstance.Play();
            }
        }
        public void Resume() => _currentClipInstance?.Resume();
        public void Pause() => _currentClipInstance?.Pause();
        public void Stop() => _currentClipInstance?.Stop();

        public void Update(GameTime gameTime)
        {
            if (!IsPlaying)
                return;

            var delta = (float) gameTime.ElapsedGameTime.TotalSeconds;
            switch (CurrentEffect)
            {
                case PlayEffect.Normal:
                    _currentClipInstance.Volume = _volume;
                    FadeVolume = 1.0f;
                    break;

                case PlayEffect.FadeIn:
                    if (FadeVolume >= 1.0f)
                    {
                        FadeVolume = 1.0f;
                        _currentClipInstance.Volume = _volume;
                        CurrentEffect = PlayEffect.Normal;
                    }
                    else
                        _currentClipInstance.Volume += FadeTick * delta;
                    break;

                case PlayEffect.FadeOut:
                    if (FadeVolume < 0.0f)
                    {
                        FadeVolume = 0.0f;
                        _currentClipInstance.Volume = FadeVolume;
                    }
                    else
                        _currentClipInstance.Volume -= FadeTick * delta;
                    break;
            }
        }
    }
}

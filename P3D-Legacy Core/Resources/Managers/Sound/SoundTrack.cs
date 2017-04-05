using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using P3D.Legacy.Core.Resources.Managers.Music;
using P3D.Legacy.Core.Storage;
using P3D.Legacy.Core.Storage.Files.ContentFiles;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Resources.Managers.Sound
{
    // Mute
    public class SoundEffectTrack
    {
        private static string Exceptions(string soundEffectName)
        {
            //Exceptions due to old definitions:
            switch (soundEffectName)
            {
                case "healing":
                    soundEffectName = "pokemon_heal";
                    break;
            }

            return soundEffectName.ToLowerInvariant();
        }

        public List<string> SoundEffectHistoryList = new List<string>();

        private Dictionary<string, SoundEffectClip> SoundEffectFiles { get; } = new Dictionary<string, SoundEffectClip>();

        private string CurrentSoundEffectName { get; set; } = "";
        private SoundEffectClip CurrentSoundEffect => Contains(CurrentSoundEffectName) ? SoundEffectFiles[Exceptions(CurrentSoundEffectName)] : null;

        private float _volume = 0.5f;
        public float Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                if (CurrentSoundEffect != null)
                    CurrentSoundEffect.Volume = _volume;
            }
        }

        public string MusicNamePlaying => CurrentSoundEffectName;
        public bool IsPlaying => CurrentSoundEffect?.IsPlaying ?? false;

        private bool IsMuted { get; set; }

        public SoundEffectClip GetSound(string soundEffectName, bool logErrors)
        {
            var soundEffectNameLower = soundEffectName.ToLowerInvariant();

            if (SoundEffectFiles.ContainsKey(soundEffectNameLower))
                return SoundEffectFiles[soundEffectNameLower];

            if (TryAddGameModeSoundEffect(soundEffectName))
                return SoundEffectFiles[soundEffectNameLower];

            Logger.Log(Logger.LogTypes.Warning, "SoundTrack.cs: Cannot find sound file \"" + soundEffectName + "\". Return nothing.");
            return null;
        }
        private bool TryAddGameModeSoundEffect(string soundEffectNane)
        {
            var soundEffectNaneLower = soundEffectNane.ToLowerInvariant();
            var fileName = $"{soundEffectNane}.xnb";
            if (GameModeManager.ActiveGameMode.ContentFolder.MusicFolder.CheckExists(fileName) == ExistenceCheckResult.FileExists)
                return AddSoundEffect(soundEffectNaneLower, GameModeManager.ActiveGameMode.ContentFolder.SoundEffectsFolder.GetSoundEffectFile(fileName), false);
            return false;
        }

        public void LoadSounds(bool forceReplace = false)
        {
            foreach (var soundEffectFile in StorageInfo.ContentFolder.SoundEffectsFolder.GetAllSoundEffectFiles())
            {
                var fileName = soundEffectFile.InContentLocalPathWithoutExtension.Replace("\\", "|").ToLowerInvariant();
                AddSoundEffect(fileName, soundEffectFile, forceReplace);
            }

            if (Core.GameOptions.ContentPackNames.Any())
            {
                foreach (var contentPackName in Core.GameOptions.ContentPackNames)
                {
                    foreach (var soundEffectFile in StorageInfo.ContentPacksFolder.GetContentPack(contentPackName).SoundEffectsFolder.GetAllSoundEffectFiles())
                    {
                        var fileName = soundEffectFile.InContentLocalPathWithoutExtension.Replace("\\", "|").ToLowerInvariant();
                        AddSoundEffect(fileName, soundEffectFile, forceReplace);
                    }
                }
            }
        }
        private bool AddSoundEffect(string soundEffectName, SoundEffectFile soundEffectFile, bool forceReplace)
        {
            var soundEffectFileLower = soundEffectName.ToLowerInvariant();

            if (SoundEffectFiles.ContainsKey(soundEffectFileLower))
            {
                if (forceReplace && SoundEffectFiles[soundEffectFileLower].IsStandard)
                    SoundEffectFiles.Remove(soundEffectFileLower);
                else
                    return true;
            }
            SoundEffectFiles.Add(soundEffectFileLower, new SoundEffectClip(soundEffectFile));

            //if (!soundEffectFile.ForceLoad())
            //{
            //    Logger.Log(Logger.LogTypes.Warning, $"SoundTrack.cs: File at \"{soundEffectFile.LocalPath}\" is not a valid music file!");
            //    return false;
            //}
            return true;
        }


        public void Play(string soundEffectName, bool loopTrack = false) => Play(soundEffectName, 0f, 0f, Volume, false, loopTrack);
        public void Play(string soundEffectName, float pitch, float pan, float volume, bool stopMusic, bool loopTrack = false)
        {
            if (IsMuted)
                return;

            if (IsPlaying)
                Stop();

            CurrentSoundEffectName = Exceptions(soundEffectName);
            if (CurrentSoundEffect != null)
            {
                if (stopMusic)
                    MusicManager.Pause(CurrentSoundEffect.Duration);
                CurrentSoundEffect.Play(pitch, pan, volume, loopTrack);
            }
        }
        public void PlayCry(int number, float pitch, float pan, float volume) => Play($"cries|{number}", pitch, pan, volume, false);
        public void PlayCrossFade(string soundEffectName, TimeSpan duration, bool loopTrack = false)
        {
            if (!IsPlaying)
                Play(soundEffectName, loopTrack);
            else
            {
                CurrentSoundEffect.FadeOut(duration);
                CurrentSoundEffectName = soundEffectName;
                CurrentSoundEffect.Stop();
                CurrentSoundEffect.PlayFadeIn(Volume, duration, loopTrack);
            }
        }
        public void FadeIn(TimeSpan duration) => CurrentSoundEffect?.FadeIn(duration);
        public void FadeOut(TimeSpan duration) => CurrentSoundEffect?.FadeOut(duration);
        public void Resume() => CurrentSoundEffect?.Resume();
        public void Pause() => CurrentSoundEffect?.Pause();
        public void Stop() => CurrentSoundEffect?.Stop();

        public void Update(GameTime gameTime) => CurrentSoundEffect?.Update(gameTime);


        public void Mute(bool mute) => IsMuted = mute;

        public void ReloadSoundEffects()
        {
            SoundEffectFiles.Clear();
            LoadSounds();
        }

        public bool Contains(string musicName) => SoundEffectFiles.ContainsKey(Exceptions(musicName));
    }
}
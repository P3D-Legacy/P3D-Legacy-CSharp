using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

using P3D.Legacy.Core.Storage;
using P3D.Legacy.Core.Storage.Files.ContentFiles;
using P3D.Legacy.Core.Storage.Folders;
using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Resources.Managers.Music
{
    public class MusicTrack
    {
        private static string Exceptions(string musicName)
        {
            //Exceptions due to old definitions:
            switch (musicName)
            {
                case "welcome":
                    musicName = "RouteMusic1";
                    break;
                case "battle":
                    musicName = "johto_wild";
                    break;
                case "battleintro":
                case "johto_battle_intro":
                    musicName = "battle_intro";
                    break;
                case "darkcave":
                    musicName = "dark_cave";
                    break;
                case "showmearound":
                    musicName = "show_me_around";
                    break;
                case "sprouttower":
                    musicName = "sprout_tower";
                    break;
                case "johto_rival_intro":
                    musicName = "johto_rivalintro";
                    break;
                case "johto_rival_appear":
                    musicName = "johto_rival_encounter";
                    break;
                case "ilex_forest":
                case "union_cave":
                case "mt_mortar":
                case "whirlpool_islands":
                case "tohjo_falls":
                    musicName = "IlexForest";
                    break;
            }

            return musicName.ToLowerInvariant();
        }

        private Dictionary<string, MusicClip> MusicFiles { get; } = new Dictionary<string, MusicClip>();

        private string CurrentMusicName { get; set; } = "";
        private MusicClip CurrentMusic => Contains(CurrentMusicName) ? MusicFiles[Exceptions(CurrentMusicName)] : null;

        private float _volume = 0.5f;
        public float Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                if(CurrentMusic != null)
                    CurrentMusic.Volume = _volume;
            }
        }

        public string MusicNamePlaying => CurrentMusicName;
        public bool IsPlaying => CurrentMusic?.IsPlaying ?? false;

        private bool _isMuted;
        private bool IsMuted
        {
            get { return _isMuted; }
            set
            {
                _isMuted = value;
                if (MediaPlayer.IsMuted)
                {
                    Pause();
                    if(FontManager.HasLoaded)
                        Core.GameMessage.ShowMessage(Localization.GetString("game_message_music_off"), 12, FontManager.MainFont, Color.White);
                }
                else
                {
                    Resume();
                    if (FontManager.HasLoaded)
                        Core.GameMessage.ShowMessage(Localization.GetString("game_message_music_on"), 12, FontManager.MainFont, Color.White);
                }
            }
        }

        public MusicClip GetMusic(string musicName, bool logErrors)
        {
            var lowerMusicName = musicName.ToLowerInvariant();

            if (MusicFiles.ContainsKey(lowerMusicName))
                return MusicFiles[lowerMusicName];

            if (TryAddGameModeMusic(lowerMusicName))
                return MusicFiles[lowerMusicName];

            if (logErrors && lowerMusicName != "nomusic")
                Logger.Log(Logger.LogTypes.Warning, $"MusicManager.cs: Cannot find music file \"{musicName}\". Return nothing.");

            return null;
        }
        private bool TryAddGameModeMusic(string musicName)
        {
            var musicNameLower = musicName.ToLowerInvariant();
            var fileName = $"{musicName}.ogg";
            if (GameModeManager.ActiveGameMode.ContentFolder.MusicFolder.CheckExists(fileName) == ExistenceCheckResult.FileExists)
                return AddMusic(musicNameLower, GameModeManager.ActiveGameMode.ContentFolder.MusicFolder.GetMusicFile(fileName), false);
            return false;
        }

        public void LoadMusic(bool forceReplace = false)
        {
            foreach (var musicFile in new ContentFolder().MusicFolder.GetAllMusicFiles())
            {
                var fileName = musicFile.InContentLocalPathWithoutExtension.Replace("\\", "|").ToLowerInvariant();
                AddMusic(fileName, musicFile, forceReplace);
            }

            if (Core.GameOptions.ContentPackNames.Any())
            {
                foreach (var contentPackName in Core.GameOptions.ContentPackNames)
                {
                    var contentPackMusicFolder = new ContentPacksFolder().GetContentPack(contentPackName).MusicFolder;
                    foreach (var musicFile in contentPackMusicFolder.GetAllMusicFiles())
                    {
                        var fileName = musicFile.InContentLocalPathWithoutExtension.Replace("\\", "|").ToLowerInvariant();
                        AddMusic(fileName, musicFile, forceReplace);
                    }
                }
            }
            if (!GameModeManager.ActiveGameMode.IsDefaultGamemode && !Equals(GameModeManager.ActiveGameMode.ContentFolder, new ContentFolder()))
            {
                var gameModeMusicFolder = GameModeManager.ActiveGameMode.ContentFolder.MusicFolder;
                foreach (var musicFile in gameModeMusicFolder.GetAllMusicFiles())
                {
                    var fileName = musicFile.InContentLocalPathWithoutExtension.Replace("\\", "|").ToLowerInvariant();
                    AddMusic(fileName, musicFile, forceReplace);
                }
            }
        }
        private bool AddMusic(string musicName, MusicFile musicFile, bool forceReplace)
        {
            var musicNameLower = musicName.ToLowerInvariant();

            if (MusicFiles.ContainsKey(musicNameLower))
            {
                if (forceReplace && MusicFiles[musicNameLower].IsStandard)
                    MusicFiles.Remove(musicNameLower);
                else
                    return true;
            }
            MusicFiles.Add(musicNameLower, new MusicClip(musicFile));

            // TODO: Loading every music increases uned memory size duh
            //if (!musicFile.ForceLoad())
            //{
            //    Logger.Log(Logger.LogTypes.Warning, $"MusicTrack.cs: File at \"{musicFile.LocalPath}\" is not a valid music file!");
            //    return false;
            //}
            return true;
        }
        public void ReloadMusic()
        {
            MusicFiles.Clear();
            LoadMusic();
        }


        public void Play(string musicName, bool loopTrack = true)
        {
            if (IsPlaying)
                Stop();

            CurrentMusicName = Exceptions(musicName);
            CurrentMusic?.Play(Volume, loopTrack);
        }
        public void PlayCrossFade(string musicName, TimeSpan duration, bool loopTrack = true)
        {
            if (!IsPlaying)
                Play(musicName, loopTrack);
            else
            {
                CurrentMusic.FadeOut(duration);
                CurrentMusicName = musicName;
                CurrentMusic.Stop();
                CurrentMusic.PlayFadeIn(Volume, duration, loopTrack);
            }
        }
        public void PlayNoMusic() => MediaPlayer.Stop();
        public void FadeIn(TimeSpan duration) => CurrentMusic?.FadeIn(duration);
        public void FadeOut(TimeSpan duration) => CurrentMusic?.FadeOut(duration);
        public void Resume() => CurrentMusic?.Resume();
        public void Pause() => CurrentMusic?.Pause();
        public void Pause(TimeSpan duration) => CurrentMusic?.Pause(duration);
        public void Stop() => CurrentMusic?.Stop();

        public void Update(GameTime gameTime) => CurrentMusic?.Update(gameTime);


        public void Mute(bool mute) => IsMuted = mute;

        public bool Contains(string musicName) => MusicFiles.ContainsKey(Exceptions(musicName));
    }
}

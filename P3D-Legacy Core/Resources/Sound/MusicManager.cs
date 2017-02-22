using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using Microsoft.Win32;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

using P3D.Legacy.Core.Extensions;

namespace P3D.Legacy.Core.Resources.Sound
{
    public class MusicManager
    {
        private static Dictionary<string, CSong> SongFiles = new Dictionary<string, CSong>();

        private static string currentSong = "";

        public static List<string> SongList = new List<string>();

        public static void Setup()
        {
            MasterVolume = 1f;
            MediaPlayer.Volume = MasterVolume;
            Volume = 1f;
            NextSong = "";
            FadeInSpeed = 0f;
            FadeOutSpeed = 0f;
            FadeOut = false;
            FadeIn = false;
            MediaPlayer.IsRepeating = false;
        }

        public class CSong
        {
            public Song Song { get; set; }
            public string Origin { get; set; }
            public bool IsStandardSong => Origin == "Content";

            public CSong(Song song, string origin)
            {
                Song = song;
                Origin = origin;
            }
        }

        private static bool AddSong(string Name, bool forceReplace)
        {
            try
            {
                ContentManager cContent = ContentPackManager.GetContentManager("Songs\\" + Name, ".xnb,.mp3");

                bool loadSong = false;
                bool removeSong = false;

                if (SongFiles.ContainsKey(Name.ToLower()) == false)
                {
                    loadSong = true;
                }
                else if (forceReplace && SongFiles[Name.ToLower()].IsStandardSong)
                {
                    removeSong = true;
                    loadSong = true;
                }

                if (loadSong)
                {
                    Song song = null;

                    if (File.Exists(GameController.GamePath + "\\" + cContent.RootDirectory + "\\Songs\\" + Name + ".xnb") == false)
                    {
                        if (File.Exists(GameController.GamePath + "\\" + cContent.RootDirectory + "\\Songs\\" + Name + ".mp3"))
                        {
                            var ctor = typeof(Song).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new [] { typeof(string), typeof(string), typeof(int) }, null);
                            var filePath = GameController.GamePath + "\\" + cContent.RootDirectory + "\\Songs\\" + Name + ".mp3";
                            song = (Song) ctor.Invoke( new object [] { Name, filePath, 0  });
                        }
                        else
                        {
                            Logger.Log(Logger.LogTypes.Warning, "MusicManager.vb: Song at \"" + GameController.GamePath + "\\" + cContent.RootDirectory + "\\Songs\\" + Name + "\" was not found!");
                            return false;
                        }
                    }
                    else
                    {
                        song = cContent.Load<Song>("Songs\\" + Name);
                    }

                    if ((song != null))
                    {
                        if (removeSong)
                        {
                            SongFiles.Remove(Name.ToLower());
                        }
                        SongFiles.Add(Name.ToLower(), new CSong(song, cContent.RootDirectory));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.LogTypes.Warning, "MusicManager.vb: File at \"Songs\\" + Name + "\" is not a valid song file!");
                return false;
            }
            return true;
        }

        public static CSong GetSong(string Name, bool LogErrors)
        {
            //Exceptions due to old definitions:
            switch (Name.ToLower())
            {
                case "welcome":
                    Name = "RouteMusic1";
                    break;
                case "battle":
                    Name = "johto_wild";
                    break;
                case "battleintro":
                case "johto_battle_intro":
                    Name = "battle_intro";
                    break;
                case "darkcave":
                    Name = "dark_cave";
                    break;
                case "showmearound":
                    Name = "show_me_around";
                    break;
                case "sprouttower":
                    Name = "sprout_tower";
                    break;
                case "johto_rival_intro":
                    Name = "johto_rivalintro";
                    break;
                case "johto_rival_appear":
                    Name = "johto_rival_encounter";
                    break;
                case "ilex_forest":
                case "union_cave":
                case "mt_mortar":
                case "whirlpool_islands":
                case "tohjo_falls":
                    Name = "IlexForest";
                    break;
            }

            if (SongFiles.ContainsKey(Name.ToLower()))
            {
                return SongFiles[Name.ToLower()];
            }
            if (TryAddGameModeMusic(Name))
            {
                return SongFiles[Name.ToLower()];
            }
            if (Name.ToLower() != "nomusic")
            {
                Logger.Log(Logger.LogTypes.Warning, "MusicManager.vb: Cannot find music file \"" + Name + "\". Return nothing.");
            }
            return null;
        }

        private static bool TryAddGameModeMusic(string Name)
        {
            string musicfileXNB = GameController.GamePath + GameModeManager.ActiveGameMode.ContentFolder + "Songs\\" + Name + ".xnb";
            string musicfileMP3 = GameController.GamePath + GameModeManager.ActiveGameMode.ContentFolder + "Songs\\" + Name + ".mp3";
            if (File.Exists(musicfileXNB) || File.Exists(musicfileMP3))
            {
                return AddSong(Name, false);
            }
            return false;
        }

        public static void LoadMusic(bool forceReplace)
        {
            MediaPlayer.IsRepeating = true;

            var musicFiles = Directory.GetFiles(GameController.GamePath + "\\Content\\Songs\\", "*.*", SearchOption.AllDirectories);
            foreach (string musicFile in musicFiles)
            {
                string musicFileTemp = musicFile;
                if (musicFileTemp.EndsWith(".xnb"))
                {
                    bool isIntro = musicFileTemp.Contains("\\Songs\\intro\\");

                    musicFileTemp = !isIntro ? Path.GetFileNameWithoutExtension(musicFileTemp) : "intro\\" + Path.GetFileNameWithoutExtension(musicFileTemp);

                    AddSong(musicFileTemp, forceReplace);
                }
            }
            if (Core.GameOptions.ContentPackNames.Any())
            {
                foreach (string c in Core.GameOptions.ContentPackNames)
                {
                    string path = GameController.GamePath + "\\ContentPacks\\" + c + "\\Songs\\";
                    if (Directory.Exists(path))
                    {
                        var musicFiles1 = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                        foreach (string musicFile in musicFiles1)
                        {
                            var musicFileTemp = musicFile;
                            if (musicFileTemp.EndsWith(".xnb"))
                            {
                                bool isIntro = musicFileTemp.Contains("\\Songs\\intro\\");

                                musicFileTemp = !isIntro ? Path.GetFileNameWithoutExtension(musicFileTemp) : "intro\\" + Path.GetFileNameWithoutExtension(musicFileTemp);
                                AddSong(musicFileTemp, forceReplace);
                            }
                        }
                    }
                }
            }
        }


        private static bool SongExistFlag = true;
        public static void PlayMusic(string Song)
        {
            PlayMusic(Song, 0f, 0f);
        }

        public static void PlayMusic(string Song, bool SearchForIntro)
        {
            PlayMusic(Song, SearchForIntro, 0.02f, 0.02f);
        }

        public static void PlayNoMusic()
        {
            MediaPlayer.Stop();
            currentSong = "nomusic";
        }
        public static void IgnoreLastSong()
        {
            currentSong = "nomusic";
            SongList.Add("nomusic");
            IntroStarted = false;
        }
        public static void PlayMusic(string Song, bool SearchForIntro, float NewFadeInSpeed, float NewFadeOutSpeed)
        {
            string lastSong = "nomusic";

            if (IntroStarted)
            {
                lastSong = IntroContinue;
            }
            else
            {
                if (SongList.Count > 0)
                {
                    lastSong = SongList[SongList.Count - 1];
                }
            }
            MediaPlayer.IsRepeating = true;
            if (SearchForIntro && lastSong.ToLower() != Song.ToLower())
            {
                if (SongFiles.Keys.Contains("intro\\" + Song.ToLower()))
                {
                    if (SongFiles["intro\\" + Song.ToLower()].Origin == SongFiles[Song.ToLower()].Origin)
                    {
                        IntroEndTime = DateTime.Now.AddSeconds(-1) + SongFiles["intro\\" + Song.ToLower()].Song.Duration;

                        PlayMusic("intro\\" + Song.ToLower(), NewFadeInSpeed, NewFadeOutSpeed);
                        MediaPlayer.IsRepeating = false;
                        IntroContinue = Song;
                        IntroStarted = true;
                        if (NewFadeInSpeed > 0f && NewFadeOutSpeed > 0f)
                        {
                            FadeIntoIntro = true;
                        }
                    }
                    else
                    {
                        FadeIntoIntro = false;
                        PlayMusic(Song, NewFadeInSpeed, NewFadeOutSpeed);
                    }
                }
                else
                {
                    FadeIntoIntro = false;
                    PlayMusic(Song, NewFadeInSpeed, NewFadeOutSpeed);
                }
            }
            else
            {
                if (!(SearchForIntro && IntroStarted && IntroContinue.ToLower() == Song.ToLower()))
                {
                    FadeIntoIntro = false;
                    PlayMusic(Song, NewFadeInSpeed, NewFadeOutSpeed);
                }
            }
        }

        public static void PlayMusic(string Song, float NewFadeInSpeed, float NewFadeOutSpeed)
        {
            if (FadeOut)
            {
                if (currentSong.ToLower() == Song.ToLower() && NextSong.ToLower() != Song.ToLower())
                {
                    NextSong = Song.ToLower();
                    return;
                }
            }

            if (string.IsNullOrEmpty(currentSong) || Song.ToLower() != currentSong.ToLower())
            {
                if (NewFadeInSpeed > 0f && NewFadeOutSpeed > 0f)
                {
                    if (string.IsNullOrEmpty(currentSong))
                    {
                        FadeInSpeed = NewFadeInSpeed;
                        FadeIn = true;
                        Volume = 0f;
                        PlayTrack(Song);
                    }
                    else
                    {
                        NextSong = Song;
                        FadeInSpeed = NewFadeInSpeed;
                        FadeOutSpeed = NewFadeOutSpeed;
                        FadeOut = true;
                        FadeIn = false;
                    }
                }
                else
                {
                    Volume = 1f;
                    FadeIn = false;
                    FadeOut = false;
                    PlayTrack(Song);
                }
            }
        }

        private static void PlayTrack(string song)
        {
            CSong s = null;
            s = GetSong(song, true);

            MediaPlayer.Stop();

            Logger.Debug("Play [" + song + "]");

            IntroStarted = false;

            if ((s != null))
            {
                if (CanPlayMusic())
                {
                    MediaPlayer.Play(s.Song);
                }

                if (MediaPlayer.IsMuted)
                {
                    MediaPlayer.Pause();
                }

                SongList.Add(song);
                SongExistFlag = true;
            }
            else
            {
                SongList.Add("");
                SongExistFlag = false;
            }

            currentSong = song;
        }

        public static void Mute(bool mute)
        {
            if (MediaPlayer.IsMuted != mute)
            {
                MediaPlayer.IsMuted = mute;

                if (MediaPlayer.IsMuted)
                {
                    MediaPlayer.Pause();
                    Core.GameMessage.ShowMessage(Localization.GetString("game_message_music_off"), 12, FontManager.MainFont, Color.White);
                }
                else
                {
                    if (SongExistFlag)
                    {
                        MediaPlayer.Resume();
                    }
                    Core.GameMessage.ShowMessage(Localization.GetString("game_message_music_on"), 12, FontManager.MainFont, Color.White);
                }
            }
        }

        public static void ReloadMusic()
        {
            SongFiles.Clear();
            LoadMusic(false);
        }

        //Intro:
        static DateTime StartTime;
        static bool PausedForSound;
        static DateTime IntroEndTime;
        static bool IntroStarted;

        static string IntroContinue = "";
        //Fading:

        static string NextSong;
        static float FadeInSpeed;

        static float FadeOutSpeed;
        static bool FadeOut;
        static bool FadeIn;

        static bool FadeIntoIntro;
        static float Volume = 1f;
        //Actual volume people can change
        public static float MasterVolume = 1f;

        public static void Update()
        {
            if (PausedForSound)
            {
                if (DateTime.Now >= StartTime)
                {
                    PausedForSound = false;
                    if (MediaPlayer.IsMuted == false && SongExistFlag)
                    {
                        MediaPlayer.Resume();
                    }
                }
            }
            else
            {
                UpdateFade();

                if (IntroStarted)
                {
                    if (DateTime.Now >= IntroEndTime)
                    {
                        PlayTrack(IntroContinue);
                        //Plays the loop that follows the intro
                        MediaPlayer.IsRepeating = true;
                    }
                }
            }
        }


        static float LastVolume = -1f;
        private static void UpdateFade()
        {
            if (FadeIn)
            {
                Volume += FadeInSpeed;
                if (Volume >= 1f)
                {
                    Volume = 1f;
                    FadeIn = false;
                }
            }
            if (FadeOut)
            {
                Volume -= FadeOutSpeed;
                if (Volume <= 0f)
                {
                    Volume = 0f;
                    FadeOut = false;
                    FadeIn = true;

                    PlayTrack(NextSong);

                    if (FadeIntoIntro)
                    {
                        IntroStarted = true;
                        IntroEndTime = DateTime.Now.AddSeconds(-1) + SongFiles[NextSong].Song.Duration;
                        FadeIntoIntro = false;
                    }

                    currentSong = NextSong;
                    NextSong = "";
                }
            }

            if (Core.GameInstance.IsActive && LastVolume != (Volume * MasterVolume))
            {
                ForceVolumeUpdate();
            }
        }

        public static void PauseForSound(SoundEffect s)
        {
            StartTime = DateTime.Now + s.Duration;
            PausedForSound = true;
            MediaPlayer.Pause();
        }

        public static void StopMusic()
        {
            MediaPlayer.Stop();
            IntroStarted = false;
        }

        public static void Pause()
        {
            MediaPlayer.Pause();
            IntroStarted = false;
        }

        public static void ResumeMusic()
        {
            MediaPlayer.Resume();
        }

        public static string GetCurrentSong => SongList[SongList.Count - 1];

        public static bool SongExists(string songName)
        {
            CSong s = GetSong(songName, false);
            return (s != null);
        }

        [DllImport("winmm.dll", EntryPoint = "waveOutGetNumDevs", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int GetAudioOutputDevices();

        private static bool CanPlayMusic()
        {
            string errorMessage = "";

            int audioDeviceCount = GetAudioOutputDevices();
            if (audioDeviceCount > 0)
            {
                try
                {
                    string r = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\MediaPlayer\\PlayerUpgrade", "PlayerVersion", null).ToString();
                    if (!string.IsNullOrEmpty(r) && r.Contains(","))
                    {
                        string version = r.Remove(r.IndexOf(","));
                        if (version.IsNumeric())
                        {
                            if (Convert.ToInt32(version) >= 11)
                            {
                                return true;
                            }
                            errorMessage = "The installed version of the WindowsMediaPlayer (" + r + ") is smaller than 12.";
                        }
                        else
                        {
                            errorMessage = "The registry string doesn't start with a numeric value.";
                        }
                    }
                    else
                    {
                        errorMessage = "The registry string doesn't contain \",\" or is empty.";
                    }
                }
                catch
                {
                    errorMessage = "Unknown error";
                }
            }
            else
            {
                errorMessage = "No audio output device is connected to the computer.";
            }

            Logger.Log(Logger.LogTypes.Warning, "MusicManager.vb: An error occurred trying to play music: " + errorMessage);

            if (Core.GameOptions.ForceMusic)
            {
                Logger.Log(Logger.LogTypes.Message, "MusicManager.vb: Forced music to play and ignore the error occuring in music validation. Set ForceMusic to \"0\" in the options file to disable this.");
                return true;
            }

            return false;
        }

        public static void ForceVolumeUpdate()
        {
#if WINDOWS
		try {
			MediaPlayer.Volume = Volume * MasterVolume;
		} catch (NullReferenceException ex) {
			// song changed while changing volume
		}
#else
		MediaPlayer.Volume = Volume * MasterVolume;
#endif
            LastVolume = Volume * MasterVolume;
        }

    }
}

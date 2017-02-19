using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

using Microsoft.Xna.Framework.Audio;

namespace P3D.Legacy.Core.Resources.Sound
{
    public static class SoundManager
    {
        private class CSound
        {
            public SoundEffect Sound { get; set; }
            public string Origin { get; set; }

            public bool IsStandardSong => Origin == "Content";


            public CSound(SoundEffect sound, string origin) { Sound = sound; Origin = origin; }
        }


        private static bool _muted;
        private static Dictionary<string, CSound> SoundFiles { get; } = new Dictionary<string, CSound>();

        public static float Volume = 1f;


        private static bool AddSound(string name, bool forceReplace)
        {
            try
            {
                var cContent = ContentPackManager.GetContentManager("Sounds\\" + name, ".xnb,.wav");

                var loadSound = false;
                var removeSound = false;

                if (!SoundFiles.ContainsKey(name.ToLower()))
                    loadSound = true;
                else if (forceReplace && SoundFiles[name.ToLower()].IsStandardSong)
                {
                    removeSound = true;
                    loadSound = true;
                }

                if (loadSound)
                {
                    SoundEffect sound;

                    if (!File.Exists(GameController.GamePath + "\\" + cContent.RootDirectory + "\\Sounds\\" + name + ".xnb"))
                    {
                        if (File.Exists(GameController.GamePath + "\\" + cContent.RootDirectory + "\\Sounds\\" + name + ".wav"))
                        {
                            using (Stream stream = File.Open(GameController.GamePath + "\\" + cContent.RootDirectory + "\\Sounds\\" + name + ".wav", FileMode.OpenOrCreate))
                                sound = SoundEffect.FromStream(stream);
                        }
                        else
                        {
                            Logger.Log(Logger.LogTypes.Warning, "SoundManager.vb: Sound at \"" + GameController.GamePath + "\\" + cContent.RootDirectory + "\\Songs\\" + name + "\" was not found!");
                            return false;
                        }
                    }
                    else
                        sound = cContent.Load<SoundEffect>("Sounds\\" + name);

                    if (sound != null)
                    {
                        if (removeSound)
                            SoundFiles.Remove(name.ToLower());

                        SoundFiles.Add(name.ToLower(), new CSound(sound, cContent.RootDirectory));
                    }
                }
            }
            catch (Exception)
            {
                Logger.Log(Logger.LogTypes.Warning, "SoundManager.vb: File at \"Sounds\\" + name + "\" is not a valid sound file. They have to be a PCM wave file, mono or stereo, 8 or 16 bit and have to have a sample rate between 8k and 48k Hz.");
                return false;
            }
            return true;
        }

        private static SoundEffect GetSoundEffect(string name)
        {
            switch (name.ToLower())
            {
                case "healing":
                    name = "pokemon_heal";
                    break;
            }

            if (SoundFiles.ContainsKey(name.ToLower()))
                return SoundFiles[name.ToLower()].Sound;
            if (TryAddGameModeSound(name))
                return SoundFiles[name.ToLower()].Sound;
            Logger.Log(Logger.LogTypes.Warning, "SoundManager.vb: Cannot find sound file \"" + name + "\". Return nothing.");
            return null;
        }

        private static bool TryAddGameModeSound(string name)
        {
            var soundfile = GameController.GamePath + GameModeManager.ActiveGameMode.ContentPath + "Sounds\\" + name + ".xnb";
            return File.Exists(soundfile) && AddSound(name, false);
        }

        public static void LoadSounds(bool forceReplace)
        {
            var soundsFolderPath = Path.Combine(GameController.GamePath, "Content", "Sounds");
            var soundsFolderFiles = Directory.GetFiles(soundsFolderPath);
            foreach (var file in soundsFolderFiles)
            {
                var soundfile = file;
                if (soundfile.EndsWith(".xnb"))
                {
                    soundfile = Path.GetFileNameWithoutExtension(soundfile);
                    AddSound(soundfile, forceReplace);
                }
            }

            if (Core.GameOptions.ContentPackNames.Any())
            {
                foreach (string c in Core.GameOptions.ContentPackNames)
                {
                    var path = Path.Combine(GameController.GamePath, "ContentPacks", c, "Sounds");

                    if (Directory.Exists(path))
                    {
                        var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                        foreach (var file in files)
                        {
                            var soundfile = file;
                            if (soundfile.EndsWith(".xnb"))
                            {
                                soundfile = Path.GetFileNameWithoutExtension(soundfile);
                                AddSound(soundfile, forceReplace);
                            }
                        }
                    }
                }
            }
        }

        public static void PlaySound(string sound, float pitch, float pan, float volume, bool stopMusic)
        {
            if (!_muted)
            {
                var soundEffect = GetSoundEffect(sound);
                if (soundEffect != null)
                {
                    Logger.Debug("SoundEffect [" + sound + "]");

                    if (CanPlaySound())
                    {
                        soundEffect.Play(volume, pitch, pan);

                        if (stopMusic)
                            MusicManager.PauseForSound(soundEffect);
                    }
                    else
                        Logger.Log(Logger.LogTypes.Warning, "SoundManager.vb: Failed to play sound: No audio devices found.");
                }
            }
        }

        public static void PlayPokemonCry(int number) => PlayPokemonCry(number, 0f, 0f, Volume);
        public static void PlayPokemonCry(int number, float pitch, float pan) => PlayPokemonCry(number, pitch, pan, Volume);
        public static void PlayPokemonCry(int number, float pitch, float pan, float volume)
        {
            if (!_muted)
            {
                string soundfile = "Cries\\" + number + ".xnb";
                if (GameModeManager.ContentFileExists("Sounds\\" + soundfile))
                {
                    AddSound("Cries\\" + number, false);

                    var soundEffect = GetSoundEffect("Cries\\" + number);

                    if (CanPlaySound())
                        soundEffect?.Play(volume * 0.6f, pitch, pan);
                    else
                        Logger.Log(Logger.LogTypes.Warning, "SoundManager.vb: Failed to play sound: No audio devices found.");
                }
            }
        }

        public static void Mute(bool mute) { _muted = mute; }

        public static void PlaySound(string sound) => PlaySound(sound, 0f, 0f, Volume, false);
        public static void PlaySound(string sound, bool stopMusic) => PlaySound(sound, 0f, 0f, Volume, stopMusic);

        public static void ReloadSounds()
        {
            SoundFiles.Clear();
            LoadSounds(false);
        }

        [DllImport("winmm.dll", EntryPoint = "waveOutGetNumDevs", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int GetAudioOutputDevices();

        private static bool CanPlaySound() => GetAudioOutputDevices() > 0;
    }
}

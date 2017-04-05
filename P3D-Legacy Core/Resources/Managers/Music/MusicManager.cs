using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace P3D.Legacy.Core.Resources.Managers.Music
{
    public class MusicManager
    {
        public static MusicTrack MusicTrack { get; } = new MusicTrack();

        public static float Volume { get { return MusicTrack.Volume; } set { MusicTrack.Volume = value; } }

        public static bool MusicExists(string musicName) => GetMusic(musicName, false) != null;

        public static MusicClip GetMusic(string musicName, bool logErrors) => MusicTrack.GetMusic(musicName, logErrors);
        public static string GetCurrentMusic => MusicTrack.MusicNamePlaying;

        public static void LoadMusic(bool forceReplace = false) => MusicTrack.LoadMusic(forceReplace);
        public static void ReloadMusic() => MusicTrack.ReloadMusic();

        public static void PlayNoMusic() => MusicTrack.PlayNoMusic();

        public static void PlayMusic(string musicName) => PlayMusic(musicName, 0f, 0f);
        public static void PlayMusic(string musicName, bool searchForIntro) => PlayMusic(musicName, searchForIntro, 0.02f, 0.02f);
        public static void PlayMusic(string musicName, bool searchForIntro, float newFadeInSpeed, float newFadeOutSpeed)
        {
            MediaPlayer.IsRepeating = true;

            var musicNameLower = musicName.ToLowerInvariant();
            if (searchForIntro)
            {
                var introMusicNameLower = $"intro|{musicNameLower}";
                PlayMusic(MusicTrack.Contains(introMusicNameLower) ? introMusicNameLower : musicNameLower, newFadeInSpeed, newFadeOutSpeed);
            }
            else
                PlayMusic(musicNameLower, newFadeInSpeed, newFadeOutSpeed);
        }
        public static void PlayMusic(string musicName, float newFadeInSpeed, float newFadeOutSpeed)
        {
            if (!MusicTrack.IsPlaying || musicName != MusicTrack.MusicNamePlaying)
            {
                Logger.Debug($"Play [{musicName}]");
                MusicTrack.Play(musicName);
            }
        }

        public static void Stop() => MusicTrack.Stop();
        public static void Pause() => MusicTrack.Pause();
        public static void Pause(TimeSpan duration) => MusicTrack.Pause(duration);
        public static void Resume() => MusicTrack.Resume();

        public static void Mute(bool mute) => MusicTrack.Mute(mute);

        public static void Update(GameTime gameTime) => MusicTrack.Update(gameTime);
    }
}

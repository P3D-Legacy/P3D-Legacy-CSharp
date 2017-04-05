namespace P3D.Legacy.Core.Resources.Managers.Sound
{
    public static class SoundEffectManager
    {
        public static SoundEffectTrack SoundEffectTrack { get; } = new SoundEffectTrack();

        public static float Volume { get { return SoundEffectTrack.Volume; } set { SoundEffectTrack.Volume = value; } }

        public static void LoadSounds(bool forceReplace = false) => SoundEffectTrack.LoadSounds(forceReplace);

        public static void PlaySound(string sound) => PlaySound(sound, 0f, 0f, Volume, false);
        public static void PlaySound(string sound, bool stopMusic) => PlaySound(sound, 0f, 0f, Volume, stopMusic);
        public static void PlaySound(string soundEffectName, float pitch, float pan, float volume, bool stopMusic)
        {
            soundEffectName = soundEffectName.Replace("\\", "|");

            Logger.Debug($"SoundEffect [{soundEffectName}]");
            SoundEffectTrack.Play(soundEffectName, pitch, pan, volume, stopMusic);
        }

        public static void PlayPokemonCry(int number) => PlayPokemonCry(number, 0f, 0f, Volume);
        public static void PlayPokemonCry(int number, float pitch, float pan) => PlayPokemonCry(number, pitch, pan, Volume);
        public static void PlayPokemonCry(int number, float pitch, float pan, float volume)
        {
            Logger.Debug($"SoundEffect Cry:{number}");
            SoundEffectTrack.PlayCry(number, pitch, pan, volume);
        }

        public static void Mute(bool mute) => SoundEffectTrack.Mute(mute);

        public static void ReloadSounds() => SoundEffectTrack.ReloadSoundEffects();
    }
}
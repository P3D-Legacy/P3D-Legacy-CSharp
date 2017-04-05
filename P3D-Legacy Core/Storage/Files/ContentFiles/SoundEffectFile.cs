using System;

using Microsoft.Xna.Framework.Audio;

using P3D.Legacy.Core.Storage.Folders.ContentFolders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Files.ContentFiles
{
    public class SoundEffectFile : BaseChildContentFile
    {
        public static implicit operator SoundEffect(SoundEffectFile soundEffectFile) => soundEffectFile.SoundEffect;

        private SoundEffect _soundEffect;
        private SoundEffect SoundEffect => _soundEffect ?? (_soundEffect = ContentManager.Load<SoundEffect>(ContentLocalPathWithoutExtension));

        public SoundEffectFile(MusicFile file) : base(file, file.Parent) { }
        public SoundEffectFile(IFile file, BaseChildContentFolder parent) : base(file, parent) { }

        public override bool ForceLoad() => SoundEffect != null;

        public TimeSpan Duration => SoundEffect.Duration;

        public SoundEffectInstance CreateInstance() => SoundEffect.CreateInstance();
    }
}
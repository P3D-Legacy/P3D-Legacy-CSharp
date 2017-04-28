using System;

using Microsoft.Xna.Framework.Media;

using P3D.Legacy.Core.Storage.Folders.ContentFolders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Files.ContentFiles
{
    public class MusicFile : BaseChildContentFile
    {
        public static implicit operator Song(MusicFile musicFile) => musicFile.Song;

        private Song _song;
        private Song Song => _song ?? (_song = ContentManager.Load<Song>(ContentLocalPathWithoutExtension));
        public TimeSpan Duration => Song.Duration;

        public MusicFile(IFile file, BaseContentChildFolder parent) : base(file, parent) { }

        public override bool ForceLoad()
        {
            try { return Song != null; }
            catch (Exception) { return false; }         
        }
    }
}
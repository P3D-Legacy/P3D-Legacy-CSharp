using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Core.Storage.Files.ContentFiles;
using P3D.Legacy.Shared.Extensions;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders.ContentFolders
{
    public class BaseMusicFolder : BaseChildContentFolder
    {
        public BaseMusicFolder(IFolder folder, BaseContentFolder parent) : base(folder, parent) { }

        //public MusicFile GetMusicFile(string fileName) => new MusicFile(GetFile(fileName), this);
        //public IList<MusicFile> GetAllMusicFiles() => GetFiles().Where(file => file.Name.EndsWith(".ogg")).Select(file => new MusicFile(file, this)).ToList();

        public MusicFile GetMusicFile(string fileName) => GetAllMusicFiles().FirstOrDefault(file => file.InContentLocalPathWithoutExtension == fileName);
        public IList<MusicFile> GetAllMusicFiles() => this.GetAllFiles().Where(file => file.Name.EndsWith(".ogg")).Select(file => new MusicFile(file, this)).ToList();
    }

    public class MusicFolder : BaseMusicFolder
    {
        public MusicFolder(IFolder folder, BaseContentFolder parent) : base(folder, parent) { }
    }
}
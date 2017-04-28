using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Core.Storage.Files.ContentFiles;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders.ContentFolders
{
    public class BaseMusicFolder : BaseContentChildFolder
    {
        public BaseMusicFolder(IFolder folder, BaseContentFolder parent) : base(folder, parent) { }

        public MusicFile GetMusicFile(string fileName) => GetAllMusicFiles().FirstOrDefault(file => file.InContentLocalPathWithoutExtension == fileName);
        public IList<MusicFile> GetAllMusicFiles() => GetFiles("*.ogg", FolderSearchOption.AllFolders).Select(file => new MusicFile(file, this)).ToList();
    }

    public class MusicFolder : BaseMusicFolder
    {
        public MusicFolder(IFolder folder, BaseContentFolder parent) : base(folder, parent) { }
    }
}
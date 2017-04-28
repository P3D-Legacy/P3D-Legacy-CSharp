using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Core.Storage.Files.ContentFiles;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders.ContentFolders
{
    public class BaseSoundEffectsFolder : BaseContentChildFolder
    {
        public BaseSoundEffectsFolder(IFolder folder, BaseContentFolder parent) : base(folder, parent) { }

        public SoundEffectFile GetSoundEffectFile(string fileName) => GetAllSoundEffectFiles().FirstOrDefault(file => file.InContentLocalPathWithoutExtension == fileName);
        public IList<SoundEffectFile> GetAllSoundEffectFiles() => GetFiles("*.xnb", FolderSearchOption.AllFolders).Select(file => new SoundEffectFile(file, this)).ToList();
    }

    public class SoundEffectsFolder : BaseSoundEffectsFolder
    {
        public SoundEffectsFolder(IFolder folder, BaseContentFolder parent) : base(folder, parent) { }
    }
}
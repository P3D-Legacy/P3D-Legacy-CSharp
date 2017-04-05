using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Core.Storage.Files.ContentFiles;
using P3D.Legacy.Shared.Extensions;

using PCLExt.FileStorage;


namespace P3D.Legacy.Core.Storage.Folders.ContentFolders
{
    public class BaseSoundEffectsFolder : BaseChildContentFolder
    {
        public BaseSoundEffectsFolder(IFolder folder, BaseContentFolder parent) : base(folder, parent) { }

        public SoundEffectFile GetSoundEffectFile(string fileName) => GetAllSoundEffectFiles().FirstOrDefault(file => file.InContentLocalPathWithoutExtension == fileName);
        public IList<SoundEffectFile> GetAllSoundEffectFiles() => this.GetAllFiles().Where(file => file.Name.EndsWith(".xnb")).Select(file => new SoundEffectFile(file, this)).ToList();
    }

    public class SoundEffectsFolder : BaseSoundEffectsFolder
    {
        public SoundEffectsFolder(IFolder folder, BaseContentFolder parent) : base(folder, parent) { }
    }
}
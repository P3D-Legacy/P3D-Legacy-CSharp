using P3D.Legacy.Core.Storage.Folders.ContentFolders;
using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public class ContentPacksFolder : BaseFolder
    {
        public ContentPacksFolder(IFolder folder) : base(folder) { }

        public ContentPackFolder GetContentPack(string name) => new ContentPackFolder(CreateFolder(name, CreationCollisionOption.OpenIfExists));
    }

    public class ContentPackFolder : BaseContentFolder
    {
        public FontFolder FontFolder => new FontFolder(CreateFolder("Fonts", CreationCollisionOption.OpenIfExists), this);
        public MusicFolder MusicFolder => new MusicFolder(CreateFolder("Music", CreationCollisionOption.OpenIfExists), this);
        public SoundEffectsFolder SoundEffectsFolder => new SoundEffectsFolder(CreateFolder("Sound Effects", CreationCollisionOption.OpenIfExists), this);

        public ContentPackFolder(IFolder folder) : base(folder) { }
    }
}
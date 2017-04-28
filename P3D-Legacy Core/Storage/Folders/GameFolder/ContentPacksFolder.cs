using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public class ContentPacksFolder : BaseFolder
    {
        //public ContentPacksFolder() : base(new MainFolder().CreateFolder("ContentPacks", CreationCollisionOption.OpenIfExists)) { }
        public ContentPacksFolder() : base(new GameModesFolder().CreateFolder("ContentPacks", CreationCollisionOption.OpenIfExists)) { }

        public ContentFolder GetContentPack(string name) => new ContentFolder(CreateFolder(name, CreationCollisionOption.OpenIfExists));
    }

    //public class ContentPackFolder : BaseContentFolder
    //{
    //    public ContentPackFolder(IFolder folder) : base(folder) { }
    //}
}
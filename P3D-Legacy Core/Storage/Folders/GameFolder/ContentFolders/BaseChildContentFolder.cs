using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders.ContentFolders
{
    public abstract class BaseContentChildFolder : BaseFolder
    {
        public BaseContentFolder ContentFolder { get; }

        public BaseContentChildFolder(IFolder folder, BaseContentFolder contentFolder) : base(folder) { ContentFolder = contentFolder; }
    }
}

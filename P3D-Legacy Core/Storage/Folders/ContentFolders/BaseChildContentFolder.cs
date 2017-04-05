using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders.ContentFolders
{
    public abstract class BaseChildContentFolder : BaseFolder
    {
        public BaseContentFolder ContentFolder { get; }

        public BaseChildContentFolder(IFolder folder, BaseContentFolder contentFolder) : base(folder) { ContentFolder = contentFolder; }
    }
}

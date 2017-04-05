using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public class MapsFolder : BaseFolder
    {
        public MapsFolder(IFolder folder) : base(folder) { }
        public MapsFolder(string path) : base(path) { }
    }
}
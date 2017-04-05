using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public class PokeFolder : BaseFolder
    {
        public PokeFolder(IFolder folder) : base(folder) { }
        public PokeFolder(string path) : base(path) { }
    }
}
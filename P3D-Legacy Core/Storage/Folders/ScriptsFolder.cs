using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public class ScriptsFolder : BaseFolder
    {
        public ScriptsFolder(IFolder folder) : base(folder) { }
        public ScriptsFolder(string path) : base(path) { }
    }
}
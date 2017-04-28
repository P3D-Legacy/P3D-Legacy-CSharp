using P3D.Legacy.Shared.Storage.Folders;
using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public abstract class BaseGameChildFolder : BaseFolder
    {
        protected static IFolder FromLocalPath(string path)
        {
            path = path.Replace("\\", "|").Replace("/", "|");

            IFolder folder = new MainFolder();
            foreach (var folderName in path.Split('|'))
                folder = folder.CreateFolder(folderName, CreationCollisionOption.OpenIfExists);
            return folder;
        }

        public string LocalPath => Path.Remove(0, new MainFolder().Path.Length).TrimStart('/', '|');

        protected BaseGameChildFolder() : base(new GameFolder()) { }
        protected BaseGameChildFolder(IFolder folder) : base(folder) { }
    }
}
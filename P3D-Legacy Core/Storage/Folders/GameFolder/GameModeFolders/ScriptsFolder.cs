using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public class ScriptsFolder : BaseGameModeChildFolder
    {
        public ScriptsFolder() : base(new GameFolder().CreateFolder("Scripts", CreationCollisionOption.OpenIfExists)) { }
        public ScriptsFolder(string localPath) : base(FromLocalPath(localPath)) { }
    }
}
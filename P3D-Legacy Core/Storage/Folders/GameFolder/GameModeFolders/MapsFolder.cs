using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public class MapsFolder : BaseGameModeChildFolder
    {
        public MapsFolder() : base(new GameFolder().CreateFolder("maps", CreationCollisionOption.OpenIfExists)) { }
        public MapsFolder(string localPath) : base(FromLocalPath(localPath)) { }
    }
}
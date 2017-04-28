using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public class GameModesFolder : BaseGameChildFolder
    {
        public GameModesFolder() : base(new GameFolder().CreateFolder("GameModes", CreationCollisionOption.OpenIfExists)) { }
        public GameModesFolder(string localPath) : base(FromLocalPath(localPath)) { }
    }
}
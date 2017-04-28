using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public class ScreenshotsFolder : BaseGameChildFolder
    {
        public ScreenshotsFolder() : base(new GameFolder().CreateFolder("Screenshots", CreationCollisionOption.OpenIfExists)) { }
        public ScreenshotsFolder(string localPath) : base(FromLocalPath(localPath)) { }
    }
}
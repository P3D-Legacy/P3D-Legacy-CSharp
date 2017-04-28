using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public class PokeFolder : BaseGameModeChildFolder
    {
        public PokeFolder() : base(new MapsFolder().CreateFolder("poke", CreationCollisionOption.OpenIfExists)) { }
        public PokeFolder(string localPath) : base(FromLocalPath(localPath)) { }
    }
}
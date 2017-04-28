using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public class PokemonDataFolder : GameModeFolder
    {
        public PokemonDataFolder() : base(new PokemonFolder().CreateFolder("Data", CreationCollisionOption.OpenIfExists)) { }
        public PokemonDataFolder(string localPath) : base(FromLocalPath(localPath)) { }
    }
}
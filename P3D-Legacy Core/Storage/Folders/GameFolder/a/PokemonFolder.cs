using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public class PokemonFolder : GameModeFolder
    {
        public PokemonFolder() : base(new ContentFolder().CreateFolder("Pokemon", CreationCollisionOption.OpenIfExists)) { }
        public PokemonFolder(string localPath) : base(FromLocalPath(localPath)) { }
    }
}
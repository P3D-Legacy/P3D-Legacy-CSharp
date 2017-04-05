using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public class PokemonDataFolder : BaseFolder
    {
        public PokemonDataFolder(IFolder folder) : base(folder) { }
        public PokemonDataFolder(string path) : base(path) { }
    }
}
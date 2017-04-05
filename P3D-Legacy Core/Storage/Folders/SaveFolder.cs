using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using P3D.Legacy.Shared.Storage.Folders;
using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public class SaveFolder : BaseFolder
    {
        public SaveFolder(IFolder folder) : base(folder) { }
        public SaveFolder(string path) : base(path) { }

        public UserSaveFolder AutosaveFolder => new UserSaveFolder(CreateFolder("autosave", CreationCollisionOption.OpenIfExists));

        public IFile OptionsFile => CreateFile("Options.yml", CreationCollisionOption.OpenIfExists);
        public IFile KeyboardFile => CreateFile("Keyboard.yml", CreationCollisionOption.OpenIfExists);
        public IFile ServerListFile => CreateFile("server_list.dat", CreationCollisionOption.OpenIfExists);
        public IFile LastSessionFile => CreateFile("lastSession.id", CreationCollisionOption.OpenIfExists); // -- Not used

        public UserSaveFolder GetUserSaveFolder(string username) => new UserSaveFolder(CreateFolder(username, CreationCollisionOption.OpenIfExists));
        public async Task<UserSaveFolder> GetUserSaveFolderAsync(string username) => new UserSaveFolder(await CreateFolderAsync(username, CreationCollisionOption.OpenIfExists));
    }

    public class UserSaveFolder : BaseFolder
    {
        public UserSaveFolder(IFolder folder) : base(folder) { }
        public UserSaveFolder(string path) : base(path) { }

        public IFile PartyFile => CreateFile("Party.dat", CreationCollisionOption.OpenIfExists);
        public IFile PlayerFile => CreateFile("Player.dat", CreationCollisionOption.OpenIfExists);
        public IFile OptionsFile => CreateFile("Options.dat", CreationCollisionOption.OpenIfExists);
        public IFile ItemsFile => CreateFile("Items.dat", CreationCollisionOption.OpenIfExists);
        public IFile BerriesFile => CreateFile("Berries.dat", CreationCollisionOption.OpenIfExists);
        public IFile ApricornsFile => CreateFile("Apricorns.dat", CreationCollisionOption.OpenIfExists);
        public IFile DaycareFile => CreateFile("Daycare.dat", CreationCollisionOption.OpenIfExists);
        public IFile PokedexFile => CreateFile("Pokedex.dat", CreationCollisionOption.OpenIfExists);
        public IFile RegisterFile => CreateFile("Register.dat", CreationCollisionOption.OpenIfExists);
        public IFile ItemDataFile => CreateFile("ItemData.dat", CreationCollisionOption.OpenIfExists);
        public IFile BoxFile => CreateFile("Box.dat", CreationCollisionOption.OpenIfExists);
        public IFile NPCFile => CreateFile("NPC.dat", CreationCollisionOption.OpenIfExists);
        public IFile HallOfFameFile => CreateFile("HallOfFame.dat", CreationCollisionOption.OpenIfExists);
        public IFile SecretBaseFile => CreateFile("SecretBase.dat", CreationCollisionOption.OpenIfExists);
        public IFile RoamingPokemonFile => CreateFile("RoamingPokemon.dat", CreationCollisionOption.OpenIfExists);
        public IFile StatisticsFile => CreateFile("Statistics.dat", CreationCollisionOption.OpenIfExists);
    }
}
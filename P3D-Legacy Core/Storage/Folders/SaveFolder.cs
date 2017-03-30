using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public class SaveFolder : IFolder
    {
        private readonly IFolder _folder;
        public string Name => _folder.Name;
        public string Path => _folder.Path;

        public SaveFolder(IFolder folder) { _folder = folder; }
        public SaveFolder(string path) { _folder = FileSystem.Current.GetFolderFromPath(path); }

        public IFile GetFile(string name) => _folder.GetFile(name);
        public Task<IFile> GetFileAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFileAsync(name, cancellationToken);
        public IList<IFile> GetFiles() => _folder.GetFiles();
        public Task<IList<IFile>> GetFilesAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFilesAsync(cancellationToken);
        public IFile CreateFile(string desiredName, CreationCollisionOption option) => _folder.CreateFile(desiredName, option);
        public Task<IFile> CreateFileAsync(string desiredName, CreationCollisionOption option, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CreateFileAsync(desiredName, option, cancellationToken);
        public IFolder CreateFolder(string desiredName, CreationCollisionOption option) => _folder.CreateFolder(desiredName, option);
        public Task<IFolder> CreateFolderAsync(string desiredName, CreationCollisionOption option, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CreateFolderAsync(desiredName, option, cancellationToken);
        public IFolder GetFolder(string name) => _folder.GetFolder(name);
        public Task<IFolder> GetFolderAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFolderAsync(name, cancellationToken);
        public IList<IFolder> GetFolders() => _folder.GetFolders();
        public Task<IList<IFolder>> GetFoldersAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFoldersAsync(cancellationToken);
        public ExistenceCheckResult CheckExists(string name) => _folder.CheckExists(name);
        public Task<ExistenceCheckResult> CheckExistsAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CheckExistsAsync(name, cancellationToken);
        public void Delete() => _folder.Delete();
        public Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.DeleteAsync(cancellationToken);
        public IFolder Move(IFolder folder, NameCollisionOption option = NameCollisionOption.ReplaceExisting) => _folder.Move(folder, option);
        public Task<IFolder> MoveAsync(IFolder folder, NameCollisionOption option = NameCollisionOption.ReplaceExisting, CancellationToken cancellationToken = new CancellationToken()) => _folder.MoveAsync(folder, option, cancellationToken);


        public UserSaveFolder AutosaveFolder => new UserSaveFolder(CreateFolder("autosave", CreationCollisionOption.OpenIfExists));

        public IFile OptionsFile => CreateFile("Options.yml", CreationCollisionOption.OpenIfExists);
        public IFile KeyboardFile => CreateFile("Keyboard.yml", CreationCollisionOption.OpenIfExists);
        public IFile ServerListFile => CreateFile("server_list.dat", CreationCollisionOption.OpenIfExists);
        public IFile LastSessionFile => CreateFile("lastSession.id", CreationCollisionOption.OpenIfExists); // -- Not used

        public UserSaveFolder GetUserSaveFolder(string username) => new UserSaveFolder(CreateFolder(username, CreationCollisionOption.OpenIfExists));
        public async Task<UserSaveFolder> GetUserSaveFolderAsync(string username) => new UserSaveFolder(await CreateFolderAsync(username, CreationCollisionOption.OpenIfExists));
    }

    public class UserSaveFolder : IFolder
    {
        private readonly IFolder _folder;
        public string Name => _folder.Name;
        public string Path => _folder.Path;

        public UserSaveFolder(IFolder folder) { _folder = folder; }
        public UserSaveFolder(string path) { _folder = FileSystem.Current.GetFolderFromPath(path); }

        public IFile GetFile(string name) => _folder.GetFile(name);
        public Task<IFile> GetFileAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFileAsync(name, cancellationToken);
        public IList<IFile> GetFiles() => _folder.GetFiles();
        public Task<IList<IFile>> GetFilesAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFilesAsync(cancellationToken);
        public IFile CreateFile(string desiredName, CreationCollisionOption option) => _folder.CreateFile(desiredName, option);
        public Task<IFile> CreateFileAsync(string desiredName, CreationCollisionOption option, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CreateFileAsync(desiredName, option, cancellationToken);
        public IFolder CreateFolder(string desiredName, CreationCollisionOption option) => _folder.CreateFolder(desiredName, option);
        public Task<IFolder> CreateFolderAsync(string desiredName, CreationCollisionOption option, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CreateFolderAsync(desiredName, option, cancellationToken);
        public IFolder GetFolder(string name) => _folder.GetFolder(name);
        public Task<IFolder> GetFolderAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFolderAsync(name, cancellationToken);
        public IList<IFolder> GetFolders() => _folder.GetFolders();
        public Task<IList<IFolder>> GetFoldersAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFoldersAsync(cancellationToken);
        public ExistenceCheckResult CheckExists(string name) => _folder.CheckExists(name);
        public Task<ExistenceCheckResult> CheckExistsAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CheckExistsAsync(name, cancellationToken);
        public void Delete() => _folder.Delete();
        public Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.DeleteAsync(cancellationToken);
        public IFolder Move(IFolder folder, NameCollisionOption option = NameCollisionOption.ReplaceExisting) => _folder.Move(folder, option);
        public Task<IFolder> MoveAsync(IFolder folder, NameCollisionOption option = NameCollisionOption.ReplaceExisting, CancellationToken cancellationToken = new CancellationToken()) => _folder.MoveAsync(folder, option, cancellationToken);


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
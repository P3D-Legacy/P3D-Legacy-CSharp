using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using P3D.Legacy.Shared.Extensions;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public class SaveFolder : IFolder
    {
        private IFolder _folder;
        public string Name => _folder.Name;
        public string Path => _folder.Path;

        public SaveFolder(IFolder folder) { _folder = folder; }
        public SaveFolder(string path) { _folder = FileSystem.Current.GetFolderFromPathAsync(path).Result; }

        public Task<ExistenceCheckResult> CheckExistsAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CheckExistsAsync(name, cancellationToken);
        public Task<IFile> CreateFileAsync(string desiredName, CreationCollisionOption option, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CreateFileAsync(desiredName, option, cancellationToken);
        public Task<IFolder> CreateFolderAsync(string desiredName, CreationCollisionOption option, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CreateFolderAsync(desiredName, option, cancellationToken);
        public Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.DeleteAsync(cancellationToken);
        public Task<IFile> GetFileAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFileAsync(name, cancellationToken);
        public Task<IList<IFile>> GetFilesAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFilesAsync(cancellationToken);
        public Task<IFolder> GetFolderAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFolderAsync(name, cancellationToken);
        public Task<IList<IFolder>> GetFoldersAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFoldersAsync(cancellationToken);
        public Task<IFolder> MoveAsync(IFolder folder, NameCollisionOption option = NameCollisionOption.ReplaceExisting, CancellationToken cancellationToken = new CancellationToken()) => _folder.MoveAsync(folder, option, cancellationToken);


        public async Task<UserSaveFolder> GetUserSaveFolder(string username) => new UserSaveFolder(await CreateFolderAsync(username, CreationCollisionOption.OpenIfExists));

        public UserSaveFolder AutosaveFolder => AsyncExtensions.RunSync(async () => new UserSaveFolder(await CreateFolderAsync("autosave", CreationCollisionOption.OpenIfExists)));

        public IFile OptionsFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("Options.yml", CreationCollisionOption.OpenIfExists));
        public IFile KeyboardFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("Keyboard.yml", CreationCollisionOption.OpenIfExists));
        public IFile ServerListFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("server_list.dat", CreationCollisionOption.OpenIfExists));
        public IFile LastSessionFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("lastSession.id", CreationCollisionOption.OpenIfExists));
    }

    public class UserSaveFolder : IFolder
    {
        private IFolder _folder;
        public string Name => _folder.Name;
        public string Path => _folder.Path;

        public UserSaveFolder(IFolder folder) { _folder = folder; }
        public UserSaveFolder(string path) { _folder = FileSystem.Current.GetFolderFromPathAsync(path).Result; }

        public Task<ExistenceCheckResult> CheckExistsAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CheckExistsAsync(name, cancellationToken);
        public Task<IFile> CreateFileAsync(string desiredName, CreationCollisionOption option, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CreateFileAsync(desiredName, option, cancellationToken);
        public Task<IFolder> CreateFolderAsync(string desiredName, CreationCollisionOption option, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CreateFolderAsync(desiredName, option, cancellationToken);
        public Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.DeleteAsync(cancellationToken);
        public Task<IFile> GetFileAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFileAsync(name, cancellationToken);
        public Task<IList<IFile>> GetFilesAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFilesAsync(cancellationToken);
        public Task<IFolder> GetFolderAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFolderAsync(name, cancellationToken);
        public Task<IList<IFolder>> GetFoldersAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFoldersAsync(cancellationToken);
        public Task<IFolder> MoveAsync(IFolder folder, NameCollisionOption option = NameCollisionOption.ReplaceExisting, CancellationToken cancellationToken = new CancellationToken()) => _folder.MoveAsync(folder, option, cancellationToken);


        public IFile PartyFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("Party.dat", CreationCollisionOption.OpenIfExists));
        public IFile PlayerFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("Player.dat", CreationCollisionOption.OpenIfExists));
        public IFile OptionsFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("Options.dat", CreationCollisionOption.OpenIfExists));
        public IFile ItemsFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("Items.dat", CreationCollisionOption.OpenIfExists));
        public IFile BerriesFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("Berries.dat", CreationCollisionOption.OpenIfExists));
        public IFile ApricornsFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("Apricorns.dat", CreationCollisionOption.OpenIfExists));
        public IFile DaycareFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("Daycare.dat", CreationCollisionOption.OpenIfExists));
        public IFile PokedexFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("Pokedex.dat", CreationCollisionOption.OpenIfExists));
        public IFile RegisterFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("Register.dat", CreationCollisionOption.OpenIfExists));
        public IFile ItemDataFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("ItemData.dat", CreationCollisionOption.OpenIfExists));
        public IFile BoxFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("Box.dat", CreationCollisionOption.OpenIfExists));
        public IFile NPCFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("NPC.dat", CreationCollisionOption.OpenIfExists));
        public IFile HallOfFameFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("HallOfFame.dat", CreationCollisionOption.OpenIfExists));
        public IFile SecretBaseFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("SecretBase.dat", CreationCollisionOption.OpenIfExists));
        public IFile RoamingPokemonFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("RoamingPokemon.dat", CreationCollisionOption.OpenIfExists));
        public IFile StatisticsFile => AsyncExtensions.RunSync(async () => await CreateFileAsync("Statistics.dat", CreationCollisionOption.OpenIfExists));
    }
}
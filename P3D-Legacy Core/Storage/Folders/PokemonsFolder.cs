using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage
{
    public class PokemonDataFolder : IFolder
    {
        private IFolder _folder;

        public string Name => _folder.Name;
        public string Path => _folder.Path;

        public PokemonDataFolder(IFolder folder) { _folder = folder; }
        public PokemonDataFolder(string path) { _folder = FileSystem.Current.GetFolderFromPathAsync(path).Result; }

        public Task<ExistenceCheckResult> CheckExistsAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CheckExistsAsync(name, cancellationToken);
        public Task<IFile> CreateFileAsync(string desiredName, CreationCollisionOption option, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CreateFileAsync(desiredName, option, cancellationToken);
        public Task<IFolder> CreateFolderAsync(string desiredName, CreationCollisionOption option, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CreateFolderAsync(desiredName, option, cancellationToken);
        public Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.DeleteAsync(cancellationToken);
        public Task<IFile> GetFileAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFileAsync(name, cancellationToken);
        public Task<IList<IFile>> GetFilesAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFilesAsync(cancellationToken);
        public Task<IFolder> GetFolderAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFolderAsync(name, cancellationToken);
        public Task<IList<IFolder>> GetFoldersAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFoldersAsync(cancellationToken);
    }
}
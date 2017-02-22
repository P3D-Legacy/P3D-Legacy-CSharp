using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage
{
    public class LocalizationFolder : ILocalizationFolder
    {
        private const string Prefix = "Translation_";
        private const string FileExtension = ".dat";

        private IFolder _folder;

        public string Name => _folder.Name;
        public string Path => _folder.Path;

        public LocalizationFolder(IFolder folder) { _folder = folder; }
        public LocalizationFolder(string path) { _folder = FileSystem.Current.GetFolderFromPathAsync(path).Result; }

        public Task<ExistenceCheckResult> CheckExistsAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CheckExistsAsync(name, cancellationToken);
        public Task<IFile> CreateFileAsync(string desiredName, CreationCollisionOption option, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CreateFileAsync(desiredName, option, cancellationToken);
        public Task<IFolder> CreateFolderAsync(string desiredName, CreationCollisionOption option, CancellationToken cancellationToken = default(CancellationToken)) => _folder.CreateFolderAsync(desiredName, option, cancellationToken);
        public Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.DeleteAsync(cancellationToken);
        public Task<IFile> GetFileAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFileAsync(name, cancellationToken);
        public Task<IList<IFile>> GetFilesAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFilesAsync(cancellationToken);
        public Task<IFolder> GetFolderAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFolderAsync(name, cancellationToken);
        public Task<IList<IFolder>> GetFoldersAsync(CancellationToken cancellationToken = default(CancellationToken)) => _folder.GetFoldersAsync(cancellationToken);

        public async Task<ILocalizationFile> GetTranslationFileAsync(CultureInfo language, CancellationToken cancellationToken = default(CancellationToken)) => (await GetTranslationFilesAsync(cancellationToken)).Single(file => Equals(file.Language, language));
        public async Task<IList<ILocalizationFile>> GetTranslationFilesAsync(CancellationToken cancellationToken = default(CancellationToken)) => (await GetFilesAsync(cancellationToken)).Where(file => file.Name.StartsWith(Prefix) && file.Name.EndsWith(FileExtension)).Select(file => new LocalizationFile(file)).ToList<ILocalizationFile>();
        public async Task<bool> CheckTranslationExistsAsync(CultureInfo language, CancellationToken cancellationToken = default(CancellationToken)) => (await GetTranslationFilesAsync(cancellationToken)).Any(file => Equals(file.Language, language));
    }
}

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using P3D.Legacy.Core.Storage.Files;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    /*
    public interface ILocalizationFolder : IFolder
    {
        Task<ILocalizationFile> GetTranslationFileAsync(CultureInfo language, CancellationToken cancellationToken = default(CancellationToken));
        Task<IList<ILocalizationFile>> GetTranslationFilesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> CheckTranslationExistsAsync(CultureInfo language, CancellationToken cancellationToken = default(CancellationToken));
    }
    */
    // Replace with LocalizationFolder later
    public class TokensFolder : IFolder//: ILocalizationFolder
    {
        private readonly IFolder _folder;
        public string Name => _folder.Name;
        public string Path => _folder.Path;

        public TokensFolder(IFolder folder) { _folder = folder; }
        public TokensFolder(string path) { _folder = FileSystem.Current.GetFolderFromPath(path); }

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

        public TokensFile GetTranslationFile(CultureInfo language) => GetTranslationFiles().Single(file => Equals(file.Language, language));
        public async Task<TokensFile> GetTranslationFileAsync(CultureInfo language, CancellationToken cancellationToken = default(CancellationToken)) => (await GetTranslationFilesAsync(cancellationToken)).Single(file => Equals(file.Language, language));
        public IList<TokensFile> GetTranslationFiles() => GetFiles().Where(file => file.Name.StartsWith(TokensFile.Prefix) && file.Name.EndsWith(TokensFile.FileExtension)).Select(file => new TokensFile(file)).ToList();
        public async Task<IList<TokensFile>> GetTranslationFilesAsync(CancellationToken cancellationToken = default(CancellationToken)) => (await GetFilesAsync(cancellationToken)).Where(file => file.Name.StartsWith(TokensFile.Prefix) && file.Name.EndsWith(TokensFile.FileExtension)).Select(file => new TokensFile(file)).ToList();
        public bool CheckTranslationExists(CultureInfo language) => GetTranslationFiles().Any(file => Equals(file.Language, language));
        public async Task<bool> CheckTranslationExistsAsync(CultureInfo language, CancellationToken cancellationToken = default(CancellationToken)) => (await GetTranslationFilesAsync(cancellationToken)).Any(file => Equals(file.Language, language));
    }
}

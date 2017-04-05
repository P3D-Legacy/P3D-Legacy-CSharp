using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using P3D.Legacy.Core.Storage.Files;
using P3D.Legacy.Shared.Storage.Folders;
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
    public class TokensFolder : BaseFolder //: ILocalizationFolder
    {
        public TokensFolder(IFolder folder) : base(folder) { }
        public TokensFolder(string path) : base(path) { }

        public TokensFile GetTranslationFile(CultureInfo language) => GetTranslationFiles().Single(file => Equals(file.Language, language));
        public async Task<TokensFile> GetTranslationFileAsync(CultureInfo language, CancellationToken cancellationToken = default(CancellationToken)) => (await GetTranslationFilesAsync(cancellationToken)).Single(file => Equals(file.Language, language));
        public IList<TokensFile> GetTranslationFiles() => GetFiles().Where(file => file.Name.StartsWith(TokensFile.Prefix) && file.Name.EndsWith(TokensFile.FileExtension)).Select(file => new TokensFile(file)).ToList();
        public async Task<IList<TokensFile>> GetTranslationFilesAsync(CancellationToken cancellationToken = default(CancellationToken)) => (await GetFilesAsync(cancellationToken)).Where(file => file.Name.StartsWith(TokensFile.Prefix) && file.Name.EndsWith(TokensFile.FileExtension)).Select(file => new TokensFile(file)).ToList();
        public bool CheckTranslationExists(CultureInfo language) => GetTranslationFiles().Any(file => Equals(file.Language, language));
        public async Task<bool> CheckTranslationExistsAsync(CultureInfo language, CancellationToken cancellationToken = default(CancellationToken)) => (await GetTranslationFilesAsync(cancellationToken)).Any(file => Equals(file.Language, language));
    }
}

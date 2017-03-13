using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

using P3D.Legacy.Core.Storage.Files;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public interface ILocalizationFolder : IFolder
    {
        Task<ILocalizationFile> GetTranslationFileAsync(CultureInfo language, CancellationToken cancellationToken = default(CancellationToken));
        Task<IList<ILocalizationFile>> GetTranslationFilesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> CheckTranslationExistsAsync(CultureInfo language, CancellationToken cancellationToken = default(CancellationToken));
    }
}

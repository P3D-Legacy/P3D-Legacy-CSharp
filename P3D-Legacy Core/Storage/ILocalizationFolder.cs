using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage
{
    public interface ILocalizationFolder : IFolder
    {
        Task<ILocalizationFile> GetTranslationFileAsync(CultureInfo language, CancellationToken cancellationToken = default(CancellationToken));
        Task<IList<ILocalizationFile>> GetTranslationFilesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}

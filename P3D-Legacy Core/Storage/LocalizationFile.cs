using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage
{
    public class LocalizationFile : ILocalizationFile
    {
        private const string Prefix = "Translation_";
        private const string FileExtension = ".dat";

        private IFile _file;

        public string Name => _file.Name;
        public string Path => _file.Path;

        public CultureInfo Language { get; }

        public LocalizationFile(IFile file)
        {
            _file = file;

            Language = new CultureInfo(Name.Replace(Prefix, "").Replace(FileExtension, ""));
        }
        public LocalizationFile(IFile file, CultureInfo language) { _file = file; Language = language; }

        public Task CopyAsync(string newPath, NameCollisionOption collisionOption = NameCollisionOption.ReplaceExisting, CancellationToken cancellationToken = default(CancellationToken)) => _file.CopyAsync(newPath, collisionOption, cancellationToken);
        public Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken)) => _file.DeleteAsync(cancellationToken);
        public Task MoveAsync(string newPath, NameCollisionOption collisionOption = NameCollisionOption.ReplaceExisting, CancellationToken cancellationToken = default(CancellationToken)) => _file.MoveAsync(newPath, collisionOption, cancellationToken);
        public Task<Stream> OpenAsync(PCLExt.FileStorage.FileAccess fileAccess, CancellationToken cancellationToken = default(CancellationToken)) => _file.OpenAsync(fileAccess, cancellationToken);
        public Task RenameAsync(string newName, NameCollisionOption collisionOption = NameCollisionOption.FailIfExists, CancellationToken cancellationToken = default(CancellationToken)) => _file.RenameAsync(newName, collisionOption, cancellationToken);
    }
}

using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Files
{
    /*
    public interface ILocalizationFile : IFile
    {
        CultureInfo Language { get; }
    }
    */
    // TODO: Replace with LocalizationFile later
    public class TokensFile : IFile //: ILocalizationFile
    {
        public const string Prefix = "Tokens_";
        public const string FileExtension = ".dat";

        private readonly IFile _file;
        public string Name => _file.Name;
        public string Path => _file.Path;

        public CultureInfo Language { get; }

        public TokensFile(IFile file)
        {
            _file = file;

            Language = new CultureInfo(Name.Replace(Prefix, "").Replace(FileExtension, ""));
        }
        public TokensFile(IFile file, CultureInfo language) { _file = file; Language = language; }

        public Stream Open(PCLExt.FileStorage.FileAccess fileAccess) => _file.Open(fileAccess);
        public Task<Stream> OpenAsync(PCLExt.FileStorage.FileAccess fileAccess, CancellationToken cancellationToken = default(CancellationToken)) => _file.OpenAsync(fileAccess, cancellationToken);
        public void Delete() => _file.Delete();
        public Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken)) => _file.DeleteAsync(cancellationToken);
        public void Rename(string newName, NameCollisionOption collisionOption = NameCollisionOption.FailIfExists) => _file.Rename(newName, collisionOption);
        public Task RenameAsync(string newName, NameCollisionOption collisionOption = NameCollisionOption.FailIfExists, CancellationToken cancellationToken = default(CancellationToken)) => _file.RenameAsync(newName, collisionOption, cancellationToken);
        public void Move(string newPath, NameCollisionOption collisionOption = NameCollisionOption.ReplaceExisting) => _file.Move(newPath, collisionOption);
        public Task MoveAsync(string newPath, NameCollisionOption collisionOption = NameCollisionOption.ReplaceExisting, CancellationToken cancellationToken = default(CancellationToken)) => _file.MoveAsync(newPath, collisionOption, cancellationToken);
        public void Copy(string newPath, NameCollisionOption collisionOption = NameCollisionOption.ReplaceExisting) => _file.Copy(newPath, collisionOption);
        public Task CopyAsync(string newPath, NameCollisionOption collisionOption = NameCollisionOption.ReplaceExisting, CancellationToken cancellationToken = default(CancellationToken)) => _file.CopyAsync(newPath, collisionOption, cancellationToken);
    }
}

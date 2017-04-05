using System.Globalization;

using P3D.Legacy.Shared.Storage.Files;

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
    public class TokensFile : BaseFile //: ILocalizationFile
    {
        public const string Prefix = "Tokens_";
        public const string FileExtension = ".dat";

        public CultureInfo Language { get; }

        public TokensFile(IFile file) : base(file) { Language = new CultureInfo(Name.Replace(Prefix, "").Replace(FileExtension, "")); }
        public TokensFile(IFile file, CultureInfo language) : base(file) { Language = language; }
    }
}

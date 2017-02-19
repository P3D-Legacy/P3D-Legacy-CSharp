using System.Globalization;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage
{
    public interface ILocalizationFile : IFile
    {
        CultureInfo Language { get; }
    }
}

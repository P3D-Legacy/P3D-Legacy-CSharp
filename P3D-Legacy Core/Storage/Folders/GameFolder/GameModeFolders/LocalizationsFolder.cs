using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using P3D.Legacy.Shared.Data;
using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage.Folders
{
    public class LocalizationsFolder : BaseGameModeChildFolder
    {
        private static bool TryGetCultureInfo(string cultureCode, out CultureInfo culture)
        {
            try
            {
                culture = CultureInfo.GetCultureInfo(cultureCode);
                return true;
            }
            catch (CultureNotFoundException)
            {
                culture = null;
                return false;
            }
        }

        public LocalizationsFolder() : base(new GameFolder().CreateFolder("Localization", CreationCollisionOption.OpenIfExists)) { }
        public LocalizationsFolder(string localPath) : base(FromLocalPath(localPath)) { }

        public bool CheckTranslationExists(LocalizationInfo localizationInfo)
        {
            return GetTranslationFolders().Any(file =>
            {
                if (Equals(file.LocalizationInfo.CultureInfo, localizationInfo.CultureInfo))
                {
                    if (!string.IsNullOrEmpty(localizationInfo.SubLanguage) && !string.IsNullOrEmpty(file.LocalizationInfo.SubLanguage))
                        return Equals(file.LocalizationInfo.SubLanguage, localizationInfo.SubLanguage);
                    return true;
                }
                return Equals(file.LocalizationInfo.CultureInfo, localizationInfo.CultureInfo);
            });
        }
        public async Task<bool> CheckTranslationExistsAsync(LocalizationInfo localizationInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            return (await GetTranslationFoldersAsync(cancellationToken)).Any(file =>
            {
                if (Equals(file.LocalizationInfo.CultureInfo, localizationInfo.CultureInfo))
                {
                    if (!string.IsNullOrEmpty(localizationInfo.SubLanguage) && !string.IsNullOrEmpty(file.LocalizationInfo.SubLanguage))
                        return Equals(file.LocalizationInfo.SubLanguage, localizationInfo.SubLanguage);
                    return true;
                }
                return Equals(file.LocalizationInfo.CultureInfo, localizationInfo.CultureInfo);
            });
        }

        public TranslationFolder GetTranslationFolder(LocalizationInfo localizationInfo) => GetTranslationFolders().FirstOrDefault(folder => localizationInfo.Equals(folder.LocalizationInfo));
        public async Task<TranslationFolder> GetTranslationFolderAsync(LocalizationInfo localizationInfo, CancellationToken cancellationToken = default(CancellationToken)) => (await GetTranslationFoldersAsync(cancellationToken)).FirstOrDefault(folder => localizationInfo.Equals(folder.LocalizationInfo));

        public IList<TranslationFolder> GetTranslationFolders()
        {
            var list = new List<TranslationFolder>();
            foreach (var folder in GetFolders())
            {
                CultureInfo cultureInfo;
                var cultureInfoName = folder.Name.Split('_').Length > 0 ? folder.Name.Split('_')[0] : string.Empty;
                var subLanguage = folder.Name.Split('_').Length > 1 ? folder.Name.Split('_')[1] : string.Empty;
                if (TryGetCultureInfo(cultureInfoName, out cultureInfo))
                    list.Add(new TranslationFolder(folder, new LocalizationInfo(cultureInfo, subLanguage)));
            }
            return list;
        }
        public async Task<IList<TranslationFolder>> GetTranslationFoldersAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var list = new List<TranslationFolder>();
            foreach (var folder in await GetFoldersAsync(cancellationToken))
            {
                CultureInfo cultureInfo;
                var cultureInfoName = folder.Name.Split('_').Length > 0 ? folder.Name.Split('_')[0] : string.Empty;
                var subLanguage = folder.Name.Split('_').Length > 1 ? folder.Name.Split('_')[1] : string.Empty;
                if (TryGetCultureInfo(cultureInfoName, out cultureInfo))
                    list.Add(new TranslationFolder(folder, new LocalizationInfo(cultureInfo, subLanguage)));
            }
            return list;
        }
    }
}

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage
{
    public static class StorageInfo
    {
        private static string ContentFolderName = "Content";
        private static string LocalizationFolderName = "Localization";

        private static string ContentPacksFolderName = "ContentPacks";
        private static string GameModesFolderName = "GameModes";
        private static string MapsFolderName = "Maps";
        private static string SaveFolderName = "Save";
        private static string ScreenshotsFolderName = "Screenshots";
        private static string ScriptsFolderName = "Scripts";

        
        private static string OptionsFileName = "Options.yml";
        private static string KeyboardFileName = "Keyboard.yml";


        public static IFolder MainFolder => FileSystem.Current.BaseStorage;

        public static IFolder ContentFolder => MainFolder.CreateFolderAsync(ContentFolderName, CreationCollisionOption.OpenIfExists).Result;
        public static ILocalizationFolder LocalizationFolder => new LocalizationFolder(ContentFolder.CreateFolderAsync(LocalizationFolderName, CreationCollisionOption.OpenIfExists).Result);

        public static IFolder ContentPacksFolder => MainFolder.CreateFolderAsync(ContentPacksFolderName, CreationCollisionOption.OpenIfExists).Result;
        public static IFolder GameModesFolder => MainFolder.CreateFolderAsync(GameModesFolderName, CreationCollisionOption.OpenIfExists).Result;
        public static IFolder MapsFolder => MainFolder.CreateFolderAsync(MapsFolderName, CreationCollisionOption.OpenIfExists).Result;
        public static IFolder SaveFolder => MainFolder.CreateFolderAsync(SaveFolderName, CreationCollisionOption.OpenIfExists).Result;
        public static IFolder ScreenshotsFolder => MainFolder.CreateFolderAsync(ScreenshotsFolderName, CreationCollisionOption.OpenIfExists).Result;
        public static IFolder ScriptsFolder => MainFolder.CreateFolderAsync(ScriptsFolderName, CreationCollisionOption.OpenIfExists).Result;

        public static IFile OptionsFile => SaveFolder.CreateFileAsync(OptionsFileName, CreationCollisionOption.OpenIfExists).Result;
        public static IFile KeyboardFile => SaveFolder.CreateFileAsync(KeyboardFileName, CreationCollisionOption.OpenIfExists).Result;
    }
}

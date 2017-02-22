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


        public static ContentFolder MainFolder => new ContentFolder(FileSystem.Current.BaseStorage);

        public static ContentFolder ContentFolder => new ContentFolder(MainFolder.CreateFolderAsync(ContentFolderName, CreationCollisionOption.OpenIfExists).Result);
        public static LocalizationFolder LocalizationFolder => new LocalizationFolder(ContentFolder.CreateFolderAsync(LocalizationFolderName, CreationCollisionOption.OpenIfExists).Result);

        public static ContentFolder ContentPacksFolder => new ContentFolder(MainFolder.CreateFolderAsync(ContentPacksFolderName, CreationCollisionOption.OpenIfExists).Result);
        public static ContentFolder GameModesFolder => new ContentFolder(MainFolder.CreateFolderAsync(GameModesFolderName, CreationCollisionOption.OpenIfExists).Result);
        public static ContentFolder MapsFolder => new ContentFolder(MainFolder.CreateFolderAsync(MapsFolderName, CreationCollisionOption.OpenIfExists).Result);
        public static ContentFolder SaveFolder => new ContentFolder(MainFolder.CreateFolderAsync(SaveFolderName, CreationCollisionOption.OpenIfExists).Result);
        public static ContentFolder ScreenshotsFolder => new ContentFolder(MainFolder.CreateFolderAsync(ScreenshotsFolderName, CreationCollisionOption.OpenIfExists).Result);
        public static ContentFolder ScriptsFolder => new ContentFolder(MainFolder.CreateFolderAsync(ScriptsFolderName, CreationCollisionOption.OpenIfExists).Result);

        public static IFile OptionsFile => SaveFolder.CreateFileAsync(OptionsFileName, CreationCollisionOption.OpenIfExists).Result;
        public static IFile KeyboardFile => SaveFolder.CreateFileAsync(KeyboardFileName, CreationCollisionOption.OpenIfExists).Result;
    }
}

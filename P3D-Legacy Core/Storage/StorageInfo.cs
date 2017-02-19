using System;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage
{
    public static class StorageInfo
    {
        public static string GameFolderPath => AppDomain.CurrentDomain.BaseDirectory;


        private static string SaveFolderName = "Save";
        private static string LocalizationFolderName = "Localization";

        private static string OptionsFileName = "Options.yml";
        private static string KeyboardFileName = "Keyboard.yml";


        public static IFolder SaveFolder => FileSystem.Current.BaseStorage.CreateFolderAsync(SaveFolderName, CreationCollisionOption.OpenIfExists).Result;
        public static ILocalizationFolder LocalizationFolder => new LocalizationFolder(FileSystem.Current.BaseStorage.CreateFolderAsync(LocalizationFolderName, CreationCollisionOption.OpenIfExists).Result);

        public static IFile OptionsFile => SaveFolder.CreateFileAsync(OptionsFileName, CreationCollisionOption.OpenIfExists).Result;
        public static IFile KeyboardFile => SaveFolder.CreateFileAsync(KeyboardFileName, CreationCollisionOption.OpenIfExists).Result;
    }
}

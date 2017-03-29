using P3D.Legacy.Core.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage
{
    public static class StorageInfo
    {
        private static string ContentFolderName = "Content";
        private static string LocalizationFolderName = "Localization";
        private static string PokemonFolderName = "Pokemon";
        private static string PokemonDataFolderName = "Data";

        private static string ContentPacksFolderName = "ContentPacks";
        private static string GameModesFolderName = "GameModes";
        private static string MapsFolderName = "maps";
        private static string PokeFolderName = "poke";
        private static string SaveFolderName = "Save";
        private static string ScreenshotsFolderName = "Screenshots";
        private static string ScriptsFolderName = "Scripts";


        public static ContentFolder MainFolder => new ContentFolder(FileSystem.Current.BaseStorage);

        public static ContentFolder ContentFolder => new ContentFolder(MainFolder.CreateFolderAsync(ContentFolderName, CreationCollisionOption.OpenIfExists).Result);
        public static TokensFolder LocalizationFolder => new TokensFolder(ContentFolder.CreateFolderAsync(LocalizationFolderName, CreationCollisionOption.OpenIfExists).Result);

        public static ContentFolder ContentPacksFolder => new ContentFolder(MainFolder.CreateFolderAsync(ContentPacksFolderName, CreationCollisionOption.OpenIfExists).Result);
        public static ContentFolder GameModesFolder => new ContentFolder(MainFolder.CreateFolderAsync(GameModesFolderName, CreationCollisionOption.OpenIfExists).Result);
        public static MapsFolder MapFolder => new MapsFolder(MainFolder.CreateFolderAsync(MapsFolderName, CreationCollisionOption.OpenIfExists).Result);
        public static PokeFolder PokeFolder => new PokeFolder(MapFolder.CreateFolderAsync(PokeFolderName, CreationCollisionOption.OpenIfExists).Result);
        public static SaveFolder SaveFolder => new SaveFolder(MainFolder.CreateFolderAsync(SaveFolderName, CreationCollisionOption.OpenIfExists).Result);
        public static ContentFolder ScreenshotsFolder => new ContentFolder(MainFolder.CreateFolderAsync(ScreenshotsFolderName, CreationCollisionOption.OpenIfExists).Result);
        public static ScriptsFolder ScriptFolder => new ScriptsFolder(MainFolder.CreateFolderAsync(ScriptsFolderName, CreationCollisionOption.OpenIfExists).Result);
        public static PokemonDataFolder PokemonDataFolder => new PokemonDataFolder(ContentFolder.CreateFolderAsync(PokemonFolderName, CreationCollisionOption.OpenIfExists).Result.CreateFolderAsync(PokemonDataFolderName, CreationCollisionOption.OpenIfExists).Result);
    }
}

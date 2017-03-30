using P3D.Legacy.Core.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Storage
{
    public static class StorageInfo
    {
        public static ContentFolder MainFolder => new ContentFolder(FileSystem.Current.BaseStorage);

        public static ContentFolder ContentFolder => new ContentFolder(MainFolder.CreateFolder("Content", CreationCollisionOption.OpenIfExists));
        public static TokensFolder LocalizationFolder => new TokensFolder(ContentFolder.CreateFolder("Localization", CreationCollisionOption.OpenIfExists));
        public static ContentFolder PokemonFolder => new ContentFolder(ContentFolder.CreateFolder("Pokemon", CreationCollisionOption.OpenIfExists));
        public static PokemonDataFolder PokemonDataFolder => new PokemonDataFolder(PokemonFolder.CreateFolder("Data", CreationCollisionOption.OpenIfExists));

        public static ContentFolder ContentPacksFolder => new ContentFolder(MainFolder.CreateFolder("ContentPacks", CreationCollisionOption.OpenIfExists));
        public static ContentFolder GameModesFolder => new ContentFolder(MainFolder.CreateFolder("GameModes", CreationCollisionOption.OpenIfExists));
        public static MapsFolder MapFolder => new MapsFolder(MainFolder.CreateFolder("maps", CreationCollisionOption.OpenIfExists));
        public static PokeFolder PokeFolder => new PokeFolder(MapFolder.CreateFolder("poke", CreationCollisionOption.OpenIfExists));
        public static SaveFolder SaveFolder => new SaveFolder(MainFolder.CreateFolder("Save", CreationCollisionOption.OpenIfExists));
        public static ContentFolder ScreenshotsFolder => new ContentFolder(MainFolder.CreateFolder("Screenshots", CreationCollisionOption.OpenIfExists));
        public static ScriptsFolder ScriptFolder => new ScriptsFolder(MainFolder.CreateFolder("Scripts", CreationCollisionOption.OpenIfExists));
    }
}

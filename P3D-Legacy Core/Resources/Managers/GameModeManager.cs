using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Core.GameModes;
using P3D.Legacy.Core.Storage;

using PCLExt.FileStorage;

using GameRuleObject = P3D.Legacy.Core.GameModes.GameMode.GameRuleList.GameRuleObject;

namespace P3D.Legacy.Core.Resources
{
    public static class GameModeManager
    {
        private static List<GameMode> GameModeList { get; } = new List<GameMode>();
        private static int GameModePointer { get; set; }

        /// <summary>
        /// Returns the amount of loaded GameModes.
        /// </summary>
        public static int GameModeCount => GameModeList.Count;

        public static bool Initialized { get; set; }

        /// <summary>
        /// Returns the currently active GameMode.
        /// </summary>
        public static GameMode ActiveGameMode => GameModeList.Count - 1 >= GameModePointer ? GameModeList[GameModePointer] : null;


        /// <summary>
        /// Loads (or reloads) the list of GameModes. The pointer also gets reset.
        /// </summary>
        public static void LoadGameModes()
        {
            GameModeList.Clear();
            GameModePointer = 0;

            CreateDefaultGameMode();

            foreach (var folder in StorageInfo.GameModesFolder.GetFolders())
                if (folder.CheckExists(GameModeYaml.GameModeFilename) == ExistenceCheckResult.FileExists)
                    LoadAndAddGameMode(folder.Name);

            SetGameModePointer("Pokemon 3D");
            Initialized = true;
        }

        public static GameMode GetGameMode(string gameModeDirectory) => GameModeList.FirstOrDefault(gameMode => gameMode.Name == gameModeDirectory);

        /// <summary>
        /// Sets the GameModePointer to a new item.
        /// </summary>
        /// <param name="gameModeName">The Name resembeling the new GameMode.</param>
        public static void SetGameModePointer(string gameModeName)
        {
            for (var i = 0; i <= GameModeList.Count - 1; i++)
                if (GameModeList[i].Name == gameModeName)
                {
                    GameModePointer = i;
                    Logger.Debug("---Set pointer to \"" + gameModeName + "\"!---");
                    return;
                }

            Logger.Debug("Couldn't find the GameMode \"" + gameModeName + "\"!");
        }

        /// <summary>
        /// Checks if a GameMode exists.
        /// </summary>
        public static bool GameModeExists(string gameModePath) => GameModeList.Any(gameMode => gameMode.Name == gameModePath);

        /// <summary>
        /// Adds a GameMode to the list.
        /// </summary>
        /// <param name="name">The name of the GameMode directory.</param>
        private static void LoadAndAddGameMode(string name)
        {
            var newGameMode = GameMode.Load(name);
            if (newGameMode != null)
                GameModeList.Add(newGameMode);
        }

        /// <summary>
        /// Creates the default GameMode.
        /// </summary>
        public static void CreateDefaultGameMode()
        {
            if (!GameMode.Exists(GameMode.Pokemon3DGameModeName))
                GameMode.Save(GameMode.LoadPokemon3D());
        }

        #region "Shared GameModeFunctions"

        /// <summary>
        /// Returns the GameRules of the currently active GameMode.
        /// </summary>
        public static GameMode.GameRuleList GetActiveGameRules() => ActiveGameMode.GameRules;

        /// <summary>
        /// Returns the Value of a chosen GameRule from the currently active GameMode.
        /// </summary>
        /// <param name="ruleName">The RuleName to search for.</param>
        /// <param name="defaultValue"></param>
        public static GameRuleObject GetActiveGameRuleValueOrDefault(string ruleName, GameRuleObject defaultValue) => GetActiveGameRules().GetValueOrDefault(ruleName, defaultValue);


        /// <summary>
        /// Returns the correct map path to load a map from.
        /// </summary>
        /// <param name="levelFile">The levelfile containing the map.</param>
        public static IFile GetMapFile(string levelFile)
        {
            if (ActiveGameMode.IsDefaultGamemode)
                return StorageInfo.MapFolder.GetFile(levelFile);

            if (ActiveGameMode.MapFolder.CheckExists(levelFile) == ExistenceCheckResult.FileExists)
                return ActiveGameMode.MapFolder.GetFile(levelFile);

            // TODO: Log

            return StorageInfo.MapFolder.GetFile(levelFile);
        }

        /// <summary>
        /// Returns the correct script file path to load a script from.
        /// </summary>
        /// <param name="scriptFile">The file that contains the script information.</param>
        public static IFile GetScriptFile(string scriptFile)
        {
            if (ActiveGameMode.IsDefaultGamemode)
                return StorageInfo.ScriptFolder.GetFile(scriptFile);

            if (ActiveGameMode.ScriptFolder.CheckExists(scriptFile) == ExistenceCheckResult.FileExists)
                return ActiveGameMode.ScriptFolder.GetFile(scriptFile);

            // TODO: Log

            return StorageInfo.ScriptFolder.GetFile(scriptFile);
        }

        /// <summary>
        /// Returns the correct poke file path to load a Wild Pokémon Definition from.
        /// </summary>
        /// <param name="pokeFile">The file that contains the Wild Pokémon Definitions.</param>
        public static IFile GetPokeFile(string pokeFile)
        {
            if (ActiveGameMode.IsDefaultGamemode)
                return StorageInfo.PokeFolder.GetFile(pokeFile);

            if (ActiveGameMode.PokeFolder.CheckExists(pokeFile) == ExistenceCheckResult.FileExists)
                return ActiveGameMode.PokeFolder.GetFile(pokeFile);

            // TODO: Log

            return StorageInfo.PokeFolder.GetFile(pokeFile);
        }

        /// <summary>
        /// Returns the correct Pokémon data file path to load a Pokémon from.
        /// </summary>
        /// <param name="pokemonDataFile">The file which contains the Pokémon information.</param>
        public static IFile GetPokemonDataFile(string pokemonDataFile)
        {
            if (ActiveGameMode.IsDefaultGamemode)
                return StorageInfo.PokemonDataFolder.GetFile(pokemonDataFile);

            if (ActiveGameMode.PokemonDataFolder.CheckExists(pokemonDataFile) == ExistenceCheckResult.FileExists)
                return ActiveGameMode.PokemonDataFolder.GetFile(pokemonDataFile);

            // TODO: Log

            return StorageInfo.PokemonDataFolder.GetFile(pokemonDataFile);
        }

        /// <summary>
        /// Returns the correct Content file path to load content from.
        /// </summary>
        /// <param name="contentFile">The stub file path to the Content file.</param>
        public static IFile GetContentFile(string contentFile)
        {
            if (ActiveGameMode.IsDefaultGamemode)
                return StorageInfo.ContentFolder.GetFile(contentFile);

            if (ActiveGameMode.ContentFolder.CheckExists(contentFile) == ExistenceCheckResult.FileExists)
                return ActiveGameMode.ContentFolder.GetFile(contentFile);

            // TODO: Log

            return StorageInfo.ContentFolder.GetFile(contentFile);
        }


        public static bool PokeFileExists(string levelFile)
        {
            var path = ActiveGameMode.PokeFolder.CheckExists(levelFile) == ExistenceCheckResult.FileExists;
            var defaultPath = StorageInfo.PokeFolder.CheckExists(levelFile) == ExistenceCheckResult.FileExists;
            return ActiveGameMode.IsDefaultGamemode ? defaultPath : path || defaultPath;
        }

        /// <summary>
        /// Checks if a map file exists either in the active GameMode or the default GameMode.
        /// </summary>
        /// <param name="levelFile">The map file to look for.</param>
        public static bool MapFileExists(string levelFile)
        {
            var path = ActiveGameMode.MapFolder.CheckExists(levelFile) == ExistenceCheckResult.FileExists;
            var defaultPath = StorageInfo.MapFolder.CheckExists(levelFile) == ExistenceCheckResult.FileExists;
            return ActiveGameMode.IsDefaultGamemode ? defaultPath : path || defaultPath;
        }

        /// <summary>
        /// Checks if a Content file exists either in the active GameMode or the default GameMode.
        /// </summary>
        /// <param name="contentFile">The Content file to look for.</param>
        public static bool ContentFileExists(string contentFile)
        {
            var path = ActiveGameMode.ContentFolder.CheckExists(contentFile) == ExistenceCheckResult.FileExists;
            var defaultPath = StorageInfo.ContentFolder.CheckExists(contentFile) == ExistenceCheckResult.FileExists;
            return ActiveGameMode.IsDefaultGamemode ? defaultPath : path || defaultPath;

            //return ActiveGameMode.ContentFolder.CheckExists(contentFile) == ExistenceCheckResult.FileExists || StorageInfo.ContentFolder.CheckExists(contentFile) == ExistenceCheckResult.FileExists;
        }

        #endregion

    }
}

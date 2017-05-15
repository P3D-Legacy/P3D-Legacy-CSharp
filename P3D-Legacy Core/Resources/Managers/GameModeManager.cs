using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Core.GameModes;
using P3D.Legacy.Core.Storage;
using P3D.Legacy.Core.Storage.Folders;
using PCLExt.FileStorage;

using GameRuleObject = P3D.Legacy.Core.GameModes.GameMode.GameRuleList.GameRuleObject;

namespace P3D.Legacy.Core.Resources.Managers
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

            foreach (var folder in new GameModesFolder().GetFolders())
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


        private static IFile GetFile(string file, IFolder folder)
        {
            file = file.Replace("\\", "|").Replace("/", "|");
            string[] arr = file.Split('|');
            string filename = arr.Last();
            string[] folders = arr.Length > 1 ? arr.Take(arr.Length - 1).ToArray() : null;

            if (folders != null)
                foreach (var folderName in folders)
                    folder = folder.GetFolder(folderName);

            return folder.GetFile(filename);
        }

        private static bool FileExists(string file, IFolder folder)
        {
            file = file.Replace("\\", "|").Replace("/", "|");
            string[] arr = file.Split('|');
            string filename = arr.Last();
            string[] folders = arr.Length > 1 ? arr.Take(arr.Length - 1).ToArray() : null;

            if (folders != null)
                foreach (var folderName in folders)
                    try
                    { 
                        folder = folder.GetFolder(folderName);
                    }
                    catch
                    {
                        return false;
                    }

            return folder.CheckExists(filename) == ExistenceCheckResult.FileExists;
        }

        /// <summary>
        /// Returns the correct map path to load a map from.
        /// </summary>
        /// <param name="levelFile">The levelfile containing the map.</param>
        public static IFile GetMapFile(string levelFile)
        {
            if (ActiveGameMode.IsDefaultGamemode)
                return GetFile(levelFile, new MapsFolder());

            if (ActiveGameMode.MapFolder.CheckExists(levelFile) == ExistenceCheckResult.FileExists)
                return GetFile(levelFile, ActiveGameMode.MapFolder);

            // TODO: Log

            return GetFile(levelFile, new MapsFolder());
        }

        /// <summary>
        /// Returns the correct script file path to load a script from.
        /// </summary>
        /// <param name="scriptFile">The file that contains the script information.</param>
        public static IFile GetScriptFile(string scriptFile)
        {
            if (ActiveGameMode.IsDefaultGamemode)
                return GetFile(scriptFile, new ScriptsFolder());

            if (ActiveGameMode.ScriptFolder.CheckExists(scriptFile) == ExistenceCheckResult.FileExists)
                return GetFile(scriptFile, ActiveGameMode.ScriptFolder);

            // TODO: Log

            return GetFile(scriptFile, new ScriptsFolder());
        }

        /// <summary>
        /// Returns the correct poke file path to load a Wild Pokémon Definition from.
        /// </summary>
        /// <param name="pokeFile">The file that contains the Wild Pokémon Definitions.</param>
        public static IFile GetPokeFile(string pokeFile)
        {
            if (ActiveGameMode.IsDefaultGamemode)
                return GetFile(pokeFile, new PokeFolder());

            if (ActiveGameMode.PokeFolder.CheckExists(pokeFile) == ExistenceCheckResult.FileExists)
                return GetFile(pokeFile, ActiveGameMode.PokeFolder);

            // TODO: Log

            return GetFile(pokeFile, new PokeFolder());
        }

        /// <summary>
        /// Returns the correct Pokémon data file path to load a Pokémon from.
        /// </summary>
        /// <param name="pokemonDataFile">The file which contains the Pokémon information.</param>
        public static IFile GetPokemonDataFile(string pokemonDataFile)
        {
            if (ActiveGameMode.IsDefaultGamemode)
                return GetFile(pokemonDataFile, new PokemonDataFolder());

            if (ActiveGameMode.PokemonDataFolder.CheckExists(pokemonDataFile) == ExistenceCheckResult.FileExists)
                return GetFile(pokemonDataFile, ActiveGameMode.PokemonDataFolder);

            // TODO: Log

            return GetFile(pokemonDataFile, new PokemonDataFolder());
        }

        /// <summary>
        /// Returns the correct Content file path to load content from.
        /// </summary>
        /// <param name="contentFile">The stub file path to the Content file.</param>
        public static IFile GetContentFile(string contentFile)
        {
            if (ActiveGameMode.IsDefaultGamemode)
                return GetFile(contentFile, new ContentFolder());

            if (ActiveGameMode.ContentFolder.CheckExists(contentFile) == ExistenceCheckResult.FileExists)
                return GetFile(contentFile, ActiveGameMode.ContentFolder);

            // TODO: Log

            return GetFile(contentFile, new ContentFolder());
        }


        public static bool PokeFileExists(string levelFile)
        {
            var path = FileExists(levelFile, ActiveGameMode.PokeFolder);
            var defaultPath = FileExists(levelFile, new PokeFolder());
            return ActiveGameMode.IsDefaultGamemode ? defaultPath : path || defaultPath;
        }

        /// <summary>
        /// Checks if a map file exists either in the active GameMode or the default GameMode.
        /// </summary>
        /// <param name="levelFile">The map file to look for.</param>
        public static bool MapFileExists(string levelFile)
        {
            var path = FileExists(levelFile, ActiveGameMode.MapFolder);
            var defaultPath = FileExists(levelFile, new MapsFolder());
            return ActiveGameMode.IsDefaultGamemode ? defaultPath : path || defaultPath;
        }

        /// <summary>
        /// Checks if a Content file exists either in the active GameMode or the default GameMode.
        /// </summary>
        /// <param name="contentFile">The Content file to look for.</param>
        public static bool ContentFileExists(string contentFile)
        {
            var path = FileExists(contentFile, ActiveGameMode.ContentFolder);
            var defaultPath = FileExists(contentFile, new ContentFolder());
            return ActiveGameMode.IsDefaultGamemode ? defaultPath : path || defaultPath;

            //return ActiveGameMode.ContentFolder.CheckExists(contentFile) == ExistenceCheckResult.FileExists || StorageInfo.ContentFolder.CheckExists(contentFile) == ExistenceCheckResult.FileExists;
        }

        #endregion

    }
}

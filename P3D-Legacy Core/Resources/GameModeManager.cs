using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using P3D.Legacy.Core.GameModes;
using P3D.Legacy.Core.Storage;

using PCLExt.FileStorage;

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
        /// Loads (or reloads) the list of GameModes. The pointer also gets reset.
        /// </summary>
        public static async Task LoadGameModes()
        {
            GameModeList.Clear();
            GameModePointer = 0;

            CreateDefaultGameMode();

            foreach (var folder in await StorageInfo.GameModesFolder.GetFoldersAsync())
                if (await folder.CheckExistsAsync(GameModeYaml.GameModeFilename) == ExistenceCheckResult.FileExists)
                    await AddGameMode(folder.Name);

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
        /// <param name="path">The path of the GameMode directory.</param>
        private static async Task AddGameMode(string path)
        {
            var newGameMode = GameMode.ParseYaml(await GameModeYaml.LoadGameModeYaml(path));//new GameMode(Path.Combine(path, "GameMode.dat"));
            if (newGameMode != null)
                GameModeList.Add(newGameMode);
        }

        /// <summary>
        /// Creates the default GameMode.
        /// </summary>
        public static async void CreateDefaultGameMode()
        {
            var defaultGameModeYaml = GameModeYaml.Default;

            IFolder gameModeFolder;
            bool doCreateKolbenMode = false;

            if (await StorageInfo.GameModesFolder.CheckExistsAsync(defaultGameModeYaml.Name) == ExistenceCheckResult.NotFound)
            {
                doCreateKolbenMode = true;
            }
            gameModeFolder = await StorageInfo.GameModesFolder.CreateFolderAsync(defaultGameModeYaml.Name, CreationCollisionOption.OpenIfExists);

            if (await gameModeFolder.CheckExistsAsync(GameModeYaml.GameModeFilename) == ExistenceCheckResult.NotFound)
            {
                doCreateKolbenMode = true;
            }

            if (doCreateKolbenMode)
            {
                GameMode kolbenMode = GameMode.ParseYaml(GameModeYaml.Default);
                GameModeYaml.SaveGameModeYaml(GameMode.CreateYaml(kolbenMode));
            }
        }

        #region "Shared GameModeFunctions"

        /// <summary>
        /// Returns the currently active GameMode.
        /// </summary>
        public static GameMode ActiveGameMode => GameModeList.Count - 1 >= GameModePointer ? GameModeList[GameModePointer] : null;

        /// <summary>
        /// Returns the GameRules of the currently active GameMode.
        /// </summary>
        public static List<GameMode.GameRule> GetGameRules() => ActiveGameMode.GameRules;

        /// <summary>
        /// Returns the Value of a chosen GameRule from the currently active GameMode.
        /// </summary>
        /// <param name="ruleName">The RuleName to search for.</param>
        public static string GetGameRuleValue(string ruleName, string defaultValue)
        {
            start:
            var rules = GetGameRules();
            foreach (var rule in rules)
                if (rule.RuleName.ToLower() == ruleName.ToLower())
                    return rule.RuleValue;

            ActiveGameMode.GameRules.Add(new GameMode.GameRule(ruleName, defaultValue));
            goto start;
        }

        /// <summary>
        /// Returns the correct map path to load a map from.
        /// </summary>
        /// <param name="levelfile">The levelfile containing the map.</param>
        public static async Task<IFile> GetMapFile(string levelFile)
        {
            if (ActiveGameMode.IsDefaultGamemode)
                return await StorageInfo.MapsFolder.GetFileAsync(levelFile);

            if (await ActiveGameMode.MapsFolder.CheckExistsAsync(levelFile) == ExistenceCheckResult.FileExists)
                return await ActiveGameMode.MapsFolder.GetFileAsync(levelFile);

            return await StorageInfo.MapsFolder.GetFileAsync(levelFile);
        }

        /// <summary>
        /// Returns the correct script file path to load a script from.
        /// </summary>
        /// <param name="scriptFile">The file that contains the script information.</param>
        public static string GetScriptPath(string scriptFile)
        {
            var p0 = Path.Combine(GameController.GamePath, GameMode.DefaultScriptPath, scriptFile);
            if (ActiveGameMode.IsDefaultGamemode)
                return p0;

            var p1 = Path.Combine(GameController.GamePath, ActiveGameMode.ScriptPath, scriptFile);
            if (File.Exists(p1))
                return p1;

            var p2 = Path.Combine(GameController.GamePath, ActiveGameMode.ScriptPath, scriptFile);
            if (p0 != p2)
                Logger.Log(Logger.LogTypes.Message, "Script file: \"" + ActiveGameMode.ScriptPath + scriptFile + "\" does not exist in the GameMode. The game tries to load the normal file at \"\\Scripts\\" + scriptFile + "\".");

            return p0;
        }

        /// <summary>
        /// Returns the correct poke file path to load a Wild Pokémon Definition from.
        /// </summary>
        /// <param name="pokeFile">The file that contains the Wild Pokémon Definitions.</param>
        public static string GetPokeFilePath(string pokeFile)
        {
            var p0 = Path.Combine(GameController.GamePath, GameMode.DefaultPokeFilePath, pokeFile);
            if (ActiveGameMode.IsDefaultGamemode)
                return p0;

            var p1 = Path.Combine(GameController.GamePath, ActiveGameMode.PokeFilePath, pokeFile);
            if (File.Exists(p1))
                return p1;

            if (p0 != p1)
                Logger.Log(Logger.LogTypes.Message, "Poke file: \"" + ActiveGameMode.PokeFilePath + pokeFile + "\" does not exist in the GameMode. The game tries to load the normal file at \"\\maps\\poke\\" + pokeFile + "\".");

            return p0;
        }

        /// <summary>
        /// Returns the correct Pokémon data file path to load a Pokémon from.
        /// </summary>
        /// <param name="pokemonDataFile">The file which contains the Pokémon information.</param>
        public static string GetPokemonDataFilePath(string pokemonDataFile)
        {
            var p0 = Path.Combine(GameController.GamePath, GameMode.DefaultPokemonDataPath, pokemonDataFile);
            if (ActiveGameMode.IsDefaultGamemode)
                return p0;

            var p1 = Path.Combine(GameController.GamePath + ActiveGameMode.PokemonDataPath + pokemonDataFile);
            if (File.Exists(p1))
                return p1;

            if (p0 != p1)
            {
                //Logger.Log(Logger.LogTypes.Message, "Pokemon data file: \"" + Path.Combine(ActiveGameMode.PokemonDataPath + pokemonDataFile) + "\" does not exist in the GameMode. The game tries to load the normal file at ""\Content\Pokemon\Data\" && PokemonDataFile && """.")
            }

            return p0;
        }

        /// <summary>
        /// Returns the correct Content file path to load content from.
        /// </summary>
        /// <param name="contentFile">The stub file path to the Content file.</param>
        public static async Task<IFile> GetContentFile(string contentFile)
        {
            if (ActiveGameMode.IsDefaultGamemode)
                return await StorageInfo.ContentFolder.GetFileAsync(contentFile);

            if (await ActiveGameMode.ContentFolder.CheckExistsAsync(contentFile) == ExistenceCheckResult.FileExists)
                return await ActiveGameMode.ContentFolder.GetFileAsync(contentFile);

            return await StorageInfo.ContentFolder.GetFileAsync(contentFile);
        }

        /// <summary>
        /// Checks if a map file exists either in the active GameMode or the default GameMode.
        /// </summary>
        /// <param name="levelFile">The map file to look for.</param>
        public static async Task<bool> MapFileExists(string levelFile)
        {
            var path = await ActiveGameMode.MapsFolder.CheckExistsAsync(levelFile) == ExistenceCheckResult.FileExists;
            var defaultPath = await StorageInfo.MapsFolder.CheckExistsAsync(levelFile) == ExistenceCheckResult.FileExists;
            return ActiveGameMode.IsDefaultGamemode ? defaultPath : path || defaultPath;
        }

        /// <summary>
        /// Checks if a Content file exists either in the active GameMode or the default GameMode.
        /// </summary>
        /// <param name="contentFile">The Content file to look for.</param>
        public static bool ContentFileExists(string contentFile) => ActiveGameMode.ContentFolder.CheckExistsAsync(contentFile).Result == ExistenceCheckResult.FileExists || StorageInfo.ContentFolder.CheckExistsAsync(contentFile).Result == ExistenceCheckResult.FileExists;

        #endregion

    }

    public static class Constants
    {
        public static string vbNewLine = "\r\n";
        public static string vbLf = "\n";
    }
}

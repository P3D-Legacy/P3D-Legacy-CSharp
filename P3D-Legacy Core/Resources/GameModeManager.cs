using System.Collections.Generic;
using System.IO;
using System.Linq;

using P3D.Legacy.Core.Data;

namespace P3D.Legacy.Core.Resources
{
    public static class GameModeManager
    {
        private static List<GameMode> GameModeList { get; } = new List<GameMode>();
        private static int _gameModePointer;

        public static bool Initialized;

        /// <summary>
        /// Loads (or reloads) the list of GameModes. The pointer also gets reset.
        /// </summary>
        public static void LoadGameModes()
        {
            GameModeList.Clear();
            _gameModePointer = 0;

            CreateKolbenMode();

            var p0 = Path.Combine(GameController.GamePath, "GameModes");
            foreach (var gameModeFolder in Directory.GetDirectories(p0))
            {
                var p1 = Path.Combine(gameModeFolder, "GameMode.dat");
                if (File.Exists(p1))
                    AddGameMode(gameModeFolder);
            }

            SetGameModePointer("Kolben");
            Initialized = true;
        }

        public static GameMode GetGameMode(string gameModeDirectory) => GameModeList.FirstOrDefault(gameMode => gameMode.DirectoryName == gameModeDirectory);

        /// <summary>
        /// Creates the GameModes directory.
        /// </summary>
        public static void CreateGameModesFolder()
        {
            var p0 = Path.Combine(GameController.GamePath, "GameModes");
            if (!Directory.Exists(p0))
                Directory.CreateDirectory(p0);
        }

        /// <summary>
        /// Sets the GameModePointer to a new item.
        /// </summary>
        /// <param name="gameModeDirectoryName">The directory resembeling the new GameMode.</param>
        public static void SetGameModePointer(string gameModeDirectoryName)
        {
            for (var i = 0; i <= GameModeList.Count - 1; i++)
            {
                GameMode gameMode = GameModeList[i];
                if (gameMode.DirectoryName == gameModeDirectoryName)
                {
                    _gameModePointer = i;
                    Logger.Debug("---Set pointer to \"" + gameModeDirectoryName + "\"!---");
                    return;
                }
            }
            Logger.Debug("Couldn't find the GameMode \"" + gameModeDirectoryName + "\"!");
        }

        /// <summary>
        /// Returns the amount of loaded GameModes.
        /// </summary>
        public static int GameModeCount => GameModeList.Count;

        /// <summary>
        /// Checks if a GameMode exists.
        /// </summary>
        public static bool GameModeExists(string gameModePath) => GameModeList.Any(gameMode => gameMode.DirectoryName == gameModePath);

        /// <summary>
        /// Adds a GameMode to the list.
        /// </summary>
        /// <param name="path">The path of the GameMode directory.</param>
        private static void AddGameMode(string path)
        {
            GameMode newGameMode = new GameMode(Path.Combine(path, "GameMode.dat"));
            if (newGameMode.IsValid)
                GameModeList.Add(newGameMode);
        }

        /// <summary>
        /// Creates the default Kolben GameMode.
        /// </summary>
        public static void CreateKolbenMode()
        {
            var p0 = Path.Combine(GameController.GamePath, "GameModes", "Kolben");

            bool doCreateKolbenMode = false;
            if (Directory.Exists(p0))
                Directory.Delete(p0, true);

            if (!Directory.Exists(p0))
            {
                doCreateKolbenMode = true;
                Directory.CreateDirectory(p0);
            }

            var p1 = Path.Combine(p0, "GameMode.dat");
            if (!doCreateKolbenMode && !File.Exists(p1))
                doCreateKolbenMode = true;

            if (doCreateKolbenMode)
            {
                GameMode kolbenMode = GameMode.GetKolbenGameMode();
                kolbenMode.SaveToFile(p1);
            }
        }

        #region "Shared GameModeFunctions"

        /// <summary>
        /// Returns the currently active GameMode.
        /// </summary>
        public static GameMode ActiveGameMode => GameModeList.Count - 1 >= _gameModePointer ? GameModeList[_gameModePointer] : null;

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
        public static string GetMapPath(string levelFile)
        {
            var p0 = Path.Combine(GameController.GamePath, GameMode.DefaultMapPath, levelFile);
            if (ActiveGameMode.IsDefaultGamemode)
                return p0;

            var p1 = Path.Combine(GameController.GamePath, ActiveGameMode.MapPath, levelFile);
            if (File.Exists(p1))
                return p1;

            var p2 = Path.Combine(GameController.GamePath, GameMode.DefaultMapPath, levelFile);
            if (p2 != p1)
                Logger.Log(Logger.LogTypes.Message, "Map file: \"" + ActiveGameMode.MapPath + levelFile + "\" does not exist in the GameMode. The game tries to load the normal file at \"\\maps\\" + levelFile + "\".");

            return p2;
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

        public static string GetLocalizationsPath(string tokensFile)
        {
            var p1 = Path.Combine(GameController.GamePath, ActiveGameMode.LocalizationsPath, tokensFile);
            if (File.Exists(p1))
                return p1;

            return Path.Combine(GameController.GamePath, GameMode.DefaultLocalizationsPath, tokensFile);
        }

        /// <summary>
        /// Returns the correct Content file path to load content from.
        /// </summary>
        /// <param name="contentFile">The stub file path to the Content file.</param>
        public static string GetContentFilePath(string contentFile)
        {
            var p1 = Path.Combine(GameController.GamePath, GameMode.DefaultContentPath, contentFile);
            if (ActiveGameMode.IsDefaultGamemode)
                return p1;

            var p2 = Path.Combine(GameController.GamePath, ActiveGameMode.ContentPath, contentFile);
            if (File.Exists(p2))
                return p2;

            return p1;
        }

        /// <summary>
        /// Checks if a map file exists either in the active GameMode or the default GameMode.
        /// </summary>
        /// <param name="levelFile">The map file to look for.</param>
        public static bool MapFileExists(string levelFile)
        {
            var path = Path.Combine(GameController.GamePath, ActiveGameMode.MapPath, levelFile);
            var defaultPath = Path.Combine(GameController.GamePath, GameMode.DefaultMapPath, levelFile);
            if (ActiveGameMode.IsDefaultGamemode)
                path = defaultPath;

            return File.Exists(path) || File.Exists(defaultPath);
        }

        /// <summary>
        /// Checks if a Content file exists either in the active GameMode or the default GameMode.
        /// </summary>
        /// <param name="contentFile">The Content file to look for.</param>
        public static bool ContentFileExists(string contentFile)
        {
            var path = Path.Combine(GameController.GamePath, ActiveGameMode.ContentPath, contentFile);
            var defaultPath = Path.Combine(GameController.GamePath, GameMode.DefaultContentPath, contentFile);
            if (ActiveGameMode.IsDefaultGamemode)
                path = defaultPath;

            return File.Exists(path) || File.Exists(defaultPath);
        }

        #endregion

    }

    public static class Constants
    {
        public static string vbNewLine = "\r\n";
        public static string vbLf = "\n";
    }
}

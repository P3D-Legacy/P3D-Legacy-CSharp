using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public static async Task LoadGameModesAsync()
        {
            GameModeList.Clear();
            GameModePointer = 0;

            await CreateDefaultGameModeAsync();

            foreach (var folder in await StorageInfo.GameModesFolder.GetFoldersAsync())
                if (await folder.CheckExistsAsync(GameModeYaml.GameModeFilename) == ExistenceCheckResult.FileExists)
                    await LoadAndAddGameModeAsync(folder.Name);

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
        private static async Task LoadAndAddGameModeAsync(string name)
        {
            var newGameMode = await GameMode.Load(name);
            if (newGameMode != null)
                GameModeList.Add(newGameMode);
        }

        /// <summary>
        /// Creates the default GameMode.
        /// </summary>
        public static async Task CreateDefaultGameModeAsync()
        {
            if (!await GameMode.Exists(GameMode.Pokemon3DGameModeName))
                await GameMode.Save(GameMode.LoadPokemon3D());
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
        public static async Task<IFile> GetMapFileAsync(string levelFile)
        {
            if (ActiveGameMode.IsDefaultGamemode)
                return await StorageInfo.MapFolder.GetFileAsync(levelFile);

            if (await ActiveGameMode.MapFolder.CheckExistsAsync(levelFile) == ExistenceCheckResult.FileExists)
                return await ActiveGameMode.MapFolder.GetFileAsync(levelFile);

            // TODO: Log

            return await StorageInfo.MapFolder.GetFileAsync(levelFile);
        }

        /// <summary>
        /// Returns the correct script file path to load a script from.
        /// </summary>
        /// <param name="scriptFile">The file that contains the script information.</param>
        public static async Task<IFile> GetScriptFileAsync(string scriptFile)
        {
            if (ActiveGameMode.IsDefaultGamemode)
                return await StorageInfo.ScriptFolder.GetFileAsync(scriptFile);

            if (await ActiveGameMode.ScriptFolder.CheckExistsAsync(scriptFile) == ExistenceCheckResult.FileExists)
                return await ActiveGameMode.ScriptFolder.GetFileAsync(scriptFile);

            // TODO: Log

            return await StorageInfo.ScriptFolder.GetFileAsync(scriptFile);
        }

        /// <summary>
        /// Returns the correct poke file path to load a Wild Pokémon Definition from.
        /// </summary>
        /// <param name="pokeFile">The file that contains the Wild Pokémon Definitions.</param>
        public static async Task<IFile> GetPokeFileAsync(string pokeFile)
        {
            if (ActiveGameMode.IsDefaultGamemode)
                return await StorageInfo.PokeFolder.GetFileAsync(pokeFile);

            if (await ActiveGameMode.PokeFolder.CheckExistsAsync(pokeFile) == ExistenceCheckResult.FileExists)
                return await ActiveGameMode.PokeFolder.GetFileAsync(pokeFile);

            // TODO: Log

            return await StorageInfo.PokeFolder.GetFileAsync(pokeFile);
        }

        /// <summary>
        /// Returns the correct Pokémon data file path to load a Pokémon from.
        /// </summary>
        /// <param name="pokemonDataFile">The file which contains the Pokémon information.</param>
        public static async Task<IFile> GetPokemonDataFileAsync(string pokemonDataFile)
        {
            if (ActiveGameMode.IsDefaultGamemode)
                return await StorageInfo.PokemonDataFolder.GetFileAsync(pokemonDataFile);

            if (await ActiveGameMode.PokemonDataFolder.CheckExistsAsync(pokemonDataFile) == ExistenceCheckResult.FileExists)
                return await ActiveGameMode.PokemonDataFolder.GetFileAsync(pokemonDataFile);

            // TODO: Log

            return await StorageInfo.PokemonDataFolder.GetFileAsync(pokemonDataFile);
        }

        /// <summary>
        /// Returns the correct Content file path to load content from.
        /// </summary>
        /// <param name="contentFile">The stub file path to the Content file.</param>
        public static async Task<IFile> GetContentFileAsync(string contentFile)
        {
            if (ActiveGameMode.IsDefaultGamemode)
                return await StorageInfo.ContentFolder.GetFileAsync(contentFile);

            if (await ActiveGameMode.ContentFolder.CheckExistsAsync(contentFile) == ExistenceCheckResult.FileExists)
                return await ActiveGameMode.ContentFolder.GetFileAsync(contentFile);

            // TODO: Log

            return await StorageInfo.ContentFolder.GetFileAsync(contentFile);
        }


        public static async Task<bool> PokeFileExistsAsync(string levelFile)
        {
            var path = await ActiveGameMode.PokeFolder.CheckExistsAsync(levelFile) == ExistenceCheckResult.FileExists;
            var defaultPath = await StorageInfo.PokeFolder.CheckExistsAsync(levelFile) == ExistenceCheckResult.FileExists;
            return ActiveGameMode.IsDefaultGamemode ? defaultPath : path || defaultPath;
        }

        /// <summary>
        /// Checks if a map file exists either in the active GameMode or the default GameMode.
        /// </summary>
        /// <param name="levelFile">The map file to look for.</param>
        public static async Task<bool> MapFileExistsAsync(string levelFile)
        {
            var path = await ActiveGameMode.MapFolder.CheckExistsAsync(levelFile) == ExistenceCheckResult.FileExists;
            var defaultPath = await StorageInfo.MapFolder.CheckExistsAsync(levelFile) == ExistenceCheckResult.FileExists;
            return ActiveGameMode.IsDefaultGamemode ? defaultPath : path || defaultPath;
        }

        /// <summary>
        /// Checks if a Content file exists either in the active GameMode or the default GameMode.
        /// </summary>
        /// <param name="contentFile">The Content file to look for.</param>
        public static async Task<bool> ContentFileExistsAsync(string contentFile)
            => await ActiveGameMode.ContentFolder.CheckExistsAsync(contentFile) == ExistenceCheckResult.FileExists || await StorageInfo.ContentFolder.CheckExistsAsync(contentFile) == ExistenceCheckResult.FileExists;

        #endregion

    }
}

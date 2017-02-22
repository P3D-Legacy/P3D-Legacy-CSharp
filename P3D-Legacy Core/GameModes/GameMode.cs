using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

using Microsoft.Xna.Framework;

using P3D.Legacy.Core.Extensions;
using P3D.Legacy.Core.Storage;

using PCLExt.FileStorage;

// IO:
namespace P3D.Legacy.Core.GameModes
{
    public partial class GameMode
    {
        public static GameMode ParseYaml(GameModeYaml gameModeYaml)
        {
            return new GameMode(
                gameModeYaml.Name,
                gameModeYaml.Description,
                gameModeYaml.Version,
                gameModeYaml.Author,

                gameModeYaml.MapPath,
                "",//gameModeYaml.ScriptPath,
                "",//gameModeYaml.PokeFilePath,
                "",//gameModeYaml.PokemonDataPath,
                gameModeYaml.ContentFolder,
                gameModeYaml.LocalizationsFolder,

                gameModeYaml.GameRules,

                gameModeYaml.StartMap,
                gameModeYaml.StartPosition,
                gameModeYaml.StartRotation,
                gameModeYaml.StartLocationName,
                gameModeYaml.StartDialogue,
                gameModeYaml.StartColor,

                gameModeYaml.PokemonRange,

                gameModeYaml.IntroMusic,

                gameModeYaml.SkinColors,
                gameModeYaml.SkinFiles,
                gameModeYaml.SkinNames);

            /*
            Name = gameModeYaml.Name;
            Description = gameModeYaml.Description;
            Version = gameModeYaml.Version;
            Author = gameModeYaml.Author;

            MapPath = gameModeYaml.MapPath;
            ScriptPath = gameModeYaml.ScriptPath;
            PokeFilePath = gameModeYaml.PokeFilePath;
            PokemonDataPath = gameModeYaml.PokemonDataPath;
            ContentFolder = gameModeYaml.ContentFolder;
            LocalizationsFolder = gameModeYaml.LocalizationsFolder;

            GameRules = gameModeYaml.GameRules;

            StartMap = gameModeYaml.StartMap;
            StartPosition = gameModeYaml.StartPosition;
            StartRotation = gameModeYaml.StartRotation;
            StartLocationName = gameModeYaml.StartLocationName;
            StartDialogue = gameModeYaml.StartDialogue;
            StartColor = gameModeYaml.StartColor;

            PokemonAppear = gameModeYaml.PokemonAppear;

            IntroMusic = gameModeYaml.IntroMusic;

            SkinColors = gameModeYaml.SkinColors;
            SkinFiles = gameModeYaml.SkinFiles;
            SkinNames = gameModeYaml.SkinNames;
            */
        }

        public static GameModeYaml CreateYaml(GameMode gameMode)
        {
            return new GameModeYaml()
            {
                Name = gameMode.Name,
                Description = gameMode.Description,
                Version = gameMode.Version,
                Author = gameMode.Author,

                MapPath = StorageInfo.ContentFolder,//gameMode.MapPath,
                ScriptPath = StorageInfo.ContentFolder,//gameMode.ScriptPath,
                PokeFilePath = StorageInfo.ContentFolder,//gameMode.PokeFilePath,
                PokemonDataPath = StorageInfo.ContentFolder,//gameMode.PokemonDataPath,
                ContentFolder = gameMode.ContentFolder,
                LocalizationsFolder = gameMode.LocalizationsFolder,

                GameRules = gameMode.GameRules,

                StartMap = gameMode.StartMap,
                StartPosition = gameMode.StartPosition,
                StartRotation = gameMode.StartRotation,
                StartLocationName = gameMode.StartLocationName,
                StartDialogue = gameMode.StartDialogue,
                StartColor = gameMode.StartColor,

                PokemonRange = gameMode.PokemonRange,

                IntroMusic = gameMode.IntroMusic,

                SkinColors = gameMode.SkinColors,
                SkinFiles = gameMode.SkinFiles,
                SkinNames = gameMode.SkinNames,
            };
        }


        public static readonly string DefaultContentPath = System.IO.Path.Combine("Content");
        public static readonly string DefaultMapPath = System.IO.Path.Combine("maps");
        public static readonly string DefaultScriptPath = System.IO.Path.Combine("Scripts");
        public static readonly string DefaultPokeFilePath = System.IO.Path.Combine("maps", "poke");
        public static readonly string DefaultPokemonDataPath = System.IO.Path.Combine("Content", "Pokemon", "Data");


        public bool IsDefaultGamemode => Name == "Pokemon 3D";


        /// <summary>
        /// Create a new GameMode.
        /// </summary>
        /// <param name="name">The name of the new GameMode.</param>
        /// <param name="description">The description of the new GameMode.</param>
        /// <param name="version">The version of the new GameMode. Warning: This doesn't have to be a number!</param>
        /// <param name="author">The author of the new GameMode.</param>
        /// <param name="mapPath">The MapPath used from the new GameMode to load maps from.</param>
        /// <param name="scriptPath">The ScriptPath used from the new GameMode to load scripts from.</param>
        /// <param name="pokeFilePath"></param>
        /// <param name="pokemonDataPath">The Pokemon-Datapath to load Pokemon data from.</param>
        /// <param name="contentPath">The path to load images, sound and music from.</param>
        /// <param name="localizationsPath"></param>
        /// <param name="gameRules">The GameRules that apply to the new GameMode.</param>
        /// <param name="startMap">The start map for the new GameMode.</param>
        /// <param name="startPosition">The start position for the new GameMode.</param>
        /// <param name="startRotation">The start rotation for the new GameMode.</param>
        /// <param name="startLocationName">The start location name for the new GameMode.</param>
        /// <param name="startDialogue"></param>
        /// <param name="startColor"></param>
        /// <param name="pokemonAppear">The Pokémon that appear on the new game screen for the new GameMode.</param>
        /// <param name="introMusic">The intro music that plays on the new game screen for the new GameMode.</param>
        /// <param name="skinColors">The skin colors for the new GameMode. Must be the same amount as SkinFiles and SkinNames.</param>
        /// <param name="skinFiles">The skin files for the new GameMode. Must be the same amount as SkinColors and SkinNames.</param>
        /// <param name="skinNames">The skin names for the new GameMode. Must be the same amount as SkinFiles and SkinColors.</param>
        private GameMode(string name, string description, string version, string author, IFolder mapPath, string scriptPath, string pokeFilePath, string pokemonDataPath, IFolder contentPath, ILocalizationFolder localizationsPath,
            List<GameRule> gameRules, string startMap, Vector3 startPosition, float startRotation, string startLocationName, string startDialogue, Color startColor, int[] pokemonRange, string introMusic, List<Color> skinColors,
            List<string> skinFiles, List<string> skinNames)
        {
            Name = name;
            Description = description;
            Version = version;
            Author = author;
            MapsFolder = mapPath;
            _scriptPath = scriptPath;
            _pokeFilePath = pokeFilePath;
            _pokemonDataPath = pokemonDataPath;
            ContentFolder = contentPath;
            LocalizationsFolder = localizationsPath;
            GameRules = gameRules;

            StartMap = startMap;
            StartPosition = startPosition;
            StartRotation = startRotation;
            StartLocationName = startLocationName;
            StartDialogue = startDialogue;
            StartColor = startColor;
            PokemonRange = pokemonRange;
            IntroMusic = introMusic;
            SkinColors = skinColors;
            SkinFiles = skinFiles;
            SkinNames = skinNames;
        }


        

  
        public class GameRule
        {
            /// <summary>
            /// The name of this GameRule.
            /// </summary>
            public string RuleName { get; }

            /// <summary>
            /// The Value of this GameRule.
            /// </summary>
            public string RuleValue { get; }

            /// <summary>
            /// Creates a new GameRule.
            /// </summary>
            /// <param name="name">The name of the game rule.</param>
            /// <param name="value">The value of the game rule.</param>
            public GameRule(string name, string value)
            {
                RuleName = name;
                RuleValue = value;
            }
        }
    }
}
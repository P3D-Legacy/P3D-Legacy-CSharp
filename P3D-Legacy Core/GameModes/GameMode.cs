using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using P3D.Legacy.Core.Storage;

using PCLExt.FileStorage;

// IO:
namespace P3D.Legacy.Core.GameModes
{
    public partial class GameMode
    {
        public static string Pokemon3DGameModeName = "Pokemon 3D";

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
        }
        public static GameModeYaml CreateYaml(GameMode gameMode)
        {
            return new GameModeYaml()
            {
                Name = gameMode.Name,
                Description = gameMode.Description,
                Version = gameMode.Version,
                Author = gameMode.Author,

                MapPath = gameMode.MapsFolder,//gameMode.MapPath,
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

        public static GameMode LoadPokemon3D() => ParseYaml(GameModeYaml.Pokemon3D);

        public static async Task<GameMode> Load(string name) => ParseYaml(await GameModeYaml.LoadGameModeYaml(name));
        public static async Task Save(GameMode gameMode) => await GameModeYaml.SaveGameModeYaml(CreateYaml(gameMode));
        public static async Task<bool> Exists(string name)
        {
            if (await StorageInfo.GameModesFolder.CheckExistsAsync(name) == ExistenceCheckResult.NotFound)
                return false;

            var gameModeFolder = await StorageInfo.GameModesFolder.CreateFolderAsync(name, CreationCollisionOption.OpenIfExists);

            if (await gameModeFolder.CheckExistsAsync(GameModeYaml.GameModeFilename) == ExistenceCheckResult.NotFound)
                return false;

            return true;
        }


        public static readonly string DefaultContentPath = System.IO.Path.Combine("Content");
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
        /// <param name="pokemonRange">The Pokémon that appear on the new game screen for the new GameMode.</param>
        /// <param name="introMusic">The intro music that plays on the new game screen for the new GameMode.</param>
        /// <param name="skinColors">The skin colors for the new GameMode. Must be the same amount as SkinFiles and SkinNames.</param>
        /// <param name="skinFiles">The skin files for the new GameMode. Must be the same amount as SkinColors and SkinNames.</param>
        /// <param name="skinNames">The skin names for the new GameMode. Must be the same amount as SkinFiles and SkinColors.</param>
        private GameMode(string name, string description, string version, string author, ContentFolder mapPath, string scriptPath, string pokeFilePath, string pokemonDataPath, ContentFolder contentPath, LocalizationFolder localizationsPath, GameRuleList gameRules,
            string startMap, Vector3 startPosition, float startRotation, string startLocationName, string startDialogue, Color startColor, int[] pokemonRange, string introMusic, List<Color> skinColors, List<string> skinFiles, List<string> skinNames)
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




        public class GameRuleList : IEnumerable<KeyValuePair<string, GameRuleList.GameRuleObject>>
        {
            public abstract class GameRuleObject
            {
                public static implicit operator GameRuleObject(string value) => new GameRuleString(value);
                public static implicit operator string(GameRuleObject value) => (value as GameRuleString)?.ValueString ?? value.ToString();

                public static implicit operator GameRuleObject(double value) => new GameRuleDouble(value);
                public static implicit operator double(GameRuleObject value) => (value as GameRuleDouble)?.ValueDouble ?? 0.0d;

                public static implicit operator GameRuleObject(int value) => new GameRuleInteger(value);
                public static implicit operator int(GameRuleObject value) => (value as GameRuleInteger)?.ValueInt ?? 0;

                public static implicit operator GameRuleObject(bool value) => new GameRuleBoolean(value);
                public static implicit operator bool(GameRuleObject value) => (value as GameRuleBoolean)?.ValueBool ?? false;


                public static bool operator ==(GameRuleObject gameRuleObject, string value) => ((GameRuleString) gameRuleObject)?.ValueString == value;
                public static bool operator !=(GameRuleObject gameRuleObject, string value) => !(gameRuleObject == value);
                // TODO: Epsilon
                public static bool operator ==(GameRuleObject gameRuleObject, double value) => ((GameRuleDouble) gameRuleObject)?.ValueDouble == value;
                public static bool operator !=(GameRuleObject gameRuleObject, double value) => !(gameRuleObject == value);

                public static bool operator ==(GameRuleObject gameRuleObject, int value) => ((GameRuleInteger) gameRuleObject)?.ValueInt == value;
                public static bool operator !=(GameRuleObject gameRuleObject, int value) => !(gameRuleObject == value);

                public static bool operator ==(GameRuleObject gameRuleObject, bool value) => ((GameRuleBoolean) gameRuleObject)?.ValueBool == value;
                public static bool operator !=(GameRuleObject gameRuleObject, bool value) => !(gameRuleObject == value);

                public static bool operator >(GameRuleObject gameRuleObject, double value) => ((GameRuleDouble) gameRuleObject)?.ValueDouble > value;
                public static bool operator <(GameRuleObject gameRuleObject, double value) => ((GameRuleDouble) gameRuleObject)?.ValueDouble < value;

                public static bool operator >(GameRuleObject gameRuleObject, int value) => ((GameRuleInteger) gameRuleObject)?.ValueInt > value;
                public static bool operator <(GameRuleObject gameRuleObject, int value) => ((GameRuleInteger) gameRuleObject)?.ValueInt < value;


                public abstract override string ToString();


                public override bool Equals(object obj)
                {
                    if (ReferenceEquals(null, obj)) return false;
                    if (ReferenceEquals(this, obj)) return true;
                    //if (obj.GetType() != GetType()) return false;
                    return Equals((GameRuleObject)obj);
                }
                protected bool Equals(GameRuleObject other)
                {
                    if (this is GameRuleString && other is GameRuleString)
                    {
                        return Equals(((GameRuleString)this).ValueString, ((GameRuleString)other).ValueString);
                    }

                    if (this is GameRuleDouble && other is GameRuleDouble)
                    {
                        return Equals(((GameRuleDouble) this).ValueDouble, ((GameRuleDouble) other).ValueDouble);
                    }

                    if (this is GameRuleInteger && other is GameRuleInteger)
                    {
                        return Equals(((GameRuleInteger)this).ValueInt, ((GameRuleInteger)other).ValueInt);
                    }

                    if (this is GameRuleBoolean && other is GameRuleBoolean)
                    {
                        return Equals(((GameRuleBoolean)this).ValueBool, ((GameRuleBoolean)other).ValueBool);
                    }

                    return false;
                }
                public override int GetHashCode() => ToString().GetHashCode();
            }
            public class GameRuleString : GameRuleObject
            {
                public string ValueString { get; set; }

                public GameRuleString(string value) { ValueString = value; }

                public override string ToString() => ValueString;
            }
            public class GameRuleDouble : GameRuleObject
            {
                public double ValueDouble { get; set; }

                public GameRuleDouble(double value) { ValueDouble = value; }

                public override string ToString() => ValueDouble.ToString(CultureInfo.InvariantCulture);
            }
            public class GameRuleInteger : GameRuleObject
            {
                public int ValueInt { get; set; }

                public GameRuleInteger(int value) { ValueInt = value; }

                public override string ToString() => ValueInt.ToString(CultureInfo.InvariantCulture);
            }
            public class GameRuleBoolean : GameRuleObject
            {
                public bool ValueBool { get; set; }

                public GameRuleBoolean(bool value) { ValueBool = value; }

                public override string ToString() => ValueBool.ToString(CultureInfo.InvariantCulture);
            }


            private Dictionary<string, GameRuleObject> GameRuleDictionary { get; } = new Dictionary<string, GameRuleObject>();

            public int Count => GameRuleDictionary.Count;

            public T GetValue<T>(string ruleName) where T : GameRuleObject => (T) GameRuleDictionary[ruleName];
            public T GetValueOrDefault<T>(string ruleName, T defaultValue) where T : GameRuleObject
            {
                if (GameRuleDictionary.TryGetValue(ruleName, out GameRuleObject value))
                    return (T) value;

                GameRuleDictionary.Add(ruleName, defaultValue);
                return defaultValue;
            }

            public void Add(string ruleName, string ruleValue) => GameRuleDictionary.Add(ruleName, ruleValue);
            public void Add(string ruleName, int ruleValue) => GameRuleDictionary.Add(ruleName, ruleValue);
            public void Add(string ruleName, bool ruleValue) => GameRuleDictionary.Add(ruleName, ruleValue);
            public void Add(string ruleName, GameRuleObject ruleValue) => GameRuleDictionary.Add(ruleName, ruleValue);

            public void Remove(string ruleName) => GameRuleDictionary.Remove(ruleName);

            public bool Contains(string ruleName) => GameRuleDictionary.ContainsKey(ruleName);

            public void Clear() => GameRuleDictionary.Clear();


            public IEnumerator<KeyValuePair<string, GameRuleObject>> GetEnumerator() => GameRuleDictionary.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GameRuleDictionary.GetEnumerator();
        }
    }
}
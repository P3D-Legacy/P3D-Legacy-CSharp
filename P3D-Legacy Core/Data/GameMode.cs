using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;

using P3D.Legacy.Core.Extensions;
using P3D.Legacy.Core.Resources;

namespace P3D.Legacy.Core.Data
{
    public class GameMode
    {
        private bool _loaded;

        private string _usedFileName = "";

        /// <summary>
        /// Is this GameMode loaded correctly?
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (_loaded)
                {
                    if (Name.ToLower() == "pokemon 3d" && DirectoryName.ToLower() != "kolben")
                    {
                        Logger.Log(Logger.LogTypes.Message, "Unofficial GameMode with the name \"Pokemon 3D\" exists (in folder: \"" + DirectoryName + "\")!");
                        return false;
                    }
                    //If _name <> "" And _description <> "" And _version <> "" And _author <> "" And _mapPath <> "" And _scriptPath <> "" And _pokeFilePath <> "" And
                    //    _pokemonDataPath <> "" And _startMap <> "" And _startLocationName <> "" And _pokemonAppear <> "" And _introMusic <> "" And _skinColors.Count > 0 And _skinFiles.Count > 0 And _skinNames.Count > 0 Then
                    //    Return True
                    //End If
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// The name of the directory this GameMode is placed in. Not the full path.
        /// </summary>
        public string DirectoryName
        {
            get
            {
                if (string.IsNullOrEmpty(_usedFileName))
                    return string.Empty;
                var directory = _usedFileName.Remove(_usedFileName.LastIndexOf("\\", StringComparison.Ordinal));
                directory = directory.Remove(0, directory.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                return directory;
            }
        }

        /// <summary>
        /// The name of the directory this GameMode is placed in.
        /// </summary>
        public string Path => string.IsNullOrEmpty(_usedFileName) ? string.Empty : _usedFileName.Remove(_usedFileName.LastIndexOf("\\", StringComparison.Ordinal) + 1);

        /// <summary>
        /// Create a new GameMode.
        /// </summary>
        /// <param name="fileName">The file the gamemode should load from.</param>
        public GameMode(string fileName) { Load(fileName); }

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
        public GameMode(string name, string description, string version, string author, string mapPath, string scriptPath, string pokeFilePath, string pokemonDataPath, string contentPath, string localizationsPath,
            List<GameRule> gameRules, string startMap, Vector3 startPosition, float startRotation, string startLocationName, string startDialogue, Color startColor, string pokemonAppear, string introMusic, List<Color> skinColors,
            List<string> skinFiles, List<string> skinNames)
        {
            Name = name;
            Description = description;
            Version = version;
            Author = author;
            _mapPath = mapPath;
            _scriptPath = scriptPath;
            _pokeFilePath = pokeFilePath;
            _pokemonDataPath = pokemonDataPath;
            _contentPath = contentPath;
            _localizationsPath = localizationsPath;
            GameRules = gameRules;

            StartMap = startMap;
            StartPosition = startPosition;
            StartRotation = startRotation;
            StartLocationName = startLocationName;
            StartDialogue = startDialogue;
            StartColor = startColor;
            _pokemonAppear = pokemonAppear;
            IntroMusic = introMusic;
            SkinColors = skinColors;
            SkinFiles = skinFiles;
            SkinNames = skinNames;

            _loaded = true;
        }

        /// <summary>
        /// This loads the GameMode.
        /// </summary>
        private void Load(string fileName)
        {
            if (File.Exists(fileName))
            {
                var data = File.ReadAllLines(fileName);

                foreach (var line in data)
                {
                    if (!string.IsNullOrEmpty(line) && line.CountSeperators("|") > 0)
                    {
                        var pointer = line.Remove(line.IndexOf("|", StringComparison.Ordinal));
                        var value = line.Remove(0, line.IndexOf("|", StringComparison.Ordinal) + 1);

                        switch (pointer.ToLower())
                        {
                            case "name":
                                Name = value;
                                break;

                            case "description":
                                Description = value;
                                break;

                            case "version":
                                Version = value;
                                break;

                            case "author":
                                Author = value;
                                break;

                            case "mappath":
                                _mapPath = value;
                                break;

                            case "scriptpath":
                                _scriptPath = value;
                                break;

                            case "pokefilepath":
                                _pokeFilePath = value;
                                break;

                            case "pokemondatapath":
                                _pokemonDataPath = value;
                                break;

                            case "contentpath":
                                _contentPath = value;
                                break;

                            case "localizationspath":
                                _localizationsPath = value;
                                break;

                            case "gamerules":
                                if (!string.IsNullOrEmpty(value) && value.Contains("(") && value.Contains(")") && value.Contains("|"))
                                {
                                    var rules = value.Split(Convert.ToChar(")"));
                                    foreach (var rule in rules)
                                    {
                                        if (rule.StartsWith("("))
                                        {
                                            var ruleTemp = rule.Remove(0, 1);
                                            GameRules.Add(new GameRule(ruleTemp.GetSplit(0, "|"), ruleTemp.GetSplit(1, "|")));
                                        }
                                    }
                                }
                                break;

                            case "startmap":
                                StartMap = value;
                                break;

                            case "startposition":
                                var positionList = value.Split(Convert.ToChar(","));
                                StartPosition = positionList.Length >= 3 ? new Vector3(Convert.ToSingle(positionList[0].Replace(".", GameController.DecSeparator)), Convert.ToSingle(positionList[1].Replace(".", GameController.DecSeparator)), Convert.ToSingle(positionList[2].Replace(".", GameController.DecSeparator))) : Vector3.Zero;
                                break;

                            case "startrotation":
                                StartRotation = Convert.ToSingle(value.Replace(".", GameController.DecSeparator));
                                break;

                            case "startlocationname":
                                StartLocationName = value;
                                break;

                            case "startdialogue":
                                StartDialogue = value;
                                break;

                            case "startcolor":
                                if (!string.IsNullOrEmpty(value) && value.CountSplits(",") == 3)
                                {
                                    string[] c = value.Split(Convert.ToChar(","));
                                    StartColor = new Color(Convert.ToInt32(c[0]), Convert.ToInt32(c[1]),
                                        Convert.ToInt32(c[2]));
                                }
                                else
                                {
                                    StartColor = new Color(59, 123, 165);
                                }
                                break;

                            case "pokemonappear":
                                _pokemonAppear = value;

                                if (Convert.ToInt32(value) == 0)
                                    PokemonRange = new[] {1, 252};
                                else
                                {
                                    if (value.Contains("-"))
                                    {
                                        var v1 = Convert.ToInt32(value.GetSplit(0, "-"));
                                        var v2 = Convert.ToInt32(value.GetSplit(1, "-")) + 1;

                                        PokemonRange = new[] {v1, v2};
                                    }
                                    else
                                    {
                                        PokemonRange = new[] {Convert.ToInt32(value), Convert.ToInt32(value) + 1};
                                    }
                                }
                                break;

                            case "intromusic":
                                IntroMusic = value;
                                break;

                            case "skincolors":
                            {
                                foreach (var color in value.Split(Convert.ToChar(",")))
                                {
                                    var c = new Color(Convert.ToInt32(color.GetSplit(0, ";")),
                                        Convert.ToInt32(color.GetSplit(1, ";")), Convert.ToInt32(color.GetSplit(2, ";")));
                                    SkinColors.Add(c);
                                }
                            }
                                break;

                            case "skinfiles":
                            {
                                foreach (var skinFile in value.Split(Convert.ToChar(",")))
                                    SkinFiles.Add(skinFile);
                            }
                                break;

                            case "skinnames":
                            {
                                foreach (var skinName in value.Split(Convert.ToChar(",")))
                                    SkinNames.Add(skinName);
                            }
                                break;
                        }
                    }
                }

                _loaded = true;

                _usedFileName = fileName;
            }
        }

        /// <summary>
        /// Reload the GameMode from an already used file.
        /// </summary>
        public void Reload() => Load(_usedFileName);

        /// <summary>
        /// Reload the GameMode.
        /// </summary>
        /// <param name="fileName">Use this file to reload the GameMode from.</param>
        public void Reload(string fileName) => Load(fileName);

        /// <summary>
        /// Returns the default Kolben Game Mode.
        /// </summary>
        public static GameMode GetKolbenGameMode()
        {
            var skinColors = new List<Color>
            {
                new Color(248, 176, 32),
                new Color(248, 216, 88),
                new Color(56, 88, 200),
                new Color(216, 96, 112),
                new Color(56, 88, 152),
                new Color(239, 90, 156)
            };
            var skinFiles = new List<string>
            {
                "Ethan",
                "Lyra",
                "Nate",
                "Rosa",
                "Hilbert",
                "Hilda"
            };
            var skinNames = new List<string>
            {
                "Ethan",
                "Lyra",
                "Nate",
                "Rosa",
                "Hilbert",
                "Hilda"
            };

            var gameMode = new GameMode("Pokemon 3D", "The normal game mode.", GameController.GAMEVERSION, "Kolben Games", "maps", "Scripts", System.IO.Path.Combine("maps", "poke"), System.IO.Path.Combine("Content", "Pokemon", "Data"), "Content", System.IO.Path.Combine("Content", "Localization"),
                new List<GameRule>(), "yourroom.dat", new Vector3(1f, 0.1f, 3f), MathHelper.PiOver2, "Your Room", "", new Color(59, 123, 165), "0", "welcome", skinColors,
                skinFiles, skinNames);

            var gameRules = new List<GameRule>
            {
                new GameRule("MaxLevel", "100"),
                new GameRule("OnlyCaptureFirst", "0"),
                new GameRule("ForceRename", "0"),
                new GameRule("DeathInsteadOfFaint", "0"),
                new GameRule("CanUseHealItems", "1"),
                new GameRule("Difficulty", "0"),
                new GameRule("LockDifficulty", "0"),
                new GameRule("GameOverAt0Pokemon", "0"),
                new GameRule("CanGetAchievements", "1"),
                new GameRule("ShowFollowPokemon", "1")
            };

            gameMode.GameRules.AddRange(gameRules);

            return gameMode;
        }

        /// <summary>
        /// Export this GameMode to a file.
        /// </summary>
        /// <param name="file">The file this GameMode should get exported to.</param>
        public void SaveToFile(string file)
        {
            string s = "Name|" + Name + Constants.vbNewLine + "Description|" + Description + Constants.vbNewLine + "Version|" + Version + Constants.vbNewLine + "Author|" + Author + Constants.vbNewLine + "MapPath|" + _mapPath + Constants.vbNewLine + "ScriptPath|" + _scriptPath + Constants.vbNewLine + "PokeFilePath|" + _pokeFilePath + Constants.vbNewLine + "PokemonDataPath|" + _pokemonDataPath + Constants.vbNewLine + "ContentPath|" + _contentPath + Constants.vbNewLine + "LocalizationsPath|" + _localizationsPath + Constants.vbNewLine;

            string gameRuleString = "Gamerules|";
            foreach (GameRule rule in GameRules)
            {
                gameRuleString += "(" + rule.RuleName + "|" + rule.RuleValue + ")";
            }

            s += gameRuleString + Constants.vbNewLine + "StartMap|" + StartMap + Constants.vbNewLine + "StartPosition|" + StartPosition.X.ToString().Replace(GameController.DecSeparator, ".") + "," + StartPosition.Y.ToString().Replace(GameController.DecSeparator, ".") + "," + StartPosition.Z.ToString().Replace(GameController.DecSeparator, ".") + Constants.vbNewLine + "StartRotation|" + StartRotation.ToString().Replace(GameController.DecSeparator, ".") + Constants.vbNewLine + "StartLocationName|" + StartLocationName + Constants.vbNewLine + "StartDialogue|" + StartDialogue + Constants.vbNewLine + "StartColor|" + StartColor.R + "," + StartColor.G + "," + StartColor.B + Constants.vbNewLine + "PokemonAppear|" + _pokemonAppear + Constants.vbNewLine + "IntroMusic|" + IntroMusic + Constants.vbNewLine;

            string skinColorsString = "SkinColors|";
            int iSc = 0;
            foreach (Color skinColor in SkinColors)
            {
                if (iSc > 0)
                {
                    skinColorsString += ",";
                }

                skinColorsString += skinColor.R + ";" + skinColor.G + ";" + skinColor.B;

                iSc += 1;
            }

            s += skinColorsString + Constants.vbNewLine;

            string skinFilesString = "SkinFiles|";
            int iSf = 0;
            foreach (string skinFile in SkinFiles)
            {
                if (iSf > 0)
                {
                    skinFilesString += ",";
                }

                skinFilesString += skinFile;

                iSf += 1;
            }

            s += skinFilesString + Constants.vbNewLine;

            string skinNamesString = "SkinNames|";
            int iSn = 0;
            foreach (string skinName in SkinNames)
            {
                if (iSn > 0)
                {
                    skinNamesString += ",";
                }

                skinNamesString += skinName;

                iSn += 1;
            }

            s += skinNamesString;

            string folder = System.IO.Path.GetDirectoryName(file);
            if (Directory.Exists(folder) == false)
                Directory.CreateDirectory(folder);

            File.WriteAllText(file, s);
        }

        public bool IsDefaultGamemode => Name == "Pokemon 3D";

        #region "Paths"

        public static readonly string DefaultContentPath = System.IO.Path.Combine("Content");
        public static readonly string DefaultMapPath = System.IO.Path.Combine("maps");
        public static readonly string DefaultScriptPath = System.IO.Path.Combine("Scripts");
        public static readonly string DefaultPokeFilePath = System.IO.Path.Combine("maps", "poke");
        public static readonly string DefaultPokemonDataPath = System.IO.Path.Combine("Content", "Pokemon", "Data");
        public static readonly string DefaultLocalizationsPath = System.IO.Path.Combine("Content", "Localization");

        #endregion

        #region "GameMode"

        /// <summary>
        /// The name of this GameMode.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The Description of the GameMode. This may contain multiple lines.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The version of the GameMode. Warning: This doesn't have to be a number!
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// The author of the GameMode.
        /// </summary>
        public string Author { get; set; } = string.Empty;

        /// <summary>
        /// The MapPath used from this GameMode to load maps from.
        /// </summary>
        public string MapPath
        {
            get { return _mapPath.Replace("$Mode", "\\GameModes\\" + DirectoryName); }
            set { _mapPath = value; }
        }
        private string _mapPath = string.Empty;

        /// <summary>
        /// The ScriptPath from this GameMode to load scripts from.
        /// </summary>
        public string ScriptPath
        {
            get { return _scriptPath.Replace("$Mode", "\\GameModes\\" + DirectoryName); }
            set { _scriptPath = value; }
        }
        private string _scriptPath = string.Empty;

        /// <summary>
        /// The .poke file directory from this GameMode.
        /// </summary>
        public string PokeFilePath
        {
            get { return _pokeFilePath.Replace("$Mode", "\\GameModes\\" + DirectoryName); }
            set { _pokeFilePath = value; }
        }
        private string _pokeFilePath = string.Empty;

        /// <summary>
        /// The Pokemon-Datapath to load Pokemon data from.
        /// </summary>
        public string PokemonDataPath
        {
            get { return _pokemonDataPath.Replace("$Mode", "\\GameModes\\" + DirectoryName); }
            set { _pokemonDataPath = value; }
        }
        private string _pokemonDataPath = string.Empty;

        /// <summary>
        /// The content path to load images, sounds and music from.
        /// </summary>
        public string ContentPath
        {
            get { return _contentPath.Replace("$Mode", "\\GameModes\\" + DirectoryName); }
            set { _contentPath = value; }
        }
        private string _contentPath = string.Empty;

        /// <summary>
        /// The localizations path to load additional tokens from. Tokens that are already existing get overritten.
        /// </summary>
        public string LocalizationsPath
        {
            get { return _localizationsPath.Replace("$Mode", "\\GameModes\\" + DirectoryName); }
            set { _localizationsPath = value; }
        }
        private string _localizationsPath = string.Empty;

        /// <summary>
        /// The GameRules that apply to this GameMode.
        /// </summary>
        public List<GameRule> GameRules { get; } = new List<GameRule>();

        #endregion

        #region "StartUp"

        /// <summary>
        /// The start map for this GameMode.
        /// </summary>
        public string StartMap { get; set; } = string.Empty;

        /// <summary>
        /// The start position for this GameMode.
        /// </summary>
        public Vector3 StartPosition { get; set; }

        /// <summary>
        /// The start rotation for this GameMode.
        /// </summary>
        public float StartRotation { get; set; }

        /// <summary>
        /// The start location name for this GameMode.
        /// </summary>
        public string StartLocationName { get; set; } = string.Empty;

        /// <summary>
        /// The dialogue said in the intro of the game. Split in 3 different texts: intro dialogue, after Pokémon jumped out, after name + character choose.
        /// </summary>
        public string StartDialogue { get; set; } = string.Empty;

        /// <summary>
        /// The default background color in the intro sequence.
        /// </summary>
        public Color StartColor { get; set; } = new Color(59, 123, 165);

        /// <summary>
        /// The Pokémon that appear on the new game screen for this GameMode.
        /// </summary>
        public string PokemonAppear
        {
            get { return _pokemonAppear; }
            set
            {
                _pokemonAppear = value;

                if (Convert.ToInt32(value) == 0)
                    PokemonRange = new [] { 1, 252 };
                else
                {
                    if (value.Contains("-"))
                    {
                        int v1 = Convert.ToInt32(value.GetSplit(0, "-"));
                        int v2 = Convert.ToInt32(value.GetSplit(1, "-")) + 1;

                        PokemonRange = new [] { v1, v2 };
                    }
                    else
                        PokemonRange = new [] { Convert.ToInt32(value), Convert.ToInt32(value) + 1 };
                }
            }
        }
        private string _pokemonAppear = string.Empty;

        /// <summary>
        /// The Pokémon range that will appear on the new game screen for this GameMode.
        /// </summary>
        public int[] PokemonRange { get; private set; }

        /// <summary>
        /// The intro music that plays on the new game screen for this GameMode.
        /// </summary>
        public string IntroMusic { get; set; } = string.Empty;

        /// <summary>
        /// The skin colors for this GameMode. Must be the same amount as SkinFiles and SkinNames.
        /// </summary>
        public List<Color> SkinColors { get; } = new List<Color>();

        /// <summary>
        /// The skin files for this GameMode. Must be the same amount as SkinNames and SkinColors.
        /// </summary>
        public List<string> SkinFiles { get; } = new List<string>();

        /// <summary>
        /// The skin names for this GameMode. Must be the same amount as SkinFiles and SkinColors.
        /// </summary>
        public List<string> SkinNames { get; } = new List<string>();

        #endregion

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
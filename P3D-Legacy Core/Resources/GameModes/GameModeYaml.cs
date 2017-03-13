using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using P3D.Legacy.Core.GameModes.YamlConverters;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Storage;
using P3D.Legacy.Core.Storage.Folders;

using PCLExt.FileStorage;

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Core.GameModes
{
    public class GameModeYaml
    {
        public const string GameModeFilename = "GameMode.yml";

        public static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder().EmitDefaults()
            .WithTypeConverter(new Vector3Converter()).WithTypeConverter(new ColorConverter())
            .WithTypeConverter(new LocalizationFolderLocalConverter()).WithTypeConverter(new ContentFolderLocalConverter()).WithTypeConverter(new MapsFolderLocalConverter()).WithTypeConverter(new ScriptsFolderLocalConverter())
            .WithTypeConverter(new GameRuleListConverter());

        public static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder().IgnoreUnmatchedProperties()
            .WithTypeConverter(new Vector3Converter()).WithTypeConverter(new ColorConverter())
            .WithTypeConverter(new LocalizationFolderLocalConverter()).WithTypeConverter(new ContentFolderLocalConverter()).WithTypeConverter(new MapsFolderLocalConverter()).WithTypeConverter(new ScriptsFolderLocalConverter())
            .WithTypeConverter(new GameRuleListConverter());


        public static GameModeYaml Pokemon3D => new GameModeYaml
        {
            Name = "Pokemon 3D",
            Description = "The standard game mode.",
            Version = GameController.GAMEVERSION,
            Author = "The P3D Team",

            MapFolder = StorageInfo.MapFolder,
            ScriptFolder = StorageInfo.ScriptFolder,
            PokeFolder = StorageInfo.PokeFolder,
            PokemonDataFolder = StorageInfo.PokemonDataFolder,
            ContentFolder = StorageInfo.ContentFolder,
            LocalizationFolder = StorageInfo.LocalizationFolder,

            GameRules =
            {
                {"MaxLevel", 100},
                { "OnlyCaptureFirst", false},
                { "ForceRename", false},
                { "DeathInsteadOfFaint", false},
                { "CanUseHealItems", true},
                { "Difficulty", 0},
                { "LockDifficulty", false},
                { "GameOverAt0Pokemon", false},
                { "CanGetAchievements", true},
                { "ShowFollowPokemon", true}
            },
            
            StartMap = "yourroom.dat",
            StartPosition = new Vector3(1f, 0.1f, 3f),
            StartRotation = MathHelper.PiOver2,
            StartLocationName = "Your Room",
            StartDialogue = "",
            StartColor = new Color(59, 123, 165),

            PokemonRange = new int[] { 1, 252 },

            IntroMusic = "welcome",

            SkinColors =
            {
                new Color(248, 176, 32),
                new Color(248, 216, 88),
                new Color(56, 88, 200),
                new Color(216, 96, 112),
                new Color(56, 88, 152),
                new Color(239, 90, 156)
            },
            SkinFiles =
            {
                "Ethan",
                "Lyra",
                "Nate",
                "Rosa",
                "Hilbert",
                "Hilda"
            },
            SkinNames =
            {
                "Ethan",
                "Lyra",
                "Nate",
                "Rosa",
                "Hilbert",
                "Hilda"
            }, 

        };

        public static async Task SaveGameModeYaml(GameModeYaml gameModeYaml)
        {
            var serializer = SerializerBuilder.Build();
            var gameModeFolder = await StorageInfo.GameModesFolder.CreateFolderAsync(gameModeYaml.Name, CreationCollisionOption.OpenIfExists);
            var gameModeFile = await gameModeFolder.CreateFileAsync(GameModeFilename, CreationCollisionOption.ReplaceExisting);
            await gameModeFile.WriteAllTextAsync(serializer.Serialize(gameModeYaml));
        }
        public static async Task<GameModeYaml> LoadGameModeYaml(string gameModeName)
        {
            var deserializer = DeserializerBuilder.Build();
            try
            {
                var gameModeFolder = await StorageInfo.GameModesFolder.CreateFolderAsync(gameModeName, CreationCollisionOption.OpenIfExists);
                var gameModeFile = await gameModeFolder.GetFileAsync(GameModeFilename);
                var data = await gameModeFile.ReadAllTextAsync();
                var deserialized = deserializer.Deserialize<GameModeYaml>(data);
                return deserialized ?? Pokemon3D;
            }
            catch (YamlException)
            {
                Logger.Log(Logger.LogTypes.Warning, $"Error while trying to deserialize GameMode {gameModeName}");
                return Pokemon3D;
            }
        }



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
        public MapsFolder MapFolder { get; set; } = StorageInfo.MapFolder;

        /// <summary>
        /// The ScriptPath from this GameMode to load scripts from.
        /// </summary>
        public ScriptsFolder ScriptFolder { get; set; } = StorageInfo.ScriptFolder;

        /// <summary>
        /// The .poke file directory from this GameMode.
        /// </summary>
        public PokeFolder PokeFolder { get; set; } = StorageInfo.PokeFolder;

        /// <summary>
        /// The Pokemon Data path to load Pokemon data from.
        /// </summary>
        public PokemonDataFolder PokemonDataFolder { get; set; } = StorageInfo.PokemonDataFolder;

        /// <summary>
        /// The Content path to load images, sounds and music from.
        /// </summary>
        public ContentFolder ContentFolder { get; set; } = StorageInfo.ContentFolder;

        /// <summary>
        /// The Localizations path to load additional tokens from. Tokens that are already existing get overritten.
        /// </summary>
        public LocalizationFolder LocalizationFolder { get; set; } = StorageInfo.LocalizationFolder;


        /// <summary>
        /// The GameRules that apply to this GameMode.
        /// </summary>
        public GameMode.GameRuleList GameRules { get; set; } = new GameMode.GameRuleList();


        /// <summary>
        /// The start map for this GameMode.
        /// </summary>
        public string StartMap { get; set; } = string.Empty;

        /// <summary>
        /// The start position for this GameMode.
        /// </summary>
        public Vector3 StartPosition { get; set; } = Vector3.Zero;

        /// <summary>
        /// The start rotation for this GameMode.
        /// </summary>
        public float StartRotation { get; set; } = 0.0f;

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
        /// The Pokémon range that will appear on the new game screen for this GameMode.
        /// </summary>
        public int[] PokemonRange { get; set; } = { 1, 252 };


        /// <summary>
        /// The intro music that plays on the new game screen for this GameMode.
        /// </summary>
        public string IntroMusic { get; set; } = string.Empty;


        /// <summary>
        /// The skin colors for this GameMode. Must be the same amount as SkinFiles and SkinNames.
        /// </summary>
        public List<Color> SkinColors { get; set; } = new List<Color>();

        /// <summary>
        /// The skin files for this GameMode. Must be the same amount as SkinNames and SkinColors.
        /// </summary>
        public List<string> SkinFiles { get; set; } = new List<string>();

        /// <summary>
        /// The skin names for this GameMode. Must be the same amount as SkinFiles and SkinColors.
        /// </summary>
        public List<string> SkinNames { get; set; } = new List<string>();
    }
}

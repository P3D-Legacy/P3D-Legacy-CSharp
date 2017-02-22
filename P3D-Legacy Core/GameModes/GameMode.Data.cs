using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Storage;

using PCLExt.FileStorage;

// IO:
namespace P3D.Legacy.Core.GameModes
{
    public partial class GameMode
    {
        /// The name of the directory this GameMode is placed in.
        /// </summary>
        public IFolder Path => StorageInfo.GameModesFolder.CreateFolderAsync(Name, CreationCollisionOption.OpenIfExists).Result;



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
        public IFolder MapsFolder { get; set; } = StorageInfo.MapsFolder;

        /// <summary>
        /// The ScriptPath from this GameMode to load scripts from.
        /// </summary>
        public string ScriptPath
        {
            get { return _scriptPath.Replace("$Mode", "\\GameModes\\" + Name); }
            set { _scriptPath = value; }
        }
        private string _scriptPath = string.Empty;

        /// <summary>
        /// The .poke file directory from this GameMode.
        /// </summary>
        public string PokeFilePath
        {
            get { return _pokeFilePath.Replace("$Mode", "\\GameModes\\" + Name); }
            set { _pokeFilePath = value; }
        }
        private string _pokeFilePath = string.Empty;

        /// <summary>
        /// The Pokemon Data path to load Pokemon data from.
        /// </summary>
        public string PokemonDataPath
        {
            get { return _pokemonDataPath.Replace("$Mode", "\\GameModes\\" + Name); }
            set { _pokemonDataPath = value; }
        }
        private string _pokemonDataPath = string.Empty;

        /// <summary>
        /// The Content path to load images, sounds and music from.
        /// </summary>
        public IFolder ContentFolder { get; set; } = StorageInfo.ContentFolder;

        /// <summary>
        /// The Localizations path to load additional tokens from. Tokens that are already existing get overritten.
        /// </summary>
        public ILocalizationFolder LocalizationsFolder { get; set; } = StorageInfo.LocalizationFolder;

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
        /// The Pokémon range that will appear on the new game screen for this GameMode.
        /// </summary>
        public int[] PokemonRange { get; private set; } = { 1, 252 };

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
    }
}
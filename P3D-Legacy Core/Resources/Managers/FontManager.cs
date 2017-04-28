using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Data;
using P3D.Legacy.Core.Storage;
using P3D.Legacy.Core.Storage.Folders;

//IO:
namespace P3D.Legacy.Core.Resources.Managers
{
    public static class FontManager
    {
        private static Dictionary<string, FontContainer> FontList { get; } = new Dictionary<string, FontContainer>();
        //we can maybe put language specific fonts in via localization system, we have a place to start at least

        //this sub looks for all fonts that should be in the system (base files, mode files, pack files) and shoves them into FontList
        public static void LoadFonts()
        {
            HasLoaded = false;
            FontList.Clear();

            //because the pack manager will hoover up replacements from packs and mods already we just load every font name contained in our base files
            foreach (var fontFile in new ContentFolder().FontFolder.GetFontFiles())
            {
                var fileName = fontFile.InContentLocalPathWithoutExtension.Replace("\\", "|").ToLowerInvariant();
                FontList.Add(fileName, fontFile);
            }
            //then look for ADDITIONAL fonts in packs, the ones that exist will have the user's prefered copy already
            if (Core.GameOptions.ContentPackNames.Any())
            {
                foreach (var contentPackName in Core.GameOptions.ContentPackNames)
                {
                    var contentPackFontFolder = new ContentPacksFolder().GetContentPack(contentPackName).FontFolder;
                    foreach (var fontFile in contentPackFontFolder.GetFontFiles())
                    {
                        var fileName = fontFile.InContentLocalPathWithoutExtension.Replace("\\", "|").ToLowerInvariant();
                        FontList.Add(fileName, fontFile);
                    }
                }
            }
            //if there's a game mode loaded, look in that too for additional fonts
            if (!GameModeManager.ActiveGameMode.IsDefaultGamemode && !Equals(GameModeManager.ActiveGameMode.ContentFolder, new ContentFolder()))
            {
                var gameModeFontFolder = GameModeManager.ActiveGameMode.ContentFolder.FontFolder;
                foreach (var fontFile in gameModeFontFolder.GetFontFiles())
                {
                    var fileName = fontFile.InContentLocalPathWithoutExtension.Replace("\\", "|").ToLowerInvariant();
                    FontList.Add(fileName, fontFile);
                }
            }

            HasLoaded = true;
        }

        /// <summary>
        /// Looks to see if a font is loaded. Code that uses this should generally check for a return of nothing, indicating the font does not exist.
        /// </summary>
        /// <param name="fontName">The name of the font.</param>
        public static SpriteFont GetFont(string fontName) => FontList.ContainsKey(fontName.ToLower()) ? FontList[fontName.ToLower()].SpriteFont : null;

        /// <summary>
        /// Looks to see if a FontContainer is loaded. Code that uses this should generally check for a return of nothing, indicating the font does not exist.
        /// </summary>
        /// <param name="fontName">The name of the font.</param>
        public static FontContainer GetFontContainer(string fontName) => FontList.ContainsKey(fontName.ToLower()) ? FontList[fontName.ToLower()] : null;


        public static SpriteFont MainFont => GetFont("mainfont");
        public static SpriteFont TextFont => GetFont("textfont");
        public static SpriteFont InGameFont => GetFont("ingame");
        public static SpriteFont MiniFont => GetFont("minifont");
        public static SpriteFont ChatFont => GetFont("chatfont");
        public static SpriteFont UnownFont => GetFont("unown");
        public static SpriteFont BrailleFont => GetFont("braille");

        public static bool HasLoaded { get; set; }
    }
}
using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Data;
using P3D.Legacy.Core.Storage;
using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Resources
{
    public static class FontManager
    {
        private static Dictionary<string, FontContainer> FontList { get; } = new Dictionary<string, FontContainer>();
        //we can maybe put language specific fonts in via localization system, we have a place to start at least

        //this sub looks for all fonts that should be in the system (base files, mode files, pack files) and shoves them into FontList
        public static void LoadFonts()
        {
            FontList.Clear();
            //because the pack manager will hoover up replacements from packs and mods already we just load every font name contained in our base files
            var p0 = Path.Combine(GameController.GamePath, "Content", "Fonts", "BMP");
            foreach (string file in Directory.GetFiles(p0))
            {
                if (file.EndsWith(".xnb"))
                {
                    string name = Path.GetFileNameWithoutExtension(file);
                    if (!FontList.ContainsKey(name.ToLower()))
                    {
                        var p1 = Path.Combine("Fonts", "BMP", name);
                        SpriteFont font = ContentPackManager.GetContentManager(p1, ".xnb").Load<SpriteFont>(p1);
                        FontList.Add(name.ToLower(), new FontContainer(name, font));
                    }
                }
            }
            //then look for ADDITIONAL fonts in packs, the ones that exist will have the user's prefered copy already
            foreach (string c in Core.GameOptions.ContentPackNames)
            {
                var p1 = Path.Combine(GameController.GamePath, "ContentPacks", c, "Content", "Fonts", "BMP");
                if (Directory.Exists(p1))
                {
                    foreach (var file in Directory.GetFiles(p1))
                    {
                        if (file.EndsWith(".xnb"))
                        {
                            var name = Path.GetFileNameWithoutExtension(file);
                            if (!FontList.ContainsKey(name.ToLower()))
                            {
                                var p2 = Path.Combine("Fonts", "BMP", name);
                                SpriteFont font = ContentPackManager.GetContentManager(p2, ".xnb").Load<SpriteFont>(p2);
                                FontList.Add(name.ToLower(), new FontContainer(name, font));
                            }
                        }
                    }
                }
            }
            //if there's a game mode loaded, look in that too for additional fonts
            if (!GameModeManager.ActiveGameMode.IsDefaultGamemode)
            {
                if (!Equals(GameModeManager.ActiveGameMode.ContentFolder, StorageInfo.ContentFolder))
                {
                    var p1 = GameModeManager.ActiveGameMode.ContentFolder
                        .CreateFolderAsync("Fonts", CreationCollisionOption.OpenIfExists).Result
                        .CreateFolderAsync("BMP", CreationCollisionOption.OpenIfExists).Result;

                    foreach (var file in p1.GetFilesAsync().Result)
                    {
                        if (file.Name.EndsWith(".xnb"))
                        {
                            var name = Path.GetFileNameWithoutExtension(file.Name);
                            if (!FontList.ContainsKey(name.ToLower()))
                            {
                                var p2 = Path.Combine("Fonts", "BMP", name);
                                var font = ContentPackManager.GetContentManager(p2, ".xnb").Load<SpriteFont>(p2);
                                FontList.Add(name.ToLower(), new FontContainer(name, font));
                            }
                        }
                    }
                }
            }
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
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using P3D.Legacy.Core.Data;
using P3D.Legacy.Core.Extensions;
using P3D.Legacy.Core.GameModes;
using P3D.Legacy.Core.Resources.Managers;
using P3D.Legacy.Core.Resources.Managers.Music;
using P3D.Legacy.Core.Resources.Managers.Sound;
using P3D.Legacy.Core.Storage;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Resources
{
    public static class ContentPackManager
    {
        private static Dictionary<TextureSource, TextureSource> TextureReplacements { get; } = new Dictionary<TextureSource, TextureSource>();
        private static Dictionary<string, bool> FilesExist { get; } = new Dictionary<string, bool>();
        private static Dictionary<string, int> TextureResolutions { get; } = new Dictionary<string, int>();

        public static void Load(string contentPackFile)
        {
            if (Directory.Exists(Path.Combine(GameController.GamePath, "ContentPacks")))
            {
                if (!File.Exists(contentPackFile))
                    return;
                foreach (var line in File.ReadAllLines(contentPackFile))
                {
                    switch (line.CountSplits("|"))
                    {
                        case 2:
                            //ResolutionChange
                            var textureName = line.GetSplit(0, "|");
                            var resolution = Convert.ToInt32(line.GetSplit(1, "|"));

                            if (!TextureResolutions.ContainsKey(textureName))
                                TextureResolutions.Add(textureName, resolution);
                            break;
                        case 4:
                            //TextureReplacement
                            var oldTextureName = line.GetSplit(0, "|");
                            var newTextureName = line.GetSplit(2, "|");
                            var oRs = line.GetSplit(1, "|");
                            //oRS = oldRectangleSource
                            var nRs = line.GetSplit(3, "|");
                            //nRS = newRectangleSource

                            var oldTextureSource = new TextureSource(oldTextureName, new Rectangle(Convert.ToInt32(oRs.GetSplit(0)), Convert.ToInt32(oRs.GetSplit(1)), Convert.ToInt32(oRs.GetSplit(2)), Convert.ToInt32(oRs.GetSplit(3))));
                            var newTextureSource = new TextureSource(newTextureName, new Rectangle(Convert.ToInt32(nRs.GetSplit(0)), Convert.ToInt32(nRs.GetSplit(1)), Convert.ToInt32(nRs.GetSplit(2)), Convert.ToInt32(nRs.GetSplit(3))));

                            if (!TextureReplacements.ContainsKey(oldTextureSource))
                                TextureReplacements.Add(oldTextureSource, newTextureSource);

                            break;
                    }
                }
            }
        }

        public static ContentManager GetContentManager(string file, string fileEndings)
        {
            if (Core.GameOptions.ContentPackNames.Any())
            {
                foreach (var contentPackName in Core.GameOptions.ContentPackNames)
                {
                    var contentPath = Path.Combine("ContentPacks", contentPackName);

                    foreach (var fileEnding in fileEndings.Split(Convert.ToChar(",")))
                    {
                        var path = Path.Combine(GameController.GamePath, contentPath, file + fileEnding);

                        if (!FilesExist.ContainsKey(path))
                            FilesExist.Add(path, File.Exists(path));

                        if (FilesExist[path])
                            return new ContentManager(Core.GameInstance.Services, contentPath);
                    }
                }
            }

            GameMode gameMode = GameModeManager.ActiveGameMode;
            if (!Equals(gameMode.ContentFolder, StorageInfo.ContentFolder))
            {
                if (fileEndings.Split(Convert.ToChar(",")).Any(fileEnding => gameMode.ContentFolder.CheckExists(file + fileEnding) == ExistenceCheckResult.FileExists))
                    return new ContentManager(Core.GameInstance.Services, gameMode.ContentFolder.Path);
            }

            return new ContentManager(Core.GameInstance.Services, "Content");
        }

        public static string[] GetContentPackInfo(string contentPackName)
        {
            var p0 = Path.Combine(GameController.GamePath, "ContentPacks", contentPackName, "info.dat");
            if (!File.Exists(p0))
            {
                var s = "1.00" + Environment.NewLine + "Pokémon3D" + Environment.NewLine + "[Add information here!]";
                File.WriteAllText(p0, s);
            }
            return File.ReadAllLines(p0);
        }

        public static void Clear()
        {
            TextureReplacements.Clear();
            TextureResolutions.Clear();
            FilesExist.Clear();
            MusicManager.ReloadMusic();
            SoundEffectManager.ReloadSounds();
            ModelManager.Clear();
            TextureManager.TextureList.Clear();
            // TODO
            //Water.ClearAnimationResources();
            //Whirlpool.LoadedWaterTemp = false;
            //Waterfall.ClearAnimationResources();

            Logger.Debug("---Cleared ContentPackManager---");
        }
    }
}

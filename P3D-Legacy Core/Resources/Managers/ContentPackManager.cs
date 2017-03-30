using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using P3D.Legacy.Core.Data;
using P3D.Legacy.Core.Extensions;
using P3D.Legacy.Core.GameModes;
using P3D.Legacy.Core.Resources.Sound;
using P3D.Legacy.Core.Storage;
using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Resources
{
    public static class ContentPackManager
    {
        private static Dictionary<TextureSource, TextureSource> _textureReplacements = new Dictionary<TextureSource, TextureSource>();
        private static Dictionary<string, bool> _filesExist = new Dictionary<string, bool>();

        private static Dictionary<string, int> _textureResolutions = new Dictionary<string, int>();
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

                            if (!_textureResolutions.ContainsKey(textureName))
                                _textureResolutions.Add(textureName, resolution);
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

                            if (!_textureReplacements.ContainsKey(oldTextureSource))
                                _textureReplacements.Add(oldTextureSource, newTextureSource);

                            break;
                    }
                }
            }
        }

        public static TextureSource GetTextureReplacement(string texturePath, Rectangle r)
        {
            var textureSource = new TextureSource(texturePath, r);
            var keyArray = _textureReplacements.Keys.ToArray();
            var valueArray = _textureReplacements.Values.ToArray();
            for (var i = 0; i <= _textureReplacements.Count - 1; i++)
            {
                if (keyArray[i].IsEqual(textureSource))
                    return valueArray[i];
            }
            return textureSource;
        }

        public static int GetTextureResolution(string textureName)
        {
            var keyArray = _textureResolutions.Keys.ToArray();
            var valueArray = _textureResolutions.Values.ToArray();
            for (var i = 0; i <= _textureResolutions.Count - 1; i++)
            {
                if (keyArray[i].ToLower() == textureName.ToLower())
                    return valueArray[i];
            }

            return 1;
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

                        if (!_filesExist.ContainsKey(path))
                            _filesExist.Add(path, File.Exists(path));

                        if (_filesExist[path])
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
            _textureReplacements.Clear();
            _textureResolutions.Clear();
            _filesExist.Clear();
            MusicManager.ReloadMusic();
            SoundManager.ReloadSounds();
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

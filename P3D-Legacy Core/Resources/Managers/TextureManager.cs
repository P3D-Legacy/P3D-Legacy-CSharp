using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Extensions;

namespace P3D.Legacy.Core.Resources
{
    public static class TextureManager
    {
        public static Texture2D DefaultTexture;
        public static void InitializeTextures() { DefaultTexture = Core.Content.Load<Texture2D>(Path.Combine("GUI", "no_texture")); }

        public static Dictionary<string, Texture2D> TextureList { get; } = new Dictionary<string, Texture2D>();

        public static Dictionary<KeyValuePair<int, Rectangle>, Texture2D> TextureRectList { get; } = new Dictionary<KeyValuePair<int, Rectangle>, Texture2D>();

        /// <summary>
        /// Returns a texture.
        /// </summary>
        /// <param name="name">The name of the texture.</param>
        public static Texture2D GetTexture(string name)
        {
            // TODO: TEMP FIX
            if (name.EndsWith("\\"))
                name = name.Remove(name.Length - 2, 2);

            var cContent = ContentPackManager.GetContentManager(name, ".xnb,.png");

            var tKey = Path.Combine(cContent.RootDirectory, name + ",FULL_IMAGE");

            if (!TextureList.ContainsKey(tKey))
            {
                Texture2D texture;

                var p1 = Path.Combine(GameController.GamePath, cContent.RootDirectory, name + ".xnb");
                if (!File.Exists(p1))
                {
                    var p2 = Path.Combine(GameController.GamePath, cContent.RootDirectory, name + ".png");
                    if (File.Exists(p2))
                    {
                        using (Stream stream = File.Open(p2, FileMode.OpenOrCreate))
                        {
                            try { texture = Texture2D.FromStream(Core.GraphicsDevice, stream); }
                            catch (Exception)
                            {
                                Logger.Log(Logger.LogTypes.ErrorMessage, "Something went wrong while XNA tried to load a texture. Return default.");
                                return DefaultTexture;
                            }
                        }
                    }
                    else
                    {
                        Logger.Log(Logger.LogTypes.ErrorMessage, "Texures.vb: Texture \"" + GameController.GamePath + "\\" + cContent.RootDirectory + "\\" + name + "\" was not found!");
                        return DefaultTexture;
                    }
                }
                else
                    texture = cContent.Load<Texture2D>(name);

                TextureList.Add(tKey, ApplyEffect(TextureRectangle(texture, new Rectangle(0, 0, texture.Width, texture.Height))));

                cContent.Unload();
            }

            return TextureList[tKey];
        }

        private static Texture2D ApplyEffect(Texture2D t) { /* There was a hacker check. Ignore it */ return t; }

        /// <summary>
        /// Returns a texture.
        /// </summary>
        /// <param name="name">The name of the texture.</param>
        /// <param name="r">The rectangle to get the texture from.</param>
        /// <param name="texturePath">The texturepath to load a texture from.</param>
        public static Texture2D GetTexture(string name, Rectangle r, string texturePath)
        {
            // -- This is too slow in my opition.
            var tSource = ContentPackManager.GetTextureReplacement(Path.Combine(texturePath, name), r);
            var cContent = ContentPackManager.GetContentManager(tSource.TexturePath, ".xnb,.png");
            var resolution = ContentPackManager.GetTextureResolution(Path.Combine(texturePath, name));

            var tKey = Path.Combine(cContent.RootDirectory, texturePath, name + "," + r.X + "," + r.Y + "," + r.Width + "," + r.Height + "," + resolution);
            if (!TextureList.ContainsKey(tKey))
            {
                Texture2D texture;
                var doApplyEffect = true;

                var tPath = Path.Combine(cContent.RootDirectory, texturePath, name);
                if (TextureList.ContainsKey(tPath))
                {
                    texture = TextureList[tPath];
                    doApplyEffect = false;
                }
                else
                {
                    var pathXnb = Path.Combine(GameController.GamePath, cContent.RootDirectory, tSource.TexturePath + ".xnb");
                    var pathPng = Path.Combine(GameController.GamePath, cContent.RootDirectory, tSource.TexturePath + ".png");
                    if (!File.Exists(pathXnb))
                    {
                        if (File.Exists(pathPng))
                        {
                            using (Stream stream = File.Open(pathPng, FileMode.OpenOrCreate))
                            {
                                try { texture = Texture2D.FromStream(Core.GraphicsDevice, stream); }
                                catch (Exception)
                                {
                                    Logger.Log(Logger.LogTypes.ErrorMessage, "Something went wrong while XNA tried to load a texture. Return default.");
                                    return DefaultTexture;
                                }
                            }
                        }
                        else
                        {
                            Logger.Log(Logger.LogTypes.ErrorMessage, "Textures.vb: Texture " + Path.Combine(GameController.GamePath, cContent.RootDirectory, name) + " was not found!");
                            return DefaultTexture;
                        }
                    }
                    else
                        texture = cContent.Load<Texture2D>(tSource.TexturePath);

                    TextureList.Add(Path.Combine(cContent.RootDirectory, texturePath, name), ApplyEffect(texture.Copy()));
                }

                if (doApplyEffect)
                {
                    if (!TextureList.ContainsKey(tKey))
                        TextureList.Add(tKey, ApplyEffect(TextureRectangle(texture, tSource.TextureRectangle, resolution)));
                }
                else
                {
                    if (!TextureList.ContainsKey(tKey))
                        TextureList.Add(tKey, TextureRectangle(texture, tSource.TextureRectangle, resolution));
                }

                cContent.Unload();
            }

            return TextureList[tKey];
        }

        /// <summary>
        /// Returns the texture. The default texture path is "Textures\".
        /// </summary>
        /// <param name="name">The name of the texture.</param>
        /// <param name="r">The rectangle to get from the texture.</param>
        public static Texture2D GetTexture(string name, Rectangle r) => GetTexture(name, r, "Textures");

        public static Texture2D GetTexture(Texture2D texture, Rectangle rectangle, int factor = 1)
        {
            Texture2D tex;

            if (TextureRectList.TryGetValue(new KeyValuePair<int, Rectangle>(texture.GetHashCode(), rectangle), out tex))
                return tex;

            tex = TextureRectangle(texture, rectangle, factor);
            TextureRectList.Add(new KeyValuePair<int, Rectangle>(texture.GetHashCode(), rectangle), tex);

            return tex;
        }

        private static Texture2D TextureRectangle(Texture2D texture, Rectangle rectangle, int factor = 1)
        {
            rectangle = new Rectangle(rectangle.X * factor, rectangle.Y * factor, rectangle.Width * factor, rectangle.Height * factor);

            var tRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            if (tRectangle.Contains(rectangle) == false)
            {
                Logger.Log(Logger.LogTypes.ErrorMessage, "Textures.vb: The rectangle for a texture was out of bounds!");
                return DefaultTexture;
            }

            var data = new Color[rectangle.Width*rectangle.Height];
            texture.GetData(0, rectangle, data, 0, rectangle.Width*rectangle.Height);

            var newTex = new Texture2D(Core.GraphicsDevice, rectangle.Width, rectangle.Height);
            newTex.SetData(data);

            return newTex;
        }

        public static bool TextureExist(string name)
        {
            var cContent = ContentPackManager.GetContentManager(name, ".xnb,.png");

            var pathXnb = Path.Combine(GameController.GamePath, cContent.RootDirectory, name + ".xnb");
            var pathPng = Path.Combine(GameController.GamePath, cContent.RootDirectory, name + ".png");

            return File.Exists(pathXnb) || File.Exists(pathPng);
        }
    }
}

using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Storage;

namespace P3D.Legacy.Core.Resources.Managers
{
    public static class TextureManager
    {
        public static Texture2D DefaultTexture;
        public static void InitializeTextures() => DefaultTexture = StorageInfo.ContentFolder.GetTexture("GUI|no_texture");

        public static Dictionary<string, Texture2D> TextureList { get; } = new Dictionary<string, Texture2D>();
        public static Dictionary<KeyValuePair<int, Rectangle>, Texture2D> TextureRectList { get; } = new Dictionary<KeyValuePair<int, Rectangle>, Texture2D>();


        /// <summary>
        /// Returns a texture.
        /// </summary>
        /// <param name="path">The path to the texture.</param>
        public static Texture2D GetTexture(string path)
        {
            // -- Temp workaround
            path = path.Replace(@"\", @"|");

            var key = $"{path},FULL_IMAGE";
            if (!TextureList.ContainsKey(key))
                TextureList.Add(key, StorageInfo.ContentFolder.GetTexture(path));

            return TextureList[key];
        }

        /// <summary>
        /// Returns the texture. The default texture path is "Textures\".
        /// </summary>
        /// <param name="name">The name of the texture.</param>
        /// <param name="r">The rectangle to get from the texture.</param>
        public static Texture2D GetTexture(string name, Rectangle r) => GetTexture(name, r, "Textures");

        /// <summary>
        /// Returns a texture.
        /// </summary>
        /// <param name="name">The name of the texture.</param>
        /// <param name="r">The rectangle to get the texture from.</param>
        /// <param name="texturePath">The texture path to load a texture from.</param>
        public static Texture2D GetTexture(string name, Rectangle r, string texturePath)
        {
            // -- Temp workaround
            texturePath = texturePath.Replace(@"\", @"|");

            var path = !string.IsNullOrEmpty(texturePath) ? $"{texturePath}|{name}" : name;
            var key = path + "," + r.X + "," + r.Y + "," + r.Width + "," + r.Height;
            if (!TextureList.ContainsKey(key))
                TextureList.Add(key, TextureRectangle(GetTexture(path), r));
            return TextureList[key];
        }

        public static Texture2D GetTexture(Texture2D texture, Rectangle rectangle)
        {
            Texture2D tex;

            if (TextureRectList.TryGetValue(new KeyValuePair<int, Rectangle>(texture.GetHashCode(), rectangle), out tex))
                return tex;

            tex = TextureRectangle(texture, rectangle);
            TextureRectList.Add(new KeyValuePair<int, Rectangle>(texture.GetHashCode(), rectangle), tex);

            return tex;
        }

        private static Texture2D TextureRectangle(Texture2D texture, Rectangle rectangle)
        {
            var tRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            if (tRectangle.Contains(rectangle))
            {
                var data = new Color[rectangle.Width * rectangle.Height];
                texture.GetData(0, rectangle, data, 0, rectangle.Width * rectangle.Height);

                var newTex = new Texture2D(Core.GraphicsDevice, rectangle.Width, rectangle.Height);
                newTex.SetData(data);

                return newTex;
            }

            Logger.Log(Logger.LogTypes.ErrorMessage, "TextureManager.cs: The rectangle for a texture was out of bounds!");
            return DefaultTexture;
        }

        public static bool TextureExist(string name) => StorageInfo.ContentFolder.TextureExist(name);
    }
}

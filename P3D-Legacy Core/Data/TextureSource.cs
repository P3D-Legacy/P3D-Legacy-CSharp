using Microsoft.Xna.Framework;

namespace P3D.Legacy.Core.Data
{
    public class TextureSource
    {
        public string TexturePath { get; }
        public Rectangle TextureRectangle { get; }

        public TextureSource(string texturePath, Rectangle textureRectangle)
        {
            TexturePath = texturePath;
            TextureRectangle = textureRectangle;
        }

        public string GetString() => $"{TexturePath},{TextureRectangle.X},{TextureRectangle.Y},{TextureRectangle.Width},{TextureRectangle.Height}";
        public bool IsEqual(TextureSource textureSource) => TexturePath == textureSource.TexturePath && TextureRectangle == textureSource.TextureRectangle;
        public bool IsEqual(string texturePath, Rectangle textureRectangle) => IsEqual(new TextureSource(texturePath, textureRectangle));
    }
}
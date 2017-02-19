using Microsoft.Xna.Framework.Graphics;

namespace P3D.Legacy.Core.Data
{
    /// <summary>
    /// Class that contains a SpriteFont with its corresponding name.
    /// </summary>
    public class FontContainer
    {
        /// <summary>
        /// Returns the name of the Font.
        /// </summary>
        public string FontName { get; }

        /// <summary>
        /// The SpriteFont.
        /// </summary>
        public SpriteFont SpriteFont { get; }

        /// <summary>
        /// Creates a new instance of the FontContainer class.
        /// </summary>
        /// <param name="fontName">The name of the Font.</param>
        /// <param name="font">The SpriteFont.</param>
        public FontContainer(string fontName, SpriteFont font)
        {
            FontName = fontName;
            SpriteFont = font;

            switch (fontName.ToLower())
            {
                case "braille":
                    SpriteFont.DefaultCharacter = ' ';
                    break;
                default:
                    SpriteFont.DefaultCharacter = '?';
                    break;
            }
        }
    }
}
using System.Drawing;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace P3D.Legacy.Core.Objects
{
    /// <summary>
    /// This is the BasicObject class for each Graphics-Component.
    /// </summary>
    public abstract class BasicObject
    {
        /// <summary>
        /// The visual texture of the object.
        /// </summary>
        public Texture2D Texture { get; }

        /// <summary>
        /// Width of the object.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Height of the object.
        /// </summary>
        public int Height { get; private set; }


        /// <summary>
        /// The position if the object in the window in pixels
        /// </summary>
        public Vector2 Position { get; }

        /// <summary>
        /// A visible parameter which can be used to toggle object's visibility.
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Shows that you can dispose this object in the next Unload-Method.
        /// </summary>
        public bool DisposeReady { get; set; }

        /// <summary>
        /// The size of this object (width and height)
        /// </summary>
        public Size Size
        {
            get { return new Size(Width, Height); }
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }


        /// <summary>
        /// Creates a new instance of this basic calss.
        /// </summary>
        /// <param name="texture">The texture for this object.</param>
        /// <param name="width">The width  of this object (x-axis)</param>
        /// <param name="height">The height of this object (y-axis)</param>
        /// <param name="position">The position of this object.</param>
        public BasicObject(Texture2D texture, int width, int height, Vector2 position)
        {
            Texture = texture;
            Width = width;
            Height = height;
            Position = position;
        }

    }
}

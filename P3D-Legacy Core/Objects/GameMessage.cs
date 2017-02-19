using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace P3D.Legacy.Core.Objects
{
    /// <summary>
    /// This class can show a message ingame.
    /// </summary>
    public class GameMessage : BasicObject
    {
        public enum DockStyles
        {
            Top,
            Down,
            Left,
            Right,
            None
        }
        
        private int _alpha;

        /// <summary>
        /// The duretion in milliseconds this message will be displayed.
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        /// The backgroundcolor the texture will be colored with.
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.White;

        /// <summary>
        /// The rectangle from the texture to be drawn.
        /// </summary>
        public Rectangle TextureRectangle { get; set; }

        /// <summary>
        /// Shows the message in fullscreen (overides size and dock)
        /// </summary>
        public bool Fullscreen { get; set; }

        /// <summary>
        /// Docks the message at a side of the window (overides size)
        /// </summary>
        public DockStyles Dock { get; set; } = DockStyles.Top;

        /// <summary>
        /// The text that will be displayed.
        /// </summary>
        public string Text { get; set; } = "";

        /// <summary>
        /// The position the text is drawn (relative to message-position)
        /// </summary>
        public Vector2 TextPosition { get; set; }

        public SpriteFont SpriteFont { get; set; }

        /// <summary>
        /// The color for the text.
        /// </summary>
        public Color TextColor { get; set; } = Color.White;

        /// <summary>
        /// If this value is true, the duretion has no effect on the visibility of this message.
        /// </summary>
        public bool ShowAlways { get; set; }

        public bool AlphaBlend { get; set; } = true;


        public GameMessage(Texture2D texture, int width, int height, Vector2 position) : this(texture, new System.Drawing.Size(width, height), position) { }

        public GameMessage(Texture2D texture, System.Drawing.Size size, Vector2 position) : base(texture, size.Width, size.Height, position)
        {
            Size = size;
            Visible = false;
        }

        /// <summary>
        /// Sets the properties of the text.
        /// </summary>
        /// <param name="text">The actual text to be displayed.</param>
        /// <param name="spriteFont">The spritefont, the text will be drawn with.</param>
        /// <param name="textColor">The color the text will be drawn in.</param>
        public void SetupText(string text, SpriteFont spriteFont, Color textColor)
        {
            Text = text;
            SpriteFont = spriteFont;
            TextColor = textColor;
        }

        /// <summary>
        /// Updates the message (required!)
        /// </summary>
        public void Update()
        {
            if (ShowAlways == false)
            {
                if (Duration > Convert.ToSingle(0))
                {
                    Visible = true;

                    Duration -= Convert.ToSingle(0.1);
                    if (Duration <= Convert.ToSingle(0))
                    {
                        Duration = Convert.ToSingle(0);
                    }

                    if (AlphaBlend)
                    {
                        if (_alpha < 255)
                        {
                            _alpha += 5;
                            if (_alpha > 255)
                            {
                                _alpha = 255;
                            }
                        }
                    }
                    else
                    {
                        _alpha = 255;
                    }
                }
                else
                {
                    if (AlphaBlend)
                    {
                        if (_alpha > 0)
                        {
                            _alpha -= 5;
                            if (_alpha <= 0)
                            {
                                _alpha = 0;
                                Visible = false;
                            }
                        }
                    }
                    else
                    {
                        Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// Draw the message.
        /// </summary>
        public void Draw()
        {
            if (Visible)
            {
                if (Fullscreen)
                    DrawMe(new System.Drawing.Size(Core.GraphicsDevice.Viewport.Width, Core.GraphicsDevice.Viewport.Height), new Vector2(0, 0));
                else
                {
                    switch (Dock)
                    {
                        case DockStyles.None:
                            DrawMe(Size, Position);
                            break;
                        case DockStyles.Top:
                            DrawMe(new System.Drawing.Size(Core.ScreenSize().Width, Size.Height), new Vector2(0, 0));
                            break;
                        case DockStyles.Down:
                            DrawMe(new System.Drawing.Size(Core.ScreenSize().Width, Size.Height), new Vector2(0, Core.ScreenSize().Height - Size.Height));
                            break;
                        case DockStyles.Left:
                            DrawMe(new System.Drawing.Size(Size.Width, Core.ScreenSize().Height), new Vector2(0, 0));
                            break;
                        case DockStyles.Right:
                            DrawMe(new System.Drawing.Size(Size.Width, Core.ScreenSize().Height), new Vector2(Core.ScreenSize().Width - Size.Width, 0));
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Actual drawing stuff.
        /// </summary>
        private void DrawMe(System.Drawing.Size drawSize, Vector2 drawPosition)
        {
            Core.SpriteBatch.DrawInterface(Texture, new Rectangle(Convert.ToInt32(drawPosition.X), Convert.ToInt32(drawPosition.Y), drawSize.Width, drawSize.Height), TextureRectangle, new Color(BackgroundColor.R, BackgroundColor.G, BackgroundColor.B, _alpha));
            Core.SpriteBatch.DrawInterfaceString(SpriteFont, Text, LinkVector2(Position, TextPosition, 1), new Color(TextColor.R, TextColor.G, TextColor.B, _alpha));
        }

        private Vector2 LinkVector2(Vector2 vector1, Vector2 vector2, float factor) => new Vector2(vector1.X + factor * vector2.X, vector1.Y + factor * vector2.Y);

        /// <summary>
        /// Display the message on the screen.
        /// </summary>
        /// <param name="duration">The time span the message will appear on the screen.</param>
        /// <param name="graphics">The graphics device the message will be drawn on.</param>
        public void ShowMessage(float duration, GraphicsDevice graphics)
        {
            Duration = duration;
            Visible = true;

            _alpha = 0;
        }

        public void ShowMessage(string text, float duration, SpriteFont spriteFont, Color textColor)
        {
            Text = text;
            SpriteFont = spriteFont;
            TextColor = textColor;
            Duration = duration;
            Visible = true;

            _alpha = 0;
        }

        public void HideMessage()
        {
            Visible = false;
            Duration = 0f;

            _alpha = 0;
        }
    }
}

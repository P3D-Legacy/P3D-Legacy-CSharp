using System;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace P3D.Legacy.Core
{
    /// <summary>
    /// A wrapper class for the default SpriteBatch class which extends certain functions of the original class to allow easy screen sizing.
    /// </summary>
    public class CoreSpriteBatch : SpriteBatch
    {

        /// <summary>
        /// Creates a new instance of the CoreSpriteBatch class.
        /// </summary>
        /// <param name="graphicsDevice">The GraphicsDevice where sprites will be drawn.</param>
        public CoreSpriteBatch(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {

            SetupCanvas();
        }

        #region "StateManagement"


        private bool _running;
        /// <summary>
        /// Begins the SpriteBatch.
        /// </summary>
        public void BeginBatch()
        {
            Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
            _running = true;
        }

        /// <summary>
        /// Ends the SpriteBatch.
        /// </summary>
        public void EndBatch()
        {
            End();
            _running = false;
        }

        /// <summary>
        /// If the SpriteBatch is running.
        /// </summary>
        public bool Running
        {
            get { return _running; }
        }

        #endregion

        #region "Draw"

        /// <summary>
        /// Adds a sprite to a batch of sprites for rendering using the specified texture, destination rectangle, and color. 
        /// </summary>
        /// <param name="Texture">A texture.</param>
        /// <param name="DestinationRectangle">A rectangle that specifies (in screen coordinates) the destination for drawing the sprite.</param>
        /// <param name="Color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        public void DrawInterface(Texture2D Texture, Rectangle DestinationRectangle, Color Color)
        {
            DrawInterface(Texture, DestinationRectangle, null, Color, 0f, Vector2.Zero, SpriteEffects.None, 0f, true);
        }

        /// <summary>
        /// Adds a sprite to a batch of sprites for rendering using the specified texture, destination rectangle, source rectangle, and color. 
        /// </summary>
        /// <param name="Texture">A texture.</param>
        /// <param name="DestinationRectangle">A rectangle that specifies (in screen coordinates) the destination for drawing the sprite.</param>
        /// <param name="SourceRectangle">A rectangle that specifies (in texels) the source texels from a texture. Use null to draw the entire texture. </param>
        /// <param name="Color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        public void DrawInterface(Texture2D Texture, Rectangle DestinationRectangle, Rectangle? SourceRectangle, Color Color)
        {
            DrawInterface(Texture, DestinationRectangle, SourceRectangle, Color, 0f, Vector2.Zero, SpriteEffects.None, 0f, true);
        }

        /// <summary>
        /// Adds a sprite to a batch of sprites for rendering using the specified texture, destination rectangle, source rectangle, color, rotation, origin, effects and layer. 
        /// </summary>
        /// <param name="Texture">A texture.</param>
        /// <param name="DestinationRectangle">A rectangle that specifies (in screen coordinates) the destination for drawing the sprite.</param>
        /// <param name="SourceRectangle">A rectangle that specifies (in texels) the source texels from a texture. Use null to draw the entire texture. </param>
        /// <param name="Color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        /// <param name="Rotation">Specifies the angle (in radians) to rotate the sprite about its center.</param>
        /// <param name="Origin">The sprite origin; the default is (0,0) which represents the upper-left corner.</param>
        /// <param name="Effects">Effects to apply.</param>
        /// <param name="LayerDepth">The depth of a layer. By default, 0 represents the front layer and 1 represents a back layer. Use SpriteSortMode if you want sprites to be sorted during drawing.</param>
        public void DrawInterface(Texture2D Texture, Rectangle DestinationRectangle, Rectangle? SourceRectangle, Color Color, float Rotation, Vector2 Origin, SpriteEffects Effects, float LayerDepth)
        {
            DrawInterface(Texture, DestinationRectangle, SourceRectangle, Color, Rotation, Origin, Effects, LayerDepth, true);
        }

        /// <summary>
        /// Adds a sprite to a batch of sprites for rendering using the specified texture, destination rectangle, source rectangle, color, rotation, origin, effects and layer. 
        /// </summary>
        /// <param name="Texture">A texture.</param>
        /// <param name="DestinationRectangle">A rectangle that specifies (in screen coordinates) the destination for drawing the sprite.</param>
        /// <param name="SourceRectangle">A rectangle that specifies (in texels) the source texels from a texture. Use null to draw the entire texture. </param>
        /// <param name="Color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        /// <param name="Rotation">Specifies the angle (in radians) to rotate the sprite about its center.</param>
        /// <param name="Origin">The sprite origin; the default is (0,0) which represents the upper-left corner.</param>
        /// <param name="Effects">Effects to apply.</param>
        /// <param name="LayerDepth">The depth of a layer. By default, 0 represents the front layer and 1 represents a back layer. Use SpriteSortMode if you want sprites to be sorted during drawing.</param>
        /// <param name="TransformPosition">If the position of the sprite should be transformed by screen sizing.</param>
        public void DrawInterface(Texture2D Texture, Rectangle DestinationRectangle, Rectangle? SourceRectangle, Color Color, float Rotation, Vector2 Origin, SpriteEffects Effects, float LayerDepth, bool TransformPosition)
        {
            //Do the conversion of the rectangle here.

            double x = InterfaceScale;
            if (TransformPosition)
            {
                DestinationRectangle = new Rectangle(Convert.ToInt32(DestinationRectangle.X * x), Convert.ToInt32(DestinationRectangle.Y * x), Convert.ToInt32(DestinationRectangle.Width * x), Convert.ToInt32(DestinationRectangle.Height * x));
            }
            else
            {
                DestinationRectangle = new Rectangle(Convert.ToInt32(DestinationRectangle.X * x), Convert.ToInt32(DestinationRectangle.Y * x), Convert.ToInt32(DestinationRectangle.Width), Convert.ToInt32(DestinationRectangle.Height));
            }

            Draw(Texture, DestinationRectangle, SourceRectangle, Color, Rotation, Origin, Effects, LayerDepth);
        }

        /// <summary>
        /// Adds a sprite to a batch of sprites for rendering using the specified texture, position and color. 
        /// </summary>
        /// <param name="Texture">A texture.</param>
        /// <param name="Position">The location (in screen coordinates) to draw the sprite.</param>
        /// <param name="Color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        public void DrawInterface(Texture2D Texture, Vector2 Position, Color Color)
        {
            DrawInterface(Texture, Position, null, Color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f, true);
        }

        /// <summary>
        /// Adds a sprite to a batch of sprites for rendering using the specified texture, position, source rectangle, and color. 
        /// </summary>
        /// <param name="Texture">A texture.</param>
        /// <param name="Position">The location (in screen coordinates) to draw the sprite.</param>
        /// <param name="SourceRectangle">A rectangle that specifies (in texels) the source texels from a texture. Use null to draw the entire texture. </param>
        /// <param name="Color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        public void DrawInterface(Texture2D Texture, Vector2 Position, Rectangle? SourceRectangle, Color Color)
        {
            DrawInterface(Texture, Position, SourceRectangle, Color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f, true);
        }

        /// <summary>
        /// Adds a sprite to a batch of sprites for rendering using the specified texture, position, source rectangle, color, rotation, origin, scale, effects, and layer. 
        /// </summary>
        /// <param name="Texture">A texture.</param>
        /// <param name="Position">The location (in screen coordinates) to draw the sprite.</param>
        /// <param name="SourceRectangle">A rectangle that specifies (in texels) the source texels from a texture. Use null to draw the entire texture. </param>
        /// <param name="Color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        /// <param name="Rotation">Specifies the angle (in radians) to rotate the sprite about its center.</param>
        /// <param name="Origin">The sprite origin; the default is (0,0) which represents the upper-left corner.</param>
        /// <param name="Scale">Scale factor.</param>
        /// <param name="Effects">Effects to apply.</param>
        /// <param name="LayerDepth">The depth of a layer. By default, 0 represents the front layer and 1 represents a back layer. Use SpriteSortMode if you want sprites to be sorted during drawing.</param>
        public void DrawInterface(Texture2D Texture, Vector2 Position, Rectangle? SourceRectangle, Color Color, float Rotation, Vector2 Origin, float Scale, SpriteEffects Effects, float LayerDepth)
        {
            DrawInterface(Texture, Position, SourceRectangle, Color, Rotation, Origin, new Vector2(Scale), Effects, LayerDepth, true);
        }

        /// <summary>
        /// Adds a sprite to a batch of sprites for rendering using the specified texture, position, source rectangle, color, rotation, origin, scale, effects, and layer. 
        /// </summary>
        /// <param name="Texture">A texture.</param>
        /// <param name="Position">The location (in screen coordinates) to draw the sprite.</param>
        /// <param name="SourceRectangle">A rectangle that specifies (in texels) the source texels from a texture. Use null to draw the entire texture. </param>
        /// <param name="Color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        /// <param name="Rotation">Specifies the angle (in radians) to rotate the sprite about its center.</param>
        /// <param name="Origin">The sprite origin; the default is (0,0) which represents the upper-left corner.</param>
        /// <param name="Scale">Scale factor.</param>
        /// <param name="Effects">Effects to apply.</param>
        /// <param name="LayerDepth">The depth of a layer. By default, 0 represents the front layer and 1 represents a back layer. Use SpriteSortMode if you want sprites to be sorted during drawing.</param>
        public void DrawInterface(Texture2D Texture, Vector2 Position, Rectangle? SourceRectangle, Color Color, float Rotation, Vector2 Origin, Vector2 Scale, SpriteEffects Effects, float LayerDepth)
        {
            DrawInterface(Texture, Position, SourceRectangle, Color, Rotation, Origin, Scale, Effects, LayerDepth, true);
        }

        /// <summary>
        /// Adds a sprite to a batch of sprites for rendering using the specified texture, position, source rectangle, color, rotation, origin, scale, effects, and layer. 
        /// </summary>
        /// <param name="Texture">A texture.</param>
        /// <param name="Position">The location (in screen coordinates) to draw the sprite.</param>
        /// <param name="SourceRectangle">A rectangle that specifies (in texels) the source texels from a texture. Use null to draw the entire texture. </param>
        /// <param name="Color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        /// <param name="Rotation">Specifies the angle (in radians) to rotate the sprite about its center.</param>
        /// <param name="Origin">The sprite origin; the default is (0,0) which represents the upper-left corner.</param>
        /// <param name="Scale">Scale factor.</param>
        /// <param name="Effects">Effects to apply.</param>
        /// <param name="LayerDepth">The depth of a layer. By default, 0 represents the front layer and 1 represents a back layer. Use SpriteSortMode if you want sprites to be sorted during drawing.</param>
        /// <param name="TransformPosition">If the position of the sprite should be transformed by screen sizing.</param>
        public void DrawInterface(Texture2D Texture, Vector2 Position, Rectangle? SourceRectangle, Color Color, float Rotation, Vector2 Origin, Vector2 Scale, SpriteEffects Effects, float LayerDepth, bool TransformPosition)
        {
            //Do the conversion of the rectangle and scale here.

            double x = InterfaceScale;
            if (TransformPosition)
            {
                Position = new Vector2(Convert.ToSingle(Position.X * x), Convert.ToSingle(Position.Y * x));
            }
            Scale = new Vector2(Convert.ToSingle(Scale.X * x), Convert.ToSingle(Scale.Y * x));

            Draw(Texture, Position, SourceRectangle, Color, Rotation, Origin, Scale, Effects, LayerDepth);
        }

        #endregion

        #region "DrawString"

        /// <summary>
        /// Adds a string to a batch of sprites for rendering using the specified font, text, position, and color. 
        /// </summary>
        /// <param name="Font">A font for diplaying text.</param>
        /// <param name="Text">A text string.</param>
        /// <param name="Position">The location (in screen coordinates) to draw the sprite.</param>
        /// <param name="Color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        public void DrawInterfaceString(SpriteFont Font, string Text, Vector2 Position, Color Color)
        {
            DrawInterfaceString(Font, new StringBuilder(Text), Position, Color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f, true);
        }

        /// <summary>
        /// Adds a string to a batch of sprites for rendering using the specified font, text, position, color, rotation, origin, scale, effects and layer. 
        /// </summary>
        /// <param name="Font">A font for diplaying text.</param>
        /// <param name="Text">A text string.</param>
        /// <param name="Position">The location (in screen coordinates) to draw the sprite.</param>
        /// <param name="Color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        /// <param name="Rotation">Specifies the angle (in radians) to rotate the sprite about its center.</param>
        /// <param name="Origin">The sprite origin; the default is (0,0) which represents the upper-left corner.</param>
        /// <param name="Scale">Scale factor.</param>
        /// <param name="Effects">Effects to apply.</param>
        /// <param name="LayerDepth">The depth of a layer. By default, 0 represents the front layer and 1 represents a back layer. Use SpriteSortMode if you want sprites to be sorted during drawing.</param>
        public void DrawInterfaceString(SpriteFont Font, string Text, Vector2 Position, Color Color, float Rotation, Vector2 Origin, float Scale, SpriteEffects Effects, float LayerDepth)
        {
            DrawInterfaceString(Font, new StringBuilder(Text), Position, Color, Rotation, Origin, new Vector2(Scale), Effects, LayerDepth, true);
        }

        /// <summary>
        /// Adds a string to a batch of sprites for rendering using the specified font, text, position, color, rotation, origin, scale, effects and layer. 
        /// </summary>
        /// <param name="Font">A font for diplaying text.</param>
        /// <param name="Text">A text string.</param>
        /// <param name="Position">The location (in screen coordinates) to draw the sprite.</param>
        /// <param name="Color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        /// <param name="Rotation">Specifies the angle (in radians) to rotate the sprite about its center.</param>
        /// <param name="Origin">The sprite origin; the default is (0,0) which represents the upper-left corner.</param>
        /// <param name="Scale">Scale factor.</param>
        /// <param name="Effects">Effects to apply.</param>
        /// <param name="LayerDepth">The depth of a layer. By default, 0 represents the front layer and 1 represents a back layer. Use SpriteSortMode if you want sprites to be sorted during drawing.</param>
        public void DrawInterfaceString(SpriteFont Font, string Text, Vector2 Position, Color Color, float Rotation, Vector2 Origin, Vector2 Scale, SpriteEffects Effects, float LayerDepth)
        {
            DrawInterfaceString(Font, new StringBuilder(Text), Position, Color, Rotation, Origin, Scale, Effects, LayerDepth, true);
        }

        /// <summary>
        /// Adds a string to a batch of sprites for rendering using the specified font, text, position, and color. 
        /// </summary>
        /// <param name="Font">A font for diplaying text.</param>
        /// <param name="Text">A text string.</param>
        /// <param name="Position">The location (in screen coordinates) to draw the sprite.</param>
        /// <param name="Color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        public void DrawInterfaceString(SpriteFont Font, StringBuilder Text, Vector2 Position, Color Color)
        {
            DrawInterfaceString(Font, Text, Position, Color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f, true);
        }

        /// <summary>
        /// Adds a string to a batch of sprites for rendering using the specified font, text, position, color, rotation, origin, scale, effects and layer. 
        /// </summary>
        /// <param name="Font">A font for diplaying text.</param>
        /// <param name="Text">A text string.</param>
        /// <param name="Position">The location (in screen coordinates) to draw the sprite.</param>
        /// <param name="Color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        /// <param name="Rotation">Specifies the angle (in radians) to rotate the sprite about its center.</param>
        /// <param name="Origin">The sprite origin; the default is (0,0) which represents the upper-left corner.</param>
        /// <param name="Scale">Scale factor.</param>
        /// <param name="Effects">Effects to apply.</param>
        /// <param name="LayerDepth">The depth of a layer. By default, 0 represents the front layer and 1 represents a back layer. Use SpriteSortMode if you want sprites to be sorted during drawing.</param>
        public void DrawInterfaceString(SpriteFont Font, StringBuilder Text, Vector2 Position, Color Color, float Rotation, Vector2 Origin, float Scale, SpriteEffects Effects, float LayerDepth)
        {
            DrawInterfaceString(Font, Text, Position, Color, Rotation, Origin, new Vector2(Scale), Effects, LayerDepth, true);
        }

        /// <summary>
        /// Adds a string to a batch of sprites for rendering using the specified font, text, position, color, rotation, origin, scale, effects and layer. 
        /// </summary>
        /// <param name="Font">A font for diplaying text.</param>
        /// <param name="Text">A text string.</param>
        /// <param name="Position">The location (in screen coordinates) to draw the sprite.</param>
        /// <param name="Color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        /// <param name="Rotation">Specifies the angle (in radians) to rotate the sprite about its center.</param>
        /// <param name="Origin">The sprite origin; the default is (0,0) which represents the upper-left corner.</param>
        /// <param name="Scale">Scale factor.</param>
        /// <param name="Effects">Effects to apply.</param>
        /// <param name="LayerDepth">The depth of a layer. By default, 0 represents the front layer and 1 represents a back layer. Use SpriteSortMode if you want sprites to be sorted during drawing.</param>
        public void DrawInterfaceString(SpriteFont Font, StringBuilder Text, Vector2 Position, Color Color, float Rotation, Vector2 Origin, Vector2 Scale, SpriteEffects Effects, float LayerDepth)
        {
            DrawInterfaceString(Font, Text, Position, Color, Rotation, Origin, Scale, Effects, LayerDepth, true);
        }

        /// <summary>
        /// Adds a string to a batch of sprites for rendering using the specified font, text, position, color, rotation, origin, scale, effects and layer. 
        /// </summary>
        /// <param name="Font">A font for diplaying text.</param>
        /// <param name="Text">A text string.</param>
        /// <param name="Position">The location (in screen coordinates) to draw the sprite.</param>
        /// <param name="Color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        /// <param name="Rotation">Specifies the angle (in radians) to rotate the sprite about its center.</param>
        /// <param name="Origin">The sprite origin; the default is (0,0) which represents the upper-left corner.</param>
        /// <param name="Scale">Scale factor.</param>
        /// <param name="Effects">Effects to apply.</param>
        /// <param name="LayerDepth">The depth of a layer. By default, 0 represents the front layer and 1 represents a back layer. Use SpriteSortMode if you want sprites to be sorted during drawing.</param>
        /// <param name="TransformPosition">If the position of the text should be transformed by screen sizing.</param>
        public void DrawInterfaceString(SpriteFont Font, StringBuilder Text, Vector2 Position, Color Color, float Rotation, Vector2 Origin, Vector2 Scale, SpriteEffects Effects, float LayerDepth, bool TransformPosition)
        {
            double x = InterfaceScale;
            if (TransformPosition)
            {
                Position = new Vector2(Convert.ToSingle(Position.X * x), Convert.ToSingle(Position.Y * x));
            }
            Scale = new Vector2(Convert.ToSingle(Scale.X * x), Convert.ToSingle(Scale.Y * x));

            DrawString(Font, Text, Position, Color, Rotation, Origin, Scale, Effects, LayerDepth);
        }

        #endregion

        /// <summary>
        /// The scale of the interface depending on the window size.
        /// </summary>
        public double InterfaceScale => Core.WindowSize.Height < Core.CurrentScreen.GetScreenScaleMinimum().Height || Core.WindowSize.Width < Core.CurrentScreen.GetScreenScaleMinimum().Width ? 0.5d : 1d;

        #region "Canvas"


        private Texture2D _canvasTexture;
        /// <summary>
        /// Creates the canvas texture.
        /// </summary>
        private void SetupCanvas()
        {
            _canvasTexture = new Texture2D(GraphicsDevice, 1, 1);
            _canvasTexture.SetData(new [] { Color.White });
        }

        #region "DrawRectangle"

        /// <summary>
        /// Adds a Rectangle sized sprite to a batch of sprites for rendering using the specified rectangle and color.
        /// </summary>
        /// <param name="DestinationRectangle">A rectangle.</param>
        /// <param name="Color">The color of the rectangle.</param>
        public void DrawRectangle(Rectangle DestinationRectangle, Color Color)
        {
            DrawRectangle(DestinationRectangle, Color, 0f, Vector2.Zero, 0f, false);
        }

        /// <summary>
        /// Adds a Rectangle sized sprite to a batch of sprites for rendering using the specified rectangle, color, rotation, origin and layer.
        /// </summary>
        /// <param name="DestinationRectangle">A rectangle.</param>
        /// <param name="Color">The color of the rectangle.</param>
        /// <param name="Rotation">Specifies the angle (in radians) to rotate the rectangle about its center.</param>
        /// <param name="Origin">The rectangle origin; the default is (0,0) which represents the upper-left corner.</param>
        /// <param name="LayerDepth">The depth of a layer. By default, 0 represents the front layer and 1 represents a back layer. Use SpriteSortMode if you want sprites to be sorted during drawing.</param>
        public void DrawRectangle(Rectangle DestinationRectangle, Color Color, float Rotation, Vector2 Origin, float LayerDepth)
        {
            DrawRectangle(DestinationRectangle, Color, Rotation, Origin, LayerDepth, false);
        }

        /// <summary>
        /// Adds a Rectangle sized sprite to a batch of sprites for rendering using the specified rectangle, color, rotation, origin and layer.
        /// </summary>
        /// <param name="DestinationRectangle">A rectangle.</param>
        /// <param name="Color">The color of the rectangle.</param>
        /// <param name="Rotation">Specifies the angle (in radians) to rotate the rectangle about its center.</param>
        /// <param name="Origin">The rectangle origin; the default is (0,0) which represents the upper-left corner.</param>
        /// <param name="LayerDepth">The depth of a layer. By default, 0 represents the front layer and 1 represents a back layer. Use SpriteSortMode if you want sprites to be sorted during drawing.</param>
        /// <param name="ScaleToScreen">If the rectangle should get scaled by screen sizing.</param>
        public void DrawRectangle(Rectangle DestinationRectangle, Color Color, float Rotation, Vector2 Origin, float LayerDepth, bool ScaleToScreen)
        {
            if (ScaleToScreen)
            {
                Core.SpriteBatch.DrawInterface(_canvasTexture, DestinationRectangle, null, Color, Rotation, Origin, SpriteEffects.None, LayerDepth, true);
            }
            else
            {
                Core.SpriteBatch.Draw(_canvasTexture, DestinationRectangle, null, Color, Rotation, Origin, SpriteEffects.None, LayerDepth);
            }
        }

        #endregion

        #region "DrawLine"

        //TODO: FInish line drawing stuff

        //1: Color, Position, Width, Height
        //2: Color, StartingPosition, EndingPosition, Thickness

        public void DrawLine(Vector2 Position, float Width, float Height, Color Color)
        {
            float thickness = 0f;
            float length = 0f;

            Vector2 startPos = new Vector2(Position.X, Position.Y);
            Vector2 endPos = new Vector2(Position.X + Width, Position.Y);

            if (Width > Height)
            {

                if (Width > 0)
                {

                }
            }
        }

        public void DrawLine(Vector2 StartingPosition, Vector2 EndingPosition, float Thickness, Color Color)
        {
            double angle = Convert.ToDouble(Math.Atan2(EndingPosition.Y - StartingPosition.Y, EndingPosition.X - StartingPosition.X));
            double length = Vector2.Distance(StartingPosition, EndingPosition);

            Core.SpriteBatch.Draw(_canvasTexture, StartingPosition, null, Color, Convert.ToSingle(angle), Vector2.Zero, new Vector2(Convert.ToSingle(length), Convert.ToSingle(Thickness)), SpriteEffects.None, 0);
        }

        #endregion

        #endregion

    }
}

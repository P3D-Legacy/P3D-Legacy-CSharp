using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace P3D.Legacy.Core.Screens.GUI
{
    public static class Canvas
    {
        private static Texture2D _canvas;

        public static void SetupCanvas()
        {
            Color[] tempData = new Color[1];
            tempData[0] = Color.White;

            _canvas = new Texture2D(Core.GraphicsDevice, 1, 1);
            _canvas.SetData(tempData);
        }

        public static void DrawRectangle(Rectangle rectangle, Color color)
        {
            Core.SpriteBatch.Draw(_canvas, rectangle, color);
        }

        public static void DrawRectangle(Rectangle rectangle, Color color, bool scaleToScreen)
        {
            if (scaleToScreen)
                Core.SpriteBatch.DrawInterface(_canvas, rectangle, color);
            else
                Core.SpriteBatch.Draw(_canvas, rectangle, color);
        }

        #region "Borders"

        public static void DrawBorder(int borderLength, Rectangle rectangle, Color color)
        {
            DrawBorder(borderLength, rectangle, color, false);
        }

        public static void DrawBorder(int borderLength, Rectangle rectangle, Color color, bool scaleToScreen)
        {
            DrawRectangle(new Rectangle(rectangle.X + borderLength, rectangle.Y, rectangle.Width - borderLength, borderLength), color, scaleToScreen);
            DrawRectangle(new Rectangle(rectangle.X + rectangle.Width - borderLength, rectangle.Y + borderLength, borderLength, rectangle.Height - borderLength), color, scaleToScreen);
            DrawRectangle(new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - borderLength, rectangle.Width - borderLength, borderLength), color, scaleToScreen);
            DrawRectangle(new Rectangle(rectangle.X, rectangle.Y, borderLength, rectangle.Height - borderLength), color, scaleToScreen);
        }

        public static void DrawImageBorder(Texture2D texture, int sizeMulitiplier, Rectangle rectangle, Color color, bool scaleToScreen)
        {
            Vector2 borderSize = new Vector2(rectangle.Width, rectangle.Height);

            for (var x = 0;
                x <= borderSize.X;
                x += Convert.ToInt32(Math.Floor((double) (texture.Width / 3))) * sizeMulitiplier)
            {

                for (var y = 0;
                    y <= borderSize.Y;
                    y += Convert.ToInt32(Math.Floor((double) (texture.Height / 3))) * sizeMulitiplier)
                {
                    int width = Convert.ToInt32(Math.Floor((double) (texture.Width / 3)));
                    int height = Convert.ToInt32(Math.Floor((double) (texture.Height / 3)));

                    Rectangle tile = new Rectangle(width, height, width, height);
                    if (x == 0 && y == 0)
                    {
                        tile = new Rectangle(0, 0, width, height);
                    }
                    else if (x == borderSize.X && y == 0)
                    {
                        tile = new Rectangle(width * 2, 0, width, height);
                    }
                    else if (x == 0 && y == borderSize.Y)
                    {
                        tile = new Rectangle(0, height * 2, width, height);
                    }
                    else if (x == borderSize.X && y == borderSize.Y)
                    {
                        tile = new Rectangle(width * 2, height * 2, width, height);
                    }
                    else if (x == 0)
                    {
                        tile = new Rectangle(0, height, width, height);
                    }
                    else if (y == 0)
                    {
                        tile = new Rectangle(width, 0, width, height);
                    }
                    else if (x == borderSize.X)
                    {
                        tile = new Rectangle(width * 2, height, width, height);
                    }
                    else if (y == borderSize.Y)
                    {
                        tile = new Rectangle(width, height * 2, width, height);
                    }

                    if (scaleToScreen)
                    {
                        Core.SpriteBatch.DrawInterface(texture, new Rectangle(Convert.ToInt32(Math.Floor((double) x)) + rectangle.X,
                                Convert.ToInt32(Math.Floor((double) y)) + rectangle.Y, sizeMulitiplier * width, sizeMulitiplier * height), tile, Color.White);
                    }
                    else
                    {
                        Core.SpriteBatch.Draw(texture, new Rectangle(Convert.ToInt32(Math.Floor((double) x)) + rectangle.X,
                                Convert.ToInt32(Math.Floor((double) y)) + rectangle.Y, sizeMulitiplier * width, sizeMulitiplier * height), tile, Color.White);
                    }
                }
            }
        }

        public static void DrawImageBorder(Texture2D texture, int sizeMulitiplier, Rectangle rectangle)
        {
            DrawImageBorder(texture, sizeMulitiplier, rectangle, Color.White, false);
        }

        public static void DrawImageBorder(Texture2D texture, int sizeMulitiplier, Rectangle rectangle, bool scaleToScreen)
        {
            DrawImageBorder(texture, sizeMulitiplier, rectangle, Color.White, scaleToScreen);
        }

        #endregion

        #region "ScrollBars"

        public static void DrawScrollBar(Vector2 position, int allItems, int seeAbleItems, int selection, System.Drawing.Size size, bool horizontal, Color color1, Color color2)
        {
            DrawScrollBar(position, allItems, seeAbleItems, selection, size, horizontal, color1, color2, false);
        }

        public static void DrawScrollBar(Vector2 position, int allItems, int seeAbleItems, int selection, System.Drawing.Size size, bool horizontal, Color color1, Color color2, bool scaleToScreen)
        {
            DrawRectangle(new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), size.Width, size.Height), color1, scaleToScreen);
            int canSee = seeAbleItems;
            if (horizontal == false)
            {
                int sizeY = size.Height;
                int posY = 0;
                if (allItems > seeAbleItems)
                {
                    sizeY = Convert.ToInt32((canSee / allItems) * size.Height);
                    posY = Convert.ToInt32(Math.Abs(selection) * size.Height / allItems);
                }
                DrawRectangle(new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y) + posY, size.Width, sizeY), color2, scaleToScreen);
            }
            else
            {
                int sizeX = size.Width;
                int posX = 0;
                if (allItems > seeAbleItems)
                {
                    sizeX = Convert.ToInt32((canSee * size.Width) / allItems);
                    posX = Convert.ToInt32(Math.Abs(selection) * size.Width / allItems);
                }
                DrawRectangle(new Rectangle(Convert.ToInt32(position.X) + posX, Convert.ToInt32(position.Y), sizeX, size.Height),
                    color2, scaleToScreen);
            }
        }

        public static void DrawScrollBar(Vector2 position, int allItems, int seeAbleItems, int selection,
            System.Drawing.Size size, bool horizontal, Texture2D texture1, Texture2D texture2)
        {
            DrawScrollBar(position, allItems, seeAbleItems, selection, size, horizontal, texture1, texture2, false);
        }

        public static void DrawScrollBar(Vector2 position, int allItems, int seeAbleItems, int selection,
            System.Drawing.Size size, bool horizontal, Texture2D texture1, Texture2D texture2, bool scaleToScreen)
        {
            if (scaleToScreen)
            {
                Core.SpriteBatch.DrawInterface(texture1,
                    new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), size.Width, size.Height),
                    Color.White);
            }
            else
            {
                Core.SpriteBatch.Draw(texture1,
                    new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), size.Width, size.Height),
                    Color.White);
            }
            int canSee = seeAbleItems;
            if (horizontal == false)
            {
                int sizeY = size.Height;
                int posY = 0;
                if (allItems > seeAbleItems)
                {
                    sizeY = Convert.ToInt32((canSee / allItems) * size.Height);
                    posY = Convert.ToInt32(Math.Abs(selection) * size.Height / allItems);
                }
                if (scaleToScreen)
                {
                    Core.SpriteBatch.DrawInterface(texture2,
                        new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y) + posY, size.Width, sizeY),
                        Color.White);
                }
                else
                {
                    Core.SpriteBatch.Draw(texture2,
                        new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y) + posY, size.Width, sizeY),
                        Color.White);
                }
            }
            else
            {
                int sizeX = size.Width;
                int posX = 0;
                if (allItems > seeAbleItems)
                {
                    sizeX = Convert.ToInt32((canSee / allItems) * size.Width);
                    posX = Convert.ToInt32(Math.Abs(selection) * size.Width / allItems);
                }
                if (scaleToScreen)
                {
                    Core.SpriteBatch.DrawInterface(texture2,
                        new Rectangle(Convert.ToInt32(position.X) + posX, Convert.ToInt32(position.Y), sizeX,
                            size.Height), Color.White);
                }
                else
                {
                    Core.SpriteBatch.Draw(texture2,
                        new Rectangle(Convert.ToInt32(position.X) + posX, Convert.ToInt32(position.Y), sizeX,
                            size.Height), Color.White);
                }
            }
        }

        #endregion

        private struct GradientConfiguration
        {
            //Stores the generated texture 
            private Texture2D _texture;

            private int _width;
            private int _height;
            private Color _fromColor;
            private Color _toColor;
            private bool _horizontal;

            private int _steps;

            public GradientConfiguration(int width, int height, Color fromColor, Color toColor, bool horizontal,
                int steps)
            {
                _width = width;
                _height = height;
                _fromColor = fromColor;
                _toColor = toColor;
                _horizontal = horizontal;
                _steps = steps;
                _texture = null;
                GenerateTexture();
            }

            private void GenerateTexture()
            {
                int uSize = _height;
                int vSize = _width;
                if (_horizontal == false)
                {
                    uSize = _width;
                    vSize = _height;
                }

                int diffR = Convert.ToInt32(_toColor.R) - Convert.ToInt32(_fromColor.R);
                int diffG = Convert.ToInt32(_toColor.G) - Convert.ToInt32(_fromColor.G);
                int diffB = Convert.ToInt32(_toColor.B) - Convert.ToInt32(_fromColor.B);
                int diffA = Convert.ToInt32(_toColor.A) - Convert.ToInt32(_fromColor.A);

                int stepCount = _steps;
                if (stepCount < 0)
                {
                    stepCount = uSize;
                }

                float stepSize = Convert.ToSingle(Math.Ceiling(Convert.ToSingle(uSize / stepCount)));

                Color[] colorArray = new Color[_width * _height];

                for (var cStep = 1; cStep <= stepCount; cStep++)
                {
                    int cR = Convert.ToInt32((diffR / stepCount) * cStep) + _fromColor.R;
                    int cG = Convert.ToInt32((diffG / stepCount) * cStep) + _fromColor.G;
                    int cB = Convert.ToInt32((diffB / stepCount) * cStep) + _fromColor.B;
                    int cA = Convert.ToInt32((diffA / stepCount) * cStep) + _fromColor.A;

                    if (cR < 0)
                    {
                        cR = 255 + cR;
                    }
                    if (cG < 0)
                    {
                        cG = 255 + cG;
                    }
                    if (cB < 0)
                    {
                        cB = 255 + cB;
                    }
                    if (cA < 0)
                    {
                        cA = 255 + cA;
                    }

                    //left to right gradient
                    if (_horizontal)
                    {
                        Color c = new Color(cR, cG, cB, cA);

                        int length = Convert.ToInt32(Math.Ceiling(stepSize));
                        int start = Convert.ToInt32((cStep - 1) * stepSize);

                        for (var y = start; y <= start + length - 1; y++)
                        {
                            for (var x = 0; x <= _width - 1; x++)
                            {
                                int i = x + y * _width;
                                colorArray[i] = c;
                            }
                        }
                    }
                    else
                    {
                        Color c = new Color(cR, cG, cB, cA);

                        int length = Convert.ToInt32(Math.Ceiling(stepSize));
                        int start = Convert.ToInt32((cStep - 1) * stepSize);

                        for (var x = start; x <= start + length - 1; x++)
                        {
                            for (var y = 0; y <= _height - 1; y++)
                            {
                                int i = x + y * _width;
                                colorArray[i] = c;
                            }
                        }
                    }
                }

                _texture = new Texture2D(Core.GraphicsDevice, _width, _height);
                _texture.SetData(colorArray);

            }

            public bool IsConfig(int width, int height, Color fromColor, Color toColor, bool horizontal, int steps)
                =>
                (_width == width && _height == height && _fromColor == fromColor && _toColor == toColor &&
                 _horizontal == horizontal && _steps == steps);

            public void Draw(Rectangle r)
            {
                Core.SpriteBatch.Draw(_texture, r, Color.White);
            }


        }


        static List<GradientConfiguration> _gradientConfigs = new List<GradientConfiguration>();

        public static void DrawGradient(Rectangle rectangle, Color fromColor, Color toColor, bool horizontal, int steps)
        {
            horizontal = !horizontal;
            //because fuck you.

            GradientConfiguration gConfig = new GradientConfiguration();
            bool foundConfig = false;
            foreach (GradientConfiguration g in _gradientConfigs)
            {
                if (g.IsConfig(rectangle.Width, rectangle.Height, fromColor, toColor, horizontal, steps))
                {
                    gConfig = g;
                    foundConfig = true;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            if (foundConfig == false)
            {
                gConfig = new GradientConfiguration(rectangle.Width, rectangle.Height, fromColor, toColor, horizontal,
                    steps);
                _gradientConfigs.Add(gConfig);
            }
            gConfig.Draw(rectangle);
        }

        public static void DrawLine(Color color, Vector2 startPoint, Vector2 endPoint, double width)
        {
            double angle = Convert.ToDouble(Math.Atan2(endPoint.Y - startPoint.Y, endPoint.X - startPoint.X));
            double length = Vector2.Distance(startPoint, endPoint);

            Core.SpriteBatch.Draw(_canvas, startPoint, null, color, Convert.ToSingle(angle), Vector2.Zero,
                new Vector2(Convert.ToSingle(length), Convert.ToSingle(width)), SpriteEffects.None, 0);
        }

    }
}

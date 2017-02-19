using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace P3D.Legacy.Core.Extensions
{
    public static class Texture2DExtensions
    {
        public static Texture2D Copy(this Texture2D t)
        {
            var newT = new Texture2D(Core.GraphicsDevice, t.Width, t.Height);

            var cArr = new Color[newT.Width * newT.Height];
            t.GetData(cArr);

            newT.SetData(cArr);

            return newT;
        }

        public static Texture2D ReplaceColors(this Texture2D t, Color[] InputColors, Color[] OutputColors)
        {
            Texture2D newTexture = new Texture2D(Core.GraphicsDevice, t.Width, t.Height);

            if (InputColors.Length == OutputColors.Length && InputColors.Length > 0)
            {
                Color[] Data = new Color[t.Width * t.Height];
                List<Color> newData = new List<Color>();
                t.GetData(0, null, Data, 0, t.Width * t.Height);

                for (var i = 0; i <= Data.Length - 1; i++)
                {
                    Color c = Data[i];
                    if (InputColors.Contains(c) == true)
                    {
                        for (var iC = 0; iC <= InputColors.Length - 1; iC++)
                        {
                            if (InputColors[iC] == c)
                            {
                                c = OutputColors[iC];
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                    }
                    newData.Add(c);
                }

                newTexture.SetData(newData.ToArray());
            }
            else
            {
                newTexture = t;
            }

            return newTexture;
        }
    }
}
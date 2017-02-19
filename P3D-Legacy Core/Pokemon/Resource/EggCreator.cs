using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Extensions;

namespace P3D.Legacy.Core.Pokemon.Resource
{
    public static class EggCreator
    {
        const int PUFFER = 40;
        const int MINCOLOR = 100;

        const int GRAYSCALERANGE = 2;

        public static Texture2D CreateEggSprite(BasePokemon Pokemon, Texture2D DefaultEggSprite, Texture2D EggTemplate)
        {
            //Colors:
            //Light center: 0,255,255 (get from middle color + light)
            //Darker center: 255,0,0 (get from middle color)
            //Light dots: 0,255,0
            //Darker dots: 255,255,0

            Texture2D egg = EggTemplate;
            Texture2D sprite = Pokemon.GetMenuTexture(false);

            List<Color> arr = GetColors2(sprite, new Rectangle(0, 0, sprite.Width, sprite.Height), PUFFER).ToList();

            if (arr.Count < 2)
            {
                return DefaultEggSprite;
            }
            else
            {
                while (arr.Count < 4)
                {
                    for (var i = 0; i <= arr.Count - 1; i++)
                    {
                        if (arr.Count < 4)
                        {
                            arr.Add(arr[i]);
                        }
                    }
                }
            }

            while (arr.Count > 4)
            {
                arr.RemoveAt(arr.Count - 1);
            }

            arr = arr.OrderByDescending(c => (Convert.ToInt32(c.R) + Convert.ToInt32(c.G) + Convert.ToInt32(c.B))).ToList();

            Color[] inputColors = {
            new Color(0, 255, 255),
            new Color(255, 0, 0),
            new Color(0, 255, 0),
            new Color(255, 255, 0)
        };
            egg = egg.ReplaceColors(inputColors, arr.ToArray());

            return egg;
        }

        #region "EggColorAlgorithm"

        private static Color[] GetColors2(Texture2D tex, Rectangle rect, int puffer)
        {
            Color[] data = new Color[rect.Width * rect.Height];

            if (data.Length == 0)
            {
                return new [] { Color.White };
            }

            tex.GetData<Color>(0, rect, data, 0, data.Length);

            Dictionary<Color, int> cDic = new Dictionary<Color, int>();

            foreach (Color c in data)
            {
                if (c.A != 0)
                {
                    if (c.R != c.G || c.R != c.B || c.G != c.B)
                    {
                        if (Convert.ToInt32(c.R) + Convert.ToInt32(c.G) + Convert.ToInt32(c.B) >= MINCOLOR)
                        {
                            if (IsGrayScale(c) == false)
                            {
                                Color cc = IsNear2(c, cDic.Keys.ToArray(), puffer);
                                if (cDic.ContainsKey(cc) == false)
                                {
                                    cDic.Add(cc, 1);
                                }
                                else
                                {
                                    cDic[cc] += 1;
                                }
                            }
                        }
                    }
                }
            }

            List<KeyValuePair<Color, int>> l = new List<KeyValuePair<Color, int>>();
            l.AddRange(cDic);

            l = l.OrderBy(x => x.Value).ToList();

            List<Color> returnList = new List<Color>();

            for (var i = 0; i <= 3; i++)
            {
                if (l.Count - 1 >= i)
                {
                    int addID = (l.Count - 1) - i;
                    returnList.Add(l[addID].Key);
                }
            }

            return returnList.ToArray();
        }

        private static Color IsNear2(Color c, Color[] cArr, int puffer)
        {
            foreach (Color cc in cArr)
            {
                if (Math.Abs(Convert.ToInt32(c.R) - Convert.ToInt32(cc.R)) <= puffer && Math.Abs(Convert.ToInt32(c.G) - Convert.ToInt32(cc.G)) <= puffer && Math.Abs(Convert.ToInt32(c.B) - Convert.ToInt32(cc.B)) <= puffer)
                {
                    return cc;
                }
            }
            return c;
        }

        private static bool IsGrayScale(Color c)
        {
            int range = GRAYSCALERANGE;

            double v = (Convert.ToInt32(c.R) + (c.G) + Convert.ToInt32(c.B)) / 3;
            double maxR = Math.Abs(v - c.R);
            double maxG = Math.Abs(v - c.G);
            double maxB = Math.Abs(v - c.B);

            double max = 0;
            if (maxR > max)
            {
                max = maxR;
            }
            if (maxG > max)
            {
                max = maxG;
            }
            if (maxB > max)
            {
                max = maxB;
            }

            if (max <= range)
            {
                return true;
            }
            return false;
        }

        #endregion

    }
}

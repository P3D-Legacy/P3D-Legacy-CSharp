using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using P3D.Legacy.Core.Resources;

namespace P3D.Legacy.Core.Extensions
{
    public static class StringExtensions
    {
        public static int CountSplits(this string fullString, string separator)
        {
            int i = 0;

            if (fullString.Contains(separator))
                i += fullString.Count(c => c == Convert.ToChar(separator));

            return i + 1;
        }
        public static int CountSplits(this string fullString) => CountSplits(fullString, ",");


        public static string GetSplit(this string fullString, int valueIndex, string separator)
        {
            if (!fullString.Contains(separator))
                return fullString;
            var parts = fullString.Split(new [] { separator }, StringSplitOptions.None);
            return parts.Length - 1 >= valueIndex ? parts[valueIndex] : fullString;
        }
        public static string GetSplit(this string fullString, int valueIndex) => GetSplit(fullString, valueIndex, ",");

        public static int CountSeperators(this string fullstring, string separator)
        {
            int i = 0;

            if (fullstring.Contains(separator))
                i += fullstring.Count(c => c == Convert.ToChar(separator));

            return i;
        }

        public static bool IsNumeric(this string text)
        {
            double test;
            return double.TryParse(text, out test);
        }


        public static string[] SplitAtNewline(this string s)
        {
            if (s.Contains("§") == false)
                return s.Replace(Environment.NewLine, "§").Replace(Constants.vbLf, "§").Split(Convert.ToChar("§"));
            var data = new List<string>();

            if (string.IsNullOrEmpty(s))
                return new List<string>().ToArray();

            var i = 0;
            while (!string.IsNullOrEmpty(s) && i < s.Length)
            {
                if (!s.Substring(i).StartsWith(Environment.NewLine) || !s.Substring(i).StartsWith(Constants.vbLf))
                    i += 1;
                else
                {
                    data.Add(s.Substring(0, i));
                    i += 2;
                    s = s.Remove(0, i);
                    i = 0;
                }
            }

            data.Add(s.Substring(0, i));

            return data.ToArray();
        }


        public static string ToNumberString(this bool @bool)
        {
            return @bool ? "1" : "0";
        }


        public static int Clamp(this int i, int min, int max)
        {
            if (i > max)
            {
                i = max;
            }
            if (i < min)
            {
                i = min;
            }
            return i;
        }

        public static float Clamp(this float s, float min, float max)
        {
            if (s > max)
            {
                s = max;
            }
            if (s < min)
            {
                s = min;
            }
            return s;
        }

        public static double Clamp(this double d, double min, double max)
        {
            if (d > max)
            {
                d = max;
            }
            if (d < min)
            {
                d = min;
            }
            return d;
        }

        public static decimal Clamp(this decimal d, decimal min, decimal max)
        {
            if (d > max)
            {
                d = max;
            }
            if (d < min)
            {
                d = min;
            }
            return d;
        }


        public static string ArrayToString<T>(this T[] Array, bool NewLine = false)
        {
            if (NewLine == true)
            {
                string s = "";
                for (var i = 0; i <= Array.Length - 1; i++)
                {
                    if (i != 0)
                    {
                        s += Environment.NewLine;
                    }

                    s += Array[i].ToString();
                }
                return s;
            }
            else
            {
                string s = "{";
                for (var i = 0; i <= Array.Length - 1; i++)
                {
                    if (i != 0)
                    {
                        s += ",";
                    }

                    s += Array[i].ToString();
                }
                s += "}";
                return s;
            }
        }

        public static int ToPositive(this int i)
        {
            if (i < 0)
            {
                i *= -1;
            }
            return i;
        }

        public static double xRoot(this int root, double number)
        {
            double powered = 1 / root;

            double returnNumber = Math.Pow(number, powered);

            return returnNumber;
        }

        public static string CropStringToWidth(this string s, SpriteFont font, float scale, int width)
        {
            string fulltext = s;

            if ((font.MeasureString(fulltext).X * scale) <= width)
            {
                return fulltext;
            }
            else
            {
                if (fulltext.Contains(" ") == false)
                {
                    string newText = "";
                    while (fulltext.Length > 0)
                    {
                        if ((font.MeasureString(newText + fulltext[0].ToString(NumberFormatInfo.InvariantInfo)).X * scale) > width)
                        {
                            newText += Environment.NewLine;
                            newText += fulltext[0].ToString(NumberFormatInfo.InvariantInfo);
                            fulltext.Remove(0, 1);
                        }
                        else
                        {
                            newText += fulltext[0].ToString(NumberFormatInfo.InvariantInfo);
                            fulltext.Remove(0, 1);
                        }
                    }
                    return newText;
                }
            }

            string output = "";
            string currentLine = "";
            string currentWord = "";

            while (fulltext.Length > 0)
            {
                if (fulltext.StartsWith(Environment.NewLine) == true)
                {
                    if (!string.IsNullOrEmpty(currentLine))
                    {
                        currentLine += " ";
                    }
                    currentLine += currentWord;
                    output += currentLine + Environment.NewLine;
                    currentLine = "";
                    currentWord = "";
                    fulltext = fulltext.Remove(0, 2);
                }
                else if (fulltext.StartsWith(" ") == true)
                {
                    if (!string.IsNullOrEmpty(currentLine))
                    {
                        currentLine += " ";
                    }
                    currentLine += currentWord;
                    currentWord = "";
                    fulltext = fulltext.Remove(0, 1);
                }
                else
                {
                    currentWord += fulltext[0];
                    if ((font.MeasureString(currentLine + currentWord).X * scale) >= width)
                    {
                        if (string.IsNullOrEmpty(currentLine))
                        {
                            output += currentWord + Environment.NewLine;
                            currentWord = "";
                            currentLine = "";
                        }
                        else
                        {
                            output += currentLine + Environment.NewLine;
                            currentLine = "";
                        }
                    }
                    fulltext = fulltext.Remove(0, 1);
                }
            }

            if (!string.IsNullOrEmpty(currentWord))
            {
                if (!string.IsNullOrEmpty(currentLine))
                {
                    currentLine += " ";
                }
                currentLine += currentWord;
            }
            if (!string.IsNullOrEmpty(currentLine))
            {
                output += currentLine;
            }

            return output;
        }

        public static string CropStringToWidth(this string s, SpriteFont font, int width)
        {
            return CropStringToWidth(s, font, 1f, width);
        }
    }
}

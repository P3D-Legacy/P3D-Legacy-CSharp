using System.Collections.Generic;
using System.Drawing;

using Microsoft.Xna.Framework;

using P3D.Legacy.Core.Resources;

namespace P3D.Legacy.Core.Pokemon.Resource
{
    public abstract class BasePokemonForms
    {
        protected static List<PokemonForm> PokemonList = new List<PokemonForm>();


        /// <summary>
        /// Returns the initial Additional Data, if it needs to be set at generation time of the Pokémon.
        /// </summary>
        public static string GetInitialAdditionalData(BasePokemon P)
        {
            foreach (var listP in PokemonList)
            {
                if (listP.IsNumber(P.Number))
                {
                    return listP.GetInitialAdditionalData(P);
                }
            }

            return "";
        }

        /// <summary>
        /// Returns the Animation Name of the Pokémon, the path to its Sprite/Model files.
        /// </summary>
        public static string GetAnimationName(BasePokemon P)
        {
            foreach (var listP in PokemonList)
            {
                if (listP.IsNumber(P.Number))
                {
                    string _name = listP.GetAnimationName(P).ToLower();
                    if (_name.StartsWith("mega "))
                    {
                        _name = _name.Remove(0, 5);
                        if (_name.EndsWith(" x_mega_x") || _name.EndsWith(" y_mega_y"))
                        {
                            _name = _name.Remove(_name.Length - 9, 2);
                        }
                    }
                    else if (_name.StartsWith("primal "))
                    {
                        _name = _name.Remove(0, 7);
                    }
                    return _name;
                }
            }

            return P.OriginalName;
        }

        /// <summary>
        /// Returns the grid coordinates of the Pokémon's menu sprite.
        /// </summary>
        public static Vector2 GetMenuImagePosition(BasePokemon P)
        {
            foreach (var listP in PokemonList)
            {;
                if (listP.IsNumber(P.Number) == true)
                {
                    return listP.GetMenuImagePosition(P);
                }
            }

            int x = 0;
            int y = 0;
            int n = P.Number;
            while (n > 32)
            {
                n -= 32;
                y += 1;
            }
            x = n - 1;
            return new Vector2(x, y);
        }

        /// <summary>
        /// Returns the size of the Pokémon's menu sprite.
        /// </summary>
        public static Size GetMenuImageSize(BasePokemon P)
        {
            foreach (var listP in PokemonList)
            {
                if (listP.IsNumber(P.Number) == true)
                {
                    return listP.GetMenuImageSize(P);
                }
            }

            return new Size(32, 32);
        }

        /// <summary>
        /// Returns the addition to the Pokémon's overworld sprite name.
        /// </summary>
        public static string GetOverworldAddition(BasePokemon P)
        {
            foreach (var listP in PokemonList)
            {
                if (listP.IsNumber(P.Number) == true)
                {
                    return listP.GetOverworldAddition(P);
                }
            }

            return "";
        }

        /// <summary>
        /// Returns the path to the Pokémon's overworld sprite.
        /// </summary>
        public static string GetOverworldSpriteName(BasePokemon P)
        {
            string path = "Pokemon\\Overworld\\Normal\\";
            if (P.IsShiny)
            {
                path = "Pokemon\\Overworld\\Shiny\\";
            }
            path += P.Number + GetOverworldAddition(P);
            return path;
        }

        /// <summary>
        /// Returns the Pokémon's data file.
        /// </summary>
        /// <param name="Number">The number of the Pokémon.</param>
        /// <param name="AdditionalData">The additional data of the Pokémon.</param>
        public static string GetPokemonDataFile(int Number, string AdditionalData)
        {
            // TODO
            var FileName = GameModeManager.GetPokemonDataFileAsync(Number.ToString() + ".dat").Result.Path;

            string Addition = "";

            foreach (var listP in PokemonList)
            {
                if (listP.IsNumber(Number) == true)
                {
                    Addition = listP.GetDataFileAddition(AdditionalData);
                }
            }

            if (!string.IsNullOrEmpty(Addition))
            {
                FileName = FileName.Remove(FileName.Length - 4, 4) + Addition + ".dat";
            }

            if (System.IO.File.Exists(FileName) == false)
            {
                Number = 10;
                // TODO
                FileName = GameModeManager.GetPokemonDataFileAsync(Number + ".dat").Result.Path;
            }

            return FileName;
        }

        public static string GetDefaultOverworldSpriteAddition(int Number) => "";

        public static string GetDefaultImageAddition(int Number) => "";



        protected abstract class PokemonForm
        {
            private List<int> _numbers = new List<int>();
            public PokemonForm(int Number)
            {
                this._numbers.Add(Number);
            }

            public PokemonForm(int[] Numbers)
            {
                this._numbers.AddRange(Numbers);
            }

            public virtual string GetInitialAdditionalData(BasePokemon P)
            {
                return "";
            }

            public virtual string GetAnimationName(BasePokemon P)
            {
                return P.OriginalName;
            }

            public virtual Vector2 GetMenuImagePosition(BasePokemon P)
            {
                int x = 0;
                int y = 0;
                int n = P.Number;
                while (n > 32)
                {
                    n -= 32;
                    y += 1;
                }
                x = n - 1;
                return new Vector2(x, y);
            }

            public virtual Size GetMenuImageSize(BasePokemon P)
            {
                return new Size(32, 32);
            }

            public virtual string GetOverworldAddition(BasePokemon P)
            {
                return "";
            }

            public virtual string GetDataFileAddition(string AdditionalData)
            {
                return "";
            }

            public bool IsNumber(int number)
            {
                return this._numbers.Contains(number);
            }

        }
    }
}

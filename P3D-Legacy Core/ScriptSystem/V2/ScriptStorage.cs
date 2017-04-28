using System;
using System.Collections.Generic;

using P3D.Legacy.Core.Pokemon;

namespace P3D.Legacy.Core.ScriptSystem.V2
{
    /// <summary>
    /// Storage space for scripts.
    /// Used with @storage and the storage construct.
    /// </summary>
    public static class ScriptStorage
    {

        private static Dictionary<string, BasePokemon> Pokemons = new Dictionary<string, BasePokemon>();
        private static Dictionary<string, string> Strings = new Dictionary<string, string>();
        private static Dictionary<string, int> Integers = new Dictionary<string, int>();
        private static Dictionary<string, bool> Booleans = new Dictionary<string, bool>();
        private static Dictionary<string, Item> Items = new Dictionary<string, Item>();
        private static Dictionary<string, float> Singles = new Dictionary<string, float>();
        private static Dictionary<string, double> Doubles = new Dictionary<string, double>();

        /// <summary>
        /// Returns storage content.
        /// </summary>
        /// <param name="type">The type of the storage content.</param>
        /// <param name="name">The name of the storage content.</param>
        public static object GetObject(string type, string name)
        {
            switch (type.ToLower())
            {
                case "pokemon":
                    if (Pokemons.ContainsKey(name))
                        return Pokemons[name];
                    break;

                case "string":
                case "str":
                    if (Strings.ContainsKey(name))
                        return Strings[name];
                    break;

                case "integer":
                case "int":
                    if (Integers.ContainsKey(name))
                        return Integers[name];
                    break;

                case "boolean":
                case "bool":
                    if (Booleans.ContainsKey(name))
                        return Booleans[name];
                    break;

                case "item":
                    if (Items.ContainsKey(name))
                        return Items[name];
                    break;

                case "single":
                case "sng":
                    if (Singles.ContainsKey(name))
                        return Singles[name];
                    break;

                case "double":
                case "dbl":
                    if (Doubles.ContainsKey(name))
                        return Doubles[name];
                    break;
            }

            return new DefaultNullObj();
        }

        /// <summary>
        /// Adds or updates storage content.
        /// </summary>
        /// <param name="type">The type of the storage content.</param>
        /// <param name="name">The name of the storage content.</param>
        /// <param name="newContent">The new storage content.</param>
        public static void SetObject(string type, string name, object newContent)
        {
            switch (type.ToLower())
            {
                case "pokemon":
                    if (Pokemons.ContainsKey(name))
                        Pokemons[name] = (BasePokemon) newContent;
                    else
                        Pokemons.Add(name, (BasePokemon) newContent);
                    break;

                case "string":
                case "str":
                    if (Strings.ContainsKey(name))
                        Strings[name] = Convert.ToString(newContent);
                    else
                        Strings.Add(name, Convert.ToString(newContent));
                    break;

                case "integer":
                case "int":
                    if (Integers.ContainsKey(name))
                        Integers[name] = @int(newContent);
                    else
                        Integers.Add(name, @int(newContent));
                    break;

                case "boolean":
                case "bool":
                    if (Booleans.ContainsKey(name))
                        Booleans[name] = Convert.ToBoolean(newContent);
                    else
                        Booleans.Add(name, Convert.ToBoolean(newContent));
                    break;

                case "item":
                    if (Items.ContainsKey(name))
                        Items[name] = (Item) newContent;
                    else
                        Items.Add(name, (Item) newContent);
                    break;

                case "single":
                case "sng":
                    if (Singles.ContainsKey(name))
                        Singles[name] = sng(newContent);
                    else
                        Singles.Add(name, sng(newContent));
                    break;

                case "double":
                case "dbl":
                    if (Doubles.ContainsKey(name))
                        Doubles[name] = dbl(newContent);
                    else
                        Doubles.Add(name, dbl(newContent));
                    break;
            }
        }

        /// <summary>
        /// Counts the content entries.
        /// </summary>
        /// <param name="type">The type of the content entires to count or empty for all entires.</param>
        public static int Count(string type)
        {
            if (string.IsNullOrEmpty(type))
                return Pokemons.Count + Strings.Count + Integers.Count + Booleans.Count + Items.Count;
            else
            {
                switch (type.ToLower())
                {
                    case "pokemon":
                        return Pokemons.Count;

                    case "string":
                    case "str":
                        return Strings.Count;

                    case "integer":
                    case "int":
                        return Integers.Count;

                    case "boolean":
                    case "bool":
                        return Booleans.Count;

                    case "item":
                        return Items.Count;

                    case "single":
                    case "sng":
                        return Singles.Count;

                    case "double":
                    case "dbl":
                        return Doubles.Count;
                }
            }

            return 0;
        }

        /// <summary>
        /// Clears all script storage.
        /// </summary>
        public static void Clear()
        {
            Pokemons.Clear();
            Strings.Clear();
            Integers.Clear();
            Booleans.Clear();
            Items.Clear();
            Singles.Clear();
            Doubles.Clear();
        }

        private static int @int(object expression) => ScriptConversion.ToInteger(expression);

        private static float sng(object expression) => ScriptConversion.ToSingle(expression);

        private static double dbl(object expression) => ScriptConversion.ToDouble(expression);
    }
}

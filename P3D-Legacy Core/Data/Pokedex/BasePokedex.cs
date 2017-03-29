using System;
using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Core.Extensions;
using P3D.Legacy.Core.Resources;

namespace P3D.Legacy.Core.Pokemon
{
    public abstract class BasePokedex
    {
        public abstract bool IsActivated { get; }
        public abstract int Obtained { get; }
        public abstract int Seen { get; }
        public abstract int Count { get; }

        public abstract bool HasPokemon(int pokemonNumber, bool originalList);
        public abstract int GetPlace(int pokemonNumber);
        public abstract int GetPokemonNumber(int place);


        public static bool AutoDetect = true;
        public const int POKEMONCOUNT = 721;

        #region "PlayerData"

        public static int CountEntries(string Data, int[] Type)
        {
            string[] pData = Data.SplitAtNewline();

            return pData.Select(entry => entry.Remove(0, entry.IndexOf("{", StringComparison.Ordinal) + 1))
                .Select(entry => entry.Remove(entry.Length - 1, 1))
                .Select(entry => new {Entry = entry, eID = Convert.ToInt32(entry.GetSplit(0, "|"))})
                .Select(t => Convert.ToInt32(t.Entry.GetSplit(1, "|"))).Count(Type.Contains);
        }

        public static int CountEntries(string Data, int[] Type, string[] Range)
        {
            List<int> IDs = new List<int>();

            foreach (string r in Range)
            {
                if (r.IsNumeric())
                {
                    if (!IDs.Contains(Convert.ToInt32(r)))
                        IDs.Add(Convert.ToInt32(r));
                }
                else
                {
                    if (r.Contains("-"))
                    {
                        int min = Convert.ToInt32(r.Remove(r.IndexOf("-", StringComparison.Ordinal)));
                        int max = Convert.ToInt32(r.Remove(0, r.IndexOf("-", StringComparison.Ordinal) + 1));

                        for (var i = min; i <= max; i++)
                        {
                            if (!IDs.Contains(i))
                                IDs.Add(i);
                        }
                    }
                }
            }

            string[] pData = Data.SplitAtNewline();

            return (pData.Select(entry => entry.Remove(0, entry.IndexOf("{", StringComparison.Ordinal) + 1))
                .Select(entry => entry.Remove(entry.Length - 1, 1))
                .Select(entry => new {Entry = entry, eID = Convert.ToInt32(entry.GetSplit(0, "|"))})
                .Select(t => new {t, eType = Convert.ToInt32(t.Entry.GetSplit(1, "|"))})
                .Where(t => IDs.Contains(t.t.eID))
                .Select(t => t.eType)).Count(Type.Contains);
        }

        public static int GetEntryType(string Data, int ID)
        {
            string[] pData = Data.SplitAtNewline(); //Data.Split(Convert.ToChar(Environment.NewLine));

            if (pData.Length >= ID)
            {
                if (pData[ID - 1].Contains(ID + "|"))
                {
                    string entry = pData[ID - 1];
                    return Convert.ToInt32(entry.Remove(entry.Length - 1, 1).Remove(0, entry.IndexOf("|", StringComparison.Ordinal) + 1));
                }
            }

            foreach (string Entry in pData)
            {
                if (Entry.Contains(ID + "|"))
                {
                    return Convert.ToInt32(Entry.Remove(Entry.Length - 1, 1).Remove(0, Entry.IndexOf("|", StringComparison.Ordinal) + 1));
                }
            }

            return 0;
        }

        public static string ChangeEntry(string Data, int ID, int Type)
        {
            if (Type == 0 || AutoDetect == true)
            {
                if (Data.Contains("{" + ID + "|") == true)
                {
                    int cEntry = GetEntryType(Data, ID);
                    if (cEntry < Type)
                    {
                        return Data.Replace("{" + ID + "|" + cEntry + "}", "{" + ID + "|" + Type + "}");
                    }
                    else
                    {
                        return Data;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(Data))
                    {
                        Data += Environment.NewLine;
                    }
                    Data += "{" + ID + "|" + Type + "}";
                    return Data;
                }
            }
            return Data;
        }

        public static string NewPokedex()
        {
            string Data = "";

            for (var i = 1; i <= POKEMONCOUNT; i++)
            {
                Data += "{" + i + "|0}";
                if (i != POKEMONCOUNT)
                {
                    Data += Environment.NewLine;
                }
            }

            return Data;
        }

        public static int GetLastSeen(string Data)
        {
            string[] pData = Data.SplitAtNewline();
            int lastSeen = 1;

            for (var i = 0; i < pData.Length; i++)
            {
                pData[i] = pData[i].Remove(0, pData[i].IndexOf("{", StringComparison.Ordinal) + 1);
                pData[i] = pData[i].Remove(pData[i].Length - 1, 1);

                int eID = Convert.ToInt32(pData[i].GetSplit(0, "|"));
                int eType = Convert.ToInt32(pData[i].GetSplit(1, "|"));

                if (eType > 0)
                {
                    lastSeen = eID;
                }
            }

            return lastSeen;
        }

        /*
        public static void Load()
        {
            Core.Player.Pokedexes.Clear();

            string path = GameModeManager.GetContentFilePath("Data\\pokedex.dat");
            Security.FileValidation.CheckFileValid(path, false, "Pokedex.vb");

            string[] lines = System.IO.File.ReadAllLines(path);
            foreach (string PokedexData in lines)
            {
                Core.Player.Pokedexes.Add(new Pokedex(PokedexData));
            }
        }
        */

        public static string RegisterPokemon(string Data, BasePokemon Pokemon)
        {
            if (Pokemon.IsShiny == true)
            {
                return ChangeEntry(Data, Pokemon.Number, 3);
            }
            else
            {
                return ChangeEntry(Data, Pokemon.Number, 2);
            }
        }

        #endregion
    }
}
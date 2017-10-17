using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using P3D.Legacy.Core.Extensions;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Managers;

using PCLExt.FileStorage.Extensions;

namespace P3D.Legacy.Core.Pokemon
{
    public class RoamingPokemon
    {
        public int WorldID = -1;
        public string LevelFile = "";
        public string MusicLoop = "";

        public BasePokemon PokemonReference = null;
        public RoamingPokemon(string DataLine)
        {
            string[] data = DataLine.Split(Convert.ToChar("|"));

            PokemonReference = BasePokemon.GetPokemonByData(data[5]);

            WorldID = Convert.ToInt32(data[2]);
            LevelFile = data[3];
            MusicLoop = data[4];
        }

        public string CompareData() => PokemonReference.Number + "|" + PokemonReference.Level + "|" + WorldID + "|";

        public BasePokemon GetPokemon() => PokemonReference;

        public static void ShiftRoamingPokemon(int worldID)
        {
            Logger.Debug("Shift Roaming Pokémon for world ID: " + worldID);

            string newData = "";
            foreach (string line in Core.Player.RoamingPokemonData.SplitAtNewline())
            {
                if (!string.IsNullOrEmpty(line) && line.CountSeperators("|") >= 5)
                {
                    string[] data = line.Split(Convert.ToChar("|"));

                    if (!string.IsNullOrEmpty(newData))
                    {
                        newData += Environment.NewLine;
                    }

                    if (Convert.ToInt32(data[2]) == worldID || worldID == -1)
                    {
                        var regionsFile = GameModeManager.GetScriptFile(Path.Combine("worldmap", "roaming_regions.dat"));
                        Security.FileValidation.CheckFileValid(regionsFile, false, "RoamingPokemon.vb");

                        var worldList = regionsFile.ReadAllLines();
                        var levelList = new List<string>();

                        foreach (var worldLine in worldList)
                        {
                            if (worldLine.StartsWith(Convert.ToInt32(data[2]) + "|"))
                            {
                                levelList = worldLine.Remove(0, worldLine.IndexOf("|", StringComparison.Ordinal) + 1).Split(Convert.ToChar(",")).ToList();
                            }
                        }

                        int currentIndex = levelList.IndexOf(data[3]);
                        int nextIndex = currentIndex + 1;
                        if (nextIndex > levelList.Count - 1)
                        {
                            nextIndex = 0;
                        }

                        //PokémonID,Level,regionID,startLevelFile,MusicLoop,PokemonData
                        newData += data[0] + "|" + data[1] + "|" + Convert.ToInt32(data[2]) + "|" + levelList[nextIndex] + "|" + data[4] + "|" + data[5];
                    }
                    else
                    {
                        newData += line;
                    }
                }
            }

            Core.Player.RoamingPokemonData = newData;
        }

        /// <summary>
        /// Removes the Pokemon from the list of roaming Pokemon. The Pokemon has to hold the data as Tag.
        /// </summary>
        /// <param name="p">The Pokemon containing the Tag.</param>
        public static string RemoveRoamingPokemon(RoamingPokemon p)
        {
            string compareData = p.CompareData();

            string newData = "";

            foreach (string line in Core.Player.RoamingPokemonData.SplitAtNewline())
            {
                if (line.StartsWith(compareData) == false)
                {
                    if (!string.IsNullOrEmpty(newData))
                    {
                        newData += Environment.NewLine;
                    }
                    newData += line;
                }
            }

            return newData;
        }

        public static string ReplaceRoamingPokemon(RoamingPokemon p)
        {
            string compareData = p.CompareData();

            string newData = "";

            foreach (string line in Core.Player.RoamingPokemonData.SplitAtNewline())
            {
                if (!string.IsNullOrEmpty(newData))
                {
                    newData += Environment.NewLine;
                }
                if (line.StartsWith(compareData) == false)
                {
                    newData += line;
                }
                else
                {
                    newData += p.PokemonReference.Number + "|" + p.PokemonReference.Level + "|" + p.WorldID + "|" + p.LevelFile + "|" + p.MusicLoop + "|" + p.PokemonReference.GetSaveData();
                }
            }

            return newData;
        }

    }
}

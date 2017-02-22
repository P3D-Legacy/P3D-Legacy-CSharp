using System;
using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Core.Extensions;
using P3D.Legacy.Core.Resources;

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

            this.PokemonReference = BasePokemon.GetPokemonByData(data[5]);

            this.WorldID = Convert.ToInt32(data[2]);
            this.LevelFile = data[3];
            this.MusicLoop = data[4];
        }

        public string CompareData()
        {
            return this.PokemonReference.Number.ToString() + "|" + this.PokemonReference.Level.ToString() + "|" + this.WorldID.ToString() + "|";
        }

        public BasePokemon GetPokemon()
        {
            return this.PokemonReference;
        }

        public static void ShiftRoamingPokemon(int worldID)
        {
            Logger.Debug("Shift Roaming Pokémon for world ID: " + worldID.ToString());

            string newData = "";
            foreach (string line in Core.Player.RoamingPokemonData.SplitAtNewline())
            {
                if (!string.IsNullOrEmpty(line) && line.CountSeperators("|") >= 5)
                {
                    string[] data = line.Split(Convert.ToChar("|"));

                    if (!string.IsNullOrEmpty(newData))
                    {
                        newData += Constants.vbNewLine;
                    }

                    if (Convert.ToInt32(data[2]) == worldID || worldID == -1)
                    {
                        string regionsFile = GameModeManager.GetScriptFileAsync("worldmap\\roaming_regions.dat").Result.Path;
                        // TODO
                        //Security.FileValidation.CheckFileValid(regionsFile, false, "RoamingPokemon.vb");

                        List<string> worldList = System.IO.File.ReadAllLines(regionsFile).ToList();
                        List<string> levelList = new List<string>();

                        foreach (string worldLine in worldList)
                        {
                            if (worldLine.StartsWith(Convert.ToInt32(data[2]).ToString() + "|") == true)
                            {
                                levelList = worldLine.Remove(0, worldLine.IndexOf("|") + 1).Split(Convert.ToChar(",")).ToList();
                            }
                        }

                        int currentIndex = levelList.IndexOf(data[3]);
                        int nextIndex = currentIndex + 1;
                        if (nextIndex > levelList.Count - 1)
                        {
                            nextIndex = 0;
                        }

                        //PokémonID,Level,regionID,startLevelFile,MusicLoop,PokemonData
                        newData += data[0] + "|" + data[1] + "|" + Convert.ToInt32(data[2]).ToString() + "|" + levelList[nextIndex] + "|" + data[4] + "|" + data[5];
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
                        newData += Constants.vbNewLine;
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
                    newData += Constants.vbNewLine;
                }
                if (line.StartsWith(compareData) == false)
                {
                    newData += line;
                }
                else
                {
                    newData += p.PokemonReference.Number + "|" + p.PokemonReference.Level + "|" + p.WorldID.ToString() + "|" + p.LevelFile + "|" + p.MusicLoop + "|" + p.PokemonReference.GetSaveData();
                }
            }

            return newData;
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P3D.Legacy.Core.Extensions;

namespace P3D.Legacy.Core.Pokemon
{
    public class FrontierSpawner
    {
        public static BasePokemon GetPokemon(int level, int pokemon_class, List<int> IDPreset)
        {
            List<int> validIDs = new List<int>();

            string[] files = Directory.GetFiles(GameController.GamePath + "\\Content\\Pokemon\\Data", "*.dat", SearchOption.TopDirectoryOnly);
            foreach (string f in files)
            {
                if (Path.GetFileNameWithoutExtension(f).IsNumeric())
                {
                    int newID = Convert.ToInt32(Path.GetFileNameWithoutExtension(f));
                    if (IDPreset == null || IDPreset.Contains(newID))
                    {
                        validIDs.Add(newID);
                    }
                }
            }

            int ID = validIDs[Core.Random.Next(0, validIDs.Count)];

            var p = GetPredeterminedPokemon(ID, level, pokemon_class);

            if (p == null)
            {
                p = BasePokemon.GetPokemonByID(ID);
                p.Generate(level, true);
                p.FullRestore();
            }

            return p;
        }

        private static BasePokemon GetPredeterminedPokemon(int ID, int level, int pokemon_class)
        {
            string path = GameController.GamePath + "\\Content\\Pokemon\\Data\\frontier\\" + pokemon_class.ToString() + ".dat";
            Security.FileValidation.CheckFileValid(path, false, "FrontierSpawner.vb");

            List<string> data = System.IO.File.ReadAllLines(path).ToList();

            foreach (string line in data)
            {
                string[] lData = line.Split(Convert.ToChar("|"));
                string[] InputIDs = lData[0].Split(Convert.ToChar(","));
                if (InputIDs.Contains(ID.ToString()) == true)
                {
                    int OutputID = Convert.ToInt32(lData[1]);
                    List<int> Moveset = new List<int>();
                    foreach (string move in lData[2].Split(Convert.ToChar(",")))
                    {
                        if (!string.IsNullOrEmpty(move) && move.IsNumeric())
                        {
                            Moveset.Add(Convert.ToInt32(move));
                        }
                    }
                    string[] Stats = lData[3].Split(Convert.ToChar(","));
                    string ItemID = lData[4];

                    BasePokemon p = BasePokemon.GetPokemonByID(OutputID);
                    p.Generate(level, true);
                    p.Item = null;
                    AddMoveset(ref p, Moveset.ToArray());
                    SetStats(ref p, Stats[0], Stats[1], pokemon_class);

                    if (!string.IsNullOrEmpty(ItemID))
                    {
                        p.Item = Item.GetItemByID(Convert.ToInt32(ItemID));
                    }
                    if (p.Item == null)
                    {
                        int[] items = {
                        146,
                        2009,
                        119,
                        140,
                        73,
                        74
                    };

                        p.Item = Item.GetItemByID(items[Core.Random.Next(0, items.Length)]);
                    }

                    p.FullRestore();
                    return p;
                }
            }

            return null;
        }

        private static void GiveItem(ref BasePokemon p)
        {
            if ((p != null))
            {
                if ((p.Item != null))
                {
                    int[] items = {
                    146,
                    2009,
                    119,
                    140,
                    73,
                    74
                };

                    p.Item = Item.GetItemByID(items[Core.Random.Next(0, items.Length)]);
                }
            }
        }

        private static void SetStats(ref BasePokemon p, string stat1, string stat2, int pokemon_class)
        {
            int[] IVRange = { 0, 0 };
            int standardEV = 10;
            int maxEV = 255;
            int maxIV = 31;

            switch (pokemon_class)
            {
                case 0:
                    //base
                    IVRange = new[] { 0, 20 };
                    standardEV = 4;
                    maxEV = 150;
                    maxIV = 20;
                    break;
                case 1:
                    //normal
                    IVRange = new [] { 5, 31 };
                    standardEV = 8;
                    maxEV = 200;
                    maxIV = 26;
                    break;
                case 2:
                    //master
                    IVRange = new[] { 20, 31 };
                    standardEV = 10;
                    maxEV = 255;
                    maxIV = 31;
                    break;
            }

            p.IVHP = Core.Random.Next(IVRange[0], IVRange[1] + 1);
            p.IVAttack = Core.Random.Next(IVRange[0], IVRange[1] + 1);
            p.IVDefense = Core.Random.Next(IVRange[0], IVRange[1] + 1);
            p.IVSpAttack = Core.Random.Next(IVRange[0], IVRange[1] + 1);
            p.IVSpDefense = Core.Random.Next(IVRange[0], IVRange[1] + 1);
            p.IVSpeed = Core.Random.Next(IVRange[0], IVRange[1] + 1);

            p.EVHP = standardEV;
            p.EVAttack = standardEV;
            p.EVDefense = standardEV;
            p.EVSpAttack = standardEV;
            p.EVSpDefense = standardEV;
            p.EVSpeed = standardEV;

            switch (stat1.ToLower())
            {
                case "hp":
                    p.IVHP = maxIV;
                    p.EVHP = maxEV;
                    break;
                case "atk":
                case "attack":
                    p.IVAttack = maxIV;
                    p.EVAttack = maxEV;
                    break;
                case "def":
                case "defense":
                    p.IVDefense = maxIV;
                    p.EVDefense = maxEV;
                    break;
                case "spatk":
                case "spattack":
                    p.IVSpAttack = maxIV;
                    p.EVSpAttack = maxEV;
                    break;
                case "spdef":
                case "spdefense":
                    p.IVSpDefense = maxIV;
                    p.EVSpDefense = maxEV;
                    break;
                case "speed":
                    p.IVSpeed = maxIV;
                    p.EVSpeed = maxEV;
                    break;
            }
            switch (stat2.ToLower())
            {
                case "hp":
                    p.IVHP = maxIV;
                    p.EVHP = maxEV;
                    break;
                case "atk":
                case "attack":
                    p.IVAttack = maxIV;
                    p.EVAttack = maxEV;
                    break;
                case "def":
                case "defense":
                    p.IVDefense = maxIV;
                    p.EVDefense = maxEV;
                    break;
                case "spatk":
                case "spattack":
                    p.IVSpAttack = maxIV;
                    p.EVSpAttack = maxEV;
                    break;
                case "spdef":
                case "spdefense":
                    p.IVSpDefense = maxIV;
                    p.EVSpDefense = maxEV;
                    break;
                case "speed":
                    p.IVSpeed = maxIV;
                    p.EVSpeed = maxEV;
                    break;
            }

            if (pokemon_class > 0)
            {
                switch (stat1.ToLower())
                {
                    case "hp":
                        p.Nature = BasePokemon.ConvertIDToNature(Core.Random.Next(0, 26));
                        break;
                    case "atk":
                        if (stat2.ToLower() != "def")
                        {
                            p.Nature = BasePokemon.Natures.Lonely;
                        }
                        else
                        {
                            p.Nature = BasePokemon.Natures.Relaxed;
                        }
                        break;
                    case "def":
                        if (stat2.ToLower() != "spatk")
                        {
                            p.Nature = BasePokemon.Natures.Impish;
                        }
                        else
                        {
                            p.Nature = BasePokemon.Natures.Lax;
                        }
                        break;
                    case "spatk":
                        if (stat2.ToLower() != "spdef")
                        {
                            p.Nature = BasePokemon.Natures.Rash;
                        }
                        else
                        {
                            p.Nature = BasePokemon.Natures.Modest;
                        }
                        break;
                    case "spdef":
                        if (stat2.ToLower() != "speed")
                        {
                            p.Nature = BasePokemon.Natures.Sassy;
                        }
                        else
                        {
                            p.Nature = BasePokemon.Natures.Gentle;
                        }
                        break;
                    case "speed":
                        if (stat2.ToLower() != "atk")
                        {
                            p.Nature = BasePokemon.Natures.Timid;
                        }
                        else
                        {
                            p.Nature = BasePokemon.Natures.Jolly;
                        }
                        break;
                }
            }

            p.CalculateStats();
        }

        private static void AddMoveset(ref BasePokemon p, int[] moveList)
        {
            p.Attacks.Clear();
            foreach (int moveID in moveList)
            {
                p.Attacks.Add(BaseAttack.GetAttackByID(moveID));
            }
        }

    }
}

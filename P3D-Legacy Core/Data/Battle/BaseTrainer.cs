using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

using Microsoft.Xna.Framework;

using P3D.Legacy.Core.Extensions;
using P3D.Legacy.Core.Pokemon;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Managers;

using PCLExt.FileStorage.Extensions;

namespace P3D.Legacy.Core.Battle
{
    public class BaseTrainer
    {
        public int AILevel = 0;
        public List<BaseAttack> SignatureMoves = new List<BaseAttack>();
        public List<BasePokemon> Pokemons = new List<BasePokemon>();
        public string TrainerType = "Youngster";
        public string TrainerType2 = "Youngster";
        public string Name = "Joey";
        public string Name2 = "Joey";
        public int Money = 84;
        public string SpriteName = "14";
        public string SpriteName2 = "14";
        public string Region = "Johto";
        public string Music = "Trainer";
        public string TrainerFile = "";
        public bool DoubleTrainer = false;
        public List<Item> Items = new List<Item>();
        public int Gender = -1;
        public int IntroType = 10;

        public string GameJoltID = "";
        public string VSImageOrigin = "VSIntro";
        public Vector2 VSImagePosition = new Vector2(0, 0);
        public Size VSImageSize = new Size(61, 54);

        public Vector2 BarImagePosition = new Vector2(0, 0);
        public string OutroMessage = "TRAINER_DEFAULT_MESSAGE";

        public string OutroMessage2 = "TRAINER_DEFAULT_MESSAGE";
        public string IntroMessage = "TRAINER_DEFAULT_MESSAGE";

        public string DefeatMessage = "TRAINER_DEFAULT_MESSAGE";

        public static int FrontierTrainer = -1;

        //public abstract bool IsBeaten();

        public BaseTrainer() { }
        public BaseTrainer(string TrainerFile)
        {
            this.TrainerFile = TrainerFile;

            var file = GameModeManager.GetScriptFile(Path.Combine("Trainer", TrainerFile + ".trainer"));
            // TODO:
            //FileValidation.CheckFileValid(path, false, "Trainer.vb");

            string[] Data = file.ReadAllLines();

            if (Data[0] == "[TRAINER FORMAT]")
            {
                LoadTrainer(Data);
            }
            else
            {
                LoadTrainerLegacy(Data);
            }
        }

        protected void LoadTrainerLegacy(string[] Data)
        {
            List<string> newData = new List<string>();
            List<string> sevenData = Data[7].Split(Convert.ToChar("|")).ToList();

            newData.Add("Name|" + Data[2]);
            newData.Add("TrainerClass|" + Data[1]);
            newData.Add("Money|" + Data[0]);
            newData.Add("IntroMessage|" + Data[3]);
            newData.Add("OutroMessage|" + Data[4]);
            newData.Add("DefeatMessage|" + Data[5]);
            newData.Add("TextureID|" + Data[6]);
            newData.Add("Region|" + sevenData[0]);

            Region = sevenData[0];
            Music = sevenData[1];

            newData.Add("IniMusic|" + GetIniMusicName());
            newData.Add("DefeatMusic|" + GetDefeatMusic());
            newData.Add("BattleMusic|" + GetBattleMusicName());

            newData.Add("Pokemon1|" + Data[8].Remove(0, 2));
            newData.Add("Pokemon2|" + Data[9].Remove(0, 2));
            newData.Add("Pokemon3|" + Data[10].Remove(0, 2));
            newData.Add("Pokemon4|" + Data[11].Remove(0, 2));
            newData.Add("Pokemon5|" + Data[12].Remove(0, 2));
            newData.Add("Pokemon6|" + Data[13].Remove(0, 2));

            if (Data.Length > 14)
            {
                newData.Add("Items|" + Data[14]);
            }
            if (Data.Length > 15)
            {
                newData.Add("AI|" + Data[15]);
            }
            if (Data.Length > 16)
            {
                newData.Add("Gender|" + Data[16]);
            }

            string sequenceData = "Blue,Blue";
            if (sevenData.Count == 3)
            {
                sequenceData = sevenData[2] + ",Blue";
            }
            else if (sevenData.Count > 3)
            {
                sequenceData = sevenData[2] + "," + sevenData[3];
            }
            newData.Add("IntroSequence|" + sequenceData);

            Logger.Log(Logger.LogTypes.Warning, "Trainer.vb: Converted legacy trainer file! Generated new trainer data:");
            Logger.Log(Logger.LogTypes.Message, newData.ToArray().ArrayToString());

            LoadTrainer(newData.ToArray());
        }
        protected virtual void LoadTrainer(string[] Data) { }

        protected void SetIniImage(string vsType, string barType)
        {
            switch (vsType.ToLower())
            {
                case "blue":
                case "0":
                    VSImagePosition = new Vector2(0, 0);
                    break;
                case "orange":
                case "1":
                    VSImagePosition = new Vector2(1, 0);
                    break;
                case "green":
                case "2":
                    VSImagePosition = new Vector2(0, 1);
                    break;
                case "3":
                    VSImagePosition = new Vector2(1, 1);
                    break;
                case "4":
                    VSImagePosition = new Vector2(0, 2);
                    break;
                case "5":
                    VSImagePosition = new Vector2(1, 2);
                    break;
                case "6":
                    VSImagePosition = new Vector2(0, 3);
                    break;
                case "7":
                    VSImagePosition = new Vector2(1, 3);
                    break;
                case "8":
                    VSImagePosition = new Vector2(0, 4);
                    break;
                case "9":
                    VSImagePosition = new Vector2(1, 4);
                    break;
                case "red":
                case "10":
                    VSImagePosition = new Vector2(0, 5);
                    break;
                case "11":
                    VSImagePosition = new Vector2(1, 5);
                    break;
                case "battlefrontier":
                    VSImagePosition = new Vector2(0, 0);
                    VSImageOrigin = "battlefrontier";
                    VSImageSize = new Size(275, 275);
                    break;
                default:
                    if (vsType.IsNumeric())
                    {
                        if (Convert.ToInt32(vsType) > 11)
                        {
                            int x = Convert.ToInt32(vsType);
                            int y = 0;
                            while (x > 1)
                            {
                                x -= 2;
                                y += 1;
                            }
                            VSImagePosition = new Vector2(x, y);
                        }
                    }
                    break;
            }
            switch (barType.ToLower())
            {
                case "blue":
                case "0":
                    BarImagePosition = new Vector2(0, 0);
                    break;
                case "orange":
                case "1":
                    BarImagePosition = new Vector2(1, 0);
                    break;
                case "lightgreen":
                case "2":
                    BarImagePosition = new Vector2(0, 1);
                    break;
                case "gray":
                case "3":
                    BarImagePosition = new Vector2(1, 1);
                    break;
                case "violet":
                case "4":
                    BarImagePosition = new Vector2(0, 2);
                    break;
                case "green":
                case "5":
                    BarImagePosition = new Vector2(1, 2);
                    break;
                case "yellow":
                case "6":
                    BarImagePosition = new Vector2(0, 3);
                    break;
                case "brown":
                case "7":
                    BarImagePosition = new Vector2(1, 3);
                    break;
                case "lightblue":
                case "8":
                    BarImagePosition = new Vector2(0, 4);
                    break;
                case "lightgray":
                case "9":
                    BarImagePosition = new Vector2(1, 4);
                    break;
                case "red":
                case "10":
                    BarImagePosition = new Vector2(0, 5);
                    break;
                case "11":
                    BarImagePosition = new Vector2(1, 5);
                    break;
                default:
                    if (barType.IsNumeric())
                    {
                        if (Convert.ToInt32(barType) > 11)
                        {
                            int x = Convert.ToInt32(barType);
                            int y = 0;
                            while (x > 1)
                            {
                                x -= 2;
                                y += 1;
                            }
                            BarImagePosition = new Vector2(x, y);
                        }
                    }
                    break;
            }
        }

        protected string IniMusic = "";
        protected string DefeatMusic = "";
        protected string BattleMusic = "";

        protected string InSightMusic = "trainer_encounter";
        public string GetIniMusicName()
        {
            if (!string.IsNullOrEmpty(IniMusic))
            {
                return IniMusic;
            }

            string middle = "trainer";

            switch (Music.ToLower())
            {
                case "rival":
                    middle = "rival";
                    break;
                case "leader":
                    middle = "leader";
                    break;
                case "rocket":
                    middle = "rocket";
                    break;
            }

            return Region + "_" + middle + "_intro";
        }

        public string GetDefeatMusic()
        {
            if (!string.IsNullOrEmpty(DefeatMusic))
            {
                return DefeatMusic;
            }

            string pre = "trainer";

            switch (Music.ToLower())
            {
                case "leader":
                    pre = "leader";
                    break;
            }

            return pre + "_defeat";
        }

        public string GetBattleMusicName()
        {
            if (!string.IsNullOrEmpty(BattleMusic))
            {
                return BattleMusic;
            }

            return Region.ToLower() + "_" + Music.ToLower();
        }

        public string GetInSightMusic()
        {
            return InSightMusic;
        }

        public bool HasBattlePokemon()
        {
            foreach (var Pokemon in Pokemons)
            {
                if (Pokemon.Status != BasePokemon.StatusProblems.Fainted && Pokemon.HP > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void TrainerItemUse(int ItemID)
        {
            for (var i = 0; i <= Items.Count - 1; i++)
            {
                var  item = Items[i];
                if (item.Id == ItemID)
                {
                    Items.RemoveAt(i);
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
        }

        public int CountUseablePokemon()
        {
            int i = 0;

            foreach (var p in Pokemons)
            {
                if (p.HP > 0 && p.Status != BasePokemon.StatusProblems.Fainted)
                {
                    i += 1;
                }
            }

            return i;
        }

        public static bool IsBeaten(string trainerID)
        {
            return false;
        }
    }
}

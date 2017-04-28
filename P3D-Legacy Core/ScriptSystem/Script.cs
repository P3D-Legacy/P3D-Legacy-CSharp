using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

using P3D.Legacy.Core.Entities;
using P3D.Legacy.Core.Extensions;
using P3D.Legacy.Core.Pokemon;
using P3D.Legacy.Core.Resources.Managers.Sound;
using P3D.Legacy.Core.Screens;
using P3D.Legacy.Core.ScriptSystem.V2;

namespace P3D.Legacy.Core.ScriptSystem
{
    public abstract class BaseScript
    {
        public enum ScriptTypes
        {
            Command = 100,

            @if = 101,
            when = 102,
            then = 103,
            @else = 104,
            endif = 105,
            end = 106,
            @select = 107,
            endwhen = 108,
            @return = 109,
            endscript = 110,
            @while = 111,
            endwhile = 112,
            exitwhile = 113,

            Comment = 128
        }


        public string Value { get; set; } = "";
        public ScriptTypes ScriptType { get; set; }
        public bool started { get; set; }
        public bool IsReady { get; set; }
        public bool CanContinue { get; set; } = true;

        public abstract void Initialize(string line);
        public abstract void Update(GameTime gameTime);
    }

    public class Script
    {
        public abstract class BaseScriptManager
        {
            public abstract BaseScript New();

            public abstract object EvaluateConstruct(object construct);
        }


        private static BaseScriptManager _bsm;

        protected static BaseScriptManager BSM
        {
            get
            {
                if (_bsm == null)
                {
                    var assembly = Assembly.GetEntryAssembly();
                    var type = assembly.GetTypes().SingleOrDefault(t => t.IsSubclassOf(typeof(BaseScriptManager)));
                    if (type != null)
                        _bsm = Activator.CreateInstance(type) as BaseScriptManager;
                }

                return _bsm;
            }
            set { _bsm = value; }
        }
        public static object EvaluateConstruct(object construct) => BSM.EvaluateConstruct(construct);


        public BaseScript ScriptV2 = BSM.New(); //= new ScriptV2();
        public string ScriptLine = "";

        public int Level = 0;
        public string Value
        {
            get
            {
                switch (ActionScript.CSL().ScriptVersion)
                {
                    case 2:
                        return ScriptV2.Value;
                }
                return "";
            }
            set
            {
                switch (ActionScript.CSL().ScriptVersion)
                {
                    case 2:
                        ScriptV2.Value = value;
                        break;
                }
            }
        }

        public BaseScript.ScriptTypes ScriptType
        {
            get
            {
                switch (ActionScript.CSL().ScriptVersion)
                {
                    case 2:
                        return ScriptV2.ScriptType;
                }
                return 0;
            }
            set
            {
                switch (ActionScript.CSL().ScriptVersion)
                {
                    case 2:
                        ScriptV2.ScriptType = value;
                        break;
                }
            }
        }

        public bool started
        {
            get
            {
                switch (ActionScript.CSL().ScriptVersion)
                {
                    case 2:
                        return ScriptV2.started;
                }
                return false;
            }
            set
            {
                switch (ActionScript.CSL().ScriptVersion)
                {
                    case 2:
                        ScriptV2.started = value;
                        break;
                }
            }
        }

        public bool IsReady
        {
            get
            {
                switch (ActionScript.CSL().ScriptVersion)
                {
                    case 2:
                        return ScriptV2.IsReady;
                }
                return false;
            }
            set
            {
                switch (ActionScript.CSL().ScriptVersion)
                {
                    case 2:
                        ScriptV2.IsReady = value;
                        break;
                }
            }
        }

        public bool CanContinue
        {
            get
            {
                switch (ActionScript.CSL().ScriptVersion)
                {
                    case 2:
                        return ScriptV2.CanContinue;
                }
                return false;
            }
            set
            {
                switch (ActionScript.CSL().ScriptVersion)
                {
                    case 2:
                        ScriptV2.CanContinue = value;
                        break;
                }
            }
        }

        public Script(string Line, int Level)
        {
            this.Level = Level;
            this.ScriptLine = Line;

            switch (ActionScript.CSL().ScriptVersion)
            {
                case 2:
                    if (!String.IsNullOrEmpty(Line))
                        ScriptV2.Initialize(Line);
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Level == ActionScript.ScriptLevelIndex)
            {
                switch (ActionScript.CSL().ScriptVersion)
                {
                    case 2:
                        ScriptV2.Update(gameTime);
                        break;
                }
            }
            else
            {
                this.IsReady = true;
            }
        }

        public static void NameRival(string name)
        {
            Core.Player.RivalName = name;
        }


        public static string[] SaveNPCTrade;
        public static void ExitedNPCTrade()
        {
            string message2 = SaveNPCTrade[14];
            Screen.TextBox.Show(message2, new Entity[] { }, false, false);
        }

        public static void DoNPCTrade(int pokeIndex)
        {
            Core.SetScreen(Core.CurrentScreen.PreScreen);

            BasePokemon ownPokemon = Core.Player.Pokemons[pokeIndex];

            int ownPokeID = ScriptConversion.ToInteger(Script.SaveNPCTrade[0]);
            int oppPokeID = ScriptConversion.ToInteger(Script.SaveNPCTrade[1]);

            BasePokemon oppPokemon = BasePokemon.GetPokemonByID(oppPokeID);

            int Level = ownPokemon.Level;

            if (Script.SaveNPCTrade[2].IsNumeric())
            {
                Level = ScriptConversion.ToInteger(Script.SaveNPCTrade[2]);
            }

            oppPokemon.Generate(Level, true);

            BasePokemon.Genders Gender = ownPokemon.Gender;

            if (Script.SaveNPCTrade[3].IsNumeric())
            {
                int genderID = ScriptConversion.ToInteger(Script.SaveNPCTrade[3]);
                if (genderID == -1)
                {
                    genderID = Core.Random.Next(0, 2);
                }

                switch (genderID)
                {
                    case 0:
                        Gender = BasePokemon.Genders.Male;
                        break;
                    case 1:
                        Gender = BasePokemon.Genders.Female;
                        break;
                    case 2:
                        Gender = BasePokemon.Genders.Genderless;
                        break;
                    default:
                        Gender = BasePokemon.Genders.Male;
                        break;
                }
            }

            oppPokemon.Gender = Gender;

            if (!String.IsNullOrEmpty(Script.SaveNPCTrade[4]))
            {
                oppPokemon.Attacks.Clear();
                string[] attacks = { Script.SaveNPCTrade[4] };
                if (Script.SaveNPCTrade[4].Contains(","))
                {
                    attacks = Script.SaveNPCTrade[4].Split(Convert.ToChar(","));
                }
                foreach (string attackID in attacks)
                {
                    if (oppPokemon.Attacks.Count < 4)
                    {
                        oppPokemon.Attacks.Add(BaseAttack.GetAttackByID(ScriptConversion.ToInteger(attackID)));
                    }
                }
            }

            if (!String.IsNullOrEmpty(Script.SaveNPCTrade[5]))
            {
                oppPokemon.IsShiny = Convert.ToBoolean(Script.SaveNPCTrade[5]);
            }

            oppPokemon.OT = Script.SaveNPCTrade[6];
            oppPokemon.CatchTrainerName = Script.SaveNPCTrade[7];
            oppPokemon.CatchBall = Item.GetItemByID(ScriptConversion.ToInteger(Script.SaveNPCTrade[8]));

            string itemID = Script.SaveNPCTrade[9];
            if (itemID.IsNumeric())
            {
                oppPokemon.Item = Item.GetItemByID(ScriptConversion.ToInteger(itemID));
            }

            oppPokemon.CatchLocation = Script.SaveNPCTrade[10];
            oppPokemon.CatchMethod = Script.SaveNPCTrade[11];
            oppPokemon.NickName = Script.SaveNPCTrade[12];

            string message1 = Script.SaveNPCTrade[13];
            string message2 = Script.SaveNPCTrade[14];

            string register = Script.SaveNPCTrade[15];

            if (ownPokeID == ownPokemon.Number)
            {
                Core.Player.Pokemons.RemoveAt(pokeIndex);
                Core.Player.Pokemons.Add(oppPokemon);

                int pokedexType = 2;
                if (oppPokemon.IsShiny == true)
                {
                    pokedexType = 3;
                }

                Core.Player.PokedexData = BasePokedex.ChangeEntry(Core.Player.PokedexData, oppPokemon.Number, pokedexType);

                if (!String.IsNullOrEmpty(register))
                {
                    ActionScript.RegisterID(register);
                }

                Core.Player.AddPoints(10, "Traded with NPC.");

                SoundEffectManager.PlaySound("success_small");
                Screen.TextBox.Show(message1 + "*" + Core.Player.Name + " traded~" + oppPokemon.OriginalName + " for~" + ownPokemon.OriginalName + "!", new  Entity[] { }, false, false);
            }
            else
            {
                Screen.TextBox.Show(message2, new Entity[] { }, false, false);
            }
        }

        public Script Clone()
        {
            return new Script(this.ScriptLine, this.Level);
        }

        public static List<string> ParseArguments(string inputString, char SeparatorChar = ',')
        {
            List<string> arguments = new List<string>();
            bool stringDeclaration = false;
            string data = inputString;
            string cArg = "";

            while (data.Length > 0)
            {
                if (data[0] == SeparatorChar)
                {
                    if (stringDeclaration)
                    {
                        cArg += data[0].ToString();
                    }
                    else
                    {
                        arguments.Add(cArg);
                        cArg = "";
                    }
                }
                else if (data[0] == '\"')
                {
                    if (!stringDeclaration)
                    {
                        stringDeclaration = true;
                    }
                    else
                    {
                        if (data.Length == 1 || data[1] != data[0])
                        {
                            stringDeclaration = !stringDeclaration;
                        }
                        else if (data.Length > 1 && data[1] == data[0])
                        {
                            if (stringDeclaration)
                            {
                                cArg += "\"";
                            }
                            data = data.Remove(0, 1);
                        }
                    }
                }
                else
                {
                    cArg += data[0].ToString();
                }

                data = data.Remove(0, 1);
            }

            arguments.Add(cArg);

            return arguments;
        }
    }
}

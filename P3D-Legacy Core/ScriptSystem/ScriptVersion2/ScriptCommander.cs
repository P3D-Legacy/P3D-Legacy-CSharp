using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.ScriptSystem.ScriptVersion2
{
    partial class ScriptCommander
    {
        static ScriptV2 ScriptV2;
        //Stores a value that the ScriptCommander keeps across script calls and scripts.
        static string Value = "";

        public static object Parse(string input)
        {
            return ScriptComparer.EvaluateConstruct(input);
        }

        /// <summary>
        /// If the script finished executing. If false, the script will get executed next frame.
        /// </summary>
        private static bool IsReady
        {
            get { return ScriptV2.IsReady; }
            set { ScriptV2.IsReady = value; }
        }

        /// <summary>
        /// A value to indicate if the script has been started last frame. Not automatically set. Sometimes needed for when a script runs longer than one frame.
        /// </summary>
        private static bool Started
        {
            get { return ScriptV2.started; }
            set { ScriptV2.started = value; }
        }

        /// <summary>
        /// If the ScriptController can execute the next script in the same frame once this finishes.
        /// </summary>
        private static bool CanContinue
        {
            get { return ScriptV2.CanContinue; }
            set { ScriptV2.CanContinue = value; }
        }

        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <param name="ScriptV2">The primitive script (v2).</param>
        /// <param name="inputString">The input command.</param>
        public static void ExecuteCommand(ref ScriptV2 ScriptV2, string inputString)
        {
            ScriptCommander.ScriptV2 = ScriptV2;

            string classValue = inputString;

            string mainClass = classValue;
            string subClass = "";

            int bIndex = classValue.IndexOf("(");
            if (classValue.Contains("(") == false)
            {
                bIndex = -1;
            }

            int pIndex = classValue.IndexOf(".");

            if (classValue.Contains(".") == true & (pIndex < bIndex | bIndex == -1) == true)
            {
                mainClass = classValue.Remove(classValue.IndexOf("."));
                subClass = classValue.Remove(0, classValue.IndexOf(".") + 1);
            }
            else
            {
                if (classValue.Contains("(") == true)
                {
                    mainClass = classValue.Remove(classValue.IndexOf("("));
                    subClass = classValue.Remove(0, classValue.IndexOf("(") + 1);
                }
            }

            switch (mainClass.ToLower())
            {
                case "register":
                    DoRegister(subClass);
                    break;
                case "script":
                    DoScript(subClass);
                    break;
                case "screen":
                    DoScreen(subClass);
                    break;
                case "player":
                    if (InsertSpin(inputString) == false)
                    {
                        DoPlayer(subClass);
                    }
                    break;
                case "music":
                    DoMusic(subClass);
                    break;
                case "sound":
                    DoSound(subClass);
                    break;
                case "entity":
                    if (InsertSpin(inputString) == false)
                    {
                        DoEntity(subClass);
                    }
                    break;
                case "battle":
                    DoBattle(subClass);
                    break;
                case "pokemon":
                    DoPokemon(subClass);
                    break;
                case "overworldpokemon":
                    DoOverworldPokemon(subClass);
                    break;
                case "environment":
                    DoEnvironment(subClass);
                    break;
                case "text":
                    if (InsertSpin(inputString) == false)
                    {
                        DoText(subClass);
                    }
                    break;
                case "options":
                    if (InsertSpin(inputString) == false)
                    {
                        DoOptions(subClass);
                    }
                    break;
                case "level":
                    DoLevel(subClass);
                    break;
                case "camera":
                    if (InsertSpin(inputString) == false)
                    {
                        DoCamera(subClass);
                    }
                    break;
                case "item":
                    DoItem(subClass);
                    break;
                case "storage":
                    DoStorage(subClass);
                    break;
                case "npc":
                    if (InsertSpin(inputString) == false)
                    {
                        DoNPC(subClass);
                    }
                    break;
                case "chat":
                    DoChat(subClass);
                    break;
                case "daycare":
                    DoDayCare(subClass);
                    break;
                case "pokedex":
                    DoPokedex(subClass);
                    break;
                case "radio":
                    DoRadio(subClass);
                    break;
                case "help":
                    DoHelp(subClass);
                    break;
                case "title":
                    DoTitle(subClass);
                    break;
                default:
                    Logger.Log(Logger.LogTypes.Message, "ScriptCommander.vb: This class (" + mainClass + ") doesn't exist.");
                    IsReady = true;
                    break;
            }
        }
        //crash handle

        /// <summary>
        /// Generates a script line that gets inserted infront of the current script to turn the player into the correct orientation.
        /// </summary>
        private static bool InsertSpin(string inputString)
        {
            if (ActionScript.TempSpin == true)
            {
                if (ActionScript.TempInputDirection > -1)
                {
                    if (inputString.ToLower().StartsWith("player.turnto(") == false)
                    {
                        if (Screen.Camera.GetPlayerFacingDirection() != ActionScript.TempInputDirection)
                        {
                            if (((BaseOverworldCamera)Screen.Camera).ThirdPerson == false)
                            {
                                ((BaseOverworldScreen)Core.CurrentScreen).ActionScript.Scripts.Insert(0, new Script("@player.turnto(" + ActionScript.TempInputDirection + ")", ActionScript.ScriptLevelIndex));
                                return true;
                            }
                        }
                    }
                    ActionScript.TempInputDirection = -1;
                    ActionScript.TempSpin = false;
                }
            }
            return false;
        }

        /// <summary>
        /// Opens help content from the ScriptLibrary.
        /// </summary>
        /// <param name="subClass">The subClass used in @help().</param>
        private static void DoHelp(string subClass)
        {
            if (subClass.EndsWith(")") == true)
            {
                subClass = subClass.Remove(subClass.Length - 1, 1);
            }
            Chat.AddLine(new Chat.ChatMessage("[HELP]", ScriptLibrary.GetHelpContent(subClass, 20), "0", Chat.ChatMessage.MessageTypes.CommandMessage));

            IsReady = true;
        }


        ////////////////////////////////////////////////////////////
        ////
        //// Shortens the ScriptConversion methods to shorter names.
        ////
        ////////////////////////////////////////////////////////////

        private static int @int(object expression)
        {
            return ScriptConversion.ToInteger(expression);
        }

        private static float sng(object expression)
        {
            return ScriptConversion.ToSingle(expression);
        }

        private static double dbl(object expression)
        {
            return ScriptConversion.ToDouble(expression);
        }

    }
}

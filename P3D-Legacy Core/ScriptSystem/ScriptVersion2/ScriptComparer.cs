using System;
using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Core.Resources;

namespace P3D.Legacy.Core.ScriptSystem.ScriptVersion2
{
    partial class ScriptComparer
    {

        #region "DefaultReturn"

        /// <summary>
        /// Represents the default void return, if the contruct could not return anything else.
        /// </summary>
        public class DefaultNullObj
        {

            public override string ToString()
            {
                return "return:void";
                //Just return "void" when this gets used as string to indicate that this type got returned.
            }

        }

        private static readonly DefaultNullObj _defaultNull = new DefaultNullObj();
        public static DefaultNullObj DefaultNull
        {
            get { return _defaultNull; }
        }

        #endregion

        public struct PairValue
        {
            public string Command;
            public string Argument;
        }

        /// <summary>
        /// Evaluates a script comparison between two expressions.
        /// </summary>
        /// <param name="inputString">The string containing the expression to compare.</param>
        public static bool EvaluateScriptComparison(string inputString)
        {
            return EvaluateScriptComparison(inputString, false);
        }

        /// <summary>
        /// Evaluates a script comparison between two expressions.
        /// </summary>
        /// <param name="inputString">The string containing the expression to compare.</param>
        /// <param name="caseSensitive">If the case of the strings should be evaluated.</param>
        public static bool EvaluateScriptComparison(string inputString, bool caseSensitive)
        {
            string comparer = "=";

            int level = 0;
            string setComparer = "";
            string lastComparer = "";
            int comparerIndex = inputString.IndexOf("=");
            int lastComparerIndex = 0;
            int i = 0;

            int countStarts = 0;
            foreach (char c in inputString)
            {
                switch (c)
                {
                    case '<':
                        countStarts += 1;
                        break;
                    case '>':
                        countStarts -= 1;
                        break;
                }
            }

            if (countStarts < 0)
            {
                setComparer = ">";
            }
            else if (countStarts > 0)
            {
                setComparer = "<";
            }
            else
            {
                setComparer = "=";
            }

            if (setComparer == ">")
            {
                foreach (char c in inputString)
                {
                    switch (c)
                    {
                        case '<':
                            level += 1;
                            break;
                        case '>':
                            level -= 1;
                            if (level == -1)
                            {
                                comparerIndex = i;
                                break; // TODO: might not be correct. Was : Exit For
                            }
                            break;
                    }
                    i += 1;
                }
            }
            else if (setComparer == "<")
            {
                List<int> started = new List<int>();
                foreach (char c in inputString)
                {
                    switch (c)
                    {
                        case '<':
                            started.Add(i);
                            break;
                        case '>':
                            started.RemoveAt(started.Count - 1);
                            break;
                    }

                    i += 1;
                }
                comparerIndex = started[0];
            }

            if (!string.IsNullOrEmpty(setComparer))
            {
                comparer = setComparer;
            }
            else
            {
                comparerIndex = inputString.IndexOf("=");
            }

            object compareValue = inputString.Substring(comparerIndex + 1);
            string classValue = inputString.Substring(0, comparerIndex);

            object resultValue = EvaluateConstruct(classValue);

            compareValue = EvaluateConstruct(compareValue);

            bool comparisonResult = false;

            switch (comparer)
            {
                case "=":
                    if (caseSensitive == true | resultValue as string == null | compareValue as string == null)
                    {
                        if (resultValue.Equals(compareValue))
                        {
                            comparisonResult = true;
                        }
                    }
                    else
                    {
                        if (ScriptConversion.IsBoolean(Convert.ToString(resultValue)) == true & ScriptConversion.IsBoolean(Convert.ToString(compareValue)) == true)
                        {
                            if (ScriptConversion.ToBoolean(resultValue) == ScriptConversion.ToBoolean(compareValue))
                            {
                                comparisonResult = true;
                            }
                        }
                        else
                        {
                            if (Convert.ToString(resultValue).ToLower() == Convert.ToString(compareValue).ToLower())
                            {
                                comparisonResult = true;
                            }
                        }
                    }
                    break;
                case ">":
                    if (Information.IsNumeric(resultValue) == true & Information.IsNumeric(compareValue) == true)
                    {
                        if (dbl(resultValue) > dbl(compareValue))
                        {
                            comparisonResult = true;
                        }
                    }
                    break;
                case "<":
                    if (Information.IsNumeric(resultValue) == true & Information.IsNumeric(compareValue) == true)
                    {
                        if (dbl(resultValue) < dbl(compareValue))
                        {
                            comparisonResult = true;
                        }
                    }
                    break;
            }

            return comparisonResult;
        }

        /// <summary>
        /// Evaluates a complete construct.
        /// </summary>
        /// <param name="construct">
        /// <para>The complete construct. Example: &lt;mainclass.subclass(argument)&gt;.</para>
        /// <para>If a &lt;not&gt; is put directly in front of the construct, the result will be negated.</para>
        /// </param>
        public static object EvaluateConstruct(object construct)
        {
            if (construct as string != null)
            {
                if (string.IsNullOrEmpty(Convert.ToString(construct)))
                {
                    return "";
                }

                string output = "";
                string input = construct.ToString();

                bool foundNOT = false;

                while (input.Length > 0)
                {
                    char c = FileSystem.input(0);
                    int endIndex = 0;

                    if (c == '<')
                    {
                        int level = 0;
                        input = input.Remove(0, 1);

                        for (var i = 0; i <= input.Length - 1; i++)
                        {
                            if (FileSystem.input(i) == "<")
                            {
                                level += 1;
                            }
                            if (FileSystem.input(i) == ">")
                            {
                                if (level > 0)
                                {
                                    level -= 1;
                                }
                                else
                                {
                                    endIndex = i;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                        }

                        string arg = input.Substring(0, endIndex);
                        input = input.Remove(0, endIndex + 1);

                        string classValue = Convert.ToString(arg);

                        if (classValue.StartsWith("not ") == true)
                        {
                            classValue = classValue.Remove(0, 4);
                            foundNOT = true;
                        }

                        string mainClass = classValue.Remove(classValue.IndexOf("."));
                        string subClass = classValue.Remove(0, classValue.IndexOf(".") + 1);

                        object resultValue = GetConstructReturnValue(mainClass, subClass);

                        if (resultValue.Equals(DefaultNull))
                        {
                            Logger.Log(Logger.LogTypes.Warning, string.Format("No value was returned from a construct. mainclass: {0}; subclass: {1}", mainClass, subClass));
                            resultValue = arg;
                        }

                        if (foundNOT == true)
                        {
                            string[] bools = {
                                "false",
                                "true"
                            };
                            if (bools.Contains(resultValue.ToString().ToLower()) == true)
                            {
                                switch (resultValue.ToString().ToLower())
                                {
                                    case "false":
                                        resultValue = "true";
                                        break;
                                    case "true":
                                        resultValue = "false";
                                        break;
                                }
                            }
                        }
                        foundNOT = false;

                        output += resultValue.ToString();
                    }
                    else
                    {
                        output += FileSystem.input(0);
                        input = input.Remove(0, 1);
                    }
                }

                return output;
            }
            return construct;
        }

        /// <summary>
        /// Returns a SubClass and Argument as Pair.
        /// </summary>
        /// <param name="inputString">The string to deconstruct. Example: command(argument)</param>
        public static PairValue GetSubClassArgumentPair(string inputString)
        {
            PairValue p = new PairValue();

            string command = inputString;
            string argument = "";

            if (command.Contains("(") == true & command.EndsWith(")") == true)
            {
                argument = command.Remove(0, command.IndexOf("(") + 1);
                argument = argument.Remove(argument.Length - 1, 1);
                command = command.Remove(command.IndexOf("("));
            }

            argument = Convert.ToString(EvaluateConstruct(argument));

            p.Command = command;
            p.Argument = argument;

            return p;
        }

        /// <summary>
        /// Returns a result for a construct.
        /// </summary>
        /// <param name="mainClass">The main class of the contruct.</param>
        /// <param name="subClass">The sub class of the construct.</param>
        private static object GetConstructReturnValue(string mainClass, string subClass)
        {
            switch (mainClass.ToLower())
            {
                case "pokemon":
                    return DoPokemon(subClass);
                case "overworldpokemon":
                    return DoOverworldPokemon(subClass);
                case "player":
                    return DoPlayer(subClass);
                case "environment":
                    return DoEnvironment(subClass);
                case "register":
                    return DoRegister(subClass);
                case "system":
                    return DoSystem(subClass);
                case "npc":
                    return DoNPC(subClass);
                case "inventory":
                    return DoInventory(subClass);
                case "storage":
                    return DoStorage(subClass);
                case "phone":
                    return DoPhone(subClass);
                case "entity":
                    return DoEntity(subClass);
                case "level":
                    return DoLevel(subClass);
                case "battle":
                    return DoBattle(subClass);
                case "daycare":
                    return DoDaycare(subClass);
                case "rival":
                    return DoRival(subClass);
                case "math":
                    return DoMath(subClass);
                case "pokedex":
                    return DoPokedex(subClass);
                case "radio":
                    return DoRadio(subClass);
                case "camera":
                    return DoCamera(subClass);
                case "filesystem":
                    return DoFileSystem(subClass);
            }
            return DefaultNull;
        }

        /// <summary>
        /// Returns a string for a boolean.
        /// </summary>
        /// <param name="bool">The boolean to convert.</param>
        public static string ReturnBoolean(bool @bool) => @bool == true ? "true" : "false";


        ////////////////////////////////////////////////////////////
        ////
        //// Shortens the ScriptConversion methods to shorter names.
        ////
        ////////////////////////////////////////////////////////////

        private static int @int(object expression) => ScriptConversion.ToInteger(expression);
        private static double sng(object expression) => ScriptConversion.ToSingle(expression);
        private static double dbl(object expression) => ScriptConversion.ToDouble(expression);
    }
}

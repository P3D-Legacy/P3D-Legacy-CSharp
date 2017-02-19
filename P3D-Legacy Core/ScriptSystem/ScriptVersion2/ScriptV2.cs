using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Screens;
using System.Collections.Generic;
using System.Linq;

namespace P3D.Legacy.Core.ScriptSystem.ScriptVersion2
{
    public class ScriptV2
    {
        public enum ScriptTypes : int
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


        public ScriptTypes ScriptType;

        public string Value = "";
        public bool started = false;
        public bool IsReady = false;
        public bool CanContinue = true;

        public string RawScriptLine = "";
        public void Initialize(string scriptLine)
        {
            this.RawScriptLine = scriptLine;

            char firstChar = scriptLine[0];
            switch (firstChar)
            {
                case '@':
                    this.ScriptType = ScriptTypes.Command;
                    this.Value = scriptLine.Remove(0, 1);
                    break;
                case ':':
                    string structureType = scriptLine.Remove(0, 1);

                    if (structureType.Contains(":") == true)
                    {
                        this.Value = structureType.Remove(0, structureType.IndexOf(":") + 1);
                        structureType = structureType.Remove(structureType.IndexOf(":"));
                    }

                    switch (structureType.ToLower())
                    {
                        case "if":
                            this.ScriptType = ScriptTypes.@if;
                            break;
                        case "when":
                            this.ScriptType = ScriptTypes.when;
                            break;
                        case "then":
                            this.ScriptType = ScriptTypes.then;
                            break;
                        case "else":
                            this.ScriptType = ScriptTypes.@else;
                            break;
                        case "endif":
                            this.ScriptType = ScriptTypes.endif;
                            break;
                        case "end":
                            this.ScriptType = ScriptTypes.end;
                            break;
                        case "select":
                            this.ScriptType = ScriptTypes.@select;
                            break;
                        case "endwhen":
                            this.ScriptType = ScriptTypes.endwhen;
                            break;
                        case "return":
                            this.ScriptType = ScriptTypes.@return;
                            break;
                        case "endscript":
                            this.ScriptType = ScriptTypes.endscript;
                            break;
                        case "while":
                            this.ScriptType = ScriptTypes.@while;
                            break;
                        case "endwhile":
                            this.ScriptType = ScriptTypes.endwhile;
                            break;
                        case "exitwhile":
                            this.ScriptType = ScriptTypes.exitwhile;
                            break;
                        default:
                            LogIllegalLine(scriptLine);
                            this.IsReady = true;
                            break;
                    }
                    this.CanContinue = true;
                    break;
                case '#':
                    this.ScriptType = ScriptTypes.Comment;
                    this.Value = scriptLine.Remove(0, 1);
                    this.CanContinue = true;
                    break;
                default:
                    LogIllegalLine(scriptLine);
                    this.IsReady = true;
                    this.CanContinue = true;
                    break;
            }
        }

        private void LogIllegalLine(string scriptLine)
        {
            Logger.Log(Logger.LogTypes.Message, "Illegal script line detected (" + scriptLine + ")");
        }

        public void EndScript(bool forceEnd)
        {
            ActionScript.ScriptLevelIndex -= 1;
            if (ActionScript.ScriptLevelIndex == -1 | forceEnd == true)
            {
                ActionScript.ScriptLevelIndex = -1;
                BaseOverworldScreen oS = (BaseOverworldScreen)Core.CurrentScreen;
                oS.ActionScript.Scripts.Clear();
                oS.ActionScript.reDelay = 1f;
                this.IsReady = true;
                Screen.TextBox.ReDelay = 1f;
                ActionScript.TempInputDirection = -1;
                ActionScript.TempSpin = false;
            }
        }

        public void Update()
        {
            switch (this.ScriptType)
            {
                case ScriptTypes.Command:
                    this.DoCommand();

                    break;
                case ScriptTypes.@if:
                    this.DoIf();
                    break;
                case ScriptTypes.then:
                    this.IsReady = true;
                    break;
                case ScriptTypes.@else:
                {
                    BaseOverworldScreen oS = (BaseOverworldScreen) Core.CurrentScreen;
                    oS.ActionScript.ChooseIf(true);

                    this.IsReady = true;
                }
                    break;
                case ScriptTypes.endif:
                {
                    BaseOverworldScreen oS = (BaseOverworldScreen) Core.CurrentScreen;
                    oS.ActionScript.ChooseIf(true);

                    this.IsReady = true;
                }
                    break;
                case ScriptTypes.@while:
                    this.DoWhile();
                    break;
                case ScriptTypes.endwhile:
                    this.IsReady = true;
                    break;
                case ScriptTypes.exitwhile:
                    this.DoExitWhile();

                    break;
                case ScriptTypes.@select:
                    this.DoSelect();
                    break;
                case ScriptTypes.when:
                {
                    BaseOverworldScreen oS = (BaseOverworldScreen) Core.CurrentScreen;
                    oS.ActionScript.Switch("");
                    this.IsReady = true;
                }

                    break;
                case ScriptTypes.endwhen:
                {
                    BaseOverworldScreen oS = (BaseOverworldScreen) Core.CurrentScreen;
                    oS.ActionScript.Switch("");
                    this.IsReady = true;
                }
                    break;
                case ScriptTypes.end:
                    this.EndScript(false);
                    break;
                case ScriptTypes.endscript:
                    this.EndScript(true);

                    break;
                case ScriptTypes.@return:
                    this.DoReturn();
                    this.EndScript(false);

                    break;
                case ScriptTypes.Comment:
                    Logger.Debug("ScriptV2.vb: #Comment: \"" + this.Value + "\"");
                    this.IsReady = true;
                    break;
            }
        }

        private void DoWhile()
        {
            //if T equals false, go straight to :endwhile and clear any existing while query, else initialize a while query that collects every single script line that gets executed starting with :while and ending with :endwhile
            //a exitwhile directly goes to :endwhile and clears the query
            bool T = CheckCondition();

            if (T == true)
            {
                ActionScript.CSL().WhileQuery.Clear();
                ActionScript.CSL().WhileQueryInitialized = true;
            }
            else
            {
                ActionScript.CSL().WhileQuery.Clear();
                ActionScript.CSL().WhileQueryInitialized = false;

                //Skip to endwhile:

                BaseOverworldScreen oS = (BaseOverworldScreen)Core.CurrentScreen;
                while (oS.ActionScript.Scripts.Count > 0 && oS.ActionScript.Scripts[0].ScriptV2.ScriptType != ScriptTypes.endwhile)
                {
                    oS.ActionScript.Scripts.RemoveAt(0);
                }
            }

            this.IsReady = true;
        }

        private void DoExitWhile()
        {
            ActionScript.CSL().WhileQuery.Clear();
            ActionScript.CSL().WhileQueryInitialized = true;

            //Skip to endwhile:

            BaseOverworldScreen oS = (BaseOverworldScreen)Core.CurrentScreen;
            while (oS.ActionScript.Scripts.Count > 0 && oS.ActionScript.Scripts[0].ScriptV2.ScriptType != ScriptTypes.endwhile)
            {
                oS.ActionScript.Scripts.RemoveAt(0);
            }

            this.IsReady = true;
        }

        private void DoIf()
        {
            bool T = CheckCondition();

            ActionScript.CSL().WaitingEndIf[ActionScript.CSL().IfIndex + 1] = false;
            ActionScript.CSL().CanTriggerElse[ActionScript.CSL().IfIndex + 1] = false;

            BaseOverworldScreen oS = (BaseOverworldScreen)Core.CurrentScreen;

            oS.ActionScript.ChooseIf(T);

            this.IsReady = true;
        }

        private bool CheckCondition()
        {
            string check = Value;
            bool T = false;
            bool convertNextValue = false;

            List<List<string>> ors = new List<List<string>>();

            List<string> currentOr = new List<string>();
            while (check.Contains(" <and>") == true | check.Contains(" <or> ") == true)
            {
                if (check.StartsWith(" <and> ") == true)
                {
                    check = check.Remove(0, " <and> ".Length);
                }
                else if (check.StartsWith(" <or> ") == true)
                {
                    List<string> newOr = new List<string>();
                    newOr.AddRange(currentOr.ToArray());

                    ors.Add(newOr);
                    currentOr = new List<string>();

                    check = check.Remove(0, " <or> ".Length);
                }
                else
                {
                    if (check.StartsWith("<not><") == true)
                    {
                        convertNextValue = true;
                        check = check.Remove(0, "<not>".Length);
                    }

                    int nextStop = 0;

                    int andStop = -1;
                    int orStop = -1;

                    if (check.Contains(" <and> ") == true)
                    {
                        andStop = check.IndexOf(" <and> ");
                    }
                    if (check.Contains(" <or> ") == true)
                    {
                        orStop = check.IndexOf(" <or> ");
                    }

                    if (andStop > -1 & orStop == -1)
                    {
                        nextStop = andStop;
                    }
                    else if (orStop > -1 & andStop == -1)
                    {
                        nextStop = orStop;
                    }
                    else
                    {
                        if (andStop < orStop)
                        {
                            nextStop = andStop;
                        }
                        else
                        {
                            nextStop = orStop;
                        }
                    }

                    string newCheck = check.Remove(nextStop);

                    if (convertNextValue == true)
                    {
                        convertNextValue = false;
                        newCheck = "<not>" + newCheck;
                    }

                    currentOr.Add(newCheck);

                    check = check.Remove(0, nextStop);
                }
            }
            currentOr.Add(check);

            ors.Add(currentOr);

            List<bool> results = new List<bool>();
            foreach (List<string> checkOR in ors)
            {
                bool vT = true;
                foreach (string c in checkOR)
                {
                    bool b = false;
                    if (c.StartsWith("<not>") == true)
                    {
                        c = c.Remove(0, "<not>".Length);
                        b = true;
                    }
                    bool v = ScriptVersion2.ScriptComparer.EvaluateScriptComparison(c);
                    if (b == true)
                    {
                        b = false;
                        v = !v;
                    }
                    if (v == false)
                    {
                        vT = false;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                results.Add(vT);
            }

            foreach (bool result in results)
            {
                if (result == true)
                {
                    T = true;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }

            return T;
        }

        private void DoSelect()
        {
            ActionScript.CSL().WhenIndex += 1;

            BaseOverworldScreen oS = (BaseOverworldScreen)Core.CurrentScreen;
            oS.ActionScript.Switch(ScriptVersion2.ScriptComparer.EvaluateConstruct(Value));

            this.IsReady = true;
        }

        private void DoCommand()
        {
            ScriptVersion2.ScriptCommander.ExecuteCommand(this, this.Value);
        }


        public static string TempReturn = "NULL";
        private void DoReturn()
        {
            TempReturn = ScriptVersion2.ScriptComparer.EvaluateConstruct(this.Value).ToString();
        }

    }
}

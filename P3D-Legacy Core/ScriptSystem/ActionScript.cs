using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Screens;
using P3D.Legacy.Core.World;

namespace P3D.Legacy.Core.ScriptSystem
{
    public class ActionScript
    {
        public List<Script> Scripts = new List<Script>();
        public class ScriptLevel
        {
            public List<bool> WaitingEndWhen = new List<bool>();
            public List<bool> Switched = new List<bool>();
            public List<bool> WaitingEndIf = new List<bool>();
            public List<bool> CanTriggerElse = new List<bool>();
            public int IfIndex;

            public int WhenIndex;
            public List<Script> WhileQuery = new List<Script>();

            public bool WhileQueryInitialized = false;
            public int ScriptVersion;
            public int CurrentLine;
            public string ScriptName;
        }

        /// <summary>
        /// Returns the current ScriptLevel based on the script index.
        /// </summary>
        public static ScriptLevel CSL()
        {
            return ScriptLevels[ScriptLevelIndex];
        }

        public static ScriptLevel[] ScriptLevels = new ScriptLevel[100];

        public static int ScriptLevelIndex = -1;

        public float reDelay = 0f;

        ILevel LevelRef;
        public ActionScript(ILevel LevelRef)
        {
            this.LevelRef = LevelRef;
        }

        public static int TempInputDirection = -1;

        public static bool TempSpin = false;
        public void Update()
        {
            nextScript:
            bool unlock = this.IsReady;

            if (Scripts.Count > 0)
            {
                Scripts[0].Update();
            }

            for (var i = 0; i <= Scripts.Count - 1; i++)
            {
                if (i <= Scripts.Count - 1)
                {
                    Script s = Scripts[i];

                    if (s.IsReady == true)
                    {
                        i -= 1;

                        this.AddToWhileQuery(s);
                        Scripts.Remove(s);
                        ScriptLevels[s.Level].CurrentLine += 1;

                        if (IsReady == false & s.CanContinue == true)
                        {
                            goto nextScript;
                        }
                    }
                }
            }

            if (this.IsReady == true)
            {
                if (unlock == false)
                {
                    Logger.Debug("Unlock Camera");
                    ((BaseOverworldCamera)Screen.Camera).YawLocked = false;
                    ((BaseOverworldCamera)Screen.Camera).ResetCursor();
                }
                if (reDelay > 0f)
                {
                    reDelay -= 0.1f;

                    if (reDelay <= 0f)
                    {
                        reDelay = 0f;
                    }
                }
            }
        }

        public bool IsReady
        {
            get
            {
                if (Scripts.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }


        public static bool IsInsightScript = false;
        /// <summary>
        /// Starts a script
        /// </summary>
        /// <param name="Input">The input string</param>
        /// <param name="InputType">Type of information; 0: Script path, 1: Text, 2: Direct input</param>
        public void StartScript(string Input, int InputType, bool CheckDelay = true, bool ResetInsight = true)
        {
            ScriptLevelIndex += 1;

            TempSpin = false;

            bool[] arr = new bool[100];
            ScriptLevels[ScriptLevelIndex] = new ScriptLevel
            {
                IfIndex = 0,
                WhenIndex = 0,
                ScriptVersion = 1,
                WaitingEndIf = arr.ToList(),
                WaitingEndWhen = arr.ToList(),
                CurrentLine = 0,
                ScriptName = "No script running",
                CanTriggerElse = arr.ToList(),
                Switched = arr.ToList()
            };

            ScriptLevel l = ScriptLevels[ScriptLevelIndex];

            if (ResetInsight == true)
            {
                IsInsightScript = false;
            }

            if (reDelay == 0f | CheckDelay == false)
            {
                switch (InputType)
                {
                    case 0:
                    {
                        //Start script from file
                        Logger.Debug("Start script (ID: " + Input + ")");
                        l.ScriptName = "Type: Script; Input: " + Input;

                        string path = GameModeManager.GetScriptPath(Input + ".dat");
                        Security.FileValidation.CheckFileValid(path, false, "ActionScript.vb");


                        if (System.IO.File.Exists(path) == true)
                        {
                            string Data = System.IO.File.ReadAllText(path);

                            Data = Data.Replace(Constants.vbNewLine, "^");
                            string[] ScriptData = Data.Split(Convert.ToChar("^"));

                            AddScriptLines(ScriptData);
                        }
                        else
                        {
                            Logger.Log(Logger.LogTypes.ErrorMessage, "ActionScript.vb: The script file \"" + path + "\" doesn't exist!");
                        }
                    }
                        break;
                    case 1:
                    {
                        //Display text
                        Logger.Debug("Start Script (Text: " + Input + ")");
                        l.ScriptName = "Type: Text; Input: " + Input;

                        string Data = "version=2^@text.show(" + Input + ")^" + ":end";

                        string[] ScriptData = Data.Split(Convert.ToChar("^"));

                        AddScriptLines(ScriptData);
                    }
                        break;
                    case 2:
                    {
                        //Start script from direct input
                        string activator = Environment.StackTrace.Split(new [] { Environment.NewLine }, StringSplitOptions.None)[3];
                        activator = activator.Remove(activator.IndexOf("(", StringComparison.Ordinal));

                        Logger.Debug("Start Script (DirectInput; " + activator + ")");
                        l.ScriptName = "Type: Direct; Input: " + Input;

                        string Data = Input.Replace(Constants.vbNewLine, "^");

                        string[] ScriptData = Data.Split(Convert.ToChar("^"));

                        AddScriptLines(ScriptData);
                    }
                        break;
                }
            }
        }

        private void AddScriptLines(string[] ScriptData)
        {
            int i = 0;
            ScriptLevel l = ScriptLevels[ScriptLevelIndex];
            for (var index = 0; index < ScriptData.Length; index++)
            {
                if (i == 0 & ScriptData[index].ToLower().StartsWith("version="))
                {
                    l.ScriptVersion = Convert.ToInt32(ScriptData[index].Remove(0, ("version=").Length));
                    l.CurrentLine += 1;
                }
                else
                {
                    while (ScriptData[index].StartsWith(" ") | ScriptData[index].StartsWith("\t"))
                    {
                        ScriptData[index] = ScriptData[index].Remove(0, 1);
                    }
                    while (ScriptData[index].EndsWith(" ") | ScriptData[index].EndsWith("\t"))
                    {
                        ScriptData[index] = ScriptData[index].Remove(ScriptData[index].Length - 1, 1);
                    }
                    if (!string.IsNullOrEmpty(ScriptData[index]))
                    {
                        this.Scripts.Insert(i, new Script(ScriptData[index], ScriptLevelIndex));
                        i += 1;
                    }
                }
            }
        }

        public void Switch(object Answer)
        {
            ScriptLevel l = ScriptLevels[ScriptLevelIndex];
            bool proceed = false;
            bool first = true;

            while (proceed == false)
            {
                if (Scripts.Count == 0)
                {
                    Logger.Log(Logger.LogTypes.Warning, "ActionScript.vb: Illegal \":when\" construct. Terminating execution.");
                    break; // TODO: might not be correct. Was : Exit While
                }

                Script s = Scripts[0];

                switch (s.ScriptType)
                {
                    case Script.ScriptTypes.@select:
                        if (first == false)
                        {
                            l.WhenIndex += 1;
                            l.WaitingEndWhen[l.WhenIndex] = true;
                            l.Switched[l.WhenIndex] = true;
                        }
                        break;
                    case Script.ScriptTypes.Command:
                        if (s.ScriptV2.Value.ToLower().StartsWith("options.show(") == true & first == false)
                        {
                            l.WhenIndex += 1;
                            l.WaitingEndWhen[l.WhenIndex] = true;
                            l.Switched[l.WhenIndex] = true;
                        }
                        break;
                    case Script.ScriptTypes.SwitchWhen:
                    case Script.ScriptTypes.when:
                        if (l.Switched[l.WhenIndex] == false)
                        {
                            bool equal = false;
                            string[] args = Scripts[0].Value.Split(Convert.ToChar(";"));

                            foreach (string arg in args)
                            {
                                if ((ScriptVersion2.ScriptComparer.EvaluateConstruct(arg).Equals(ScriptVersion2.ScriptComparer.EvaluateConstruct(Answer))) == true)
                                {
                                    equal = true;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }

                            if (equal == true)
                            {
                                l.WaitingEndWhen[l.WhenIndex] = false;
                                proceed = true;
                            }
                            else
                            {
                                l.WaitingEndWhen[l.WhenIndex] = true;
                            }
                        }
                        break;
                    case Script.ScriptTypes.SwitchEndWhen:
                    case Script.ScriptTypes.endwhen:
                        l.WaitingEndWhen[l.WhenIndex] = false;
                        l.Switched[l.WhenIndex] = false;
                        l.WhenIndex -= 1;
                        if (l.WaitingEndWhen[l.WhenIndex] == false)
                        {
                            proceed = true;
                        }
                        break;
                }

                this.AddToWhileQuery(Scripts[0]);
                Scripts.RemoveAt(0);
                l.CurrentLine += 1;
                first = false;
            }
        }

        public void ChooseIf(bool T)
        {
            ScriptLevel l = ScriptLevels[ScriptLevelIndex];
            bool proceed = false;

            while (proceed == false)
            {
                if (Scripts.Count == 0)
                {
                    Logger.Log(Logger.LogTypes.Warning, "ActionScript.vb: Illegal \":if\" construct. Terminating execution.");
                    break; // TODO: might not be correct. Was : Exit While
                }
                Script s = Scripts[0];

                switch (s.ScriptType)
                {
                    case Script.ScriptTypes.@if:
                    case Script.ScriptTypes.SwitchIf:
                        l.IfIndex += 1;
                        if (l.WaitingEndIf[l.IfIndex - 1] == true)
                        {
                            l.WaitingEndIf[l.IfIndex] = true;
                            l.CanTriggerElse[l.IfIndex] = false;
                        }
                        else
                        {
                            if (T == true)
                            {
                                proceed = true;
                                l.WaitingEndIf[l.IfIndex] = false;
                                l.CanTriggerElse[l.IfIndex] = false;
                            }
                            else
                            {
                                l.WaitingEndIf[l.IfIndex] = true;
                                l.CanTriggerElse[l.IfIndex] = true;
                            }
                        }
                        break;
                    case Script.ScriptTypes.@else:
                    case Script.ScriptTypes.SwitchElse:
                        if (l.CanTriggerElse[l.IfIndex] == true)
                        {
                            l.WaitingEndIf[l.IfIndex] = false;
                            proceed = true;
                        }
                        else
                        {
                            l.WaitingEndIf[l.IfIndex] = true;
                        }
                        break;
                    case Script.ScriptTypes.endif:
                    case Script.ScriptTypes.SwitchEndIf:
                        l.IfIndex -= 1;
                        if (l.WaitingEndIf[l.IfIndex] == false)
                        {
                            proceed = true;
                        }
                        break;
                }

                this.AddToWhileQuery(Scripts[0]);
                Scripts.RemoveAt(0);
                l.CurrentLine += 1;
            }

            if (Scripts.Count > 0)
            {
                Scripts[0].Update();
            }
        }

        public void AddToWhileQuery(Script RemovedScript)
        {
            if (CSL().WhileQueryInitialized == true & CSL().ScriptVersion == 2)
            {
                CSL().WhileQuery.Add(RemovedScript);

                if (RemovedScript.ScriptV2.ScriptType == ScriptV2.ScriptTypes.endwhile)
                {
                    int i = 0;

                    foreach (Script s in CSL().WhileQuery)
                    {
                        this.Scripts.Insert(i, s.Clone());
                        i += 1;
                    }

                    CSL().WhileQuery.Clear();
                    CSL().WhileQueryInitialized = false;
                }
            }
        }

        #region "Registers"

        public static bool IsRegistered(string i)
        {
            CheckTimeBasedRegisters();
            if (Core.Player.RegisterData.Contains(",") == true)
            {
                string[] Data = Core.Player.RegisterData.Split(Convert.ToChar(","));

                foreach (string d in Data)
                {
                    if (d.StartsWith("[") == true & d.EndsWith("]") == false & d.Contains("]") == true)
                    {
                        d = d.Remove(0, d.IndexOf("]") + 1);
                        if (d == i)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (d == i)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            else
            {
                if (Core.Player.RegisterData.StartsWith("[") == true & Core.Player.RegisterData.EndsWith("]") == false & Core.Player.RegisterData.Contains("]") == true)
                {
                    string d = Core.Player.RegisterData.Remove(0, Core.Player.RegisterData.IndexOf("]") + 1);
                    if (d == i)
                    {
                        return true;
                    }
                }
                else
                {
                    if (Core.Player.RegisterData == i)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        private static void CheckTimeBasedRegisters()
        {
            if (!string.IsNullOrEmpty(Core.Player.RegisterData))
            {
                List<string> Data = new [] { Core.Player.RegisterData }.ToList();
                if (Core.Player.RegisterData.Contains(",") == true)
                {
                    Data = Core.Player.RegisterData.Split(Convert.ToChar(",")).ToList();
                }

                bool removedRegisters = false;

                for (var i = 0; i <= Data.Count - 1; i++)
                {
                    if (i <= Data.Count - 1)
                    {
                        string d = Data[i];

                        if (d.StartsWith("[TIME|") == true)
                        {
                            string timeString = d.Remove(0, ("[TIME|").Length);
                            timeString = timeString.Remove(timeString.IndexOf("]"));

                            string[] timeData = timeString.Split(Convert.ToChar("|"));

                            System.DateTime regDate = UnixToTime(timeData[0]);
                            int value = Convert.ToInt32(timeData[1]);
                            string format = timeData[2];

                            bool @remove = false;

                            switch (format)
                            {
                                case "days":
                                case "day":
                                    if (DateDiff(DateInterval.Day, regDate, System.DateTime.Now) >= value)
                                    {
                                        @remove = true;
                                    }
                                    break;
                                case "minutes":
                                case "minute":
                                    if (DateDiff(DateInterval.Minute, regDate, System.DateTime.Now) >= value)
                                    {
                                        @remove = true;
                                    }
                                    break;
                                case "seconds":
                                case "second":
                                    if (DateDiff(DateInterval.Second, regDate, System.DateTime.Now) >= value)
                                    {
                                        @remove = true;
                                    }
                                    break;
                                case "years":
                                case "year":
                                    if (Convert.ToInt32(Math.Floor(DateDiff(DateInterval.Day, regDate, System.DateTime.Now) / 365)) >= value)
                                    {
                                        @remove = true;
                                    }
                                    break;
                                case "weeks":
                                case "week":
                                    if (Convert.ToInt32(Math.Floor(DateDiff(DateInterval.Day, regDate, System.DateTime.Now) / 7)) >= value)
                                    {
                                        @remove = true;
                                    }
                                    break;
                                case "months":
                                case "month":
                                    if (DateDiff(DateInterval.Month, regDate, System.DateTime.Now) >= value)
                                    {
                                        @remove = true;
                                    }
                                    break;
                                case "hours":
                                case "hour":
                                    if (DateDiff(DateInterval.Hour, regDate, System.DateTime.Now) >= value)
                                    {
                                        @remove = true;
                                    }
                                    break;
                                case "dayofweek":
                                    if (DateDiff(DateInterval.Weekday, regDate, System.DateTime.Now) >= value)
                                    {
                                        @remove = true;
                                    }
                                    break;
                            }

                            if (@remove == true)
                            {
                                Data.RemoveAt(i);
                                i -= 1;
                                removedRegisters = true;
                            }
                        }
                    }
                }

                if (removedRegisters == true)
                {
                    string s = "";

                    if (Data.Count > 0)
                    {
                        foreach (string d in Data)
                        {
                            if (!string.IsNullOrEmpty(s))
                            {
                                s += ",";
                            }
                            s += d;
                        }
                    }

                    Core.Player.RegisterData = s;
                }
            }
        }

        public static System.DateTime UnixToTime(string strUnixTime)
        {
            System.DateTime functionReturnValue = default(System.DateTime);
            functionReturnValue = DateAdd(DateInterval.Second, Conversion.Val(strUnixTime), 1 / 1 / 1970 12:00:00 AM);
            if (functionReturnValue.IsDaylightSavingTime())
            {
                functionReturnValue = DateAdd(DateInterval.Hour, 1, UnixToTime());
            }
            return functionReturnValue;
        }

        public static string TimeToUnix(System.DateTime dteDate)
        {
            if (dteDate.IsDaylightSavingTime())
            {
                dteDate = DateAdd(DateInterval.Hour, -1, dteDate);
            }
            return DateDiff(DateInterval.Second, 1 / 1 / 1970 12:00:00 AM, dteDate).ToString();
        }

        public static void RegisterID(string i)
        {
            string Data = Core.Player.RegisterData;

            if (string.IsNullOrEmpty(Data))
            {
                Data = i;
            }
            else
            {
                string[] checkData = Data.Split(Convert.ToChar(","));
                if (checkData.Contains(i) == false)
                {
                    Data += "," + i;
                }
            }

            Core.Player.RegisterData = Data;
        }

        public static void RegisterID(string name, string type, string value)
        {
            string Data = Core.Player.RegisterData;

            string reg = "[" + type.ToUpper() + "|" + value + "]" + name;

            if (string.IsNullOrEmpty(Data))
            {
                Data = reg;
            }
            else
            {
                string[] checkData = Data.Split(Convert.ToChar(","));
                if (checkData.Contains(reg) == false)
                {
                    Data += "," + reg;
                }
            }

            Core.Player.RegisterData = Data;
        }

        public static void UnregisterID(string i)
        {
            string[] checkData = Core.Player.RegisterData.Split(Convert.ToChar(","));
            string Data = "";

            List<string> checkList = checkData.ToList();
            checkList.Remove(i);

            checkData = checkList.ToArray();
            for (var a = 0; a <= checkData.Length - 1; a++)
            {
                if (a != 0)
                {
                    Data += ",";
                }

                Data += checkData[a];
            }

            Core.Player.RegisterData = Data;
        }

        public static void UnregisterID(string name, string type)
        {
            string[] Data = Core.Player.RegisterData.Split(Convert.ToChar(","));
            string newData = "";

            foreach (string line in Data)
            {
                if (line.StartsWith("[") == true & line.Contains("]") == true & line.EndsWith("]") == false)
                {
                    string lName = line.Remove(0, line.IndexOf("]", StringComparison.Ordinal) + 1);
                    string lType = line.Remove(0, 1);
                    lType = lType.Remove(lType.IndexOf("|", StringComparison.Ordinal));

                    if (lName != name | lType.ToLower() != type.ToLower())
                    {
                        if (!string.IsNullOrEmpty(newData))
                        {
                            newData += ",";
                        }
                        newData += line;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(newData))
                    {
                        newData += ",";
                    }
                    newData += line;
                }
            }

            Core.Player.RegisterData = newData;
        }

        public static void ChangeRegister(string name, string newValue)
        {
            string[] Data = Core.Player.RegisterData.Split(Convert.ToChar(","));
            string newData = "";

            foreach (string line in Data)
            {
                if (!string.IsNullOrEmpty(newData))
                {
                    newData += ",";
                }
                if (line.StartsWith("[") == true & line.Contains("]") == true & line.EndsWith("]") == false)
                {
                    string lName = line.Remove(0, line.IndexOf("]", StringComparison.Ordinal) + 1);
                    string lType = line.Remove(0, 1);
                    lType = lType.Remove(lType.IndexOf("|", StringComparison.Ordinal));

                    if (lName.ToLower() == name.ToLower())
                    {
                        newData += "[" + lType + "|" + newValue + "]" + name;
                    }
                    else
                    {
                        newData += line;
                    }
                }
                else
                {
                    newData += line;
                }
            }

            Core.Player.RegisterData = newData;
        }

        /// <summary>
        /// Returns the Value and Type of a Register with value. {Value,Type}
        /// </summary>
        /// <param name="Name">The name of the register.</param>
        public static object[] GetRegisterValue(string Name)
        {
            string[] registers = Core.Player.RegisterData.Split(Convert.ToChar(","));
            foreach (string line in registers)
            {
                if (line.StartsWith("[") == true & line.Contains("]") == true & line.EndsWith("]") == false)
                {
                    string lName = line.Remove(0, line.IndexOf("]", StringComparison.Ordinal) + 1);

                    if (lName.ToLower() == Name.ToLower())
                    {
                        string lType = line.Remove(0, 1);
                        lType = lType.Remove(lType.IndexOf("|", StringComparison.Ordinal));
                        string lValue = line.Remove(0, line.IndexOf("|", StringComparison.Ordinal) + 1);
                        lValue = lValue.Remove(lValue.IndexOf("]", StringComparison.Ordinal));

                        return new object[] { lValue, lType };
                    }
                }
            }

            return new object[] { null, null };
        }

        #endregion

    }
}

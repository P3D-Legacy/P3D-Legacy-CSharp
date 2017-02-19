using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using P3D.Legacy.Core.Debug;
using P3D.Legacy.Core.Extensions;
using P3D.Legacy.Core.Screens;
using P3D.Legacy.Core.Screens.GUI;

namespace P3D.Legacy.Core.Resources
{
    // TODO: Take from my project systeminfo stuff
    public class Logger
    {
        public enum LogTypes
        {
            Message,
            Debug,
            ErrorMessage,
            Warning,
            Entry
        }


        private const string CRASHLOGSEPARATOR = "---------------------------------------------------------------------------------";
        private static List<string> History = new List<string>();

        public static bool DisplayLog = false;
        private static string[] ErrorHeaders = {
        "I AM ERROR!",
        "Minecraft crashed.",
        "Missingno.",
        "1 ERROR",
        "GET TO DA CHOPPA",
        "Fire attacks might be super effective...",
        "Does this help?",
        "Work! Pleeeeeeeease?",
        "WHAT IS THIS?",
        "I find your lack of [ERROR] disturbing.",
        "Blame Darkfire.",
        "RTFM",
        "FEZ II announced.",
        "At least it's not a Blue Screen.",
        "Kernel PANIC",
        "I'm sorry, Dave, I'm afraid I can't do that.",
        "Never gonna give you up ~",
        "Wouldn't have happend with Swift.",
        "Team Rocket blasting off again!",
        "Snorlax just sat on your computer!",
        "Wut?",
        "Mojang buys Microsoft! Get your new Mojang operating system now. With more blocks and scrolls.",
        "HλLF-LIFE 2 confirmed",
        "(╯°□°）╯︵ ┻━┻"

    };

        const string LOGVERSION = "2.4";
        public static void Log(LogTypes LogType, string Message)
        {
            try
            {
                string currentTime = GetLogTime(System.DateTime.Now);

                string LogString = null;
                if (LogType == LogTypes.Entry)
                {
                    LogString = "]" + Message;
                }
                else
                {
                    LogString = LogType.ToString() + " (" + currentTime + "): " + Message;
                }

                Debug("Logger: " + LogString);

                string Log = "";

                if (File.Exists(GameController.GamePath + "\\log.dat") == true)
                {
                    Log = File.ReadAllText(GameController.GamePath + "\\log.dat");
                }

                if (string.IsNullOrEmpty(Log))
                {
                    Log = LogString;
                }
                else
                {
                    Log += Constants.vbNewLine + LogString;
                }

                File.WriteAllText(GameController.GamePath + "\\log.dat", Log);
            }
            catch (Exception ex)
            {
            }
        }

        public static string LogCrash(Exception ex)
        {
            return String.Empty;
            /*
            try
            {
                int w32ErrorCode = -1;

                var w32 = ex as ComponentModel.Win32Exception;
                if (w32 != null)
                {
                    w32ErrorCode = w32.ErrorCode;
                }

                string logName = "";
                var _with1 = My.Computer.Clock.LocalTime;
                string month = _with1.Month.ToString();
                if (month.Length == 1)
                {
                    month = "0" + month;
                }
                string day = _with1.Day.ToString();
                if (day.Length == 1)
                {
                    day = "0" + day;
                }
                string hour = _with1.Hour.ToString();
                if (hour.Length == 1)
                {
                    hour = "0" + hour;
                }
                string minute = _with1.Minute.ToString();
                if (minute.Length == 1)
                {
                    minute = "0" + minute;
                }
                string second = _with1.Second.ToString();
                if (second.Length == 1)
                {
                    second = "0" + second;
                }
                logName = _with1.Year + "-" + month + "-" + day + "_" + hour + "." + minute + "." + second + "_crash.dat";

                string ContentPacks = "{}";
                if ((Core.GameOptions != null))
                {
                    ContentPacks = Core.GameOptions.ContentPackNames.ArrayToString();
                }

                string GameMode = "[No GameMode loaded]";
                if ((GameModeManager.ActiveGameMode != null))
                {
                    GameMode = GameModeManager.ActiveGameMode.Name;
                }

                string OnlineInformation = "GameJolt Account: FALSE";
                if ((Core.Player != null))
                {
                    OnlineInformation = "GameJolt Account: " + Core.Player.IsGameJoltSave.ToString().ToUpper();
                    if (Core.Player.IsGameJoltSave == true)
                    {
                        OnlineInformation += " (" + Core.GameJoltSave.GameJoltID + ")";
                    }
                }

                string ScriptInfo = "Actionscript: No script running";
                if ((Core.CurrentScreen != null))
                {
                    if (Core.CurrentScreen.Identification == Screen.Identifications.OverworldScreen)
                    {
                        if (((BaseOverworldScreen) Core.CurrentScreen).ActionScript.IsReady == false)
                        {
                            ScriptInfo = "Actionscript: " + ActionScript.CSL().ScriptName + "; Line: " + ActionScript.CSL().CurrentLine;
                        }
                    }
                }

                string ServerInfo = "FALSE";
                if (BaseConnectScreen.Connected == true)
                {
                    ServerInfo = "TRUE (" + BaseJoinServerScreen.SelectedServer.GetName() + "/" + BaseJoinServerScreen.SelectedServer.GetAddressString() + ")";
                }

                string GameEnvironment = "[No Game Environment loaded]";
                if ((Core.CurrentScreen != null))
                {
                    GameEnvironment = Core.CurrentScreen.Identification.ToString();
                }

                string IsSandboxMode = "False";
                if ((Core.Player != null))
                {
                    IsSandboxMode = Core.Player.SandBoxMode.ToString();
                }

                string gameInformation = GameController.GAMENAME + " " + GameController.GAMEDEVELOPMENTSTAGE + " version: " + GameController.GAMEVERSION + " (" + GameController.RELEASEVERSION + ")" + Constants.vbNewLine + "Content Packs: " + ContentPacks + Constants.vbNewLine + "Active GameMode: " + GameMode + Constants.vbNewLine + OnlineInformation + Constants.vbNewLine + "Playing on Servers: " + ServerInfo + Constants.vbNewLine + "Game Environment: " + GameEnvironment + Constants.vbNewLine + ScriptInfo + Constants.vbNewLine + "File Validation: " + Security.FileValidation.IsValid(true).ToString() + Constants.vbNewLine + "Sandboxmode: " + IsSandboxMode;

                string ScreenState = "[Screen state object not available]";
                if ((Core.CurrentScreen != null))
                {
                    ScreenState = "Screen state for the current screen (" + Core.CurrentScreen.Identification.ToString() + ")" + Constants.vbNewLine + Constants.vbNewLine + Core.CurrentScreen.GetScreenStatus();
                }

                string architectureString = "32 Bit";
                if (Environment.Is64BitOperatingSystem == true)
                {
                    architectureString = "64 Bit";
                }

                string specs = "Operating system: " + My.Computer.Info.OSFullName + " [" + My.Computer.Info.OSVersion + "]" + Constants.vbNewLine + "Core architecture: " + architectureString + Constants.vbNewLine + "System time: " + My.Computer.Clock.LocalTime.ToString() + Constants.vbNewLine + "System language: " + Globalization.CultureInfo.CurrentCulture.EnglishName + "(" + Globalization.CultureInfo.CurrentCulture.ThreeLetterWindowsLanguageName + ") / Loaded game language: " + Localization.LanguageSuffix + Constants.vbNewLine + "Decimal separator: " + GameController.DecSeparator + Constants.vbNewLine + "Available physical memory: " + Math.Round((My.Computer.Info.TotalPhysicalMemory / Math.Pow(1024, 3)), 2).ToString() + " Gigabyte" + Constants.vbNewLine + "Available logical processors: " + Environment.ProcessorCount.ToString();

                string innerException = "NOTHING";
                if ((ex.InnerException != null))
                {
                    innerException = ex.InnerException.Message;
                }
                string message = "NOTHING";
                if ((ex.Message != null))
                {
                    message = ex.Message;
                }
                string source = "NOTHING";
                if ((ex.Source != null))
                {
                    source = ex.Source;
                }
                string StackTrace = "NOTHING";
                if ((ex.StackTrace != null))
                {
                    StackTrace = ex.StackTrace;
                }

                string helpLink = "No helplink available.";
                if ((ex.HelpLink != null))
                {
                    helpLink = ex.HelpLink;
                }

                Exception BaseException = ex.GetBaseException();

                string data = "NOTHING";
                if ((ex.Data != null))
                {
                    data = "Items: " + ex.Data.Count;
                    if (ex.Data.Count > 0)
                    {
                        data = "";
                        for (var i = 0; i <= ex.Data.Count - 1; i++)
                        {
                            if (!string.IsNullOrEmpty(data))
                            {
                                data += Constants.vbNewLine;
                            }
                            data += "[" + ex.Data.Keys(i).ToString() + ": \"" + ex.Data.Values(i).ToString() + "\"]";
                        }
                    }
                }

                ErrorInformation informationItem = new ErrorInformation(ex);

                ObjectDump objDump = new ObjectDump(Core.CurrentScreen);
                string screenDump = objDump.Dump;

                string content = "Kolben Games Crash Log V " + LOGVERSION + Constants.vbNewLine + GameController.GAMENAME + " has crashed!" + Constants.vbNewLine + "// " + ErrorHeaders(Random.Next(0, ErrorHeaders.Length)) + Constants.vbNewLine + Constants.vbNewLine + CRASHLOGSEPARATOR + Constants.vbNewLine + Constants.vbNewLine + "Game information:" + Constants.vbNewLine + Constants.vbNewLine + gameInformation + Constants.vbNewLine + Constants.vbNewLine + CRASHLOGSEPARATOR + Constants.vbNewLine + Constants.vbNewLine + ScreenState + Constants.vbNewLine + Constants.vbNewLine + CRASHLOGSEPARATOR + Constants.vbNewLine + Constants.vbNewLine + "System specifications:" + Constants.vbNewLine + Constants.vbNewLine + specs + Constants.vbNewLine + Constants.vbNewLine + CRASHLOGSEPARATOR + Constants.vbNewLine + Constants.vbNewLine + ".Net installation information:" + Constants.vbNewLine + Constants.vbNewLine + DotNetVersion.GetInstalled() + Constants.vbNewLine + CRASHLOGSEPARATOR + Constants.vbNewLine + Constants.vbNewLine + GetGraphicsCardInformation() + Constants.vbNewLine + Constants.vbNewLine + CRASHLOGSEPARATOR + Constants.vbNewLine + Constants.vbNewLine + "Error information:" + Constants.vbNewLine + Constants.vbNewLine + "Message: " + message + Constants.vbNewLine + "InnerException: " + innerException + Constants.vbNewLine + "BaseException: " + BaseException.Message + Constants.vbNewLine + "HelpLink: " + helpLink + Constants.vbNewLine + "Data: " + data + Constants.vbNewLine + "Source: " + source + Constants.vbNewLine + "Win32 Errorcode: " + w32ErrorCode.ToString() + Constants.vbNewLine + Constants.vbNewLine + CRASHLOGSEPARATOR + Constants.vbNewLine + Constants.vbNewLine + informationItem.ToString() + Constants.vbNewLine + Constants.vbNewLine + CRASHLOGSEPARATOR + Constants.vbNewLine + Constants.vbNewLine + "CallStack: " + Constants.vbNewLine + Constants.vbNewLine + StackTrace + Constants.vbNewLine + Constants.vbNewLine + CRASHLOGSEPARATOR + Constants.vbNewLine + Constants.vbNewLine + "Enviornment dump: " + Constants.vbNewLine + Constants.vbNewLine + screenDump + Constants.vbNewLine + Constants.vbNewLine + CRASHLOGSEPARATOR + Constants.vbNewLine + Constants.vbNewLine + "You should report this error." + Constants.vbNewLine + Constants.vbNewLine + "Go to \"http://pokemon3d.net/forum/forums/6/create-thread\" to report this crash there.";

                File.WriteAllText(GameController.GamePath + "\\" + logName, content);

                Interaction.MsgBox(GameController.GAMENAME + " has crashed!" + Constants.vbNewLine + "---------------------------" + Constants.vbNewLine + Constants.vbNewLine + "Here is further information:" + Constants.vbNewLine + "Message: " + ex.Message + Constants.vbNewLine + Constants.vbNewLine + "You should report this error. When you do this, please attach the crash log to the report. You can find the file in your \"Pokemon\" folder." + Constants.vbNewLine + Constants.vbNewLine + "The name of the file is: \"" + logName + "\".", MsgBoxStyle.Critical, "Pokémon3D crashed!");

                Process.Start("explorer.exe", "/select,\"" + GameController.GamePath + "\\" + logName + "\"");

                //Returns the argument to start the launcher with:
                return "\"CRASHLOG_" + GameController.GamePath + "\\" + logName + "\" " + "\"ERRORTYPE_" + informationItem.ErrorType + "\" " + "\"ERRORID_" + informationItem.ErrorID + "\" " + "\"GAMEVERSION_" + GameController.GAMEDEVELOPMENTSTAGE + " " + GameController.GAMEVERSION + "\" " + "\"CODESOURCE_" + ex.Source + "\" " + "\"TOPSTACK_" + ErrorInformation.GetStackItem(ex.StackTrace, 0) + "\"";
            }
            catch (Exception exs)
            {
                Interaction.MsgBox(exs.Message + Constants.vbNewLine + exs.StackTrace);
            }

            return "";
            */
        }


        static string longestStackEntryName = "GameModeManager.SetGameModePointer";
        public static void Debug(string message)
        {
            string stackTraceEntry = Environment.StackTrace.SplitAtNewline()[3];

            while (stackTraceEntry.StartsWith(" ") == true)
            {
                stackTraceEntry = stackTraceEntry.Remove(0, 1);
            }
            stackTraceEntry = stackTraceEntry.Remove(0, stackTraceEntry.IndexOf(" ") + 1);
            stackTraceEntry = stackTraceEntry.Remove(stackTraceEntry.IndexOf("("));
            string pointString = stackTraceEntry.Remove(stackTraceEntry.LastIndexOf("."));
            stackTraceEntry = stackTraceEntry.Remove(0, pointString.LastIndexOf(".") + 1);

            string stackOutput = stackTraceEntry;

            if (stackOutput.Length > longestStackEntryName.Length)
            {
                longestStackEntryName = stackOutput;
            }
            else
            {
                while (stackOutput.Length < longestStackEntryName.Length)
                {
                    stackOutput += " ";
                }
            }

            //Debug.Print(stackOutput + Constants.vbTab + "| " + message);
            History.Add("(" + GetLogTime(System.DateTime.Now) + ") " + message);
        }

        public static void DrawLog()
        {
            if (DisplayLog == true & History.Count > 0 & (FontManager.ChatFont != null))
            {
                List<string> items = new List<string>();
                int max = History.Count - 1;

                int itemCount = 10;
                if (Core.WindowSize.Height > 680)
                {
                    itemCount += Convert.ToInt32(Math.Floor((Core.WindowSize.Height - 680) / 16d));
                }

                int min = max - itemCount;
                if (min < 0)
                {
                    min = 0;
                }

                int maxWidth = 0;
                for (var i = min; i <= max; i++)
                {
                    float s = FontManager.ChatFont.MeasureString(History[i]).X * 0.51f;
                    if (Convert.ToInt32(s) > maxWidth)
                    {
                        maxWidth = Convert.ToInt32(s);
                    }
                }

                Canvas.DrawRectangle(new Rectangle(0, 0, maxWidth + 10, (itemCount + 1) * 16 + 2), new Color(0, 0, 0, 150));

                int c = 0;
                for (var i = min; i <= max; i++)
                {
                    Core.SpriteBatch.DrawString(FontManager.ChatFont, History[i], new Vector2(5, 2 + c * 16), Color.White, 0f, Vector2.Zero, 0.51f, SpriteEffects.None, 0f);
                    c += 1;
                }
            }
        }

        private static string GetLogTime(System.DateTime d)
        {
            string hour = d.Hour.ToString();
            string minute = d.Minute.ToString();
            string second = d.Second.ToString();

            if (hour.Length == 1)
            {
                hour = "0" + hour;
            }
            if (minute.Length == 1)
            {
                minute = "0" + minute;
            }
            if (second.Length == 1)
            {
                second = "0" + second;
            }

            return hour + ":" + minute + ":" + second;
        }

        private static string GetGraphicsCardInformation()
        {
            return String.Empty;
            /*
            string CardName = "";
            string CardRAM = "";

            ManagementObjectSearcher WmiSelect = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");

            foreach (ManagementObject WmiResults in WmiSelect.Get())
            {
                try
                {
                    if (!string.IsNullOrEmpty(CardName))
                    {
                        CardName += "; ";
                        CardRAM += "; ";
                    }

                    CardName += WmiResults.GetPropertyValue("Name").ToString();
                    CardRAM += WmiResults.GetPropertyValue("AdapterRAM").ToString();

                }
                catch (Exception ex)
                {
                }
            }

            return "Graphics Card information:" + Constants.vbNewLine + Constants.vbNewLine + "[CardName(s): \"" + CardName + "\"]" + Constants.vbNewLine + "[CardRAM(s) : \"" + CardRAM + "\"]";
            */
        }

        public class ErrorInformation
        {

            public int ErrorID = -1;
            public string ErrorType = "";
            public string ErrorDescription = "";

            public string ErrorSolution = "";

            public string ErrorIDString = "-1";
            public ErrorInformation(Exception EX)
            {
                string stackTrace = EX.StackTrace;

                if ((stackTrace != null))
                {
                    int currentIndex = 0;
                    string callSub = "";
                    analyzeStack:

                    callSub = GetStackItem(EX.StackTrace, currentIndex);

                    switch (callSub)
                    {
                        //asset issues (000-099):
                        case "Microsoft.Xna.Framework.Content.ContentManager.OpenStream":
                            ErrorID = 1;
                            ErrorDescription = "The game was unable to load an asset (a Texture, a Sound or Music).";
                            ErrorSolution = "Make sure the file requested exists on your system.";
                            break;
                        case "_2._5DHero.MusicManager.PlayMusic":
                            ErrorID = 2;
                            ErrorDescription = "The game was unable to play a music file.";
                            ErrorSolution = "Make sure the file requested exists on your system. This might be caused by an invalid file in a ContentPack.";
                            break;
                        case "Microsoft.Xna.Framework.Graphics.Texture.GetAndValidateRect":
                            ErrorID = 3;
                            ErrorDescription = "The game was unable to process a texture file.";
                            ErrorSolution = "Code composed by Microsoft caused this issue. This might be caused by an invalid file in a ContentPack.";
                            break;
                        case "Microsoft.Xna.Framework.Graphics.Texture2D.CopyData[T]":
                            ErrorID = 4;
                            ErrorDescription = "The game was unable to process a texture file.";
                            ErrorSolution = "Code composed by Microsoft caused this issue. This might be caused by an invalid file in a ContentPack. Try to update your Graphics Card drivers.";
                            break;
                        case "Microsoft.Xna.Framework.Media.MediaQueue.Play":
                            ErrorID = 5;
                            ErrorDescription = "The game was unable to load or play a music file.";
                            ErrorSolution = "It is likely that the Windows Media Player is not installed on your computer or is wrongly configured. Please reinstall the Windows Media Player.";

                            break;
                        //GameJoltIssues (100-199)
                        case "_2._5DHero.GameJolt.APICall.SetStorageData":
                            ErrorID = 100;
                            ErrorDescription = "The was unable to connect to a GameJolt server because you tried to send a command without being logged in to GameJolt.";
                            ErrorSolution = "This happend because you got logged out from GameJolt due to connection problems. Ensure that your connection to the internet is constant.";

                            break;
                        //scripts (200-299)
                        case "_2._5DHero.ScriptCommander.DoNPC":
                            ErrorID = 200;
                            ErrorDescription = "The game crashed trying to execute an NPC related command (starting with @npc.)";
                            ErrorSolution = "If this happend during your GameMode, inspect the file mentioned under \"Actionscript\".";
                            break;
                        case "_2._5DHero.Trainer..ctor":
                            ErrorID = 201;
                            ErrorDescription = "The game was unable to initialize a new instance of a trainer class.";
                            ErrorSolution = "If this is caused by your GameMode, make sure the syntax in the trainer file is correct.";
                            break;
                        case "_2._5DHero.ScriptComparer.GetArgumentValue":
                            ErrorID = 202;
                            ErrorDescription = "The game crashed trying to process a script.";
                            ErrorSolution = "If this is caused by your GameMode, make sure the syntax in the script or map file is correct.";

                            break;

                        //Crashes generated by game code (300-399)
                        case "_2._5DHero.ForcedCrash.Crash":
                            ErrorID = 300;
                            ErrorDescription = "The game crashed on purpose.";
                            ErrorSolution = "Don't hold down F3 and C at the same time for a long time ;)";
                            break;
                        case "_2._5DHero.Security.ProcessValidation.ReportProcess":
                            ErrorID = 301;
                            ErrorDescription = "A malicious process was detected. To ensure that you are not cheating or hacking, the game closed.";
                            ErrorSolution = "Close all processes with the details given in the Data of the crashlog.";
                            break;
                        case "_2._5DHero.Security.FileValidation.CheckFileValid":
                            ErrorID = 302;
                            ErrorDescription = "The game detected edited or missing files.";
                            ErrorSolution = "For online play, ensure that you are running the unmodded version of Pokémon3D. You can enable Content Packs.";

                            break;
                        //misc errors (900-999)
                        case "Microsoft.Xna.Framework.Graphics.SpriteFont.GetIndexForCharacter":
                            ErrorID = 900;
                            ErrorDescription = "The game was unable to display a certain character which is not in the standard latin alphabet.";
                            ErrorSolution = "Make sure the GameMode you are playing doesn't use any invalid characters in its scripts and maps.";
                            break;
                        case "_2._5DHero.Player.LoadPlayer":
                            ErrorID = 901;
                            ErrorDescription = "The game failed to load a save state.";
                            ErrorSolution = "There are multiple reasons for the game to fail at loading a save state. There could be a missing file in the player directory or corrupted files.";
                            break;
                        case "Microsoft.Xna.Framework.BoundingFrustum.ComputeIntersectionLine":
                            ErrorID = 902;
                            ErrorDescription = "The game failed to set up camera mechanics.";
                            ErrorSolution = "This error is getting produced by an internal Microsoft class. Please redownload the game if this error keeps appearing.";
                            break;
                        case "_2._5DHero.Pokemon.Wild":
                            ErrorID = 903;
                            ErrorDescription = "The game crashed while attempting to generate a new Pokémon.";
                            ErrorSolution = "This error could have multiple sources, so getting a solution here is difficult. If you made your own Pokémon data file for a GameMode, check it for invalid values.";

                            break;
                        case "-1":
                            //No stack line found that applies to any error setting.
                            ErrorID = -1;
                            ErrorDescription = "The error is undocumented in the error handling system.";
                            ErrorSolution = "NaN";
                            break;
                        default:
                            currentIndex += 1;
                            goto analyzeStack;
                            break;
                    }
                }

                if (ErrorID > -1)
                {
                    ErrorIDString = ErrorID.ToString();
                    while (ErrorIDString.Length < 3)
                    {
                        ErrorIDString = "0" + ErrorIDString;
                    }
                }
            }

            public override string ToString()
            {
                if (ErrorID > -1 & ErrorID < 100)
                {
                    ErrorType = "Assets";
                }
                else if (ErrorID > 99 & ErrorID < 200)
                {
                    ErrorType = "GameJolt";
                }
                else if (ErrorID > 199 & ErrorID < 300)
                {
                    ErrorType = "Scripts";
                }
                else if (ErrorID > 299 & ErrorID < 400)
                {
                    ErrorType = "Forced Crash";
                }
                else if (ErrorID > 899 & ErrorID < 1000)
                {
                    ErrorType = "Misc.";
                }
                else
                {
                    ErrorType = "NaN";
                }

                string s = "Error solution:" + Constants.vbNewLine + "(The provided solution might not work for your problem)" + Constants.vbNewLine + Constants.vbNewLine + "Error ID: " + ErrorID + Constants.vbNewLine + "Error Type: " + ErrorType + Constants.vbNewLine + "Error Description: " + ErrorDescription + Constants.vbNewLine + "Error Solution: " + ErrorSolution;
                return s;
            }

            public static string GetStackItem(string stack, int i)
            {
                if (i >= stack.SplitAtNewline().Length)
                {
                    return "-1";
                }

                string line = stack.SplitAtNewline()[i];
                string callSub = line;

                while (callSub.StartsWith(" ") == true)
                {
                    callSub = callSub.Remove(0, 1);
                }
                callSub = callSub.Remove(0, callSub.IndexOf(" ") + 1);
                callSub = callSub.Remove(callSub.IndexOf("("));

                return callSub;
            }

        }

    }


    /*
    public static class Logger
    {
        public static void Debug(string text)
        {

        }

        public static void Log(LogTypes type, string text)
        {


        }

        public enum LogTypes
        {
            ErrorMessage,
            Warning,
            Message,
            Debug
        }

        public static bool DisplayLog { get; set; }

        public static void DrawLog()
        {
            
        }
    }
    */
}

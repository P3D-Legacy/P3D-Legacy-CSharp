using System;
using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.GameJolt
{
    public static class API
    {
        public const string API_VERSION = "v1_1";

        public static string Username = "";
        public static string Token = "";
        public static string GameJoltID { get; set; }

        public static bool LoggedIn = false;

        public static Exception Exception = null;

        public static int APICallCount = 0;

        public struct JoltValue
        {
            public string Name;
            public string Value;
        }

        /// <summary>
        /// Handles received data.
        /// </summary>
        /// <param name="data">The data to work with.</param>
        public static List<JoltValue> HandleData(string data)
        {
            //Old system:
            if (data.Contains("data:\"" + Constants.vbNewLine) == true)
            {
                data = data.Replace("data:\"" + Constants.vbNewLine, "data:\"");
            }

            string[] arg = {
                Constants.vbNewLine,
                Constants.vbLf
            };

            List<string> list = data.Split(arg, StringSplitOptions.None).ToList();
            List<JoltValue> joltList = new List<JoltValue>();

            foreach (string Item in list)
            {
                if (Item.Contains(":") == true)
                {
                    string ValueName = Item.Remove(Item.IndexOf(":"));
                    string ValueContent = Item.Remove(0, Item.IndexOf(":") + 2);
                    ValueContent = ValueContent.Remove(ValueContent.Length - 1, 1);

                    JoltValue jValue = new JoltValue();
                    jValue.Name = ValueName;
                    jValue.Value = ValueContent;

                    joltList.Add(jValue);
                }
            }

            return joltList;
        }

        // TODO: Implement
        public static bool UserBanned(string gameJoltID)
        {
            return false;
        }

        public static string GetBanReasonByID(string id)
        {
            return string.Empty;
        }

        public static string BanReasonIDForUser(string gameJoltID)
        {
            return string.Empty;
        }

        public static void KickFromOnlineScreen(Screen screen)
        {
            
        }
    }
}

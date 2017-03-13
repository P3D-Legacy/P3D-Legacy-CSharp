using System;
using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.GameJolt
{
    public static class API
    {
        public struct JoltValue
        {
            public string Name;
            public string Value;
        }

        public const string API_VERSION = "v1_1";

        public const string HOST = "http://api.gamejolt.com/api/game/";

        public static string GameID { get; set; } = ""; // CLASSIFIED
        public static string GameKey { get; set; } = ""; // CLASSIFIED

        public static string Username { get; set; } = "";
        public static string Token { get; set; } = "";
        public static string GameJoltID { get; set; }

        public static bool LoggedIn { get; set; } = false;

        public static Exception Exception { get; set; } = null;

        public static int APICallCount { get; set; } = 0;

        /// <summary>
        /// Handles received data.
        /// </summary>
        /// <param name="data">The data to work with.</param>
        public static List<JoltValue> HandleData(string data)
        {
            //Old system:
            if (data.Contains("data:\"" + Environment.NewLine))
                data = data.Replace("data:\"" + Environment.NewLine, "data:\"");

            string[] arg = { Environment.NewLine, Constants.vbLf };

            var joltList = new List<JoltValue>();
            var list = data.Split(arg, StringSplitOptions.None).ToList();
            foreach (var item in list)
            {
                if (item.Contains(":"))
                {
                    var valueName = item.Remove(item.IndexOf(":", StringComparison.Ordinal));
                    var valueContent = item.Remove(0, item.IndexOf(":", StringComparison.Ordinal) + 2);
                    valueContent = valueContent.Remove(valueContent.Length - 1, 1);

                    joltList.Add(new JoltValue { Name = valueName, Value = valueContent });
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

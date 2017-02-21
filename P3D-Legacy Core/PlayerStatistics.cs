using System;
using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Core.Extensions;
using P3D.Legacy.Core.GameJolt;
using P3D.Legacy.Core.Resources;

namespace P3D.Legacy.Core
{
    public static class PlayerStatistics
    {
        public static int StatisticsCount => Statistics.Count;
        private static Dictionary<string, int> Statistics { get; } = new Dictionary<string, int>();

        public static void Load(string data)
        {
            Statistics.Clear();
            foreach (var line in data.SplitAtNewline())
            {
                if (line.Contains(","))
                {
                    string statName = line.Remove(line.IndexOf(",", StringComparison.Ordinal));
                    int statValue = Convert.ToInt32(line.Remove(0, line.IndexOf(",", StringComparison.Ordinal) + 1));

                    if (Statistics.ContainsKey(statName))
                        Statistics.Remove(statName);

                    Statistics.Add(statName, statValue);
                }
            }
        }

        public static void Track(string statName, int addition)
        {
            if (Statistics.ContainsKey(statName))
            {
                int currentValue = Statistics[statName];
                Statistics.Remove(statName);

                Statistics.Add(statName, currentValue + addition);
            }
            else
                Statistics.Add(statName, addition);

            if (API.LoggedIn)
                GameJoltStatistics.Track(statName, addition);
        }

        public static string GetData()
        {
            string s = "";
            var keyArr = Statistics.Keys.ToArray();
            var valArr = Statistics.Values.ToArray();
            for (var i = 0; i <= Statistics.Count - 1; i++)
            {
                if (!string.IsNullOrEmpty(s))
                    s += Constants.vbNewLine;

                s += keyArr[i] + "," + valArr[i];
            }
            return s;
        }
    }
}

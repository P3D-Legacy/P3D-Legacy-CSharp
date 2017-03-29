using System;
using System.Globalization;
using System.Linq;

using P3D.Legacy.Core.Resources;

namespace P3D.Legacy.Core.GameJolt
{
    public class GameJoltStatistics
    {

        public static void CreateStatistics()
        {
            string[] stats = { "steps taken" };
            //{"Obtained BP", "Ride used", "Eggs hatched", "Evolutions", "Moves learned", "Caught Pokemon", "Blackouts", "[53]Status booster used", "[25]Vitamins used", "TMs/HMs used", "[17]Medicine Items used", "[22]Evolution stones used", "[42]Repels used", "Cut used", "Surf used", "Fly used", "Strength used", "Waterfall used", "Flash used", "Rock smash used", "Whirlpool used", "Items found", "GTS Trades", "Wondertrades", "Battle Spot battles", "Trades", "PVP battles", "[2006]Berries picked", "[85]Apricorns picked", "Moves learned", "[4]Poké Balls used", "Wild battles", "Trainer battles", "Safari battles", "Bug-Catching contest battles"}
            for (var i = 0; i <= stats.Length - 1; i++)
            {
                APICall APICall = new APICall();
                APICall.SetStorageData(GetKey(stats[i]), "0", false);
            }
        }

        static readonly string[] IndicedStats = {
            "pvp wins",
            "pvp losses"
        };
        static System.DateTime lastStepTime = System.DateTime.Now;

        static int TempSteps = 0;
        public static void Track(string statName, int addition)
        {
            if (CanTrack(statName) == true)
            {
                APICall APICall = new APICall();

                if (statName.ToLower() == "steps taken")
                {
                    addition = TempSteps;
                    TempSteps = 0;
                }

                APICall.UpdateStorageData(GetKey(statName), addition.ToString(NumberFormatInfo.InvariantInfo), "add", false);
                Logger.Debug("Track online statistic: " + statName + " (" + addition.ToString(NumberFormatInfo.InvariantInfo) + ")");
            }
        }

        private static bool CanTrack(string statName)
        {
            if (IndicedStats.Contains(statName.ToLower()) == false)
            {
                if (statName.ToLower() == "steps taken")
                {
                    TempSteps += 1;
                    if ((DateTime.Now - lastStepTime).Seconds  >= 20)
                    {
                        lastStepTime = System.DateTime.Now;
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public static void GetStatisticValue(string statName, Action<string> resultFunction)
        {
            var apiCall = new APICall(resultFunction);
            apiCall.GetStorageData(GetKey(statName), false);
        }

        private static string GetKey(string statName) => "0GJSTAT_" + statName.ToLower();
    }
}

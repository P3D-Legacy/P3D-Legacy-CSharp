using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace P3D.Legacy.Core.World
{
    public abstract class BaseLevelLoader
    {
        public static List<Vector3> LoadedOffsetMapOffsets = new List<Vector3>();
        public static List<string> LoadedOffsetMapNames = new List<string>();


        //A counter across all LevelLoader instances to count how many instances across the program are active.
        protected static int Busy = 0;
        public static bool IsBusy => Busy > 0;

        protected static Dictionary<string, List<string>> tempStructureList = new Dictionary<string, List<string>>();
        public static void ClearTempStructures() => tempStructureList.Clear();

        public static string MapScript = "";
    }
}

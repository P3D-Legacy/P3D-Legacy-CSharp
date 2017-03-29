using System;
using System.Collections.Generic;
using System.Threading;

using P3D.Legacy.Core.Resources;

namespace P3D.Legacy.Core.Screens
{
    public abstract class BaseJoinServerScreen : Screen
    {
        public static int BarAnimationState = 0;


        public static Server.Server SelectedServer = null;

        public static bool Online = false;
        public static List<Thread> ClearThreadList = new List<Thread>();


        public static void AddServerMessage(string m, string server_name)
        {
            if (System.IO.File.Exists(GameController.GamePath + "\\Save\\server_list.dat") == false)
            {
                System.IO.File.WriteAllText(GameController.GamePath + "\\Save\\server_list.dat", "");
            }

            string newData = "";

            string[] data = System.IO.File.ReadAllLines(GameController.GamePath + "\\Save\\server_list.dat");
            foreach (string line in data)
            {
                if (!string.IsNullOrEmpty(newData))
                {
                    newData += Environment.NewLine;
                }
                if (line.StartsWith(server_name + ",") == true)
                {
                    string[] lineData = line.Split(Convert.ToChar(","));
                    newData += lineData[0] + "," + lineData[1] + "," + m;
                }
                else
                {
                    newData += line;
                }
            }

            System.IO.File.WriteAllText(GameController.GamePath + "\\Save\\server_list.dat", newData);
        }
    }
}

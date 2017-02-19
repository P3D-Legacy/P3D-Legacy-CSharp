using System;
using System.Linq;

namespace P3D.Legacy.Core
{
    public static class CommandLineArgHandler
    {
        public static bool ForceGraphics { get; private set; } = false;

        public static void Initialize(string[] args)
        {
            if (args.Length > 0)
            {
                if (args.Any(arg => arg == "-forcegraphics"))
                {
                    ForceGraphics = true;
                }
            }

            foreach (string arg in args)
            {
                if (arg.Contains(":"))
                {
                    string identifier = arg.Remove(arg.IndexOf(":", StringComparison.Ordinal));
                    string value = arg.Remove(0, arg.IndexOf(":", StringComparison.Ordinal) + 1);

                    //TODO
                    /*
                    switch (identifier)
                    {
                        case "MAP":
                            MapPreviewScreen.DetectMapPath(value);
                            break;
                    }
                    */
                }
            }
        }
    }
}

using System;
using Microsoft.Xna.Framework.Input;
using P3D.Legacy.Core.Input;

namespace P3D.Legacy.Core.Debug
{
    public class ForcedCrash
    {
        static float Delay = 14f;

        public static void Update()
        {
            if (KeyBoardHandler.KeyDown(Core.KeyBindings.DebugControl) && KeyBoardHandler.KeyDown(Keys.C))
            {
                //Debug.Print("CRASH IN: " + Delay.ToString(NumberFormatInfo.InvariantInfo));
                Delay -= 0.1f;
                if (Delay <= 0f)
                {
                    Crash();
                }
            }
            else
            {
                Delay = 14f;
            }
        }

        private static void Crash()
        {
            bool canCrash = true;
            if (Core.Player.loadedSave)
            {
                if (Core.Player.IsGamejoltSave || !Core.Player.SandBoxMode)
                {
                    canCrash = false;
                }
            }
            if (canCrash)
            {
                Exception ex = new Exception("Forced the game to crash.");
                throw ex;
            }
            else
            {
                Delay = 14f;
            }
        }

    }
}

using System;
using System.IO;
using System.Linq;
using System.Reflection;

using Microsoft.Xna.Framework;

using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.Debug
{
    public class DebugDisplay
    {

        /// <summary>
        /// Renders the debug information.
        /// </summary>
        public static void Draw()
        {
            if (Core.CurrentScreen.CanDrawDebug)
            {
                string isDebugString = "";
                if (GameController.IS_DEBUG_ACTIVE)
                {
                    isDebugString = " (Debugmode / " + File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location) + ")";
                }

                bool ActionscriptActive = true;
                if (Core.CurrentScreen.Identification == Screen.Identifications.OverworldScreen)
                {
                    ActionscriptActive = ((BaseOverworldScreen) Core.CurrentScreen).ActionScriptIsReady;
                }

                string thirdPersonString = "";
                if (Screen.Camera.Name == "Overworld")
                {
                    BaseOverworldCamera c = (BaseOverworldCamera)Screen.Camera;
                    if (c.ThirdPerson)
                    {
                        thirdPersonString = " / " + c.ThirdPersonOffset;
                    }
                }

                string s = GameController.GAMENAME + " " + GameController.GAMEDEVELOPMENTSTAGE + " " + GameController.GAMEVERSION + " / FPS: " + Math.Round((double) Core.GameInstance.FPSMonitor.Value, 0) + isDebugString + Constants.vbNewLine + Screen.Camera.Position.ToString() + thirdPersonString + Constants.vbNewLine + Screen.Camera.Yaw + "; " + Screen.Camera.Pitch + Constants.vbNewLine + "E: " + DrawnVertices + "/" + MaxVertices + Constants.vbNewLine + "C: " + MaxDistance + " A: " + ActionscriptActive;

                if (Core.GameOptions.ContentPackNames.Any())
                {
                    string contentPackString = "";
                    foreach (string ContentPackName in Core.GameOptions.ContentPackNames)
                    {
                        if (!string.IsNullOrEmpty(contentPackString))
                        {
                            contentPackString += ", ";
                        }
                        contentPackString += ContentPackName;
                    }
                    contentPackString = "Loaded ContentPacks: " + contentPackString;
                    s += Constants.vbNewLine + contentPackString;
                }

                Core.SpriteBatch.DrawInterfaceString(FontManager.MainFont, s, new Vector2(7, 7), Color.Black);
                Core.SpriteBatch.DrawInterfaceString(FontManager.MainFont, s, new Vector2(5, 5), Color.White);
            }
        }

        #region "RenderDataTracking"

        //Values for tracking render data of the level.

        /// <summary>
        /// The amount of vertices rendered in the last frame.
        /// </summary>
        public static int DrawnVertices { get; set; } = 0;

        /// <summary>
        /// The maximum amount of vertices that are present in the current scene.
        /// </summary>
        public static int MaxVertices { get; set; } = 0;

        /// <summary>
        /// The distance of the vertex to the camera, that is the furthest away from the camera.
        /// </summary>
        public static int MaxDistance { get; set; } = 0;

        #endregion

    }
}

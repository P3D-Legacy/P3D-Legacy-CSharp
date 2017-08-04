using System;
using System.Drawing.Imaging;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using P3D.Legacy.Core.Entities.Other;
using P3D.Legacy.Core.Input;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Managers;
using P3D.Legacy.Core.Resources.Managers.Music;
using P3D.Legacy.Core.Resources.Managers.Sound;
using P3D.Legacy.Core.Settings;
using P3D.Legacy.Core.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Core
{
    public static class MainGameFunctions
    {
        public static void FunctionKeys()
        {
            if (KeyBoardHandler.KeyPressed(Core.KeyBindings.ScreenShot) && Core.CurrentScreen.CanTakeScreenshot )
                CaptureScreen();

            if (KeyBoardHandler.KeyPressed(Core.KeyBindings.FullScreen) && Core.CurrentScreen.CanGoFullscreen)
                ToggleFullScreen();

            if (KeyBoardHandler.KeyPressed(Core.KeyBindings.DebugControl))
            {
                Core.GameOptions.ShowDebug = !Core.GameOptions.ShowDebug;
                Options.SaveOptions(Core.GameOptions);
            }
            if (KeyBoardHandler.KeyPressed(Core.KeyBindings.GUIControl))
            {
                Core.GameOptions.ShowGUI = !Core.GameOptions.ShowGUI;
                Options.SaveOptions(Core.GameOptions);
            }
            if (KeyBoardHandler.KeyPressed(Core.KeyBindings.MuteMusic) && Core.CurrentScreen.CanMuteMusic)
            {
                Core.GameOptions.Muted = !Core.GameOptions.Muted;
                MusicManager.Mute(Core.GameOptions.Muted);
                SoundEffectManager.Mute(Core.GameOptions.Muted);
                Options.SaveOptions(Core.GameOptions);
                Core.CurrentScreen.ToggledMute();
            }

            if (KeyBoardHandler.KeyPressed(Core.KeyBindings.LightKey))
                Core.GameOptions.LightingEnabled = !Core.GameOptions.LightingEnabled;

            if (KeyBoardHandler.KeyDown(Core.KeyBindings.DebugControl))
            {
                if (KeyBoardHandler.KeyPressed(Keys.F))
                    TextureManager.TextureList.Clear();

                if (KeyBoardHandler.KeyPressed(Keys.S))
                    Core.SetWindowSize(new Vector2(1200, 680));

                if (KeyBoardHandler.KeyPressed(Keys.L))
                    Logger.DisplayLog = !Logger.DisplayLog;
            }
            if (ControllerHandler.ButtonPressed(Buttons.Back, true))
            {
                Core.GameOptions.GamePadEnabled = !Core.GameOptions.GamePadEnabled;
                Core.GameMessage.ShowMessage(Core.GameOptions.GamePadEnabled ? "Enabled XBOX 360 GamePad support." : "Disabled XBOX 360 GamePad support.", 12, FontManager.MainFont, Color.White);
                Options.SaveOptions(Core.GameOptions);
            }

            if (KeyBoardHandler.KeyPressed(Keys.B) && KeyBoardHandler.KeyDown(Core.KeyBindings.DebugControl))
                Core.GameOptions.DrawViewBox = !Core.GameOptions.DrawViewBox;
        }

        private static void CaptureScreen()
        {
            try
            {
                Core.GameMessage.HideMessage();

                var fileName = $"{DateTime.Now:yyyy-MM-dd_HH.mm.ss}.png";
                var file = new ScreenshotsFolder().CreateFile(fileName, CreationCollisionOption.ReplaceExisting);

                if (!Core.GraphicsManager.IsFullScreen)
                {
                    using (var b = new System.Drawing.Bitmap(Core.WindowSize.Width, Core.WindowSize.Height))
                    using (var g = System.Drawing.Graphics.FromImage(b))
                    using (var fileStream = file.Open(FileAccess.ReadAndWrite))
                    {
                        g.CopyFromScreen(Core.Window.ClientBounds.X, Core.Window.ClientBounds.Y, 0, 0, new System.Drawing.Size(b.Width, b.Height));
                        b.Save(fileStream, ImageFormat.Png);
                    }
                }
                else
                {
                    using (var screenshot = new RenderTarget2D(Core.GraphicsDevice, Core.WindowSize.Width, Core.WindowSize.Height, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8))
                    using (var fileStream = file.Open(FileAccess.ReadAndWrite))
                    {
                        Core.GraphicsDevice.SetRenderTarget(screenshot);
                        Core.Draw();
                        Core.GraphicsDevice.SetRenderTarget(null);

                        screenshot.SaveAsPng(fileStream, Core.WindowSize.Width, Core.WindowSize.Height);
                    }
                }

                Core.GameMessage.SetupText($"{Localization.GetString("game_message_screenshot")}{fileName}", FontManager.MainFont, Color.White);
                Core.GameMessage.ShowMessage(12, Core.GraphicsDevice);
            }
            catch (Exception ex) { Logger.Log(Logger.LogTypes.ErrorMessage, $"Basic.vb: {Localization.GetString("game_message_screenshot_failed")}. More information: {ex.Message}"); }
        }

        private static void ToggleFullScreen()
        {
            if (Core.GraphicsManager.IsFullScreen == false)
            {
                if (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width != System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width ||
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height != System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height)
                {
                    Core.GraphicsManager.PreferredBackBufferWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                    Core.GraphicsManager.PreferredBackBufferHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                    Core.WindowSize = new Rectangle(0, 0, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);

                }
                else
                {
                    Core.GraphicsManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    Core.GraphicsManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                    Core.WindowSize = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
                }

                //System.Windows.Forms.Application.VisualStyleState = Windows.Forms.VisualStyles.VisualStyleState.ClientAndNonClientAreasEnabled;

                Core.GraphicsManager.ToggleFullScreen();

                Core.GameMessage.ShowMessage(Localization.GetString("game_message_fullscreen_on"), 12, FontManager.MainFont, Color.White);
            }
            else
            {
                Core.GraphicsManager.PreferredBackBufferWidth = 1200;
                Core.GraphicsManager.PreferredBackBufferHeight = 680;
                Core.WindowSize = new Rectangle(0, 0, 1200, 680);

                //System.Windows.Forms.Application.VisualStyleState = Windows.Forms.VisualStyles.VisualStyleState.ClientAndNonClientAreasEnabled;

                Core.GraphicsManager.ToggleFullScreen();

                Core.GameMessage.ShowMessage(Localization.GetString("game_message_fullscreen_off"), 12, FontManager.MainFont, Color.White);
            }

            Core.GraphicsManager.ApplyChanges();
            BaseNetworkPlayer.ScreenRegionChanged();
        }
    }
}

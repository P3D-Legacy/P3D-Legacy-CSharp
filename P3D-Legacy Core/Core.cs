using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using P3D.Legacy.Core.Debug;
using P3D.Legacy.Core.Entities;
using P3D.Legacy.Core.GameJolt.Profiles;
using P3D.Legacy.Core.Input;
using P3D.Legacy.Core.Interfaces;
using P3D.Legacy.Core.Network;
using P3D.Legacy.Core.Objects;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Managers;
using P3D.Legacy.Core.Resources.Managers.Music;
using P3D.Legacy.Core.Resources.Managers.Sound;
using P3D.Legacy.Core.Screens;
using P3D.Legacy.Core.Screens.GUI;
using P3D.Legacy.Core.Server;
using P3D.Legacy.Core.Settings;
using P3D.Legacy.Core.Storage.Folders;
using PCLExt.FileStorage;
using Keyboard = P3D.Legacy.Core.Settings.Keyboard;

namespace P3D.Legacy.Core
{
    public static class Core
    {
        public static CoreSpriteBatch SpriteBatch;
        public static GraphicsDevice GraphicsDevice;
        public static GraphicsDeviceManager GraphicsManager;
        public static ContentManager Content;
        public static GameTime GameTime;
        public static GameController GameInstance;

        public static Random Random = new Random();

        public static GameWindow Window;
        public static Rectangle WindowSize = new Rectangle(0, 0, 1200, 680);

        public static GameMessage GameMessage;

        public static ServersManager ServersManager;

        public static Screen CurrentScreen;
        public static IPlayer Player;

        public static GamejoltSave GameJoltSave;

        public static Options GameOptions;
        public static Keyboard KeyBindings;

        public static SamplerState Sampler;

        public static Color BackgroundColor = new Color(173, 216, 255);

        public static Dictionary<string, List<List<Entity>>> OffsetMaps = new Dictionary<string, List<List<Entity>>>();

        public static void Initialize(GameController gameReference)
        {
            GameInstance = gameReference;

            GraphicsManager = GameInstance.Graphics;
            GraphicsDevice = GameInstance.GraphicsDevice;
            Content = GameInstance.Content;
            SpriteBatch = new CoreSpriteBatch(GraphicsDevice);
            Window = GameInstance.Window;

            if (CommandLineArgHandler.ForceGraphics)
                Window.Title = GameController.GAMENAME + " " + GameController.GAMEDEVELOPMENTSTAGE + " (FORCED GRAPHICS)";
            else
                Window.Title = GameController.GAMENAME + " " + GameController.GAMEDEVELOPMENTSTAGE;

            GameOptions = Options.LoadOptions();
            KeyBindings = Keyboard.LoadKeyboard();

            GraphicsManager.PreferredBackBufferWidth = Convert.ToInt32(GameOptions.WindowSize.X);
            GraphicsManager.PreferredBackBufferHeight = Convert.ToInt32(GameOptions.WindowSize.Y);
            GraphicsDevice.PresentationParameters.BackBufferFormat = SurfaceFormat.Rgba1010102;
            GraphicsDevice.PresentationParameters.DepthStencilFormat = DepthFormat.Depth24Stencil8;

            WindowSize = new Rectangle(0, 0, Convert.ToInt32(GameOptions.WindowSize.X), Convert.ToInt32(GameOptions.WindowSize.Y));

            GraphicsManager.PreferMultiSampling = true;

            GraphicsManager.ApplyChanges();

            Canvas.SetupCanvas();
            //Player = new Player();
            GameJoltSave = new GamejoltSave();

            GameMessage = new GameMessage(null, new System.Drawing.Size(0, 0), new Vector2(0, 0));

            ServersManager = new ServersManager();

            Sampler = new SamplerState
            {
                Filter = TextureFilter.Point,
                AddressU = TextureAddressMode.Clamp,
                AddressV = TextureAddressMode.Clamp
            };
            GraphicsDevice.SamplerStates[0] = Sampler;

            //SetScreen(new SplashScreen(GameInstance));
            SetScreen(Screen.CreateSplashScreen((GameInstance)));
        }

        public static void LoadContent()
        {
            GameModeManager.LoadGameModes();
            Logger.Debug("Loaded game modes.");

            FontManager.LoadFonts();

            Screen.TextBox.TextFont = FontManager.GetFontContainer("textfont");
            Logger.Debug("Loaded fonts.");

            TextureManager.InitializeTextures();
            MusicManager.LoadMusic();
            SoundEffectManager.LoadSounds(false);
            Logger.Debug("Loaded content.");

            Logger.Debug("Validated files. Result: " + Security.FileValidation.IsValid(true));
            if (!Security.FileValidation.IsValid(false))
                Logger.Log(Logger.LogTypes.Warning, "Core.vb: File Validation failed! Download a fresh copy of the game to fix this issue.");

            GameMessage = new GameMessage(TextureManager.DefaultTexture, new System.Drawing.Size(10, 40), new Vector2(0, 0))
            {
                Dock = GameMessage.DockStyles.Top,
                BackgroundColor = Color.Black,
                TextPosition = new Vector2(10, 10)
            };
            Logger.Debug("Gamemessage initialized.");

            GameOptions = Options.LoadOptions();

            var p0 = Path.Combine(GameController.GamePath + "Temp");
            if (Directory.Exists(p0))
            {
                try
                {
                    Directory.Delete(p0, true);
                    Logger.Log(Logger.LogTypes.Message, "Core.vb: Deleted Temp directory.");
                }
                catch (Exception) { Logger.Log(Logger.LogTypes.Warning, "Core.vb: Failed to delete the Temp directory."); }
            }

            StaffProfile.SetupStaff();

            //ScriptVersion2.ScriptLibrary.InitializeLibrary();
        }

        public static void Update(GameTime gameTime)
        {
            GameTime = gameTime;

            KeyBoardHandler.Update();
            ControllerHandler.Update();

            BaseConnectScreen.UpdateConnectSet();

            if (!GameInstance.IsActive)
            {
                if (CurrentScreen.CanBePaused)
                    Core.SetScreen(Screen.CreatePauseScreen(CurrentScreen));
            }
            else
            {
                if (KeyBoardHandler.KeyPressed(KeyBindings.Escape) || ControllerHandler.ButtonDown(Buttons.Start))
                    CurrentScreen.EscapePressed();
            }

            CurrentScreen?.Update(gameTime);
            if (CurrentScreen.CanChat)
            {
                if (KeyBoardHandler.KeyPressed(KeyBindings.Chat) || ControllerHandler.ButtonPressed(Buttons.RightShoulder))
                {
                    if (BaseJoinServerScreen.Online || Player.SandBoxMode || GameController.IS_DEBUG_ACTIVE)
                        SetScreen(Screen.CreateChatScreen((CurrentScreen)));
                }
            }

            MainGameFunctions.FunctionKeys();
            MusicManager.Update(gameTime);

            GameMessage.Update();
            Controls.MakeMouseVisible();

            MouseHandler.Update();

            LoadingDots.Update();
            ForcedCrash.Update();

            ServersManager.Update();
        }

        public static void Draw()
        {
            GraphicsDevice.Clear(BackgroundColor);

            if (SpriteBatch.Running)
                SpriteBatch.EndBatch();
            else
            {
                SpriteBatch.BeginBatch();

                GraphicsDevice.DepthStencilState = DepthStencilState.Default;

                GraphicsDevice.SamplerStates[0] = Sampler;
                CurrentScreen.Draw();

                if (Player != null)
                {
                    if (Player.IsGameJoltSave)
                        Emblem.DrawNewEmblems();
                    Player.DrawLevelUp();
                }

                //if (BaseJoinServerScreen.Online || Player.SandBoxMode || GameController.IS_DEBUG_ACTIVE)
                //{
                //    if (CurrentScreen.Identification != Screen.Identifications.ChatScreen)
                //        BaseChatScreen.DrawNewMessages();
                //}

                GameMessage.Draw();
                OnlineStatus.Draw();

                Logger.DrawLog();

                SpriteBatch.EndBatch();

                Render();
            }
        }

        /// <summary>
        /// Intended for rendering 3D models on top of sprites.
        /// </summary>
        private static void Render() => CurrentScreen.Render();

        public static void SetScreen(Screen newScreen)
        {
            CurrentScreen?.ChangeFrom();

            CurrentScreen = newScreen;

            if (ControllerHandler.IsConnected())
                GameInstance.IsMouseVisible = GameInstance.IsMouseVisible && newScreen.MouseVisible;
            else
                GameInstance.IsMouseVisible = CurrentScreen.MouseVisible;

            CurrentScreen.ChangeTo();
        }

        public static Vector2 GetMiddlePosition(System.Drawing.Size offsetFull) => new Vector2(Convert.ToSingle(WindowSize.Width / 2) - Convert.ToSingle(offsetFull.Width / 2), Convert.ToSingle(WindowSize.Height / 2) - Convert.ToSingle(offsetFull.Height / 2));

        public static void StartThreadedSub(ParameterizedThreadStart s) => new Thread(s) { IsBackground = true }.Start();

        public static Rectangle ScreenSize()
        {
            double x = SpriteBatch.InterfaceScale;
            if (x == 1d)
                return WindowSize;
            return new Rectangle(Convert.ToInt32(WindowSize.X / x), Convert.ToInt32(WindowSize.Y / x), Convert.ToInt32(WindowSize.Width / x), Convert.ToInt32(WindowSize.Height / x));
        }
        public static Rectangle ScaleScreenRec(Rectangle rec)
        {
            double x = SpriteBatch.InterfaceScale;
            if (x == 1d)
                return rec;
            return new Rectangle(Convert.ToInt32(rec.X * x), Convert.ToInt32(rec.Y * x), Convert.ToInt32(rec.Width * x), Convert.ToInt32(rec.Height * x));
        }
        public static Vector2 ScaleScreenVec(Vector2 vec)
        {
            double x = SpriteBatch.InterfaceScale;
            if (x == 1d)
                return vec;
            return new Vector2(Convert.ToSingle(vec.X * x), Convert.ToSingle(vec.Y * x));
        }
        public static void SetWindowSize(Vector2 size)
        {
            GraphicsManager.PreferredBackBufferWidth = Convert.ToInt32(size.X);
            GraphicsManager.PreferredBackBufferHeight = Convert.ToInt32(size.Y);

            GraphicsManager.ApplyChanges();

            WindowSize = new Rectangle(0, 0, Convert.ToInt32(size.X), Convert.ToInt32(size.Y));
        }

        public static void Exit()
        {
            GameInstance.Exit();
        }
    }
}
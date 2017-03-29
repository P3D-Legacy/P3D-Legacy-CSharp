using System;
using System.Globalization;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using P3D.Legacy.Core.Debug;
using P3D.Legacy.Core.Entities.Other;
using P3D.Legacy.Core.GameJolt;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Screens;
using P3D.Legacy.Core.Security;

namespace P3D.Legacy.Core
{
    /// <summary>
    /// Controls the game's main workflow.
    /// </summary>
    public class GameController : Game
    {
        /// <summary>
        /// The current version of the game.
        /// </summary>
        public const string GAMEVERSION = "0.55";

        /// <summary>
        /// The number of released iterations of the game.
        /// </summary>
        public const string RELEASEVERSION = "93";

        /// <summary>
        /// The development stage the game is in.
        /// </summary>
        public const string GAMEDEVELOPMENTSTAGE = "Indev";

        /// <summary>
        /// The name of the game.
        /// </summary>
        public const string GAMENAME = "Pokémon 3D";

        /// <summary>
        /// The name of the developer that appears on the title screen.
        /// </summary>
        public const string DEVELOPER_NAME = "P3D Team";

        /// <summary>
        /// If the game should set the GameJolt online version to the current online version.
        /// </summary>
        public const bool UPDATEONLINEVERSION = false;

        /// <summary>
        /// If the Debug Mode is active.
        /// </summary>
#if DEBUG
        public const bool IS_DEBUG_ACTIVE = true;
#else
        public const bool IS_DEBUG_ACTIVE = false;
#endif


        public GraphicsDeviceManager Graphics;

        public FPSMonitor FPSMonitor;
        public Action<GameController> Action;

        public GameController(Action<GameController> action)
        {
            Action = action;

            Activated += DGame_Activated;
            Deactivated += DGame_Deactivated;
            Graphics = new GraphicsDeviceManager(this) { PreferMultiSampling = false };
            Graphics.PreparingDeviceSettings += Graphics_PreparingDeviceSettings;
            Content.RootDirectory = "Content";

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;
            Window.ClientSizeChanged += OnResize;

            FPSMonitor = new FPSMonitor();

            var gameHacked = File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "temp"));
            if (gameHacked)
                HackerAlerts.Activate();
        }
        private void Graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.GraphicsProfile = GraphicsProfile.HiDef;
            e.GraphicsDeviceInformation.PresentationParameters.MultiSampleCount = 2;
        }

        protected override void Initialize()
        {
            Core.Initialize(this);
            Action(this);
            base.Initialize();
        }

        protected override void LoadContent() { }
        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            Core.Update(gameTime);
            base.Update(gameTime);

            SessionManager.Update();
            FPSMonitor.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Core.Draw();

            base.Draw(gameTime);

            FPSMonitor.DrawnFrame();
        }

        //public static string DecSeparator => new NumberFormatInfo().NumberDecimalSeparator;
        public static string DecSeparator => NumberFormatInfo.InvariantInfo.NumberDecimalSeparator;

        protected override void OnExiting(object sender, EventArgs args)
        {
            SessionManager.Close();

            // TODO
            if (Core.ServersManager.ServerConnection.Connected)
                Core.ServersManager.ServerConnection.Abort();

            Logger.Debug("---Exit Game---");
        }

        protected void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            Core.WindowSize = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);

            if (Core.CurrentScreen != null)
            {
                Core.CurrentScreen.SizeChanged();
                Screen.TextBox.PositionY = Core.WindowSize.Height - 160f;
            }
            BaseNetworkPlayer.ScreenRegionChanged();
        }

        private void DGame_Activated(object sender, EventArgs e)
        {
            BaseNetworkPlayer.ScreenRegionChanged();
        }

        private void DGame_Deactivated(object sender, EventArgs e)
        {
            BaseNetworkPlayer.ScreenRegionChanged();
        }

        /// <summary>
        /// If the player hacked any instance of Pokémon3D at some point.
        /// </summary>
        public static bool Hacker { get; set; }

        /// <summary>
        /// The path to the game folder.
        /// </summary>
        /// TODO: REMOVE!
        public static string GamePath => AppDomain.CurrentDomain.BaseDirectory;



        private static Point MinResolution => new Point(600, 360);
        private void OnResize(object sender, EventArgs e)
        {
            if (Graphics.GraphicsDevice.Viewport.Width < MinResolution.X || Graphics.GraphicsDevice.Viewport.Height < MinResolution.Y)
                Resize(MinResolution);
        }
        private void Resize(Point size)
        {
            if (size.X < MinResolution.X || size.Y < MinResolution.Y)
                return;

            Graphics.PreferredBackBufferWidth = size.X;
            Graphics.PreferredBackBufferHeight = size.Y;

            Graphics.ApplyChanges();
        }
    }
}
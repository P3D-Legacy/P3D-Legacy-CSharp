using System;
using System.IO;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Screens.GUI;

namespace P3D.Legacy.Core.Screens
{
    /*
    internal class SplashScreen : Screen
    {
        private static readonly string LicenseText = "\"MonoGame\", the MonoGame Logo and source code are copyrights of MonoGame Team (monogame.net)." + Constants.vbNewLine + "Pokémon 3D is not affiliated with Nintendo, Creatures Inc. or GAME FREAK Inc.";
        private readonly Texture2D _monoGameLogo;
        private readonly SpriteFont _licenseFont;

        private readonly Vector2 _licenseTextSize;
        private float _delay = 7f;
        private Thread _loadThread;
        private bool _startedLoad;

        private GameController _game;
        public SplashScreen(GameController gameReference)
        {
            _game = gameReference;

            CanBePaused = false;
            CanMuteMusic = false;
            CanChat = false;
            CanTakeScreenshot = false;
            CanDrawDebug = false;
            MouseVisible = true;
            CanGoFullscreen = false;

            _monoGameLogo = Core.Content.Load<Texture2D>(Path.Combine("GUI", "Logos", "MonoGame"));
            _licenseFont = Core.Content.Load<SpriteFont>(Path.Combine("Fonts", "BMP", "mainFont"));
            _licenseTextSize = _licenseFont.MeasureString(LicenseText);

            Identification = Identifications.SplashScreen;
        }

        public override void Draw()
        {
            Canvas.DrawRectangle(Core.WindowSize, Color.Black);

            Core.SpriteBatch.Draw(_monoGameLogo, new Vector2(Convert.ToSingle(Core.WindowSize.Width / 2 - _monoGameLogo.Width / 2), Convert.ToSingle(Core.WindowSize.Height / 2 - _monoGameLogo.Height / 2 - 50)), Color.White);

            Core.SpriteBatch.DrawString(_licenseFont, LicenseText, new Vector2(Convert.ToSingle(Core.WindowSize.Width / 2 - _licenseTextSize.X / 2), Convert.ToSingle(Core.WindowSize.Height - _licenseTextSize.Y - 50)), Color.White);
        }

        public override void Update()
        {
            if (_startedLoad == false)
            {
                _startedLoad = true;

                _loadThread = new Thread(LoadContent);
                _loadThread.Start();
            }

            if (_loadThread.IsAlive == false)
            {
                if (_delay == 0f || GameController.IS_DEBUG_ACTIVE)
                {
                    Core.GraphicsManager.ApplyChanges();

                    Logger.Debug("---Loading content ready---");

                    //if (Core.GameOptions.MapViewMode)
                    //    Core.SetScreen(new MapPreviewScreen());
                    //else
                    //    Core.SetScreen(new MainMenuScreen());
                    //Core.SetScreen(New TransitionScreen(Me, New IntroScreen(), Color.Black, False))
                }
            }

            _delay -= 0.1f;
        }

        private void LoadContent()
        {
            Logger.Debug("---Start loading content---");

            Core.LoadContent();
        }
    }
    */
}

using System;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using P3D.Legacy.Core.Input;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.DebugC
{
    public class DebugScreen
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _monospaceFont;

        private readonly MngStringBuilder _mngStringBuilder = new MngStringBuilder(2048);

        private long _maxGcMemory;

        private double _fps;
        private double _smoothfps = 60;
        private double _frame;
        private double _smoothfpsShow = 60;
        private double _minfps = 1000;
        private double _minfpsshort = 1000;
        private int _minfpstick;

        private bool _offFrame = true;
        private static bool _clearCommand;

        // Console
        public static bool ConsoleOpen;

        public void Initialize(GraphicsDevice graphicsDevice)
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void LoadContent(ContentManager content)
        {
            //_monospaceFont = FontManager.ChatFont;
            _monospaceFont = content.Load<SpriteFont>("Fonts/monospace");
        }

        public void Update(GameTime gameTime)
        {
            _offFrame = gameTime.TotalGameTime.Milliseconds % 1000 <= 500;

            if (KeyBoardHandler.KeyPressed(Keys.I))
                ConsoleOpen = !ConsoleOpen;
        }
        public void Draw(GameTime gameTime)
        {
            if (ConsoleOpen)
            {
                _fps = 0.9 * _fps + 0.1 * (1000 / gameTime.ElapsedGameTime.TotalMilliseconds);

                _smoothfps = 0.98 * _smoothfps + 0.02 * _fps;
                if (gameTime.TotalGameTime.TotalMilliseconds - _frame > 500)
                {
                    _smoothfpsShow = _smoothfps;
                    _frame = gameTime.TotalGameTime.TotalMilliseconds;
                    _maxGcMemory -= 1024;

                    _minfpstick++;
                    if (_minfpstick == 4) // all 2 seconds
                    {
                        _minfpsshort = (_minfpsshort + _smoothfps) / 2;
                        _minfpstick = 0;
                    }
                }


                if (_fps < _minfpsshort && gameTime.TotalGameTime.TotalSeconds > 1)
                    _minfpsshort = _fps;

                if (Math.Abs(_minfpsshort - _minfps) > 0.1f)
                    _minfps = _minfpsshort;

                _spriteBatch.Begin();


                var totalmemory = GC.GetTotalMemory(false);
                if (_maxGcMemory < totalmemory)
                    _maxGcMemory = totalmemory;

                //clear
                _mngStringBuilder.Length = 0;

                _mngStringBuilder
                    .Append(GameController.GAMENAME).Append(" ").Append(GameController.GAMEDEVELOPMENTSTAGE).Append(" ").Append(GameController.GAMEVERSION).AppendLine()
                    .Append("Main Thread: ").AppendTrim(gameTime.ElapsedGameTime.TotalMilliseconds).Append(" ms").AppendLine()
                    .Append("FPS: ").Append((int) Math.Round(_fps)).Append(" ... ").Append((int) Math.Round(_smoothfpsShow)).Append(" > ").Append((int) Math.Round(_minfps)).AppendLine()
                    .Append(Core.WindowSize.Width).Append(" x ").Append(Core.WindowSize.Height).AppendLine()
                    .Append("Memory(GC): ").Append(totalmemory / 1024 / 1024).Append(" ... ").Append(_maxGcMemory / 1024 / 1024).AppendLine()
                    .Append("X: ").AppendTrim(Screen.Camera.Position.X).AppendLine()
                    .Append("Y: ").AppendTrim(Screen.Camera.Position.Y).AppendLine()
                    .Append("Z: ").AppendTrim(Screen.Camera.Position.Z).AppendLine()
                    .Append("Yaw: ").AppendTrim(Screen.Camera.Yaw).AppendLine()
                    .Append("Pitch: ").AppendTrim(Screen.Camera.Pitch).AppendLine();

                if (Core.GameOptions.ContentPackNames.Any())
                {
                    _mngStringBuilder.Append("Loaded Content Packs: ");
                    foreach (var contentPackName in Core.GameOptions.ContentPackNames)
                        _mngStringBuilder.Append(contentPackName).Append(", ");
                    _mngStringBuilder.AppendLine();
                }

                _spriteBatch.DrawString(_monospaceFont, _mngStringBuilder.StringBuilder, new Vector2(11.0f, 11.0f), Color.Black);
                _spriteBatch.DrawString(_monospaceFont, _mngStringBuilder.StringBuilder, new Vector2(10.0f, 10.0f), Color.White);

                _spriteBatch.End();
            }
        }
    }
}

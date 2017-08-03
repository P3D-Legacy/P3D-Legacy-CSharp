using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using P3D.Legacy.Core.Debug;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.DebugC
{
    public class DebugScreen : DrawableGameComponent
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

        public DebugScreen(Game game) : base(game)
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _monospaceFont = new ContentManager(Game.Services, "Content").Load<SpriteFont>("Fonts/monospace");
        }

        public override void Update(GameTime gameTime)
        {
            _offFrame = gameTime.TotalGameTime.Milliseconds % 1000 <= 500;
        }
        public override void Draw(GameTime gameTime)
        {
            if (Core.CurrentScreen.CanDrawDebug && Core.GameOptions.ShowDebug)
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


                var isDebugString = "";
                if (GameController.IS_DEBUG_ACTIVE)
                    isDebugString = " (Debugmode / " + File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location) + ")";

                var actionScriptActive = true;
                if (Core.CurrentScreen is BaseOverworldScreen screen)
                    actionScriptActive = screen.ActionScriptIsReady;

                var thirdPersonString = "";
                if (Screen.Camera is BaseOverworldCamera camera && camera.ThirdPerson)
                        thirdPersonString = camera.ThirdPersonOffset.ToString();

                var contentPacksString = "";
                if (Core.GameOptions.ContentPackNames.Any())
                {
                    var contentPackString = "";
                    foreach (var contentPackName in Core.GameOptions.ContentPackNames)
                    {
                        if (!string.IsNullOrEmpty(contentPackString))
                            contentPackString += ", ";
   
                        contentPackString += contentPackName;
                    }
                    contentPackString = "Loaded ContentPacks: " + contentPackString;
                    contentPacksString += Environment.NewLine + contentPackString;
                }

                var totalmemory = GC.GetTotalMemory(false);
                if (_maxGcMemory < totalmemory)
                    _maxGcMemory = totalmemory;


                //clear
                _mngStringBuilder.Length = 0;

                _mngStringBuilder
                    .Append(GameController.GAMENAME).Append(" ").Append(GameController.GAMEDEVELOPMENTSTAGE).Append(" ")
                    .Append(GameController.GAMEVERSION).Append(" ").Append(isDebugString).AppendLine()
                    .Append("Main Thread: ").AppendTrim(gameTime.ElapsedGameTime.TotalMilliseconds).Append(" ms")
                    .AppendLine()
                    .Append("FPS: ").Append((int) Math.Round(_fps)).Append(" ... ")
                    .Append((int) Math.Round(_smoothfpsShow)).Append(" > ").Append((int) Math.Round(_minfps))
                    .AppendLine()
                    .Append(Core.WindowSize.Width).Append(" x ").Append(Core.WindowSize.Height).AppendLine()
                    .Append("Memory(GC): ").Append(totalmemory / 1024 / 1024).Append(" ... ")
                    .Append(_maxGcMemory / 1024 / 1024).AppendLine()
                    .Append("X: ").AppendTrim(Screen.Camera.Position.X).AppendLine()
                    .Append("Y: ").AppendTrim(Screen.Camera.Position.Y).AppendLine()
                    .Append("Z: ").AppendTrim(Screen.Camera.Position.Z).AppendLine()
                    .Append("Yaw: ").AppendTrim(Screen.Camera.Yaw).AppendLine()
                    .Append("Pitch: ").AppendTrim(Screen.Camera.Pitch).AppendLine()
                    .Append("ThirdPerson: ").Append(thirdPersonString).AppendLine()
                    .Append("ActionScript: ").Append(actionScriptActive).AppendLine()
                    .Append("DrawnVertices: ").Append(RenderTracker.DrawnVertices).AppendLine()
                    .Append("MaxVertices: ").Append(RenderTracker.MaxVertices).AppendLine()
                    .Append("MaxDistance: ").Append(RenderTracker.MaxDistance).AppendLine()
                    .Append(contentPacksString);

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

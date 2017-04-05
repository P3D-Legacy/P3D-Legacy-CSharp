using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using P3D.Legacy.Core.Dialogues;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Managers;
using P3D.Legacy.Core.World;

namespace P3D.Legacy.Core.Screens
{
    public abstract class Screen
    {
        public abstract class ScreenManager
        {
            public abstract BaseConnectScreen CreateConnectScreen(BaseConnectScreen.Modes myMode, string header, string message, Screen currentScreen);
            public abstract Screen CreateSplashScreen(GameController gameInstance);
            public abstract Screen CreatePauseScreen(Screen prevScreen);
            public abstract Screen CreateChatScreen(Screen prevScreen);
        }

        private static ScreenManager _sm;
        protected static ScreenManager SM
        {
            get
            {
                if (_sm == null)
                {
                    var assembly = Assembly.GetEntryAssembly();
                    var type = assembly.GetTypes().SingleOrDefault(t => t.IsSubclassOf(typeof(ScreenManager)));
                    if (type != null)
                        _sm = Activator.CreateInstance(type) as ScreenManager;
                }

                return _sm;
            }
            set { _sm = value; }
        }
        public static BaseConnectScreen CreateConnectScreen(BaseConnectScreen.Modes myMode, string header, string message, Screen currentScreen) =>
            SM.CreateConnectScreen(myMode, header, message, currentScreen);
        public static Screen CreateSplashScreen(GameController gameInstance) => SM.CreateSplashScreen(gameInstance);
        public static Screen CreatePauseScreen(Screen prevScreen) => SM.CreatePauseScreen(prevScreen);
        public static Screen CreateChatScreen(Screen prevScreen) => SM.CreateChatScreen(prevScreen);



        public enum Identifications
        {
            MainMenuScreen,
            OverworldScreen,
            MenuScreen,
            PokedexSelectScreen,
            PokedexScreen,
            PokedexViewScreen,
            PokedexSearchScreen,
            PokedexHabitatScreen,
            PokemonScreen,
            PokemonStatusScreen,
            InventoryScreen,
            BerryScreen,
            TrainerScreen,
            PauseScreen,
            SaveScreen,
            NewGameScreen,
            OptionScreen,
            StorageSystemScreen,
            TradeScreen,
            MapScreen,
            ChatScreen,
            UseItemScreen,
            ItemDetailScreen,
            ChooseItemScreen,
            ChoosePokemonScreen,
            EvolutionScreen,
            ApricornScreen,
            TransitionScreen,
            BattleCatchScreen,
            BlackOutScreen,
            BattlePokemonScreen,
            CreditsScreen,
            DonationScreen,
            NameObjectScreen,
            LearnAttackScreen,
            SecretBaseScreen,
            PokegearScreen,
            BattleGrowStatsScreen,
            DaycareScreen,
            HatchEggScreen,
            SplashScreen,
            GameJoltLoginScreen,
            GameJoltUserViewerScreen,
            GameJoltLobbyScreen,
            GameJoltAddFriendScreen,
            ChooseAttackScreen,
            BattleScreen,
            BattleIniScreen,
            BattleAnimationScreen,
            GtsMainScreen,
            GtsInboxScreen,
            GtsSearchScreen,
            GtsSelectLevelScreen,
            GtsSelectPokemonScreen,
            GtsSelectGenderScreen,
            GtsSetupScreen,
            GtsEditTradeScreen,
            GtsSelectAreaScreen,
            GtsSelectUserScreen,
            GtsTradeScreen,
            GtsTradingScreen,
            TeachMovesScreen,
            OfflineGameWarningScreen,
            HallofFameScreen,
            ViewModelScreen,
            MailSystemScreen,
            PvpLobbyScreen,
            InputScreen,
            JoinServerScreen,
            ConnectScreen,
            AddServerScreen,
            MysteryEventScreen,
            DirectTradeScreen,
            StorageSystemFilterScreen,
            WonderTradeScreen,
            RegisterBattleScreen,
            StatisticsScreen,
            MapPreviewScreen,
            IntroScreen
        }

        #region Shared values

        /// <summary>
        /// A global camera instance, that carries over screen instances.
        /// </summary>
        public static Camera Camera { get; set; }

        /// <summary>
        /// A global level instance, that carries over screen instances.
        /// </summary>
        public static ILevel Level { get; set; }

        /// <summary>
        /// A global BasicEffect instance, that carries over screen instances.
        /// </summary>
        public static BasicEffect Effect { get; set; }

        /// <summary>
        /// A global SkyDome instance, that carries over screen instances.
        /// </summary>
        public static ISkyDome SkyDome { get; set; }

        /// <summary>
        /// A global TextBox instance, that carries over screen instances.
        /// </summary>
        public static TextBox TextBox { get; set; } = new TextBox();

        /// <summary>
        /// A global ChooseBox instance, that carries over screen instances.
        /// </summary>
        public static IChooseBox ChooseBox { get; set; } //= new ChooseBox();

        /// <summary>
        /// A global PokemonImageView instance, that carries over screen instances.
        /// </summary>
        public static PokemonImageView PokemonImageView { get; set; } = new PokemonImageView();

        #endregion

        #region Fields

        /// <summary>
        /// A value to store the screen that underlies the current screen in.
        /// </summary>
        public Screen PreScreen { get; set; } = null;

        /// <summary>
        /// The ID of the screen.
        /// </summary>
        public Identifications Identification { get; set; } = Identifications.MainMenuScreen;

        /// <summary>
        /// Wether the mouse is visible on the screen.
        /// </summary>
        /// <remarks>The default value is "False".</remarks>
        public bool MouseVisible { get; set; }

        /// <summary>
        /// Wether the game can be paused when pressing Escape.
        /// </summary>
        /// <remarks>The default value is "True".</remarks>
        public bool CanBePaused { get; set; } = true;

        /// <summary>
        /// Wether the game can be muted by pressing M (default).
        /// </summary>
        /// <remarks>The default value is "True".</remarks>
        public bool CanMuteMusic { get; set; } = true;

        /// <summary>
        /// Wether the ChatScreen can be opened by pressing T (default).
        /// </summary>
        /// <remarks>The default value is "True".</remarks>
        public bool CanChat { get; set; } = true;

        /// <summary>
        /// Wether a screenshot can be taken by pressing F2 (default).
        /// </summary>
        /// <remarks>The default value is "True".</remarks>
        public bool CanTakeScreenshot { get; set; } = true;

        /// <summary>
        /// Wether the debug information can be drawn on the screen.
        /// </summary>
        /// <remarks>The default value is "True".</remarks>
        public bool CanDrawDebug { get; set; } = true;

        /// <summary>
        /// Wether the game can switch its fullscreen state by pressing F11 (default).
        /// </summary>
        /// <remarks>The default value is "True".</remarks>
        public bool CanGoFullscreen { get; set; } = true;

        //Sets if the screen gets updated during its set as a FadeOut screen on the TransitionScreen.
        public bool UpdateFadeOut = false;
        //Sets if the screen gets updated during its set as a FadeIn screen on the TransitionScreen.
        public bool UpdateFadeIn = false;

        #endregion

        /// <summary>
        /// Sets all default fields of the screen instance.
        /// </summary>
        /// <param name="identification">The ID of the screen.</param>
        /// <param name="mouseVisible">Sets if the mouse is visible on the screen.</param>
        /// <param name="canBePaused">Sets if the PauseScreen can be opened by pressing Escape.</param>
        /// <param name="canMuteMusic">Sets if the M button (default) can mute the music.</param>
        /// <param name="canChat">Sets if the T button (default) can open the chat screen.</param>
        /// <param name="canTakeScreenshot">Sets if the F2 button (default) can take a screenshot.</param>
        /// <param name="canDrawDebug">Sets if the debug information can be drawn on this screen.</param>
        /// <param name="canGoFullscreen">Sets if the F11 button (default) can sets the game to fullscreen (or back).</param>
        private void Setup(Identifications identification, bool mouseVisible, bool canBePaused, bool canMuteMusic, bool canChat, bool canTakeScreenshot, bool canDrawDebug, bool canGoFullscreen)
        {
            Identification = identification;
            MouseVisible = mouseVisible;
            CanBePaused = canBePaused;
            CanChat = canChat;
            CanDrawDebug = canDrawDebug;
            CanGoFullscreen = canGoFullscreen;
            CanMuteMusic = canMuteMusic;
            CanTakeScreenshot = canTakeScreenshot;
        }

        /// <summary>
        /// The base draw function of a screen.
        /// </summary>
        /// <remarks>Contains no default code.</remarks>
        public virtual void Draw() { }

        /// <summary>
        /// The base render function of the screen. Used to render models above sprites.
        /// </summary>
        public virtual void Render() { }

        /// <summary>
        /// The base update fucntion of a screen.
        /// </summary>
        /// <remarks>Contains no default code.</remarks>
        public virtual void Update() { }

        /// <summary>
        /// An event that gets raised when this screen gets changed to.
        /// </summary>
        /// <remarks>Contains no default code.</remarks>
        public virtual void ChangeTo() { }

        /// <summary>
        /// An event that gets raised when this screen gets changed from.
        /// </summary>
        /// <remarks>Contains no default code.</remarks>
        public virtual void ChangeFrom() { }

        /// <summary>
        /// Returns if this screen instance is the currently active screen (set in the global Basic.CurrentScreen).
        /// </summary>
        public bool IsCurrentScreen() => Core.CurrentScreen.Identification == Identification;

        /// <summary>
        /// An event that gets raised when the window handle size changed.
        /// </summary>
        /// <remarks>Contains no default code.</remarks>
        public virtual void SizeChanged() { }

        /// <summary>
        /// A void that gets raised when the mute option of the game gets toggled.
        /// </summary>
        /// <remarks>Contains no default code.</remarks>
        public virtual void ToggledMute() { }

        /// <summary>
        /// An event that is getting raised when the Escape button is getting pressed. The PauseScreen is getting brought up if the CanBePaused field is set to true.
        /// </summary>
        public virtual void EscapePressed()
        {
            //If the game can be paused on this screen, open the PauseScreen.
            if (Core.CurrentScreen.CanBePaused)
                Core.SetScreen(Screen.CreatePauseScreen(Core.CurrentScreen));
        }

        /// <summary>
        /// Draws XBOX controls on the bottom right of the screen.
        /// </summary>
        /// <param name="descriptions">The button types and descriptions.</param>
        /// <remarks>Calculates the position and calls DrawGamePadControls(Descriptions,Position)</remarks>
        public void DrawGamePadControls(Dictionary<Buttons, string> descriptions)
        {
            int x = Core.WindowSize.Width;
            //Store the x position of the start of the controls render.

            //Loop through the buttons and add to the x location.
            var keyArray = descriptions.Keys.ToArray();
            var valueArray = descriptions.Values.ToArray();
            for (var i = 0; i <= descriptions.Count - 1; i++)
            {
                switch (keyArray[i])
                {
                    case Buttons.A:
                    case Buttons.B:
                    case Buttons.X:
                    case Buttons.Y:
                    case Buttons.Start:
                    case Buttons.LeftStick:
                    case Buttons.RightStick:
                    case Buttons.LeftTrigger:
                    case Buttons.RightTrigger:
                        x -= 32 + 4;
                        break;
                    case Buttons.LeftShoulder:
                    case Buttons.RightShoulder:
                        x -= 64 + 4;
                        break;
                }

                //Add to the x location for the length of the string and a separator.
                x -= Convert.ToInt32(FontManager.MainFont.MeasureString(valueArray[i]).X) + 16;
            }

            //Finally, render the buttons:
            DrawGamePadControls(descriptions, new Vector2(x, Core.WindowSize.Height - 40));
        }

        /// <summary>
        /// Generic void to render XBOX Gamepad controls on the screen.
        /// </summary>
        /// <param name="descriptions">The button types and descriptions.</param>
        /// <param name="position">The position to draw the buttons.</param>
        public void DrawGamePadControls(Dictionary<Buttons, string> descriptions, Vector2 position)
        {
            //Only if a Gamepad is connected and the screen is active, render the buttons:
            if (GamePad.GetState(PlayerIndex.One).IsConnected && Core.GameOptions.GamePadEnabled && IsCurrentScreen())
            {
                //Transform the position to integers and store the current drawing position:
                int x = Convert.ToInt32(position.X);
                int y = Convert.ToInt32(position.Y);

                //Loop through the button list:
                var keyArray = descriptions.Keys.ToArray();
                var valueArray = descriptions.Values.ToArray();
                for (var i = 0; i <= descriptions.Count - 1; i++)
                {
                    string t = "GUI|GamePad|xboxController";
                    //Store the texture path.
                    int width = 32;
                    //Store the width of the image.
                    int height = 32;
                    //Store the height of the image.

                    //Get the correct button image and size (currently, all buttons use the same size of 32x32 pixels).
                    switch (keyArray[i])
                    {
                        case Buttons.A:
                            t += "ButtonA";
                            break;
                        case Buttons.B:
                            t += "ButtonB";
                            break;
                        case Buttons.X:
                            t += "ButtonX";
                            break;
                        case Buttons.Y:
                            t += "ButtonY";
                            break;
                        case Buttons.LeftShoulder:
                            t += "LeftShoulder";
                            break;
                        case Buttons.RightShoulder:
                            t += "RightShoulder";
                            break;
                        case Buttons.LeftStick:
                            t += "LeftStick";
                            break;
                        case Buttons.RightStick:
                            t += "RightStick";
                            break;
                        case Buttons.LeftTrigger:
                            t += "LeftTrigger";
                            break;
                        case Buttons.RightTrigger:
                            t += "RightTrigger";
                            break;
                        case Buttons.Start:
                            t += "Start";
                            break;
                    }

                    //Draw the buttons (first, the "shadow" with a black color, then the real button).
                    Core.SpriteBatch.Draw(TextureManager.GetTexture(t), new Rectangle(x + 2, y + 2, width, height), Color.Black);
                    Core.SpriteBatch.Draw(TextureManager.GetTexture(t), new Rectangle(x, y, width, height), Color.White);

                    //Add the button width and a little offset to the drawing position:
                    x += width + 4;

                    //Draw the button description (again, with a shadow):
                    Core.SpriteBatch.DrawString(FontManager.MainFont, valueArray[i], new Vector2(x + 3, y + 7), Color.Black);
                    Core.SpriteBatch.DrawString(FontManager.MainFont, valueArray[i], new Vector2(x, y + 4), Color.White);

                    //Add the text width and the offset for the next button description to the drawing position:
                    x += Convert.ToInt32(FontManager.MainFont.MeasureString(valueArray[i]).X) + 16;
                }
            }
        }

        /// <summary>
        /// Returns the screen status of the current screen. Override this function to return a screen state.
        /// </summary>
        public virtual string GetScreenStatus() => "Screen state not implemented for screen class: " + Identification;

        /// <summary>
        /// Returns the minimum size for the screen size to display a large interface before switching to the small size.
        /// </summary>
        /// <remarks>The default size is 800x620 pixels.</remarks>
        public virtual System.Drawing.Size GetScreenScaleMinimum() => new System.Drawing.Size(800, 620);


        public static void SetRenderDistance(EnvironmentTypeEnum environmentType, WeatherEnum weather)
        {
            if (weather == WeatherEnum.Fog)
            {
                Effect.FogStart = -40;
                Effect.FogEnd = 12;

                Camera.FarPlane = 15;
                goto endsub;
            }

            if (weather == WeatherEnum.Blizzard)
            {
                Effect.FogStart = -40;
                Effect.FogEnd = 20;

                Camera.FarPlane = 24;
                goto endsub;
            }

            if (weather == WeatherEnum.Thunderstorm)
            {
                Effect.FogStart = -40;
                Effect.FogEnd = 20;

                Camera.FarPlane = 24;
                goto endsub;
            }

            switch (environmentType)
            {
                case EnvironmentTypeEnum.Cave:
                case EnvironmentTypeEnum.Dark:
                case EnvironmentTypeEnum.Forest:
                    switch (Core.GameOptions.RenderDistance)
                    {
                        case 0:
                            Effect.FogStart = -2;
                            Effect.FogEnd = 19;

                            Camera.FarPlane = 20;
                            break;

                        case 1:
                            Effect.FogStart = -2;
                            Effect.FogEnd = 39;

                            Camera.FarPlane = 40;
                            break;

                        case 2:
                            Effect.FogStart = -2;
                            Effect.FogEnd = 59;

                            Camera.FarPlane = 60;
                            break;

                        case 3:
                            Effect.FogStart = -5;
                            Effect.FogEnd = 79;

                            Camera.FarPlane = 80;
                            break;

                        case 4:
                            Effect.FogStart = -20;
                            Effect.FogEnd = 99;

                            Camera.FarPlane = 100;
                            break;
                    }
                    break;

                case EnvironmentTypeEnum.Inside:
                    switch (Core.GameOptions.RenderDistance)
                    {
                        case 0:
                            Effect.FogStart = 16;
                            Effect.FogEnd = 19;

                            Camera.FarPlane = 20;
                            break;

                        case 1:
                            Effect.FogStart = 36;
                            Effect.FogEnd = 39;

                            Camera.FarPlane = 40;
                            break;

                        case 2:
                            Effect.FogStart = 56;
                            Effect.FogEnd = 59;

                            Camera.FarPlane = 60;
                            break;

                        case 3:
                            Effect.FogStart = 76;
                            Effect.FogEnd = 79;

                            Camera.FarPlane = 80;
                            break;

                        case 4:
                            Effect.FogStart = 96;
                            Effect.FogEnd = 99;

                            Camera.FarPlane = 100;
                            break;
                    }
                    break;

                case EnvironmentTypeEnum.Outside:
                    //switch (World.World.GetTime)
                    switch (DayTime.Night)
                    {
                        case DayTime.Night:
                            switch (Core.GameOptions.RenderDistance)
                            {
                                case 0:
                                    Effect.FogStart = -2;
                                    Effect.FogEnd = 19;

                                    Camera.FarPlane = 20;
                                    break;

                                case 1:
                                    Effect.FogStart = -2;
                                    Effect.FogEnd = 39;

                                    Camera.FarPlane = 40;
                                    break;

                                case 2:
                                    Effect.FogStart = -2;
                                    Effect.FogEnd = 59;

                                    Camera.FarPlane = 60;
                                    break;

                                case 3:
                                    Effect.FogStart = -5;
                                    Effect.FogEnd = 79;

                                    Camera.FarPlane = 80;
                                    break;

                                case 4:
                                    Effect.FogStart = -20;
                                    Effect.FogEnd = 99;

                                    Camera.FarPlane = 100;
                                    break;
                            }
                            break;

                        case DayTime.Morning:
                            switch (Core.GameOptions.RenderDistance)
                            {
                                case 0:
                                    Screen.Effect.FogStart = 16;
                                    Screen.Effect.FogEnd = 19;

                                    Screen.Camera.FarPlane = 20;
                                    break;

                                case 1:
                                    Screen.Effect.FogStart = 36;
                                    Screen.Effect.FogEnd = 39;

                                    Screen.Camera.FarPlane = 40;
                                    break;

                                case 2:
                                    Screen.Effect.FogStart = 56;
                                    Screen.Effect.FogEnd = 59;

                                    Screen.Camera.FarPlane = 60;
                                    break;

                                case 3:
                                    Screen.Effect.FogStart = 76;
                                    Screen.Effect.FogEnd = 79;

                                    Screen.Camera.FarPlane = 80;
                                    break;

                                case 4:
                                    Screen.Effect.FogStart = 96;
                                    Screen.Effect.FogEnd = 99;

                                    Screen.Camera.FarPlane = 100;
                                    break;
                            }
                            break;

                        case DayTime.Day:
                            switch (Core.GameOptions.RenderDistance)
                            {
                                case 0:
                                    Screen.Effect.FogStart = 16;
                                    Screen.Effect.FogEnd = 19;

                                    Screen.Camera.FarPlane = 20;
                                    break;

                                case 1:
                                    Screen.Effect.FogStart = 36;
                                    Screen.Effect.FogEnd = 39;

                                    Screen.Camera.FarPlane = 40;
                                    break;

                                case 2:
                                    Screen.Effect.FogStart = 56;
                                    Screen.Effect.FogEnd = 59;

                                    Screen.Camera.FarPlane = 60;
                                    break;

                                case 3:
                                    Screen.Effect.FogStart = 76;
                                    Screen.Effect.FogEnd = 79;

                                    Screen.Camera.FarPlane = 80;
                                    break;

                                case 4:
                                    Screen.Effect.FogStart = 96;
                                    Screen.Effect.FogEnd = 99;

                                    Screen.Camera.FarPlane = 100;
                                    break;
                            }
                            break;

                        case DayTime.Evening:
                            switch (Core.GameOptions.RenderDistance)
                            {
                                case 0:
                                    Screen.Effect.FogStart = 0;
                                    Screen.Effect.FogEnd = 19;

                                    Screen.Camera.FarPlane = 20;
                                    break;

                                case 1:
                                    Screen.Effect.FogStart = 0;
                                    Screen.Effect.FogEnd = 39;

                                    Screen.Camera.FarPlane = 40;
                                    break;

                                case 2:
                                    Screen.Effect.FogStart = 0;
                                    Screen.Effect.FogEnd = 59;

                                    Screen.Camera.FarPlane = 60;
                                    break;

                                case 3:
                                    Screen.Effect.FogStart = 0;
                                    Screen.Effect.FogEnd = 79;

                                    Screen.Camera.FarPlane = 80;
                                    break;

                                case 4:
                                    Screen.Effect.FogStart = 0;
                                    Screen.Effect.FogEnd = 99;

                                    Screen.Camera.FarPlane = 100;
                                    break;
                            }
                            break;
                    }
                    break;

                case EnvironmentTypeEnum.Underwater:
                    switch (Core.GameOptions.RenderDistance)
                    {
                        case 0:
                            Effect.FogStart = 0;
                            Effect.FogEnd = 19;

                            Camera.FarPlane = 20;
                            break;

                        case 1:
                            Effect.FogStart = 0;
                            Effect.FogEnd = 39;

                            Camera.FarPlane = 40;
                            break;

                        case 2:
                            Effect.FogStart = 0;
                            Effect.FogEnd = 59;

                            Camera.FarPlane = 60;
                            break;

                        case 3:
                            Effect.FogStart = 0;
                            Effect.FogEnd = 79;

                            Camera.FarPlane = 80;
                            break;

                        case 4:
                            Effect.FogStart = 0;
                            Effect.FogEnd = 99;

                            Camera.FarPlane = 100;
                            break;
                    }
                    break;
            }

            if (Core.GameOptions.RenderDistance >= 5)
            {
                Effect.FogStart = 999;
                Effect.FogEnd = 1000;

                Camera.FarPlane = 1000;
            }
            endsub:

            Camera.CreateNewProjection(Camera.FOV);
        }
    }
}

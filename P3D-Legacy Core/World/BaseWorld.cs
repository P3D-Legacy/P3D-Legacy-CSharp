using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Screens;
using P3D.Legacy.Core.Extensions;

namespace P3D.Legacy.Core.World
{
    public abstract class BaseWorld
    {
        public abstract EnvironmentTypeEnum EnvironmentType { get; set; }
        public abstract WeatherEnum CurrentWeather { get; set; }

        public abstract void Initialize(int environmentType, int weatherType);



        /// <summary>
        /// Returns the region weather and gets the server weather if needed.
        /// </summary>
        public static WeatherEnum GetCurrentRegionWeather() => NeedServerObject ? ServerWeather : RegionWeather;

        public static WeatherEnum RegionWeather { get; set; } = WeatherEnum.Clear;

        public static bool RegionWeatherSet { get; set; } = false;

        public static bool IsAurora { get; set; } = false;

        public static int WeekOfYear => Convert.ToInt32(((DateTime.Now.DayOfYear - ((int)DateTime.Now.DayOfWeek - 1)) / 7) + 1);

        public static SeasonEnum CurrentSeason
        {
            get
            {
                if (NeedServerObject)
                    return ServerSeason;

                switch (WeekOfYear % 4)
                {
                    case 1:
                        return SeasonEnum.Winter;
                    case 2:
                        return SeasonEnum.Spring;
                    case 3:
                        return SeasonEnum.Summer;
                    case 0:
                        return SeasonEnum.Fall;
                }
                return SeasonEnum.Summer;
            }
        }

        public static DayTime GetTime
        {
            get
            {
                var time = DayTime.Day;

                var hour = DateTime.Now.Hour;
                if (NeedServerObject)
                {
                    var data = ServerTimeData.Split(Convert.ToChar(","));
                    hour = Convert.ToInt32(data[0]);
                }

                switch (CurrentSeason)
                {
                    case SeasonEnum.Winter:
                        if (hour > 18 || hour < 7)
                            time = DayTime.Night;
                        else if (hour > 6 && hour < 11)
                            time = DayTime.Morning;
                        else if (hour > 10 && hour < 17)
                            time = DayTime.Day;
                        else if (hour > 16 && hour < 19)
                            time = DayTime.Evening;
                        break;

                    case SeasonEnum.Spring:
                        if (hour > 19 || hour < 5)
                            time = DayTime.Night;
                        else if (hour > 4 && hour < 10)
                            time = DayTime.Morning;
                        else if (hour > 9 && hour < 17)
                            time = DayTime.Day;
                        else if (hour > 16 && hour < 20)
                            time = DayTime.Evening;
                        break;

                    case SeasonEnum.Summer:
                        if (hour > 20 || hour < 4)
                            time = DayTime.Night;
                        else if (hour > 3 && hour < 9)
                            time = DayTime.Morning;
                        else if (hour > 8 && hour < 19)
                            time = DayTime.Day;
                        else if (hour > 18 && hour < 21)
                            time = DayTime.Evening;
                        break;

                    case SeasonEnum.Fall:
                        if (hour > 19 || hour < 6)
                            time = DayTime.Night;
                        else if (hour > 5 && hour < 10)
                            time = DayTime.Morning;
                        else if (hour > 9 && hour < 18)
                            time = DayTime.Day;
                        else if (hour > 17 && hour < 20)
                            time = DayTime.Evening;
                        break;
                }

                return time;
            }
        }

        public static SeasonEnum ServerSeason = SeasonEnum.Spring;
        public static WeatherEnum ServerWeather = WeatherEnum.Clear; //Format: Hour,Minute,Second
        public static string ServerTimeData = "0,0,0";

        public static DateTime LastServerDataReceived = DateTime.Now;
        public static int SecondsOfDay
        {
            get
            {
                if (NeedServerObject)
                {
                    string[] data = ServerTimeData.Split(Convert.ToChar(","));
                    int hours = Convert.ToInt32(data[0]);
                    int minutes = Convert.ToInt32(data[1]);
                    int seconds = Convert.ToInt32(data[2]);

                    seconds += Convert.ToInt32(Math.Abs((DateTime.Now - LastServerDataReceived).Seconds)); // TODO: TotalSeconds?

                    return hours * 3600 + minutes * 60 + seconds;
                }
                else
                {
                    return DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second;
                }
            }
        }

        public static int MinutesOfDay
        {
            get
            {
                if (NeedServerObject)
                {
                    string[] data = ServerTimeData.Split(Convert.ToChar(","));
                    int hours = Convert.ToInt32(data[0]);
                    int minutes = Convert.ToInt32(data[1]);

                    minutes += Convert.ToInt32(Math.Abs((DateTime.Now - LastServerDataReceived).Minutes)); // TODO: TotalMinutes?

                    return hours * 60 + minutes;
                }
                else
                {
                    return DateTime.Now.Hour * 60 + DateTime.Now.Minute;
                }
            }
        }

        private static bool NeedServerObject => BaseJoinServerScreen.Online && BaseConnectScreen.Connected;

        public static WeatherEnum GetRegionWeather(SeasonEnum season)
        {
            var r = Core.Random.Next(0, 100);

            switch (season)
            {
                case SeasonEnum.Winter:
                    if (r < 20)
                        return WeatherEnum.Rain;
                    else if (r >= 20 && r < 50)
                        return WeatherEnum.Clear;
                    else
                        return WeatherEnum.Snow;

                case SeasonEnum.Spring:
                    if (r < 5)
                        return WeatherEnum.Snow;
                    else if (r >= 5 && r < 40)
                        return WeatherEnum.Rain;
                    else
                        return WeatherEnum.Clear;

                case SeasonEnum.Summer:
                    if (r < 10)
                        return WeatherEnum.Rain;
                    else
                        return WeatherEnum.Clear;

                case SeasonEnum.Fall:
                    if (r < 5)
                        return WeatherEnum.Snow;
                    else if (r >= 5 && r < 80)
                        return WeatherEnum.Rain;
                    else
                        return WeatherEnum.Clear;
            }

            return WeatherEnum.Clear;
        }

        public static WeatherEnum GetWeatherFromWeatherType(int weatherType)
        {
            switch (weatherType)
            {
                case 0:
                    //RegionWeather
                    return GetCurrentRegionWeather();
                case 1:
                    //Clear
                    return WeatherEnum.Clear;
                case 2:
                    //Rain
                    return WeatherEnum.Rain;
                case 3:
                    //Snow
                    return WeatherEnum.Snow;
                case 4:
                    //Underwater
                    return WeatherEnum.Underwater;
                case 5:
                    //Sunny
                    return WeatherEnum.Sunny;
                case 6:
                    //Fog
                    return WeatherEnum.Fog;
                case 7:
                    //Sandstorm
                    return WeatherEnum.Sandstorm;
                case 8:
                    return WeatherEnum.Ash;
                case 9:
                    return WeatherEnum.Blizzard;
                case 10:
                    return WeatherEnum.Thunderstorm;
            }
            return WeatherEnum.Clear;
        }
        public static int GetWeatherTypeFromWeather(WeatherEnum weather)
        {
            switch (weather)
            {
                case WeatherEnum.Clear:
                    return 1;
                case WeatherEnum.Rain:
                    return 2;
                case WeatherEnum.Snow:
                    return 3;
                case WeatherEnum.Underwater:
                    return 4;
                case WeatherEnum.Sunny:
                    return 5;
                case WeatherEnum.Fog:
                    return 6;
                case WeatherEnum.Sandstorm:
                    return 7;
                case WeatherEnum.Ash:
                    return 8;
                case WeatherEnum.Blizzard:
                    return 9;
                case WeatherEnum.Thunderstorm:
                    return 10;
                default:
                    return 0;
            }
        }

        private static Vector2 _weatherOffset = new Vector2(0, 0);

        private static List<Rectangle> _objectsList = new List<Rectangle>();
        public static WeatherEnum[] NoParticlesList = { WeatherEnum.Clear, WeatherEnum.Sunny, WeatherEnum.Fog };

        /*
        public static void DrawWeather(WeatherEnum mapWeather)
        {
            if (NoParticlesList.Contains(mapWeather) == false)
            {
                if (Core.GameOptions.GraphicStyle == 1)
                {
                    Screen.Identifications[] identifications = {
                        Screen.Identifications.OverworldScreen,
                        Screen.Identifications.MainMenuScreen,
                        Screen.Identifications.BattleScreen,
                        Screen.Identifications.BattleCatchScreen
                    };

                    if (identifications.Contains(Core.CurrentScreen.Identification))
                    {
                        if (Core.CurrentScreen.Identification == Screen.Identifications.OverworldScreen)
                        {
                            if (Screen.TextBox.Showing == false)
                                GenerateParticles(0, mapWeather);
                        }
                        else
                            GenerateParticles(0, mapWeather);
                    }
                }
                else
                {
                    Texture2D T = null;

                    int size = 128;
                    int opacity = 30;

                    switch (mapWeather)
                    {
                        case WeatherEnum.Rain:
                            T = TextureManager.GetTexture("Textures\\Weather\\rain");

                            _weatherOffset.X += 8;
                            _weatherOffset.Y += 16;
                            break;

                        case WeatherEnum.Thunderstorm:
                            T = TextureManager.GetTexture("Textures\\Weather\\rain");

                            _weatherOffset.X += 12;
                            _weatherOffset.Y += 20;

                            opacity = 50;
                            break;

                        case WeatherEnum.Snow:
                            T = TextureManager.GetTexture("Textures\\Weather\\snow");

                            _weatherOffset.X += 1;
                            _weatherOffset.Y += 1;
                            break;

                        case WeatherEnum.Blizzard:
                            T = TextureManager.GetTexture("Textures\\Weather\\snow");

                            _weatherOffset.X += 8;
                            _weatherOffset.Y += 2;

                            opacity = 80;
                            break;

                        case WeatherEnum.Sandstorm:
                            T = TextureManager.GetTexture("Textures\\Weather\\sand");

                            _weatherOffset.X += 4;
                            _weatherOffset.Y += 1;

                            opacity = 80;
                            size = 48;
                            break;

                        case WeatherEnum.Underwater:
                            T = TextureManager.GetTexture("Textures\\Weather\\bubble");

                            if (Core.Random.Next(0, 100) == 0)
                            {
                                _objectsList.Add(new Rectangle(Core.Random.Next(0, Core.WindowSize.Width - 32), Core.WindowSize.Height, 32, 32));
                            }

                            for (var i = 0; i <= _objectsList.Count - 1; i++)
                            {
                                Rectangle r = _objectsList[i];
                                _objectsList[i] = new Rectangle(r.X, r.Y - 2, r.Width, r.Height);

                                Core.SpriteBatch.Draw(T, _objectsList[i], new Color(255, 255, 255, 150));
                            }

                            break;

                        case WeatherEnum.Ash:
                            T = TextureManager.GetTexture("Textures\\Weather\\ash2");

                            _weatherOffset.Y += 1;
                            opacity = 65;
                            size = 48;
                            break;
                    }

                    if (_weatherOffset.X >= size)
                        _weatherOffset.X = 0;

                    if (_weatherOffset.Y >= size)
                        _weatherOffset.Y = 0;

                    switch (mapWeather)
                    {
                        case WeatherEnum.Rain:
                        case WeatherEnum.Snow:
                        case WeatherEnum.Sandstorm:
                        case WeatherEnum.Ash:
                        case WeatherEnum.Blizzard:
                        case WeatherEnum.Thunderstorm:
                            for (var x = -size; x <= Core.WindowSize.Width; x += size)
                            {
                                for (var y = -size; y <= Core.WindowSize.Height; y += size)
                                {
                                    Core.SpriteBatch.Draw(T, new Rectangle(Convert.ToInt32(x + _weatherOffset.X), Convert.ToInt32(y + _weatherOffset.Y), size, size), new Color(255, 255, 255, opacity));
                                }
                            }

                            break;
                    }
                }
            }
        }
        */

        /*
        public static void GenerateParticles(int chance, WeatherEnum mapWeather)
        {
            if (mapWeather == WeatherEnum.Thunderstorm)
            {
                if (Core.Random.Next(0, 250) == 0)
                {
                    float pitch = -(Core.Random.Next(8, 11) / 10f);
                    //Debug.Print(pitch.ToString())
                    SoundManager.PlaySound("Battle\\Effects\\effect_thunderbolt", pitch, 0f, SoundManager.Volume, false);
                }
            }

            if (!BaseLevelLoader.IsBusy)
            {
                Screen.Identifications[] validScreen = {
                    Screen.Identifications.OverworldScreen,
                    Screen.Identifications.BattleScreen,
                    Screen.Identifications.BattleCatchScreen,
                    Screen.Identifications.MainMenuScreen
                };

                if (validScreen.Contains(Core.CurrentScreen.Identification) == true)
                {
                    if (Core.CurrentScreen.Identification == Screen.Identifications.OverworldScreen)
                    {
                        if (((BaseOverworldScreen) Core.CurrentScreen).ActionScript.IsReady == false)
                        {
                            return;
                        }
                    }

                    Texture2D T = null;

                    float speed = 0;
                    Vector3 scale = new Vector3(1);
                    int range = 3;

                    switch (mapWeather)
                    {
                        case WeatherEnum.Rain:
                            speed = 0.1f;
                            T = TextureManager.GetTexture("Textures\\Weather\\rain3");
                            if (chance > -1)
                                chance = 3;
                            scale = new Vector3(0.03f, 0.06f, 0.1f);
                            break;
                        case WeatherEnum.Thunderstorm:
                            speed = 0.15f;
                            switch (Core.Random.Next(0, 4))
                            {
                                case 0:
                                    T = TextureManager.GetTexture("Textures\\Weather\\rain2");
                                    scale = new Vector3(0.1f, 0.1f, 0.1f);
                                    break;
                                default:
                                    T = TextureManager.GetTexture("Textures\\Weather\\rain3");
                                    scale = new Vector3(0.03f, 0.06f, 0.1f);
                                    break;
                            }
                            if (chance > -1)
                                chance = 1;
                            break;
                        case WeatherEnum.Snow:
                            speed = 0.02f;
                            T = TextureManager.GetTexture("Textures\\Weather\\snow2");
                            if (chance > -1)
                                chance = 5;
                            scale = new Vector3(0.03f, 0.03f, 0.1f);
                            break;
                        case WeatherEnum.Underwater:
                            speed = -0.02f;
                            T = TextureManager.GetTexture("Textures\\Weather\\bubble");
                            if (chance > -1)
                                chance = 60;
                            scale = new Vector3(0.5f);
                            range = 1;
                            break;
                        case WeatherEnum.Sandstorm:
                            speed = 0.1f;
                            T = TextureManager.GetTexture("Textures\\Weather\\sand");
                            if (chance > -1)
                                chance = 4;
                            scale = new Vector3(0.03f, 0.03f, 0.1f);
                            break;
                        case WeatherEnum.Ash:
                            speed = 0.02f;
                            T = TextureManager.GetTexture("Textures\\Weather\\ash");
                            if (chance > -1)
                                chance = 20;
                            scale = new Vector3(0.03f, 0.03f, 0.1f);
                            break;
                        case WeatherEnum.Blizzard:
                            speed = 0.1f;
                            T = TextureManager.GetTexture("Textures\\Weather\\snow");
                            if (chance > -1)
                                chance = 1;
                            scale = new Vector3(0.12f, 0.12f, 0.1f);
                            break;
                    }

                    if (chance == -1)
                    {
                        chance = 1;
                    }

                    Vector3 cameraPosition = Screen.Camera.Position;
                    if (Screen.Camera.Name == "Overworld")
                    {
                        cameraPosition = ((BaseOverworldCamera) Screen.Camera).CPosition;
                    }

                    if (Core.Random.Next(0, chance) == 0)
                    {
                        for (var x = cameraPosition.X - range; x <= cameraPosition.X + range; x++)
                        {
                            for (var z = cameraPosition.Z - range; z <= cameraPosition.Z + range; z++)
                            {
                                if (z != 0 || x != 0)
                                {
                                    float rY = Convert.ToSingle(Core.Random.Next(0, 40) / 10) - 2f;
                                    float rX = Convert.ToSingle(Core.Random.NextDouble()) - 0.5f;
                                    float rZ = Convert.ToSingle(Core.Random.NextDouble()) - 0.5f;
                                    //Particle p = new Particle(new Vector3(x + rX, cameraPosition.Y + 1.8f + rY, z + rZ), { T }, { 0, 0 }, Core.Random.Next(0, 2), scale, BaseModel.BillModel, new Vector3(1));
                                    p.MoveSpeed = speed;
                                    if (mapWeather == WeatherEnum.Rain)
                                    {
                                        p.Opacity = 0.7f;
                                    }
                                    if (mapWeather == WeatherEnum.Thunderstorm)
                                    {
                                        p.Opacity = 1f;
                                    }
                                    if (mapWeather == WeatherEnum.Underwater)
                                    {
                                        p.Position.Y = 0f;
                                        p.Destination = 10;
                                        p.Behavior = Particle.Behaviors.Rising;
                                    }
                                    if (mapWeather == WeatherEnum.Sandstorm)
                                    {
                                        p.Behavior = Particle.Behaviors.LeftToRight;
                                        p.Destination = cameraPosition.X + 5;
                                        p.Position.X -= 2;
                                    }
                                    if (mapWeather == WeatherEnum.Blizzard)
                                    {
                                        p.Opacity = 1f;
                                    }
                                    Screen.Level.Entities.Add(p);
                                }
                            }
                        }
                    }
                }
            }
        }
        */

        private static Dictionary<Texture2D, Texture2D> _seasonTextureBuffer = new Dictionary<Texture2D, Texture2D>();

        private static SeasonEnum _bufferSeason = SeasonEnum.Fall;
        public static Texture2D GetSeasonTexture(Texture2D seasonTexture, Texture2D T)
        {
            if (_bufferSeason != CurrentSeason)
            {
                _bufferSeason = CurrentSeason;
                _seasonTextureBuffer.Clear();
            }

            if ((T == null) == false)
            {
                if (_seasonTextureBuffer.ContainsKey(T))
                {
                    return _seasonTextureBuffer[T];
                }

                int x = 0;
                int y = 0;
                switch (CurrentSeason)
                {
                    case SeasonEnum.Winter:
                        x = 0;
                        y = 0;
                        break;
                    case SeasonEnum.Spring:
                        x = 2;
                        y = 0;
                        break;
                    case SeasonEnum.Summer:
                        x = 0;
                        y = 2;
                        break;
                    case SeasonEnum.Fall:
                        x = 2;
                        y = 2;
                        break;
                }

                Color[] inputColors = new [] 
                {
                    new Color(0, 0, 0),
                    new Color(85, 85, 85),
                    new Color(170, 170, 170),
                    new Color(255, 255, 255)
                }.Reverse().ToArray();
                List<Color> outputColors = new List<Color>();

                Color[] data = new Color[4];
                seasonTexture.GetData(0, new Rectangle(x, y, 2, 2), data, 0, 4);

                _seasonTextureBuffer.Add(T, T.ReplaceColors(inputColors, data));
                return _seasonTextureBuffer[T];
            }
            return null;
        }
    }
}

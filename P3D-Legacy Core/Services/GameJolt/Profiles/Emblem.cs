using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Extensions;
using P3D.Legacy.Core.Network;
using P3D.Legacy.Core.Pokemon;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Screens;
using P3D.Legacy.Core.Screens.GUI;

namespace P3D.Legacy.Core.GameJolt.Profiles
{
    /// <summary>
    /// A class to handle emblem rendering and management.
    /// </summary>
    public class Emblem
    {
        #region "Enumrations"

        /// <summary>
        /// The names of male trainer Element.Types.
        /// </summary>
        public enum MaleEmblemSpriteType
        {
            Preschooler = 0,
            SchoolKid = 1,
            Youngster = 2,
            Waiter = 3,
            Backpacker = 4,
            Pokefan = 5,
            Butler = 6,
            Cheerleader = 7,
            Clerk = 8,
            PokemonBreeder = 9,
            Drummer = 10,
            Cyclist = 11,
            RichBoy = 12,
            Ranger = 13,
            Athlete = 14,
            Scientist = 15,
            Doctor = 16,
            Gentleman = 17,
            AceTrainer = 18,
            Veteran = 19
        }

        /// <summary>
        /// The names of female trainer Element.Types.
        /// </summary>
        public enum FemaleEmblemSpriteType
        {
            Preschooler = 0,
            SchoolKid = 1,
            Lass = 2,
            Waitress = 3,
            Backpacker = 4,
            Pokefan = 5,
            Maid = 6,
            Cheerleader = 7,
            Clerk = 8,
            PokemonBreeder = 9,
            Guitarist = 10,
            Cyclist = 11,
            Lady = 12,
            Ranger = 13,
            Athlete = 14,
            Scientist = 15,
            Nurse = 16,
            Socialite = 17,
            AceTrainer = 18,
            Veteran = 19
        }

        /// <summary>
        /// The sprites of male trainers.
        /// </summary>
        public enum MaleEmblemSprites
        {
            NN40 = 0,
            NN42 = 1,
            NN44 = 2,
            NN46 = 3,
            NN48 = 4,
            NN51 = 5,
            NN74 = 6,
            NN76 = 7,
            NN53 = 8,
            NN55 = 9,
            NN80 = 10,
            NN72 = 11,
            NN57 = 12,
            NN59 = 13,
            NN78 = 14,
            NN64 = 15,
            NN66 = 16,
            NN68 = 17,
            NN70 = 18,
            NN38 = 19
        }

        /// <summary>
        /// The sprites of female trainers.
        /// </summary>
        public enum FemaleEmblemSprites
        {
            NN41 = 0,
            NN43 = 1,
            NN45 = 2,
            NN47 = 3,
            NN50 = 4,
            NN52 = 5,
            NN75 = 6,
            NN77 = 7,
            NN54 = 8,
            NN56 = 9,
            NN81 = 10,
            NN73 = 11,
            NN58 = 12,
            NN60 = 13,
            NN79 = 14,
            NN65 = 15,
            NN67 = 16,
            NN69 = 17,
            NN71 = 18,
            NN39 = 19
        }

        #endregion

        /// <summary>
        /// Renders an emblem to the screen.
        /// </summary>
        /// <param name="Name">The name of the player.</param>
        /// <param name="ID">The GameJolt ID.</param>
        /// <param name="Points">The points of the player.</param>
        /// <param name="Gender">The gender of the player (0=male, 1=female).</param>
        /// <param name="EmblemBackground">The emblem background name.</param>
        /// <param name="Position">The position on the screen.</param>
        /// <param name="Scale">The scale of the emblem.</param>
        /// <param name="SpriteTexture">An alternative sprite to draw.</param>
        public static void Draw(string Name, string ID, int Points, string Gender, string EmblemBackground, Vector2 Position, float Scale, Texture2D SpriteTexture)
        {
            Draw(Name, ID, Points, Gender, EmblemBackground, Position, Scale, SpriteTexture, null);
        }

        /// <summary>
        /// Renders an emblem to the screen.
        /// </summary>
        /// <param name="Name">The name of the player.</param>
        /// <param name="ID">The GameJolt ID.</param>
        /// <param name="Points">The points of the player.</param>
        /// <param name="Gender">The gender of the player (0=male, 1=female).</param>
        /// <param name="EmblemBackground">The emblem background name.</param>
        /// <param name="Position">The position on the screen.</param>
        /// <param name="Scale">The scale of the emblem.</param>
        /// <param name="SpriteTexture">An alternative sprite to draw.</param>
        /// <param name="PokemonList">A list of 0-6 Pokémon to render below the player information.</param>
        public static void Draw(string Name, string ID, int Points, string Gender, string EmblemBackground, Vector2 Position, float Scale, Texture2D SpriteTexture, List<BasePokemon> PokemonList)
        {
            //Generate OT:
            string OT = ID;
            while (OT.Length < 5)
            {
                OT = "0" + OT;
            }

            //Check if user is banned.
            bool UserBanned = API.UserBanned(ID);

            string PlayerName = Name + " (" + OT + ")";

            string PlayerPoints = Convert.ToString(Points);
            int PlayerLevel = GetPlayerLevel(Points);

            Texture2D PlayerTexture = SpriteTexture;
            if (PlayerTexture == null)
            {
                PlayerTexture = GetPlayerSprite(PlayerLevel, ID, Gender);
            }

            Size frameSize = new Size(Convert.ToInt32(PlayerTexture.Width / 3), Convert.ToInt32(PlayerTexture.Height / 4));

            string PlayerTitle = GetPlayerTitle(PlayerLevel, ID, Gender);
            if (UserBanned == true)
            {
                PlayerTitle = "Lonely";
            }

            Texture2D EmblemBackgroundTexture = null;
            Microsoft.Xna.Framework.Color EmblemFontColor = GetEmblemFontColor(EmblemBackground);

            if (UserBanned == true)
            {
                EmblemBackgroundTexture = GetEmblemBackgroundTexture("missingno");
                EmblemFontColor = Microsoft.Xna.Framework.Color.White;
            }
            else
            {
                EmblemBackgroundTexture = GetEmblemBackgroundTexture(EmblemBackground);
            }

            Core.SpriteBatch.Draw(EmblemBackgroundTexture, new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(128 * Scale), Convert.ToInt32(32 * Scale)), Microsoft.Xna.Framework.Color.White);
            Core.SpriteBatch.Draw(PlayerTexture, new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(32 * Scale), Convert.ToInt32(32 * Scale)), new Microsoft.Xna.Framework.Rectangle(0, frameSize.Height * 2, frameSize.Width, frameSize.Height), Microsoft.Xna.Framework.Color.White);

            if (PokemonList == null || PokemonList.Count == 0)
            {
                Core.SpriteBatch.DrawString(FontManager.MiniFont, PlayerName + Environment.NewLine + PlayerTitle + Environment.NewLine + Environment.NewLine + "Level: " + PlayerLevel + Environment.NewLine + "(Points: " + PlayerPoints + ")", new Vector2(32 * Scale + 10 + Position.X, 10 + Position.Y), EmblemFontColor, 0f, Vector2.Zero, Convert.ToSingle(Scale / 4), SpriteEffects.None, 0f);
            }
            else
            {
                Core.SpriteBatch.DrawString(FontManager.MiniFont, PlayerName + Environment.NewLine + PlayerTitle + Environment.NewLine + "Level: " + PlayerLevel + Environment.NewLine + "(Points: " + PlayerPoints + ")", new Vector2(32 * Scale + 10 + Position.X, 6 + Position.Y), EmblemFontColor, 0f, Vector2.Zero, Convert.ToSingle(Scale / 4), SpriteEffects.None, 0f);

                for (var i = 0; i <= 5; i++)
                {
                    Core.SpriteBatch.Draw(TextureManager.GetTexture("GUI\\Menus\\Menu", new Microsoft.Xna.Framework.Rectangle(192, 0, 32, 32), ""), new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(32 * Scale + (10 / 4) * Scale + Position.X + i * (10 * Scale)), Convert.ToInt32(Position.Y + 22.5f * Scale), Convert.ToInt32(Scale * 8), Convert.ToInt32(Scale * 8)), Microsoft.Xna.Framework.Color.White);

                    if (PokemonList.Count - 1 >= i)
                    {
                        var p = PokemonList[i];
                        Core.SpriteBatch.Draw(p.GetMenuTexture(), new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(32 * Scale + (10 / 4) * Scale + Position.X + i * (10 * Scale)), Convert.ToInt32(Position.Y + 22.5f * Scale), Convert.ToInt32(Scale * 8), Convert.ToInt32(Scale * 8)), Microsoft.Xna.Framework.Color.White);
                    }
                }
            }

            if (UserBanned == false)
            {
                if (IsFriend(ID) == true)
                {
                    Core.SpriteBatch.Draw(TextureManager.GetTexture("GUI\\Menus\\Menu", new Microsoft.Xna.Framework.Rectangle(80, 144, 32, 32), ""), new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(32 * Convert.ToSingle(Scale / 4)), Convert.ToInt32(32 * Convert.ToSingle(Scale / 4))), Microsoft.Xna.Framework.Color.White);
                }
                if (SentRequest(ID) == true)
                {
                    Core.SpriteBatch.Draw(TextureManager.GetTexture("GUI\\Menus\\Menu", new Microsoft.Xna.Framework.Rectangle(112, 176, 32, 32), ""), new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(32 * Convert.ToSingle(Scale / 4)), Convert.ToInt32(32 * Convert.ToSingle(Scale / 4))), Microsoft.Xna.Framework.Color.White);
                }
                if (ReceivedRequest(ID) == true)
                {
                    Core.SpriteBatch.Draw(TextureManager.GetTexture("GUI\\Menus\\Menu", new Microsoft.Xna.Framework.Rectangle(80, 176, 32, 32), ""), new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(32 * Convert.ToSingle(Scale / 4)), Convert.ToInt32(32 * Convert.ToSingle(Scale / 4))), Microsoft.Xna.Framework.Color.White);
                }
            }
            else
            {
                Core.SpriteBatch.Draw(TextureManager.GetTexture("GUI\\Menus\\Menu", new Microsoft.Xna.Framework.Rectangle(144, 176, 32, 32), ""), new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(32 * Convert.ToSingle(Scale / 4)), Convert.ToInt32(32 * Convert.ToSingle(Scale / 4))), Microsoft.Xna.Framework.Color.White);
            }
        }

        #region "EmblemHelperFunctions"

        public static int GetPlayerLevel(int playerPoints)
        {
            int level = Convert.ToInt32(Math.Floor(3.xRoot((4 / 5) * playerPoints * 10)));

            level = level.Clamp(1, 100);

            return level;
        }

        public static string GetPlayerTitle(int level, string id, string gender)
        {
            foreach (StaffProfile staffMember in StaffProfile.Staff)
            {
                if (staffMember.GameJoltID.ToLower() == id.ToLower() && !string.IsNullOrEmpty(staffMember.RankName))
                {
                    return staffMember.RankName;
                }
            }

            int t = Convert.ToInt32(Math.Ceiling(level / 5d)) - 1;

            t = t.Clamp(0, 19);

            if (gender == "1")
            {
                return ((FemaleEmblemSpriteType)t).ToString();
            }
            return ((MaleEmblemSpriteType)t).ToString();
        }

        public static string GetPlayerSpriteFile(int level, string id, string gender)
        {
            foreach (StaffProfile staffMember in StaffProfile.Staff)
            {
                if (staffMember.GameJoltID.ToLower() == id.ToLower() && !string.IsNullOrEmpty(staffMember.Sprite))
                {
                    return staffMember.Sprite;
                }
            }

            int t = Convert.ToInt32(Math.Ceiling(level / 5d)) - 1;

            t = t.Clamp(0, 19);
            string tFile = ((MaleEmblemSprites)t).ToString();
            if (gender == "1")
            {
                tFile = ((FemaleEmblemSprites)t).ToString();
            }
            if (tFile.StartsWith("NN") == true)
            {
                tFile = tFile.Remove(0, 2);
            }

            return tFile;
        }

        public static Texture2D GetPlayerSprite(int level, string id, string gender)
        {
            foreach (StaffProfile staffMember in StaffProfile.Staff)
            {
                if (staffMember.GameJoltID.ToLower() == id.ToLower() && !string.IsNullOrEmpty(staffMember.Sprite))
                {
                    return TextureManager.GetTexture("Textures\\NPC\\" + staffMember.Sprite);
                }
            }

            int t = Convert.ToInt32(Math.Ceiling(level / 5d)) - 1;

            t = t.Clamp(0, 19);
            string tFile = ((MaleEmblemSprites)t).ToString();
            if (gender == "1")
            {
                tFile = ((FemaleEmblemSprites)t).ToString();
            }
            if (tFile.StartsWith("NN") == true)
            {
                tFile = tFile.Remove(0, 2);
            }

            return TextureManager.GetTexture("Textures\\NPC\\" + tFile);
        }

        private static Texture2D GetEmblemBackgroundTexture(string emblemName)
        {
            //Don't load from TextureManager, because ContentPack emblems are not allowed.
            return Core.Content.Load<Texture2D>("Textures\\Emblem\\" + emblemName);
        }

        public static Microsoft.Xna.Framework.Color GetEmblemFontColor(string emblemName)
        {
            switch (emblemName.ToLower())
            {
                case "alph":
                case "genetics":
                case "legendary":
                case "stars":
                case "champion":
                case "overkill":
                case "cyber":
                case "glowing":
                case "material":
                case "fog":
                case "mineral":
                case "storm":
                case "eggsplosion":
                case "missingno":
                case "thunder":
                case "rainbow":
                case "marsh":
                case "volcano":
                case "earth":
                case "shooting star":
                    return Microsoft.Xna.Framework.Color.White;
                case "eevee":
                case "pokedex":
                case "snow":
                case "trainer":
                case "kanto":
                case "glacier":
                case "hive":
                case "plain":
                case "zephyr":
                case "rising":
                case "mailman":
                case "cascade":
                case "boulder":
                case "unodostres":
                case "silver ability":
                case "gold ability":
                case "silver knowledge":
                case "gold knowledge":
                case "johto":
                    return Microsoft.Xna.Framework.Color.Black;
            }
            return Microsoft.Xna.Framework.Color.White;
        }

        public static int GetPointsForLevel(int level)
        {
            double points = Math.Ceiling((5 / 4) * (Math.Pow(level, 3) / 10));
            return Convert.ToInt32(points);
        }

        #endregion

        #region "OnlineSprites"


        static Dictionary<string, Texture2D> TempDownloadedSprites = new Dictionary<string, Texture2D>();
        public static bool HasDownloadedSprite(string GameJoltID)
        {
            return TempDownloadedSprites.ContainsKey(GameJoltID);
        }

        public static Texture2D GetOnlineSprite(string GameJoltID)
        {
            if (API.UserBanned(GameJoltID))
            {
                return null;
            }

            if (TempDownloadedSprites.ContainsKey(GameJoltID) == true)
            {
                Texture2D tempT = TempDownloadedSprites[GameJoltID];
                return tempT;
            }

            Texture2D t = DownloadTexture2D.n_Remote_Texture2D(Core.GraphicsDevice, "" + GameJoltID + ".png", false);
            // CLASSIFIED

            if (TempDownloadedSprites.ContainsKey(GameJoltID) == false)
            {
                TempDownloadedSprites.Add(GameJoltID, t);
            }

            if ((t != null))
            {
                if (t.Width >= 96 && t.Height >= 128)
                {
                    if (t.Width / 3 == t.Height / 4)
                    {
                        return t;
                    }
                }
            }
            else
            {
                Logger.Debug("GetOnlineSprite.vb: Getting sprite for " + GameJoltID + " failed.");
            }

            return null;
        }

        public static void ClearOnlineSpriteCache()
        {
            try
            {
                TempDownloadedSprites.Clear();
            }
            catch
            {
            }
        }

        #endregion

        #region "UserEmblem"

        public string EmblemS = "trainer";
        public int Points = 0;

        public string Gender = "0";
        public string Username = "";
        public string GameJoltID = "";
        public bool ValidProfile = false;
        private int loadedInstances = 0;
        public bool startedLoading = false;
        public Texture2D DownloadedSprite = null;

        public List<BasePokemon> OnlineTeam = null;
        public bool DoneLoading
        {
            get
            {
                if (loadedInstances == 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Texture2D BackgroundTexture
        {
            get { return GetEmblemBackgroundTexture(EmblemS); }
        }

        public Texture2D SpriteTexture
        {
            get
            {
                if ((this.DownloadedSprite != null))
                {
                    return this.DownloadedSprite;
                }
                return GetPlayerSprite(GetPlayerLevel(Points), GameJoltID, Gender);
            }
        }


        string PublicKeys = "";
        public Emblem(string username)
        {
            this.StartLoading(username);
        }

        public Emblem(string GameJoltID, int unimportant)
        {
            StartLoadingID(GameJoltID);
        }

        public Emblem(string username, string Keys) : this(username)
        {
            PublicKeys = Keys;
        }

        public Emblem(string username, string Keys, bool startLoading)
        {
            PublicKeys = Keys;
            this.Username = username;
            if (startLoading == true)
            {
                this.StartLoading(username);
            }
        }

        public void StartLoading(string userName)
        {
            APICall APICall = new APICall(GotGameJoltID);
            APICall.FetchUserdata(userName);

            this.Username = userName;
            this.startedLoading = true;
        }

        public void StartLoadingID(string GameJoltID)
        {
            this.GameJoltID = GameJoltID;

            APICall APICall = new APICall(GotGameJoltID);
            APICall.FetchUserdataByID(GameJoltID);

            this.startedLoading = true;
        }

        public Emblem(string username, string user_id, int points, string gender, string emblem)
        {
            this.Username = username;
            this.GameJoltID = user_id;
            this.Points = points;
            this.Gender = gender;
            this.EmblemS = emblem;

            Texture2D t = GetOnlineSprite(this.GameJoltID);
            DownloadedSprite = t;

            loadedInstances = 3;
            ValidProfile = true;
        }

        private void GotGameJoltID(string result)
        {
            List<GameJolt.API.JoltValue> list = GameJolt.API.HandleData(result);
            bool founduserid = false;

            foreach (GameJolt.API.JoltValue Item in list)
            {
                if (Item.Name.ToLower() == "id")
                {
                    this.GameJoltID = Item.Value;
                    ValidProfile = true;

                    if (!string.IsNullOrEmpty(PublicKeys))
                    {
                        GotPublicKeys(PublicKeys);
                    }
                    else
                    {
                        APICall APICall = new APICall(GotPublicKeys);
                        APICall.GetKeys(false, "saveStorageV" + GamejoltSave.VERSION + "|" + GameJoltID + "|*");
                    }

                    APICall APICall1 = new APICall(GotOnlineTeamKey);
                    APICall1.GetKeys(false, "RegisterBattleV" + BaseRegisterBattleScreen.REGISTERBATTLEVERSION + "|" + this.GameJoltID + "|*");

                    Texture2D t = GetOnlineSprite(this.GameJoltID);
                    DownloadedSprite = t;

                    founduserid = true;
                }
                if (Item.Name.ToLower() == "username")
                {
                    this.Username = Item.Value;
                }
            }

            if (founduserid == false)
                loadedInstances = 3;
        }

        private void GotPublicKeys(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            bool[] exists = new bool[4];

            foreach (API.JoltValue Item in list)
            {
                if (Item.Value == "saveStorageV" + GamejoltSave.VERSION + "|" + GameJoltID + "|points")
                {
                    if (exists[0] == false)
                    {
                        APICall APICall = new APICall(GetPlayerPoints);
                        APICall.GetStorageData(Item.Value, false);
                        exists[0] = true;
                    }
                }
                if (Item.Value == "saveStorageV" + GamejoltSave.VERSION + "|" + GameJoltID + "|emblem")
                {
                    if (exists[1] == false)
                    {
                        APICall APICall = new APICall(GetPlayerEmblem);
                        APICall.GetStorageData(Item.Value, false);
                        exists[1] = true;
                    }
                }
                if (Item.Value == "saveStorageV" + GamejoltSave.VERSION + "|" + GameJoltID + "|gender")
                {
                    if (exists[2] == false)
                    {
                        APICall APICall = new APICall(GetPlayerGender);
                        APICall.GetStorageData(Item.Value, false);
                        exists[2] = true;
                    }
                }
            }

            if (exists[0] == false)
            {
                Points = 0;
                loadedInstances += 1;
            }
            if (exists[1] == false)
            {
                EmblemS = "trainer";
                loadedInstances += 1;
            }
            if (exists[2] == false)
            {
                this.Gender = "0";
                loadedInstances += 1;
            }
        }

        private void GotOnlineTeamKey(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);
            foreach (API.JoltValue Item in list)
            {
                if (Item.Value.StartsWith("RegisterBattleV" + BaseRegisterBattleScreen.REGISTERBATTLEVERSION + "|" + this.GameJoltID + "|") == true)
                {
                    if (this.OnlineTeam == null)
                    {
                        APICall APICall = new APICall(GetOnlineTeam);
                        APICall.GetStorageData(Item.Value, false);
                    }
                }
            }
        }

        private void GetPlayerPoints(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                Points = Convert.ToInt32(data.Replace("\\\"", "\""));
            }
            else
            {
                Points = 0;
            }

            loadedInstances += 1;
        }

        private void GetPlayerEmblem(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                EmblemS = data.Replace("\\\"", "\"");
            }
            else
            {
                EmblemS = "trainer";
            }

            loadedInstances += 1;
        }

        private void GetPlayerGender(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                Gender = data.Replace("\\\"", "\"");
            }
            else
            {
                Gender = "0";
            }

            loadedInstances += 1;
        }

        private void GetOnlineTeam(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                this.OnlineTeam = new List<BasePokemon>();
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));
                string[] dataArray = data.SplitAtNewline();

                foreach (string line in dataArray)
                {
                    if (line.StartsWith("{") == true && line.EndsWith("}") == true)
                    {
                        string pokemonData = line.Replace("\\\"", "\"");

                        this.OnlineTeam.Add(BasePokemon.GetPokemonByData(pokemonData));
                    }
                }
            }
            else
            {
                this.OnlineTeam = null;
            }
        }

        public void Draw(Vector2 Position, int Size)
        {
            Draw(Username, GameJoltID, Points, Gender, EmblemS, Position, Size, this.DownloadedSprite, this.OnlineTeam);
        }

        public bool IsFriend()
        {
            if (this.DoneLoading == true)
            {
                if (this.GameJoltID != Core.GameJoltSave.GameJoltID)
                {
                    string[] Friends = Core.GameJoltSave.Friends.Split(Convert.ToChar(","));
                    if (Friends.Length > 0)
                    {
                        if (Friends.Contains(this.GameJoltID) == true)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public static bool IsFriend(string gameJoltID)
        {
            if (gameJoltID != Core.GameJoltSave.GameJoltID)
            {
                string[] Friends = Core.GameJoltSave.Friends.Split(Convert.ToChar(","));
                if (Friends.Length > 0)
                {
                    if (Friends.Contains(gameJoltID))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool SentRequest(string gameJoltID)
        {
            if (gameJoltID != Core.GameJoltSave.GameJoltID)
            {
                string[] Requests = Core.GameJoltSave.SentRequests.Split(Convert.ToChar(","));
                if (Requests.Length > 0)
                {
                    if (Requests.Contains(gameJoltID))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool ReceivedRequest(string gameJoltID)
        {
                if (gameJoltID != Core.GameJoltSave.GameJoltID)
                {
                    string[] Requests = Core.GameJoltSave.ReceivedRequests.Split(Convert.ToChar(","));
                    if (Requests.Length > 0)
                    {
                        if (Requests.Contains(gameJoltID))
                        {
                            return true;
                        }
                    }
                }

                return false;
        }

        public bool HasOnlineTeam => OnlineTeam?.Count > 0;

        #endregion

        #region "Trophy"

        public static int EmblemToTrophyID(string emblem)
        {
            switch (emblem.ToLower())
            {
                case "alph":
                    return 1958;
                case "material":
                    return 1960;
                case "cyber":
                    return 1973;
                case "johto":
                    return 1963;
                case "kanto":
                    return 1962;
                case "legendary":
                    return 1964;
                case "genetics":
                    return 1972;
                case "unodostres":
                    return 1974;
                case "champion":
                    return 1959;
                case "snow":
                    return 1967;
                case "eevee":
                    return 1961;
                case "stars":
                    return 1971;
                case "glowing":
                    return 1968;
                case "overkill":
                    return 1969;
                case "pokedex":
                    return 1970;
                case "zephyr":
                    return 1994;
                case "hive":
                    return 1995;
                case "plain":
                    return 1996;
                case "fog":
                    return 1997;
                case "storm":
                    return 1998;
                case "mineral":
                    return 1999;
                case "glacier":
                    return 2000;
                case "rising":
                    return 2001;
                case "eggsplosion":
                    return 2581;
                case "mailman":
                    return 3746;
                case "silver ability":
                    return 4765;
                case "silver knowledge":
                    return 4767;
                case "gold ability":
                    return 4766;
                case "gold knowledge":
                    return 4768;
                case "boulder":
                    return 5776;
                case "cascade":
                    return 5777;
                case "thunder":
                    return 5767;
                case "rainbow":
                    return 8677;
                case "marsh":
                    return 8678;
                case "soul":
                    return 10829;
                case "volcano":
                    return 8752;
                case "earth":
                    return 17001;
                case "shooting star":
                    return 17559;
                default:
                    return 0;
            }
        }

        public static string TrophyIDToEmblem(int trophy_id)
        {
            switch (trophy_id)
            {
                case 1958:
                    return "alph";
                case 1959:
                    return "champion";
                case 1960:
                    return "material";
                case 1961:
                    return "eevee";
                case 1962:
                    return "kanto";
                case 1963:
                    return "johto";
                case 1964:
                    return "legendary";
                case 1967:
                    return "snow";
                case 1968:
                    return "glowing";
                case 1969:
                    return "overkill";
                case 1970:
                    return "pokedex";
                case 1971:
                    return "stars";
                case 1972:
                    return "genetics";
                case 1973:
                    return "cyber";
                case 1974:
                    return "unodostres";
                case 1994:
                    return "zephyr";
                case 1995:
                    return "hive";
                case 1996:
                    return "plain";
                case 1997:
                    return "fog";
                case 1998:
                    return "storm";
                case 1999:
                    return "mineral";
                case 2000:
                    return "glacier";
                case 2001:
                    return "rising";
                case 2581:
                    return "eggsplosion";
                case 3746:
                    return "mailman";
                case 4765:
                    return "silver ability";
                case 4766:
                    return "gold ability";
                case 4767:
                    return "silver knowledge";
                case 4768:
                    return "gold knowledge";
                case 5767:
                    return "thunder";
                case 5776:
                    return "boulder";
                case 5777:
                    return "cascade";
                case 8677:
                    return "rainbow";
                case 8678:
                    return "marsh";
                case 10829:
                    return "soul";
                case 8752:
                    return "volcano";
                case 17001:
                    return "earth";
                case 17559:
                    return "shooting star";
                default:
                    return "fail";
            }
        }

        public static void GetAchievedEmblems()
        {
            APICall APICall = new APICall(AddAchievedEmblems);
            APICall.FetchAllAchievedTrophies();
        }

        private static void AddAchievedEmblems(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                int currentTrophyID = 0;

                for (var i = 0; i <= list.Count - 1; i++)
                {
                    switch (list[i].Name)
                    {
                        case "id":
                            currentTrophyID = Convert.ToInt32(list[i].Value);
                            break;
                        case "achieved":
                            if (list[i].Value != "false")
                            {
                                string newEmblem = TrophyIDToEmblem(currentTrophyID);
                                if (newEmblem != "fail")
                                {
                                    if (Core.GameJoltSave.AchievedEmblems.Contains(newEmblem) == false)
                                    {
                                        Core.GameJoltSave.AchievedEmblems.Add(newEmblem);
                                    }
                                }
                            }
                            break;
                    }
                }
            }

            if (Core.GameJoltSave.AchievedEmblems.Contains("trainer") == false)
            {
                Core.GameJoltSave.AchievedEmblems.Add("trainer");
            }
        }

        public static void AchieveEmblem(string emblem)
        {
            if (Core.Player.IsGamejoltSave == true)
            {
                if (Core.GameJoltSave.AchievedEmblems.Contains(emblem.ToLower()) == false)
                {
                    int trophy_id = EmblemToTrophyID(emblem);

                    APICall APICall = new APICall(AddedAchievement);
                    APICall.TrophyAchieved(trophy_id);

                    APICall APICallShow = new APICall(ShowAchievedEmblem);
                    APICallShow.FetchTrophy(trophy_id);
                }
            }
            if (Core.Player.EarnedAchievements.Contains(emblem.ToLower()) == false)
            {
                if (BaseConnectScreen.Connected)
                {
                    Core.ServersManager.ServerConnection.SendGameStateMessage("achieved the emblem \"" + emblem.ToUpper() + "\"!");
                }
                Core.Player.EarnedAchievements.Add(emblem.ToLower());
            }
        }

        private static void AddedAchievement(string result)
        {
            APICall APICall = new APICall(AddAchievedEmblems);
            APICall.FetchAllAchievedTrophies();
        }

        private static void ShowAchievedEmblem(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            foreach (API.JoltValue line in list)
            {
                switch (line.Name.ToLower())
                {
                    case "title":
                        achieved_emblem_title = line.Value;
                        break;
                    case "description":
                        achieved_emblem_description = line.Value;
                        break;
                    case "difficulty":
                        achieved_emblem_difficulty = line.Value;
                        break;
                    case "image_url":
                        Thread t = new Thread(DownloadAchievedEmblemTextrure);
                        t.IsBackground = true;
                        t.Start(line.Value);
                        break;
                }
            }
        }

        private static void DownloadAchievedEmblemTextrure(object url)
        {
            Texture2D t = DownloadTexture2D.n_Remote_Texture2D(Core.GraphicsDevice, url.ToString(), true);

            achieved_emblem_Texture = t;

            displayEmblemDelay = 35f;
        }

        static float displayEmblemDelay = 0f;

        static int emblemPositionX = Core.WindowSize.Width;
        static string achieved_emblem_description = "";
        static Texture2D achieved_emblem_Texture = null;
        static string achieved_emblem_title = "";

        static string achieved_emblem_difficulty = "";
        public static void SetDebugAchieve(string emblem)
        {
            if (Core.GameJoltSave.AchievedEmblems.Contains(emblem) == false)
            {
                displayEmblemDelay = 35f;

                Core.GameJoltSave.AchievedEmblems.Add(emblem);
            }
        }

        public static void DrawNewEmblems()
        {
            if (displayEmblemDelay > 0f)
            {
                displayEmblemDelay -= 0.1f;
                if (displayEmblemDelay <= 6.4f)
                {
                    if (emblemPositionX < Core.WindowSize.Width)
                    {
                        emblemPositionX += 8;
                    }
                }
                else
                {
                    if (emblemPositionX > Core.WindowSize.Width - 512)
                    {
                        emblemPositionX -= 8;
                    }
                }

                Canvas.DrawRectangle(new Microsoft.Xna.Framework.Rectangle(emblemPositionX + 10, 0, 512, 98), Microsoft.Xna.Framework.Color.Black);

                if ((achieved_emblem_Texture != null))
                {
                    Core.SpriteBatch.Draw(achieved_emblem_Texture, new Microsoft.Xna.Framework.Rectangle(emblemPositionX + 2, 2, 75, 75), Microsoft.Xna.Framework.Color.White);
                }

                Core.SpriteBatch.Draw(TextureManager.GetTexture("Textures\\Emblem\\border"), new Microsoft.Xna.Framework.Rectangle(emblemPositionX, 0, 79, 98), Microsoft.Xna.Framework.Color.White);

                Microsoft.Xna.Framework.Color fontColor = Microsoft.Xna.Framework.Color.White;
                switch (achieved_emblem_difficulty.ToLower())
                {
                    case "bronze":
                        fontColor = new Microsoft.Xna.Framework.Color(220, 171, 117);
                        break;
                    case "silver":
                        fontColor = new Microsoft.Xna.Framework.Color(207, 207, 207);
                        break;
                    case "gold":
                        fontColor = new Microsoft.Xna.Framework.Color(255, 207, 39);
                        break;
                    case "platinum":
                        fontColor = new Microsoft.Xna.Framework.Color(172, 201, 202);
                        break;
                }
                Core.SpriteBatch.DrawString(FontManager.MiniFont, achieved_emblem_difficulty, new Vector2(emblemPositionX + (38 - Convert.ToInt32(FontManager.MiniFont.MeasureString(achieved_emblem_difficulty).X / 2)), 77), fontColor);

                Core.SpriteBatch.DrawString(FontManager.MiniFont, "Achieved new emblem background: " + achieved_emblem_title, new Vector2(emblemPositionX + 88, 4), fontColor);

                string desText = achieved_emblem_description.CropStringToWidth(FontManager.MiniFont, 300);

                Core.SpriteBatch.DrawString(FontManager.MiniFont, desText, new Vector2(emblemPositionX + 94, 24), Microsoft.Xna.Framework.Color.White);

                if (displayEmblemDelay <= 0f)
                {
                    displayEmblemDelay = 0f;
                    emblemPositionX = Core.WindowSize.Width;
                }
            }
        }

        #endregion

    }
}

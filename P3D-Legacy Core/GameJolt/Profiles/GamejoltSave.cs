using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Data;
using P3D.Legacy.Core.GameModes;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Screens;
using P3D.Legacy.Core.Security;

namespace P3D.Legacy.Core.GameJolt.Profiles
{
    public class GamejoltSave : HashSecureBase
    {
        //Offset in Fields array:
        private const int ID_APRICORNS = 0;
        private const int ID_BERRIES = 1;
        private const int ID_BOX = 2;
        private const int ID_DAYCARE = 3;
        private const int ID_ITEMDATA = 4;
        private const int ID_ITEMS = 5;
        private const int ID_NPC = 6;
        private const int ID_OPTIONS = 7;
        private const int ID_PARTY = 8;
        private const int ID_PLAYER = 9;
        private const int ID_POKEDEX = 10;
        private const int ID_REGISTER = 11;
        private const int ID_HALLOFFAME = 12;
        private const int ID_SECRETBASE = 13;
        private const int ID_ROAMINGPOKEMON = 14;
        private const int ID_STATISTICS = 15;

        //Extra, SAVEFILECOUNT will get added.
        private const int ID_PLAYERPOINTS = 0;
        private const int ID_PLAYEREMBLEM = 1;
        private const int ID_PLAYERGENDER = 2;
        private const int ID_FRIENDS = 3;
        private const int ID_SENTREQUESTS = 4;

        private const int ID_RECEIVEDREQUESTS = 5;
        //The amount of files to download.
        //SAVEFILEs represent the amount of files that would get stored in an offline save.
        //EXTRADATA is stuff that does get saved extra and that offline profiles don't have, like global points, friends etc.
        public const int SAVEFILECOUNT = 16;
        public const int EXTRADATADOWNLOADCOUNT = 6;

        public const int EXTRADATAUPLOADCOUNT = 3;
        //The current version of the save files.
        //WARNING WARNING WARNING WARNING WARNING WARNING WARNING WARNING
        //   Changing this will break all current online saves!
        //WARNING WARNING WARNING WARNING WARNING WARNING WARNING WARNING

        public const string VERSION = "1";
        /// <summary>
        /// Apricorn data
        /// </summary>
        public string Apricorns { get; private set; } = "";

        /// <summary>
        /// Berry data
        /// </summary>
        public string Berries { get; private set; } = "";

        /// <summary>
        /// Box data
        /// </summary>
        public string Box { get; private set; } = "";

        /// <summary>
        /// Daycare data
        /// </summary>
        public string Daycare { get; private set; } = "";

        /// <summary>
        /// ItemData data
        /// </summary>
        public string ItemData { get; private set; } = "";

        /// <summary>
        /// Item data
        /// </summary>
        public string Items { get; private set; } = "";

        /// <summary>
        /// NPC data
        /// </summary>
        public string NPC { get; private set; } = "";

        /// <summary>
        /// Option data
        /// </summary>
        public string Options { get; private set; } = "";

        /// <summary>
        /// Party data
        /// </summary>
        public string Party { get; private set; } = "";

        /// <summary>
        /// Player data
        /// </summary>
        public string Player { get; private set; } = "";

        /// <summary>
        /// Pokedex data
        /// </summary>
        public string Pokedex { get; private set; } = "";

        /// <summary>
        /// Register data
        /// </summary>
        public string Register { get; private set; } = "";

        /// <summary>
        /// HallOfFame data
        /// </summary>
        public string HallOfFame { get; private set; } = "";

        /// <summary>
        /// SecretBase data
        /// </summary>
        public string SecretBase { get; private set; } = "";

        /// <summary>
        /// RoamingPokemon data
        /// </summary>
        public string RoamingPokemon { get; private set; } = "";

        /// <summary>
        /// Statistics data
        /// </summary>
        public string Statistics { get; private set; } = "";

        public string Friends { get; set; } = "";

        public string EmblemS { get; set; } = "trainer";

        public List<string> AchievedEmblems { get; set; } = new List<string>();

        public string SentRequests { get; set; } = "";

        public string ReceivedRequests { get; set; } = "";

        public Texture2D DownloadedSprite { get; set; }


        public int Points
        {
            get
            {
                Assert("_points", _points);
                return _points;
            }
            set
            {
                Assert("_points", _points, value);
                _points = value;
            }
        }

        public string Gender
        {
            get
            {
                Assert("_gender", _gender);
                return _gender;
            }
            set
            {
                Assert("_gender", _gender, value);
                _gender = value;
            }
        }

        public string GameJoltID => _gameJoltID;

        private int _points = 0;
        private string _gender = "0";

        private string _gameJoltID = "";
        private List<bool> _downloadedFlags = new List<bool>();

        /// <summary>
        /// Indicates if the download finished.
        /// </summary>
        public bool DownloadFinished => DownloadProgress == TotalDownloadItems;

        /// <summary>
        /// Returns the amount of downloaded items.
        /// </summary>
        public int DownloadProgress
        {
            get
            {
                int c = 0;
                for (var i = 0; i <= _downloadedFlags.Count - 1; i++)
                {
                    if (i <= _downloadedFlags.Count - 1)
                    {
                        bool b = _downloadedFlags[i];
                        if (b)
                        {
                            c += 1;
                        }
                    }
                }
                return c;
            }
        }

        /// <summary>
        /// The total files to download from the server.
        /// </summary>
        public static int TotalDownloadItems => SAVEFILECOUNT + EXTRADATADOWNLOADCOUNT;

        /// <summary>
        /// If the download of this save failed at some point.
        /// </summary>
        public bool DownloadFailed { get; private set; } = false;

        /// <summary>
        /// Handles the download failing.
        /// </summary>
        /// <param name="ex">The exception getting thrown.</param>
        private void DownloadFailedHandler(Exception ex)
        {
            DownloadFailed = true;

            Logger.Log(Logger.LogTypes.Warning, "The download of a GameJolt save failed with this message: " + ex.Message);
        }

        /// <summary>
        /// Starts to download a GameJolt save.
        /// </summary>
        /// <param name="GameJoltID">The GameJolt ID of the save to download.</param>
        /// <param name="MainSave">If this save is a main save download. Disable this flag to also download additional data.</param>
        public void DownloadSave(string GameJoltID, bool MainSave)
        {
            _gameJoltID = GameJoltID;
            DownloadFailed = false;

            //Fill fields to contain as many items as items to download.
            _downloadedFlags.Clear();
            for (var i = 1; i <= SAVEFILECOUNT + EXTRADATADOWNLOADCOUNT; i++)
            {
                _downloadedFlags.Add(false);
            }

            AchievedEmblems.Clear();

            Apricorns = "";
            Berries = "";
            Box = "";
            Daycare = "";
            ItemData = "";
            Items = "";
            NPC = "";
            Options = "";
            Party = "";
            Player = "";
            Pokedex = "";
            Register = "";
            HallOfFame = "";
            SecretBase = "";
            RoamingPokemon = "";
            Statistics = "";

            Friends = "";
            if (MainSave == true)
            {
                BaseGTSSetupScreen.BaseGTSEditTradeScreen.BaseSelectFriendScreen.Clear();
                //Clear temp friends
            }

            APICall APIPublicCall = new APICall(GotPublicKeys);
            APIPublicCall.GetKeys(false, "saveStorageV" + VERSION + "|" + GameJoltID + "|*");

            APICall APIPrivateCall = new APICall(GotPrivateKeys);
            APIPrivateCall.GetKeys(true, "saveStorageV" + VERSION + "|" + GameJoltID + "|*");

            if (MainSave == true)
            {
                Emblem.GetAchievedEmblems();
            }

            APICall APIFriendsCall = new APICall(SaveFriends);
            APIFriendsCall.FetchFriendList(GameJoltID);

            APICall APISentCall = new APICall(SaveSentRequests);
            APISentCall.FetchSentFriendRequest();

            APICall APIReceivedCall = new APICall(SaveReceivedRequests);
            APIReceivedCall.FetchReceivedFriendRequests();

            if (MainSave == true)
            {
                Thread t = new Thread(DownloadSpriteSub);
                t.IsBackground = true;
                t.Start();
            }
        }

        private void DownloadSpriteSub()
        {
            DownloadedSprite = Emblem.GetOnlineSprite(GameJoltID);
        }

        private void GotPublicKeys(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            bool existsPoints = false;
            bool existsEmblem = false;
            bool existsGender = false;

            foreach (API.JoltValue Item in list)
            {
                if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|points")
                {
                    APICall APICall = new APICall(SavePlayerPoints);
                    APICall.CallFails += DownloadFailedHandler;
                    APICall.GetStorageData(Item.Value, false);

                    existsPoints = true;
                }
                if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|emblem")
                {
                    APICall APICall = new APICall(SavePlayerEmblem);
                    APICall.CallFails += DownloadFailedHandler;
                    APICall.GetStorageData(Item.Value, false);

                    existsEmblem = true;
                }
                if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|gender")
                {
                    APICall APICall = new APICall(SavePlayerGender);
                    APICall.CallFails += DownloadFailedHandler;
                    APICall.GetStorageData(Item.Value, false);

                    existsGender = true;
                }
            }

            if (existsPoints == false)
            {
                Points = 0;
                _downloadedFlags[SAVEFILECOUNT + ID_PLAYERPOINTS] = true;

                UpdatePlayerScore();
            }
            if (existsEmblem == false)
            {
                EmblemS = "trainer";
                _downloadedFlags[SAVEFILECOUNT + ID_PLAYEREMBLEM] = true;
            }
            if (existsGender == false)
            {
                Gender = "0";
                _downloadedFlags[SAVEFILECOUNT + ID_PLAYERGENDER] = true;
            }
        }

        public void UpdatePlayerScore()
        {
            APICall APICall = new APICall();

            APICall.AddScore(Points + " Points", Points, "14908");
        }

        private void GotPrivateKeys(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            bool[] exists = {

            };
            for (var i = 0; i <= SAVEFILECOUNT - 1; i++)
            {
                List<bool> l = exists.ToList();
                l.Add(false);
                exists = l.ToArray();
            }

            foreach (API.JoltValue Item in list)
            {
                if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|apricorns")
                {
                    APICall APICall = new APICall(SaveApricorns);
                    APICall.CallFails += DownloadFailedHandler;
                    APICall.GetStorageData(Item.Value, true);

                    exists[ID_APRICORNS] = true;
                }
                else if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|berries")
                {
                    {
                        APICall APICall = new APICall(SaveBerries);
                        APICall.CallFails += DownloadFailedHandler;
                        APICall.GetStorageData(Item.Value, true);

                        exists[ID_BERRIES] = true;
                    }
                }
                else if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|box")
                {
                    {
                        APICall APICall = new APICall(SaveBox);
                        APICall.CallFails += DownloadFailedHandler;
                        APICall.GetStorageData(Item.Value, true);

                        exists[ID_BOX] = true;
                    }
                }
                else if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|daycare")
                {
                    {
                        APICall APICall = new APICall(SaveDaycare);
                        APICall.CallFails += DownloadFailedHandler;
                        APICall.GetStorageData(Item.Value, true);

                        exists[ID_DAYCARE] = true;
                    }
                }
                else if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|itemdata")
                {
                    {
                        APICall APICall = new APICall(SaveItemData);
                        APICall.CallFails += DownloadFailedHandler;
                        APICall.GetStorageData(Item.Value, true);

                        exists[ID_ITEMDATA] = true;
                    }
                }
                else if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|items")
                {
                    {
                        APICall APICall = new APICall(SaveItems);
                        APICall.CallFails += DownloadFailedHandler;
                        APICall.GetStorageData(Item.Value, true);

                        exists[ID_ITEMS] = true;
                    }
                }
                else if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|npc")
                {
                    {
                        APICall APICall = new APICall(SaveNPC);
                        APICall.CallFails += DownloadFailedHandler;
                        APICall.GetStorageData(Item.Value, true);

                        exists[ID_NPC] = true;
                    }
                }
                else if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|options")
                {
                    {
                        APICall APICall = new APICall(SaveOptions);
                        APICall.CallFails += DownloadFailedHandler;
                        APICall.GetStorageData(Item.Value, true);

                        exists[ID_OPTIONS] = true;
                    }
                }
                else if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|party")
                {
                    {
                        APICall APICall = new APICall(SaveParty);
                        APICall.CallFails += DownloadFailedHandler;
                        APICall.GetStorageData(Item.Value, true);

                        exists[ID_PARTY] = true;
                    }
                }
                else if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|player")
                {
                    {
                        APICall APICall = new APICall(SavePlayer);
                        APICall.CallFails += DownloadFailedHandler;
                        APICall.GetStorageData(Item.Value, true);

                        exists[ID_PLAYER] = true;
                    }
                }
                else if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|pokedex")
                {
                    {
                        APICall APICall = new APICall(SavePokedex);
                        APICall.CallFails += DownloadFailedHandler;
                        APICall.GetStorageData(Item.Value, true);

                        exists[ID_POKEDEX] = true;
                    }
                }
                else if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|register")
                {
                    {
                        APICall APICall = new APICall(SaveRegister);
                        APICall.CallFails += DownloadFailedHandler;
                        APICall.GetStorageData(Item.Value, true);

                        exists[ID_REGISTER] = true;
                    }
                }
                else if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|halloffame")
                {
                    {
                        APICall APICall = new APICall(SaveHallOfFame);
                        APICall.CallFails += DownloadFailedHandler;
                        APICall.GetStorageData(Item.Value, true);

                        exists[ID_HALLOFFAME] = true;
                    }
                }
                else if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|secretbase")
                {
                    {
                        APICall APICall = new APICall(SaveSecretBase);
                        APICall.CallFails += DownloadFailedHandler;
                        APICall.GetStorageData(Item.Value, true);

                        exists[ID_SECRETBASE] = true;
                    }
                }
                else if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|roamingpokemon")
                {
                    {
                        APICall APICall = new APICall(SaveRoamingPokemon);
                        APICall.CallFails += DownloadFailedHandler;
                        APICall.GetStorageData(Item.Value, true);

                        exists[ID_ROAMINGPOKEMON] = true;
                    }
                }
                else if (Item.Value == "saveStorageV" + VERSION + "|" + GameJoltID + "|statistics")
                {
                    {
                        APICall APICall = new APICall(SaveStatistics);
                        APICall.CallFails += DownloadFailedHandler;
                        APICall.GetStorageData(Item.Value, true);

                        exists[ID_STATISTICS] = true;
                    }
                }
            }

            if (exists[ID_BERRIES] == false)
            {
                Berries = BaseNewGameScreen.GetBerryData();
            }
            if (exists[ID_OPTIONS] == false)
            {
                Options = BaseNewGameScreen.GetOptionsData();
            }
            if (exists[ID_PLAYER] == false)
            {
                Player = GetPlayerData();
            }

            for (var i = 0; i <= SAVEFILECOUNT - 1; i++)
            {
                if (exists[i] == false)
                {
                    _downloadedFlags[i] = true;
                }
            }
        }

        private void SavePlayerPoints(string result)
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

            UpdatePlayerScore();

            _downloadedFlags[SAVEFILECOUNT + ID_PLAYERPOINTS] = true;
        }

        private void SavePlayerEmblem(string result)
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

            _downloadedFlags[SAVEFILECOUNT + ID_PLAYEREMBLEM] = true;
        }

        private void SavePlayerGender(string result)
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

            _downloadedFlags[SAVEFILECOUNT + ID_PLAYERGENDER] = true;
        }

        private void SaveApricorns(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                Apricorns = data.Replace("\\\"", "\"");
            }
            else
            {
                Apricorns = "";
            }

            _downloadedFlags[ID_APRICORNS] = true;
        }

        private void SaveBerries(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                Berries = data.Replace("\\\"", "\"");
            }
            else
            {
                Berries = BaseNewGameScreen.GetBerryData();
            }

            _downloadedFlags[ID_BERRIES] = true;
        }

        private void SaveBox(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                Box = data.Replace("\\\"", "\"");
            }
            else
            {
                Box = "";
            }

            _downloadedFlags[ID_BOX] = true;
        }

        private void SaveDaycare(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                Daycare = data.Replace("\\\"", "\"");
            }
            else
            {
                Daycare = "";
            }

            _downloadedFlags[ID_DAYCARE] = true;
        }

        private void SaveItemData(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                ItemData = data.Replace("\\\"", "\"");
            }
            else
            {
                ItemData = "";
            }

            _downloadedFlags[ID_ITEMDATA] = true;
        }

        private void SaveItems(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                Items = data.Replace("\\\"", "\"");
            }
            else
            {
                Items = "";
            }

            _downloadedFlags[ID_ITEMS] = true;
        }

        private void SaveNPC(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                NPC = data.Replace("\\\"", "\"");
            }
            else
            {
                NPC = "";
            }

            _downloadedFlags[ID_NPC] = true;
        }

        private void SaveOptions(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                Options = data.Replace("\\\"", "\"");
            }
            else
            {
                Options = BaseNewGameScreen.GetOptionsData();
            }

            _downloadedFlags[ID_OPTIONS] = true;
        }

        private void SaveParty(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                Party = data.Replace("\\\"", "\"");
            }
            else
            {
                Party = "";
            }

            _downloadedFlags[ID_PARTY] = true;
        }

        private void SavePlayer(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                Player = data.Replace("\\\"", "\"");
            }
            else
            {
                Player = GetPlayerData();
            }

            _downloadedFlags[ID_PLAYER] = true;
        }

        private void SavePokedex(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                Pokedex = data.Replace("\\\"", "\"");
            }
            else
            {
                Pokedex = "";
            }

            _downloadedFlags[ID_POKEDEX] = true;
        }

        private void SaveRegister(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                Register = data.Replace("\\\"", "\"");
            }
            else
            {
                Register = "";
            }

            _downloadedFlags[ID_REGISTER] = true;
        }

        private void SaveHallOfFame(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                HallOfFame = data.Replace("\\\"", "\"");
            }
            else
            {
                HallOfFame = "";
            }

            _downloadedFlags[ID_HALLOFFAME] = true;
        }

        private void SaveSecretBase(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                SecretBase = data.Replace("\\\"", "\"");
            }
            else
            {
                SecretBase = "";
            }

            _downloadedFlags[ID_SECRETBASE] = true;
        }

        private void SaveRoamingPokemon(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                RoamingPokemon = data.Replace("\\\"", "\"");
            }
            else
            {
                RoamingPokemon = "";
            }

            _downloadedFlags[ID_ROAMINGPOKEMON] = true;
        }

        private void SaveStatistics(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (Convert.ToBoolean(list[0].Value) == true)
            {
                string data = result.Remove(0, 22);
                data = data.Remove(data.LastIndexOf("\""));

                Statistics = data.Replace("\\\"", "\"");
            }
            else
            {
                Statistics = "";
            }

            _downloadedFlags[ID_STATISTICS] = true;
        }

        private void SaveFriends(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (list.Count > 1)
            {
                Friends = "";

                foreach (API.JoltValue Item in list)
                {
                    if (Item.Name == "friend_id")
                    {
                        if (!string.IsNullOrEmpty(Friends))
                        {
                            Friends += ",";
                        }
                        Friends += Item.Value;
                    }
                }
            }
            else
            {
                Friends = "";
            }

            _downloadedFlags[SAVEFILECOUNT + ID_FRIENDS] = true;
        }

        private void SaveSentRequests(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (list.Count > 1)
            {
                SentRequests = "";

                foreach (API.JoltValue Item in list)
                {
                    if (Item.Name == "user_id")
                    {
                        if (!string.IsNullOrEmpty(SentRequests))
                        {
                            SentRequests += ",";
                        }
                        SentRequests += Item.Value;
                    }
                }
            }
            else
            {
                SentRequests = "";
            }

            _downloadedFlags[SAVEFILECOUNT + ID_SENTREQUESTS] = true;
        }

        private void SaveReceivedRequests(string result)
        {
            List<API.JoltValue> list = API.HandleData(result);

            if (list.Count > 1)
            {
                ReceivedRequests = "";

                foreach (API.JoltValue Item in list)
                {
                    if (Item.Name == "user_id")
                    {
                        if (!string.IsNullOrEmpty(ReceivedRequests))
                        {
                            ReceivedRequests += ",";
                        }
                        ReceivedRequests += Item.Value;
                    }
                }
            }
            else
            {
                ReceivedRequests = "";
            }

            _downloadedFlags[SAVEFILECOUNT + ID_RECEIVEDREQUESTS] = true;
        }

        #region "DefaultData"

        private string GetPlayerData()
        {
            GameMode GameMode = GameModeManager.ActiveGameMode;

            string ot = GameJoltID;
            while (ot.Length < 5)
            {
                ot = "0" + ot;
            }

            string s = "Name|" + API.Username + Constants.vbNewLine + "Position|1,0.1,3" + Constants.vbNewLine + "MapFile|yourroom.dat" + Constants.vbNewLine + "Rotation|1.570796" + Constants.vbNewLine + "RivalName|???" + Constants.vbNewLine + "Money|3000" + Constants.vbNewLine + "Badges|0" + Constants.vbNewLine + "Gender|Male" + Constants.vbNewLine + "PlayTime|0,0,0" + Constants.vbNewLine + "OT|" + ot + Constants.vbNewLine + "Points|0" + Constants.vbNewLine + "hasPokedex|0" + Constants.vbNewLine + "hasPokegear|0" + Constants.vbNewLine + "freeCamera|1" + Constants.vbNewLine + "thirdPerson|0" + Constants.vbNewLine + "skin|" + Emblem.GetPlayerSpriteFile(1, Core.GameJoltSave.GameJoltID, Core.GameJoltSave.Gender) + Constants.vbNewLine + "location|Your Room" + Constants.vbNewLine + "battleAnimations|2" + Constants.vbNewLine + "BoxAmount|5" + Constants.vbNewLine + "LastRestPlace|yourroom.dat" + Constants.vbNewLine + "LastRestPlacePosition|1,0.1,3" + Constants.vbNewLine + "DiagonalMovement|0" + Constants.vbNewLine + "RepelSteps|0" + Constants.vbNewLine + "LastSavePlace|yourroom.dat" + Constants.vbNewLine + "LastSavePlacePosition|1,0.1,3" + Constants.vbNewLine + "Difficulty|" + GameModeManager.GetGameRuleValue("Difficulty", "0") + Constants.vbNewLine + "BattleStyle|0" + Constants.vbNewLine + "saveCreated|" + GameController.GAMEDEVELOPMENTSTAGE + " " + GameController.GAMEVERSION + Constants.vbNewLine + "LastPokemonPosition|999,999,999" + Constants.vbNewLine + "DaycareSteps|0" + Constants.vbNewLine + "GameMode|Kolben" + Constants.vbNewLine + "PokeFiles|" + Constants.vbNewLine + "VisitedMaps|yourroom.dat" + Constants.vbNewLine + "TempSurfSkin|Hilbert" + Constants.vbNewLine + "Surfing|0" + Constants.vbNewLine + "ShowModels|1" + Constants.vbNewLine + "GTSStars|4" + Constants.vbNewLine + "SandBoxMode|0" + Constants.vbNewLine + "EarnedAchievements|";

            return s;
        }

        #endregion

        public void ResetSave()
        {
            Points = 0;
            EmblemS = "trainer";
            Gender = "0";

            Apricorns = "";
            Berries = BaseNewGameScreen.GetBerryData();
            Box = "";
            Daycare = "";
            ItemData = "";
            Items = "";
            NPC = "";
            Options = BaseNewGameScreen.GetOptionsData();
            Party = "";
            Player = GetPlayerData();
            Pokedex = "";
            Register = "";
            HallOfFame = "";
            SecretBase = "";
            RoamingPokemon = "";
            Statistics = "";
        }

    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;

using P3D.Legacy.Core.Security;

namespace P3D.Legacy.Core.GameJolt
{
    public class APICall
    {
        public delegate void DelegateCallSub(string result);

        public struct JoltParameter
        {
            public string Name;
            public string Value;
        }

        public enum RequestMethod
        {
            GET,
            POST
        }

        private class APIURL
        {
            private Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
            private string BaseURL { get; set; }

            public APIURL(string baseURL)
            {
                BaseURL = baseURL;

                if (!BaseURL.StartsWith("/"))
                    BaseURL = "/" + baseURL;

                if (!BaseURL.EndsWith("/"))
                    BaseURL += "/";
            }

            public void AddKeyValuePair(string key, string value) => Values.Add(key, value);

            public string GetURL
            {
                get
                {
                    string url = HOST + API.API_VERSION + BaseURL;

                    var keys = Values.Keys.ToArray();
                    var values = Values.Values.ToArray();
                    for (var i = 0; i <= Values.Count - 1; i++)
                    {
                        string appendString = "";
                        if (i == 0)
                        {
                            appendString += "?";
                        }
                        else
                        {
                            appendString += "&";
                        }
                        appendString += keys[i] + "=";

                        appendString += HttpUtility.UrlEncode(values[i]);

                        url += appendString;
                    }

                    return url;
                }
            }
        }

        

        public DelegateCallSub CallSub;

        private string Username { get; set; }
        private string Token { get; set; }
        private bool LoggedIn { get; set; }

        private const string CONST_GAMEID = ""; // CLASSIFIED
        private const string CONST_GAMEKEY = ""; // CLASSIFIED
        private const string HOST = "http://api.gamejolt.com/api/game/";

        Exception Exception = null;
        public event CallFailsEventHandler CallFails;
        public delegate void CallFailsEventHandler(Exception ex);
        public event CallSucceededEventHandler CallSucceeded;
        public delegate void CallSucceededEventHandler(string returnData);

        private string GameID => StringObfuscation.DeObfuscate(CONST_GAMEID);
        private string GameKey => StringObfuscation.DeObfuscate(CONST_GAMEKEY);

        public APICall()
        {
            Username = API.username;
            Token = API.token;

            LoggedIn = API.LoggedIn;
        }
        public APICall(DelegateCallSub callSub)
        {
            CallSub = callSub;

            Username = API.username;
            Token = API.token;

            LoggedIn = API.LoggedIn;
        }

        public void VerifyUser(string newUsername, string newToken)
        {
            API.username = newUsername;
            API.token = newToken;
            Username = newUsername;
            Token = newToken;

            APIURL url = new APIURL("/users/auth/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("username", Username);
            url.AddKeyValuePair("user_token", Token);

            Initialize(url.GetURL, RequestMethod.GET);
        }

        #region "Storage"

        public void SetStorageData(string key, string data, bool useUsername)
        {
            if (useUsername == true)
            {
                if (LoggedIn == false)
                {
                    Exception up = new Exception("User not logged in!");
                    //Happens when a user tries to send an API call but is not logged in.

                    throw up;
                }
                else
                {
                    APIURL url = new APIURL("/data-store/set/");
                    url.AddKeyValuePair("game_id", GameID);
                    url.AddKeyValuePair("username", Username);
                    url.AddKeyValuePair("user_token", Token);
                    url.AddKeyValuePair("key", key);

                    Initialize(url.GetURL, RequestMethod.POST, data);
                }
            }
            else
            {
                APIURL url = new APIURL("/data-store/set/");
                url.AddKeyValuePair("game_id", GameID);
                url.AddKeyValuePair("key", key);

                Initialize(url.GetURL, RequestMethod.POST, data);
            }
        }

        public void UpdateStorageData(string key, string value, string operation, bool useUsername)
        {
            if (useUsername == true)
            {
                if (LoggedIn == false)
                {
                    Exception up = new Exception("User not logged in!");
                    //Happens when a user tries to send an API call but is not logged in.

                    throw up;
                }
                else
                {
                    APIURL url = new APIURL("/data-store/update/");
                    url.AddKeyValuePair("game_id", GameID);
                    url.AddKeyValuePair("username", Username);
                    url.AddKeyValuePair("user_token", Token);
                    url.AddKeyValuePair("key", key);
                    url.AddKeyValuePair("operation", operation);
                    url.AddKeyValuePair("value", value);

                    Initialize(url.GetURL, RequestMethod.GET);
                }
            }
            else
            {
                APIURL url = new APIURL("/data-store/update/");
                url.AddKeyValuePair("game_id", GameID);
                url.AddKeyValuePair("key", key);
                url.AddKeyValuePair("operation", operation);
                url.AddKeyValuePair("value", value);

                Initialize(url.GetURL, RequestMethod.GET);
            }
        }

        public void SetStorageData(string[] keys, string[] dataItems, bool[] useUsernames)
        {
            if (keys.Length != dataItems.Length || keys.Length != useUsernames.Length)
            {
                Exception ex = new Exception("The data arrays do not have the same lengths.");
                ex.Data.Add("Keys Length", keys.Length);
                ex.Data.Add("Data Length", dataItems.Length);
                ex.Data.Add("Username permission Length", useUsernames.Length);
                throw ex;
            }

            string url = HOST + API.API_VERSION + "/batch/" + "?game_id=" + GameID + "&parallel=true";
            string postDataURL = "";

            for (var i = 0; i <= keys.Length - 1; i++)
            {
                string key = keys[i];
                string data = dataItems[i];
                bool useUsername = useUsernames[i];

                if (useUsername == true && LoggedIn == false)
                {
                    throw new Exception("User not logged in!");
                }

                if (useUsername == true)
                {
                    postDataURL += "&requests[]=" + HttpUtility.UrlEncode(GetHashedURL("/data-store/set/" + "?game_id=" + GameID + "&username=" + Username + "&user_token=" + Token + "&key=" + HttpUtility.UrlEncode(key) + "&data=" + HttpUtility.UrlEncode(data)));
                }
                else
                {
                    postDataURL += "&requests[]=" + HttpUtility.UrlEncode(GetHashedURL("/data-store/set/" + "?game_id=" + GameID + "&key=" + HttpUtility.UrlEncode(key) + "&data=" + HttpUtility.UrlEncode(data)));
                }
            }

            Initialize(url, RequestMethod.POST, postDataURL);
        }

        public void SetStorageDataRestricted(string key, string data)
        {
            string url = HOST + API.API_VERSION + "/data-store/set/" + "?game_id=" + GameID + "&key=" + key + "&restriction_username=" + API.username + "&restriction_user_token=" + API.token;

            Initialize(url, RequestMethod.POST, data);
        }

        public void GetStorageData(string key, bool useUsername)
        {
            if (useUsername == true)
            {
                if (LoggedIn == false)
                {
                    throw new Exception("User not logged in!");
                }
                else
                {
                    APIURL url = new APIURL("/data-store/");
                    url.AddKeyValuePair("game_id", GameID);
                    url.AddKeyValuePair("username", Username);
                    url.AddKeyValuePair("user_token", Token);
                    url.AddKeyValuePair("key", key);

                    Initialize(url.GetURL, RequestMethod.GET);
                }
            }
            else
            {
                APIURL url = new APIURL("/data-store/");
                url.AddKeyValuePair("game_id", GameID);
                url.AddKeyValuePair("key", key);

                Initialize(url.GetURL, RequestMethod.GET);
            }
        }

        public void GetStorageData(string[] keys, bool useUsername)
        {
            if (useUsername == true)
            {
                if (LoggedIn == false)
                {
                    throw new Exception("User not logged in!");
                }
                else
                {
                    string url = HOST + API.API_VERSION + "/batch/";

                    bool firstURL = true;

                    foreach (string key in keys)
                    {
                        string keyURL = "?";
                        if (firstURL == false)
                        {
                            keyURL = "&";
                        }

                        keyURL += "requests[]=" + HttpUtility.UrlEncode(GetHashedURL(HOST + API.API_VERSION + "/data-store/" + "?game_id=" + GameID + "&username=" + Username + "&user_token=" + Token + "&key=" + key));

                        url += keyURL;

                        firstURL = false;
                    }

                    url += "&game_id=" + GameID;

                    Initialize(url, RequestMethod.GET);
                }
            }
            else
            {
                string url = HOST + API.API_VERSION + "/batch/";

                bool firstURL = true;

                foreach (string key in keys)
                {
                    string keyURL = "?";
                    if (firstURL == false)
                    {
                        keyURL = "&";
                    }

                    keyURL += "requests[]=" + HttpUtility.UrlEncode(GetHashedURL(HOST + API.API_VERSION + "/data-store/" + "?game_id=" + GameID + "&key=" + key));

                    url += keyURL;

                    firstURL = false;
                }

                url += "&game_id=" + GameID;

                Initialize(url, RequestMethod.GET);
            }
        }

        public void FetchUserdata(string username)
        {
            APIURL url = new APIURL("/users/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("username", username);

            Initialize(url.GetURL, RequestMethod.GET);
        }

        public void FetchUserdataByID(string user_id)
        {
            APIURL url = new APIURL("/users/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("user_id", user_id);

            Initialize(url.GetURL, RequestMethod.GET);
        }

        public void GetKeys(bool useUsername, string pattern)
        {
            if (useUsername == true)
            {
                if (LoggedIn == false)
                {
                    throw new Exception("User not logged in!");
                }
                else
                {
                    APIURL url = new APIURL("/data-store/get-keys/");
                    url.AddKeyValuePair("game_id", GameID);
                    url.AddKeyValuePair("username", Username);
                    url.AddKeyValuePair("user_token", Token);
                    url.AddKeyValuePair("pattern", pattern);

                    Initialize(url.GetURL, RequestMethod.GET);
                }
            }
            else
            {
                APIURL url = new APIURL("/data-store/get-keys/");
                url.AddKeyValuePair("game_id", GameID);
                url.AddKeyValuePair("pattern", pattern);

                Initialize(url.GetURL, RequestMethod.GET);
            }
        }

        public void RemoveKey(string key, bool useUsername)
        {
            if (useUsername == true)
            {
                if (LoggedIn == false)
                {
                    throw new Exception("User Not logged in!");
                }
                else
                {
                    APIURL url = new APIURL("/data-store/remove/");
                    url.AddKeyValuePair("game_id", GameID);
                    url.AddKeyValuePair("username", Username);
                    url.AddKeyValuePair("user_token", Token);
                    url.AddKeyValuePair("key", key);

                    Initialize(url.GetURL, RequestMethod.POST);
                }
            }
            else
            {
                APIURL url = new APIURL("/data-store/remove/");
                url.AddKeyValuePair("game_id", GameID);
                url.AddKeyValuePair("key", key);

                Initialize(url.GetURL, RequestMethod.POST);
            }
        }

        #endregion

        #region "Sessions"

        public void OpenSession()
        {
            APIURL url = new APIURL("/sessions/open/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("username", Username);
            url.AddKeyValuePair("user_token", Token);

            Initialize(url.GetURL, RequestMethod.GET);
        }

        public void CheckSession()
        {
            APIURL url = new APIURL("/sessions/ping/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("username", Username);
            url.AddKeyValuePair("user_token", Token);

            Initialize(url.GetURL, RequestMethod.GET);
        }

        public void PingSession()
        {
            APIURL url = new APIURL("/sessions/ping/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("username", Username);
            url.AddKeyValuePair("user_token", Token);

            Initialize(url.GetURL, RequestMethod.GET);
        }

        public void CloseSession()
        {
            APIURL url = new APIURL("/sessions/close/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("username", Username);
            url.AddKeyValuePair("user_token", Token);

            Initialize(url.GetURL, RequestMethod.GET);
        }

        #endregion

        #region "Trophy"

        public void FetchAllTrophies()
        {
            APIURL url = new APIURL("/trophies/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("username", Username);
            url.AddKeyValuePair("user_token", Token);

            Initialize(url.GetURL, RequestMethod.GET);
        }

        public void FetchAllAchievedTrophies()
        {
            APIURL url = new APIURL("/trophies/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("username", Username);
            url.AddKeyValuePair("user_token", Token);
            url.AddKeyValuePair("achieved", "true");

            Initialize(url.GetURL, RequestMethod.GET);
        }

        public void FetchTrophy(int trophy_id)
        {
            APIURL url = new APIURL("/trophies/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("username", Username);
            url.AddKeyValuePair("user_token", Token);
            url.AddKeyValuePair("trophy_id", trophy_id.ToString());

            Initialize(url.GetURL, RequestMethod.GET);
        }

        public void TrophyAchieved(int trophy_id)
        {
            APIURL url = new APIURL("/trophies/add-achieved/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("username", Username);
            url.AddKeyValuePair("user_token", Token);
            url.AddKeyValuePair("trophy_id", trophy_id.ToString());

            Initialize(url.GetURL, RequestMethod.POST);
        }

        public void RemoveTrophyAchieved(int trophy_id)
        {
            APIURL url = new APIURL("/trophies/remove-achieved/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("username", Username);
            url.AddKeyValuePair("user_token", Token);
            url.AddKeyValuePair("trophy_id", trophy_id.ToString());

            Initialize(url.GetURL, RequestMethod.POST);
        }

        #endregion

        #region "ScoreTable"

        public void FetchTable(int score_count, string table_id)
        {
            APIURL url = new APIURL("/scores/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("limit", score_count.ToString());
            url.AddKeyValuePair("table_id", table_id);

            Initialize(url.GetURL, RequestMethod.GET);
        }

        public void FetchUserRank(string table_id, int sort)
        {
            APIURL url = new APIURL("/scores/get-rank/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("sort", sort.ToString());
            url.AddKeyValuePair("table_id", table_id);

            Initialize(url.GetURL, RequestMethod.GET);
        }

        public void AddScore(string score, int sort, string table_id)
        {
            APIURL url = new APIURL("/scores/add/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("username", Username);
            url.AddKeyValuePair("user_token", Token);
            url.AddKeyValuePair("score", score);
            url.AddKeyValuePair("sort", sort.ToString());
            url.AddKeyValuePair("table_id", table_id);

            Initialize(url.GetURL, RequestMethod.POST);
        }

        #endregion

        #region "Friends"

        public void FetchFriendList(string user_id)
        {
            APIURL url = new APIURL("/friends/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("user_id", user_id);

            Initialize(url.GetURL, RequestMethod.GET);
        }

        public void FetchSentFriendRequest()
        {
            APIURL url = new APIURL("/friends/sent-requests/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("username", Username);
            url.AddKeyValuePair("user_token", Token);

            Initialize(url.GetURL, RequestMethod.GET);
        }

        public void FetchReceivedFriendRequests()
        {
            APIURL url = new APIURL("/friends/received-requests/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("username", Username);
            url.AddKeyValuePair("user_token", Token);

            Initialize(url.GetURL, RequestMethod.GET);
        }

        public void SendFriendRequest(string targetuserID)
        {
            APIURL url = new APIURL("/friends/send-request/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("username", Username);
            url.AddKeyValuePair("user_token", Token);
            url.AddKeyValuePair("target_user_id", targetuserID);

            Initialize(url.GetURL, RequestMethod.POST);
        }

        public void CancelFriendRequest(string targetUserID)
        {
            APIURL url = new APIURL("/friends/cancel-request/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("username", Username);
            url.AddKeyValuePair("user_token", Token);
            url.AddKeyValuePair("target_user_id", targetUserID);

            Initialize(url.GetURL, RequestMethod.POST);
        }

        public void AcceptFriendRequest(string targetUserID)
        {
            APIURL url = new APIURL("/friends/accept-request/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("username", Username);
            url.AddKeyValuePair("user_token", Token);
            url.AddKeyValuePair("target_user_id", targetUserID);

            Initialize(url.GetURL, RequestMethod.POST);
        }

        public void DeclineFriendRequest(string targetUserID)
        {
            APIURL url = new APIURL("/friends/decline-request/");
            url.AddKeyValuePair("game_id", GameID);
            url.AddKeyValuePair("username", Username);
            url.AddKeyValuePair("user_token", Token);
            url.AddKeyValuePair("target_user_id", targetUserID);

            Initialize(url.GetURL, RequestMethod.POST);
        }

        #endregion

        private string url = "";
        private string PostData = "";

        private string GetHashedURL(string url)
        {
            MD5 m = MD5.Create();

            byte[] data = m.ComputeHash(Encoding.UTF8.GetBytes(url + GameKey));

            StringBuilder sBuild = new StringBuilder();

            for (var i = 0; i <= data.Length - 1; i++)
            {
                sBuild.Append(data[i].ToString("x2"));
            }

            string newurl = url + "&signature=" + sBuild.ToString();

            return newurl;
        }

        private void Initialize(string url, RequestMethod method, string PostData = "")
        {
            Exception = null;

            string newurl = GetHashedURL(url + "&format=keypair");

            //Debug.Print(newurl);
            //Intentional

            if (method == RequestMethod.POST)
            {
                this.url = newurl;
                this.PostData = PostData;

                Thread t = new Thread(POSTRequst);
                t.IsBackground = true;
                t.Start();
            }
            else
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(newurl);
                    request.Method = method.ToString();

                    request.BeginGetResponse(EndResult, request);
                }
                catch (Exception ex)
                {
                    API.APICallCount -= 1;
                    if (CallFails != null)
                    {
                        CallFails(ex);
                    }
                }
            }

            API.APICallCount += 1;
        }

        private void POSTRequst()
        {
            string gotData = "";
            bool gotDataSuccess = false;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowWriteStreamBuffering = true;
                request.Method = "POST";
                string post = "data=" + PostData;
                request.ContentLength = post.Length;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ServicePoint.Expect100Continue = false;

                StreamWriterLock writer = new StreamWriterLock(request.GetRequestStream());
                writer.Write(post);
                writer.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                gotData = new StreamReader(response.GetResponseStream()).ReadToEnd();
                gotDataSuccess = true;
            }
            catch (Exception ex)
            {
                if (CallFails != null)
                {
                    CallFails(ex);
                }
            }
            finally
            {
                API.APICallCount -= 1;
            }

            //Handle data outside of the try...catch because the result function could throw an error:
            if (gotDataSuccess == true)
            {
                if ((CallSub != null))
                {
                    CallSub(gotData);
                    if (CallSucceeded != null)
                    {
                        CallSucceeded(gotData);
                    }
                }
            }
        }

        private void EndResult(IAsyncResult result)
        {
            string data = "";

            try
            {
                if (result.IsCompleted)
                {
                    HttpWebRequest request = (HttpWebRequest)result.AsyncState;

                    data = new StreamReader(request.EndGetResponse(result).GetResponseStream()).ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                if (CallFails != null)
                {
                    CallFails(ex);
                }
            }
            finally
            {
                API.APICallCount -= 1;
            }

            //Handle data outside of the try...catch because the result function could throw an error:
            if (!string.IsNullOrEmpty(data))
            {
                if ((CallSub != null))
                {
                    if (CallSucceeded != null)
                    {
                        CallSucceeded(data);
                    }
                    CallSub(data);
                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using P3D.Legacy.Core.Security;

namespace P3D.Legacy.Core.GameJolt
{
    public class APICall
    {
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

        private class APIURL : IEnumerable<KeyValuePair<string, string>>
        {
            private Dictionary<string, string> Values { get; } = new Dictionary<string, string>();
            private string BaseURL { get; }

            public APIURL(string baseURL)
            {
                BaseURL = baseURL;

                if (!BaseURL.StartsWith("/"))
                    BaseURL = "/" + baseURL;

                if (!BaseURL.EndsWith("/"))
                    BaseURL += "/";
            }

            public void Add(string key, string value) => Values.Add(key, value);

            public string GetURL
            {
                get
                {
                    var url = API.HOST + API.API_VERSION + BaseURL;

                    var keys = Values.Keys.ToArray();
                    var values = Values.Values.ToArray();
                    for (var i = 0; i <= Values.Count - 1; i++)
                    {
                        var appendString = "";

                        if (i == 0)
                            appendString += "?";
                        else
                            appendString += "&";

                        appendString += keys[i] + "=";

                        appendString += HttpUtility.UrlEncode(values[i]);

                        url += appendString;
                    }

                    return url;
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => Values.GetEnumerator();
        }
        

        public Action<string> CallSub;

        public event Action<Exception> CallFails;
        public event Action<string> CallSucceeded;

        private static string GameID => StringObfuscation.DeObfuscate(API.GameID);
        private static string GameKey => StringObfuscation.DeObfuscate(API.GameKey);
        
        private string Username { get; set; }
        private string Token { get; set; }
        private bool LoggedIn { get; set; }

        private Exception Exception { get; set; } = null; // TODO: For object dump?


        public APICall()
        {
            Username = API.Username;
            Token = API.Token;

            LoggedIn = API.LoggedIn;
        }
        public APICall(Action<string> callSub)
        {
            CallSub = callSub;

            Username = API.Username;
            Token = API.Token;

            LoggedIn = API.LoggedIn;
        }

        public async void VerifyUser(string newUsername, string newToken)
        {
            API.Username = newUsername;
            API.Token = newToken;
            Username = newUsername;
            Token = newToken;

            var url = new APIURL("/users/auth/")
            {
                {"game_id", GameID},
                {"username", Username},
                {"user_token", Token}
            };

            await Initialize(url.GetURL, RequestMethod.GET);
        }

        #region "Storage"

        public async void SetStorageData(string key, string data, bool useUsername)
        {
            if (useUsername)
            {
                if (!LoggedIn)
                {
                    var up = new Exception("User not logged in!"); //Happens when a user tries to send an API call but is not logged in.
                    throw up;
                }
                else
                {
                    var url = new APIURL("/data-store/set/")
                    {
                        {"game_id", GameID},
                        {"username", Username},
                        {"user_token", Token},
                        {"key", key}
                    };

                    await Initialize(url.GetURL, RequestMethod.POST, data);
                }
            }
            else
            {
                var url = new APIURL("/data-store/set/")
                {
                    {"game_id", GameID},
                    { "key", key}
                };

                await Initialize(url.GetURL, RequestMethod.POST, data);
            }
        }

        public async void UpdateStorageData(string key, string value, string operation, bool useUsername)
        {
            if (useUsername)
            {
                if (!LoggedIn)
                {
                    var up = new Exception("User not logged in!"); //Happens when a user tries to send an API call but is not logged in.
                    throw up;
                }
                else
                {
                    var url = new APIURL("/data-store/update/")
                    {
                        {"game_id", GameID},
                        {"username", Username},
                        {"user_token", Token},
                        {"key", key},
                        {"operation", operation},
                        {"value", value}
                    };

                    await Initialize(url.GetURL, RequestMethod.GET);
                }
            }
            else
            {
                var url = new APIURL("/data-store/update/")
                {
                    {"game_id", GameID},
                    {"key", key},
                    {"operation", operation},
                    {"value", value}
                };

                await Initialize(url.GetURL, RequestMethod.GET);
            }
        }

        public async void SetStorageData(string[] keys, string[] dataItems, bool[] useUsernames)
        {
            if (keys.Length != dataItems.Length || keys.Length != useUsernames.Length)
            {
                var ex = new Exception("The data arrays do not have the same lengths.");
                ex.Data.Add("Keys Length", keys.Length);
                ex.Data.Add("Data Length", dataItems.Length);
                ex.Data.Add("Username permission Length", useUsernames.Length);
                throw ex;
            }

            var url = API.HOST + API.API_VERSION + "/batch/" + "?game_id=" + GameID + "&parallel=true";
            var postDataURL = "";

            for (var i = 0; i <= keys.Length - 1; i++)
            {
                var key = keys[i];
                var data = dataItems[i];
                var useUsername = useUsernames[i];

                if (useUsername && !LoggedIn)
                    throw new Exception("User not logged in!");

                if (useUsername)
                    postDataURL += "&requests[]=" + HttpUtility.UrlEncode(GetHashedURL("/data-store/set/" + "?game_id=" + GameID + "&username=" + Username + "&user_token=" + Token + "&key=" + HttpUtility.UrlEncode(key) + "&data=" + HttpUtility.UrlEncode(data)));
                else
                    postDataURL += "&requests[]=" + HttpUtility.UrlEncode(GetHashedURL("/data-store/set/" + "?game_id=" + GameID + "&key=" + HttpUtility.UrlEncode(key) + "&data=" + HttpUtility.UrlEncode(data)));
            }

            await Initialize(url, RequestMethod.POST, postDataURL);
        }

        public async void SetStorageDataRestricted(string key, string data)
        {
            var url = API.HOST + API.API_VERSION + "/data-store/set/" + "?game_id=" + GameID + "&key=" + key + "&restriction_username=" + API.Username + "&restriction_user_token=" + API.Token;

            await Initialize(url, RequestMethod.POST, data);
        }

        public async void GetStorageData(string key, bool useUsername)
        {
            if (useUsername)
            {
                if (!LoggedIn)
                    throw new Exception("User not logged in!");
                else
                {
                    var url = new APIURL("/data-store/")
                    {
                        {"game_id", GameID},
                        {"username", Username},
                        {"user_token", Token},
                        {"key", key}
                    };

                    await Initialize(url.GetURL, RequestMethod.GET);
                }
            }
            else
            {
                var url = new APIURL("/data-store/")
                {
                    {"game_id", GameID},
                    { "key", key}
                };

                await Initialize(url.GetURL, RequestMethod.GET);
            }
        }

        public async void GetStorageData(string[] keys, bool useUsername)
        {
            if (useUsername)
            {
                if (!LoggedIn)
                    throw new Exception("User not logged in!");
                else
                {
                    string url = API.HOST + API.API_VERSION + "/batch/";

                    bool firstURL = true;

                    foreach (string key in keys)
                    {
                        string keyURL = "?";
                        if (firstURL == false)
                        {
                            keyURL = "&";
                        }

                        keyURL += "requests[]=" + HttpUtility.UrlEncode(GetHashedURL(API.HOST + API.API_VERSION + "/data-store/" + "?game_id=" + GameID + "&username=" + Username + "&user_token=" + Token + "&key=" + key));

                        url += keyURL;

                        firstURL = false;
                    }

                    url += "&game_id=" + GameID;

                    await Initialize(url, RequestMethod.GET);
                }
            }
            else
            {
                string url = API.HOST + API.API_VERSION + "/batch/";

                bool firstURL = true;

                foreach (string key in keys)
                {
                    string keyURL = "?";
                    if (firstURL == false)
                    {
                        keyURL = "&";
                    }

                    keyURL += "requests[]=" + HttpUtility.UrlEncode(GetHashedURL(API.HOST + API.API_VERSION + "/data-store/" + "?game_id=" + GameID + "&key=" + key));

                    url += keyURL;

                    firstURL = false;
                }

                url += "&game_id=" + GameID;

                await Initialize(url, RequestMethod.GET);
            }
        }

        public async void FetchUserdata(string username)
        {
            var url = new APIURL("/users/")
            {
                {"game_id", GameID},
                { "username", username}
            };

            await Initialize(url.GetURL, RequestMethod.GET);
        }

        public async void FetchUserdataByID(string user_id)
        {
            var url = new APIURL("/users/")
            {
                {"game_id", GameID},
                { "user_id", user_id}
            };

            await Initialize(url.GetURL, RequestMethod.GET);
        }

        public async void GetKeys(bool useUsername, string pattern)
        {
            if (useUsername)
            {
                if (!LoggedIn)
                    throw new Exception("User not logged in!");
                else
                {
                    var url = new APIURL("/data-store/get-keys/")
                    {
                        {"game_id", GameID},
                        {"username", Username},
                        {"user_token", Token},
                        {"pattern", pattern}
                    };

                    await Initialize(url.GetURL, RequestMethod.GET);
                }
            }
            else
            {
                var url = new APIURL("/data-store/get-keys/")
                {
                    {"game_id", GameID},
                    { "pattern", pattern}
                };

                await Initialize(url.GetURL, RequestMethod.GET);
            }
        }

        public async void RemoveKey(string key, bool useUsername)
        {
            if (useUsername)
            {
                if (!LoggedIn)
                    throw new Exception("User Not logged in!");
                else
                {
                    var url = new APIURL("/data-store/remove/")
                    {
                        {"game_id", GameID},
                        {"username", Username},
                        {"user_token", Token},
                        {"key", key}
                    };

                    await Initialize(url.GetURL, RequestMethod.POST);
                }
            }
            else
            {
                var url = new APIURL("/data-store/remove/")
                {
                    {"game_id", GameID},
                    { "key", key}
                };

                await Initialize(url.GetURL, RequestMethod.POST);
            }
        }

        #endregion

        #region "Sessions"

        public async void OpenSession()
        {
            var url = new APIURL("/sessions/open/")
            {
                {"game_id", GameID},
                {"username", Username},
                {"user_token", Token}
            };

            await Initialize(url.GetURL, RequestMethod.GET);
        }

        public async void CheckSession()
        {
            var url = new APIURL("/sessions/ping/")
            {
                {"game_id", GameID},
                { "username", Username},
                { "user_token", Token}
            };

            await Initialize(url.GetURL, RequestMethod.GET);
        }

        public async void PingSession()
        {
            var url = new APIURL("/sessions/ping/")
            {
                {"game_id", GameID},
                {"username", Username},
                {"user_token", Token}
            };

            await Initialize(url.GetURL, RequestMethod.GET);
        }

        public async void CloseSession()
        {
            var url = new APIURL("/sessions/close/")
            {
                {"game_id", GameID},
                {"username", Username},
                {"user_token", Token}
            };

            await Initialize(url.GetURL, RequestMethod.GET);
        }

        #endregion

        #region "Trophy"

        public async void FetchAllTrophies()
        {
            var url = new APIURL("/trophies/")
            {
                {"game_id", GameID},
                { "username", Username},
                { "user_token", Token}
            };

            await Initialize(url.GetURL, RequestMethod.GET);
        }

        public async void FetchAllAchievedTrophies()
        {
            var url = new APIURL("/trophies/")
            {
                {"game_id", GameID},
                {"username", Username},
                {"user_token", Token},
                {"achieved", "true"}
            };

            await Initialize(url.GetURL, RequestMethod.GET);
        }

        public async void FetchTrophy(int trophy_id)
        {
            var url = new APIURL("/trophies/")
            {
                {"game_id", GameID},
                {"username", Username},
                {"user_token", Token},
                {"trophy_id", trophy_id.ToString(NumberFormatInfo.InvariantInfo)}
            };

            await Initialize(url.GetURL, RequestMethod.GET);
        }

        public async void TrophyAchieved(int trophy_id)
        {
            var url = new APIURL("/trophies/add-achieved/")
            {
                {"game_id", GameID},
                {"username", Username},
                {"user_token", Token},
                {"trophy_id", trophy_id.ToString(NumberFormatInfo.InvariantInfo)}
            };

            await Initialize(url.GetURL, RequestMethod.POST);
        }

        public async void RemoveTrophyAchieved(int trophy_id)
        {
            var url = new APIURL("/trophies/remove-achieved/")
            {
                {"game_id", GameID},
                {"username", Username},
                {"user_token", Token},
                {"trophy_id", trophy_id.ToString(NumberFormatInfo.InvariantInfo)}
            };

            await Initialize(url.GetURL, RequestMethod.POST);
        }

        #endregion

        #region "ScoreTable"

        public async void FetchTable(int score_count, string table_id)
        {
            var url = new APIURL("/scores/")
            {
                {"game_id", GameID},
                {"limit", score_count.ToString(NumberFormatInfo.InvariantInfo)},
                {"table_id", table_id}
            };

            await Initialize(url.GetURL, RequestMethod.GET);
        }

        public async void FetchUserRank(string table_id, int sort)
        {
            var url = new APIURL("/scores/get-rank/")
            {
                {"game_id", GameID},
                {"sort", sort.ToString(NumberFormatInfo.InvariantInfo)},
                {"table_id", table_id}
            };

            await Initialize(url.GetURL, RequestMethod.GET);
        }

        public async void AddScore(string score, int sort, string table_id)
        {
            var url = new APIURL("/scores/add/")
            {
                {"game_id", GameID},
                {"username", Username},
                {"user_token", Token},
                {"score", score},
                {"sort", sort.ToString(NumberFormatInfo.InvariantInfo)},
                {"table_id", table_id}
            };

            await Initialize(url.GetURL, RequestMethod.POST);
        }

        #endregion

        #region "Friends"

        public async void FetchFriendList(string user_id)
        {
            var url = new APIURL("/friends/")
            {
                {"game_id", GameID},
                { "user_id", user_id}
            };

            await Initialize(url.GetURL, RequestMethod.GET);
        }

        public async void FetchSentFriendRequest()
        {
            var url = new APIURL("/friends/sent-requests/")
            {
                {"game_id", GameID},
                {"username", Username},
                {"user_token", Token}
            };

            await Initialize(url.GetURL, RequestMethod.GET);
        }

        public async void FetchReceivedFriendRequests()
        {
            var url = new APIURL("/friends/received-requests/")
            {
                {"game_id", GameID},
                {"username", Username},
                {"user_token", Token}
            };

            await Initialize(url.GetURL, RequestMethod.GET);
        }

        public async void SendFriendRequest(string targetuserID)
        {
            var url = new APIURL("/friends/send-request/")
            {
                {"game_id", GameID},
                {"username", Username},
                {"user_token", Token},
                {"target_user_id", targetuserID}
            };

            await Initialize(url.GetURL, RequestMethod.POST);
        }

        public async void CancelFriendRequest(string targetUserID)
        {
            var url = new APIURL("/friends/cancel-request/")
            {
                {"game_id", GameID},
                {"username", Username},
                {"user_token", Token},
                {"target_user_id", targetUserID}
            };

            await Initialize(url.GetURL, RequestMethod.POST);
        }

        public async void AcceptFriendRequest(string targetUserID)
        {
            var url = new APIURL("/friends/accept-request/")
            {
                {"game_id", GameID},
                {"username", Username},
                {"user_token", Token},
                {"target_user_id", targetUserID}
            };

            await Initialize(url.GetURL, RequestMethod.POST);
        }

        public async void DeclineFriendRequest(string targetUserID)
        {
            var url = new APIURL("/friends/decline-request/")
            {
                {"game_id", GameID},
                {"username", Username},
                {"user_token", Token},
                {"target_user_id", targetUserID}
            };

            await Initialize(url.GetURL, RequestMethod.POST);
        }

        #endregion

        private string url = "";
        private string PostData = "";

        private string GetHashedURL(string url)
        {
            byte[] data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(url + GameKey));

            var sBuild = new StringBuilder();
            for (var i = 0; i <= data.Length - 1; i++)
                sBuild.Append(data[i].ToString("x2"));
            return url + "&signature=" + sBuild;
        }

        private async Task Initialize(string url, RequestMethod method, string postData = "")
        {
            Exception = null;

            string newurl = GetHashedURL(url + "&format=keypair");

            //Debug.Print(newurl);
            //Intentional

            if (method == RequestMethod.POST)
            {
                this.url = newurl;
                PostData = postData;

                POSTRequst();
            }
            else
            {
                var data = string.Empty;
                try
                {
                    var request = (HttpWebRequest) WebRequest.Create(newurl);
                    request.Method = method.ToString();
                    var response = (HttpWebResponse) await request.GetResponseAsync();
                    data = new StreamReader(response.GetResponseStream()).ReadToEnd();
                }
                catch (Exception ex)
                {
                    API.APICallCount -= 1;
                    CallFails?.Invoke(ex);
                }

                if (!string.IsNullOrEmpty(data))
                {
                    CallSucceeded?.Invoke(data);
                    CallSub?.Invoke(data);
                }
            }

            API.APICallCount += 1;
        }

        private async Task POSTRequst()
        {
            string gotData = "";
            bool gotDataSuccess = false;

            try
            {
                var request = (HttpWebRequest) WebRequest.Create(url);
                request.AllowWriteStreamBuffering = true;
                request.Method = "POST";
                var post = "data=" + PostData;
                request.ContentLength = post.Length;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ServicePoint.Expect100Continue = false;

                var writer = new StreamWriterLock(await request.GetRequestStreamAsync());
                writer.Write(post);
                writer.Close();
                var response = (HttpWebResponse) await request.GetResponseAsync();

                gotData = new StreamReader(response.GetResponseStream()).ReadToEnd();
                gotDataSuccess = true;
            }
            catch (Exception ex) { CallFails?.Invoke(ex); }
            finally { API.APICallCount -= 1; }

            //Handle data outside of the try...catch because the result function could throw an error:
            if (gotDataSuccess)
            {
                if (CallSub != null)
                {
                    CallSub(gotData);
                    CallSucceeded?.Invoke(gotData);
                }
            }
        }
    }
}

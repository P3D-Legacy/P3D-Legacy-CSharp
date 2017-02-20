using System;
using System.Collections.Generic;
using P3D.Legacy.Core.GameJolt;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Shared;

namespace P3D.Legacy.Core
{
    public static class CommandLineArgHandler
    {
        public static bool ForceGraphics { get; private set; }

        public static void Initialize(string[] args)
        {
            var options = LaunchArgsHandler.ParseArgs(args);
            ForceGraphics = options.ForceGraphics;

            if (options.GameJoltYaml != null)
            {
                APICall.GAMEID = options.GameJoltYaml.GameId;
                APICall.GAMEKEY = options.GameJoltYaml.GameKey;
                API.Username = options.GameJoltYaml.Username;
                API.Token = options.GameJoltYaml.Token;

                APICall apiCall1 = new APICall(result1 =>
                {
                    List<API.JoltValue> list1 = API.HandleData(result1);
                    if (Convert.ToBoolean(list1[0].Value))
                    {
                        API.LoggedIn = true;


                        APICall apiCall2 = new APICall(result2 =>
                        {
                            List<API.JoltValue> list2 = API.HandleData(result2);
                            foreach (API.JoltValue Item in list2)
                            {
                                if (Item.Name == "id")
                                {
                                    API.GameJoltID = Item.Value;
                                    //set the public shared field to the GameJolt ID.

                                    if (GameController.UPDATEONLINEVERSION == true &
                                        GameController.IS_DEBUG_ACTIVE == true)
                                    {
                                        APICall APICall = new APICall();
                                        APICall.SetStorageDataRestricted("ONLINEVERSION", GameController.GAMEVERSION);
                                        Logger.Debug("UPDATED ONLINE VERSION TO: " + GameController.GAMEVERSION);
                                    }
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                        });
                        apiCall2.FetchUserdata(API.Username);
                    }
                    else
                    {
                        API.LoggedIn = false;
                    }
                });
                apiCall1.VerifyUser(API.Username, API.Token);
            }
        }
    }
}

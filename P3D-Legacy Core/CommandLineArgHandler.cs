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

                var apiCall1 = new APICall(result1 =>
                {
                    var list1 = API.HandleData(result1);
                    if (Convert.ToBoolean(list1[0].Value))
                    {
                        API.LoggedIn = true;


                        var apiCall2 = new APICall(result2 =>
                        {
                            var list2 = API.HandleData(result2);
                            foreach (var Item in list2)
                            {
                                if (Item.Name == "id")
                                {
                                    API.GameJoltID = Item.Value;
                                    //set the public shared field to the GameJolt ID.

                                    if (GameController.UPDATEONLINEVERSION && GameController.IS_DEBUG_ACTIVE)
                                    {
                                        var apiCall = new APICall();
                                        apiCall.SetStorageDataRestricted("ONLINEVERSION", GameController.GAMEVERSION);
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

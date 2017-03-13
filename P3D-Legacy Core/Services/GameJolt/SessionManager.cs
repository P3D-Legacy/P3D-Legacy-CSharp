using System;

using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.GameJolt
{
    public static class SessionManager
    {
        private static bool SessionStarted { get; set; }
        private static string Status { get; set; } = "Idle";
        private static DateTime LastPing { get; set; } = DateTime.Now;

        public static void Update()
        {
            if (API.LoggedIn)
            {
                switch (Status)
                {
                    case "Idle":
                        if (!SessionStarted)
                            Start();
                        else
                            Ping();
                        break;

                    case "WaitingForOpen":
                        Logger.Debug("Session started!");
                        Status = "Idle";
                        SessionStarted = true;
                        break;
                }
            }
            else
                Status = "Idle";
        }

        public static void Ping()
        {
            int diff = Convert.ToInt32((DateTime.Now - LastPing).Seconds);
            if (diff >= 30)
            {
                Logger.Debug("Ping session...");

                LastPing = DateTime.Now;

                if (API.LoggedIn)
                {
                    var apiCall = new APICall();
                    apiCall.CallFails += Kick;
                    apiCall.PingSession();
                }
            }
        }

        public static void Start()
        {
            if (API.LoggedIn && Status != "WaitingForCheck")
            {
                Logger.Debug("Starting session...");

                Status = "WaitingForCheck";

                var apiCall = new APICall(CheckedSession);
                apiCall.CheckSession();
            }
        }

        public static void CheckedSession(string result)
        {
            var list = API.HandleData(result);

            var apiCall = new APICall();
            apiCall.OpenSession();

            Status = "WaitingForOpen";

            LastPing = DateTime.Now;

            if (list[0].Value == "true")
                Logger.Log(Logger.LogTypes.Warning, "SessionManager.vb: Tried to log in with an already logged in account!");
        }

        public static void Close()
        {
            if (API.LoggedIn)
            {
                Logger.Debug("Closing session...");

                var apiCall = new APICall();
                apiCall.CloseSession();

                LastPing = DateTime.Now;
                SessionStarted = false;
            }
        }

        private static void Kick(Exception exception)
        {
            if (API.LoggedIn)
            {
                API.LoggedIn = false;
                SessionStarted = false;
                Status = "Idle";

                if (Core.Player.IsGamejoltSave)
                    BaseConnectScreen.Setup(Screen.CreateConnectScreen(BaseConnectScreen.Modes.Disconnect, "Disconnected", "The GameJolt server doesn't respond.", Core.CurrentScreen));
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.GameJolt
{
    public abstract class BaseLogInScreen : Screen
    {
        public abstract class LogInScreenManager
        {
            public abstract bool UserBanned(string id);

            public abstract string BanReasonIDForUser(string user_ID);

            public abstract string GetBanReasonByID(string banReasonID);

            /// <summary>
            /// This gets called from all GameJolt screens. If the player is no longer connected to GameJolt, it opens up the login screen.
            /// </summary>
            public abstract void KickFromOnlineScreen(Screen setToScreen);
        }

        private static LogInScreenManager _lism;
        protected static LogInScreenManager LISM
        {
            get
            {
                if (_lism == null)
                {
                    var assembly = Assembly.GetEntryAssembly();
                    var type = assembly.GetTypes().SingleOrDefault(t => t.IsSubclassOf(typeof(LogInScreenManager)));
                    if (type != null)
                        _lism = Activator.CreateInstance(type) as LogInScreenManager;
                }

                return _lism;
            }
            set { _lism = value; }
        }

        public static bool UserBanned(string id) => LISM.UserBanned(id);
        public static string BanReasonIDForUser(string user_ID) => LISM.BanReasonIDForUser(user_ID);
        public static string GetBanReasonByID(string banReasonID) => LISM.GetBanReasonByID(banReasonID);
        public static void KickFromOnlineScreen(Screen setToScreen) => LISM.KickFromOnlineScreen(setToScreen);
    }
}

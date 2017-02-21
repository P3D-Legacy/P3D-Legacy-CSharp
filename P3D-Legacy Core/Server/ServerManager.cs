using System;
using System.Linq;
using System.Reflection;

using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.Server
{
    public class ServersManager
    {
        public abstract class ServersManagerManager
        {
            public abstract IServerConnection CreateServerConnection();
            public abstract IPlayerManager CreatePlayerManager();
        }

        private static ServersManagerManager _smm;
        protected static ServersManagerManager SMM
        {
            get
            {
                if (_smm == null)
                {
                    var assembly = Assembly.GetEntryAssembly();
                    var type = assembly.GetTypes().SingleOrDefault(t => t.IsSubclassOf(typeof(ServersManagerManager)));
                    if (type != null)
                        _smm = Activator.CreateInstance(type) as ServersManagerManager;
                }

                return _smm;
            }
            set { _smm = value; }
        }
        public static IServerConnection CreateServerConnection() => SMM.CreateServerConnection();
        public static IPlayerManager CreatePlayerManager() => SMM.CreatePlayerManager();



        public const string PROTOCOLVERSION = "0.5";

        public PlayerCollection PlayerCollection { get; } = new PlayerCollection();
        public IServerConnection ServerConnection { get; }
        public IPlayerManager PlayerManager { get; }

        private int _OwnID = 0;
        public int ID
        {
            get { return _OwnID; }
            set
            {
                _OwnID = value;
                PlayerManager.ReceivedID = true;
            }
        }
        
        public ServersManager()
        {
            ServerConnection = CreateServerConnection();
            PlayerManager = CreatePlayerManager();

            //_PlayerCollection = new PlayerCollection();
            //_ServerConnection = new ServerConnection();
            //_PlayerManager = new PlayerManager();
        }

        private void Reset()
        {
            _OwnID = 0;

            ServerConnection.Abort();
            PlayerCollection.Clear();
            PlayerManager.Reset();
        }

        public void Connect(object ServerObject)
        {
            //Conver the ServerObject back to a Server instance and start the connection.
            var Server = (Server) ServerObject;

            Logger.Debug("Try to connect to server: " + Server.IP + ":" + Server.Port);

            Reset();
            //Reset all prior connections.

            Logger.Debug("Start connection");
            ServerConnection.Connect(Server);

            //Set online to true:
            BaseJoinServerScreen.Online = true;
        }

        /// <summary>
        /// Updates the ServersManager and sends the player data package if needed.
        /// </summary>
        public void Update()
        {
            if (BaseJoinServerScreen.Online && BaseConnectScreen.Connected)
            {
                if (PlayerManager.HasNewPlayerData)
                {
                    ServerConnection.SendGameData();
                }
            }
        }

    }
}

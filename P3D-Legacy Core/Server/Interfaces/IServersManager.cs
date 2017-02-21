namespace P3D.Legacy.Core.Server
{
    public interface IServersManager
    {
        PlayerCollection PlayerCollection { get; }
        IServerConnection ServerConnection { get; }
        IPlayerManager PlayerManager { get; }
        int ID { get; set; }


        void Connect(object serverObject);

        /// <summary>
        /// Updates the ServersManager and sends the player data package if needed.
        /// </summary>
        void Update();
    }
}

namespace P3D.Legacy.Core.Server
{
    public interface IServerConnection
    {
        /// <summary>
        /// Returns the current connection status.
        /// </summary>
        bool Connected { get; }

        /// <summary>
        /// Connects to a server.
        /// </summary>
        /// <param name="server">The server to connect to.</param>
        void Connect(Server server);

        /// <summary>
        /// Aborts the threads and closes any open streams.
        /// </summary>
        void Abort();

        /// <summary>
        /// Disconnects the player from the server and opens the main menu.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Disconnects the player from the server and opens the DisconnectScreen (if there's a value for Header or Message).
        /// </summary>
        /// <param name="header">The header to display on the ConnectScreen.</param>
        /// <param name="message">The Message to display on the ConnectScreen.</param>
        void Disconnect(string header, string message);

        /// <summary>
        /// Start the ping thread.
        /// </summary>
        void StartPing();

        /// <summary>
        /// Stopping the ping thread.
        /// </summary>
        void StopPing();
        void SendGameData();
        void SendChatMessage(string message);

        void SendGameStateMessage(string message);

        /// <summary>
        /// Send a package object to the server.
        /// </summary>
        void SendPackage(IPackage package);
    }
}

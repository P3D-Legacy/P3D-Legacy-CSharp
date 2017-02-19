namespace P3D.Legacy.Core.Server
{
    public enum PackageTypes
    {
        GameData = 0,

        /// <summary>
        /// Not used anymore, use GameData instead.
        /// </summary>
        PlayData = 1,

        PrivateMessage = 2,
        ChatMessage = 3,
        Kicked = 4,
        ID = 7,
        CreatePlayer = 8,
        DestroyPlayer = 9,
        ServerClose = 10,
        ServerMessage = 11,
        WorldData = 12,
        Ping = 13,
        GamestateMessage = 14,

        TradeRequest = 30,
        TradeJoin = 31,
        TradeQuit = 32,

        TradeOffer = 33,
        TradeStart = 34,

        BattleRequest = 50,
        BattleJoin = 51,
        BattleQuit = 52,

        BattleOffer = 53,
        BattleStart = 54,

        BattleClientData = 55,
        BattleHostData = 56,
        BattlePokemonData = 57,

        ServerInfoData = 98,
        ServerDataRequest = 99
    }
}
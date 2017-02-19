namespace P3D.Legacy.Core.Server
{
    public interface IPlayerManager
    {
        bool ReceivedWorldData { set; }
        bool ReceivedID { set; }
        bool NeedsUpdate { set; }
        bool ReceivedIniData { get; }
        bool HasNewPlayerData { get; }

        void Reset();
        void UpdatePlayers();
        IPackage CreatePlayerDataPackage();
        void ApplyLastPackage(IPackage newP);
    }
}

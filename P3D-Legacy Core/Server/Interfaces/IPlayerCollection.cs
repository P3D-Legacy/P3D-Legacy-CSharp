using System.Collections.Generic;

using P3D.Legacy.Core.Interfaces;

namespace P3D.Legacy.Core.Server
{
    public interface IPlayerCollection : IList<IPlayer>
    {
        /// <summary>
        /// Removes all players from the collection that have the specified name.
        /// </summary>
        void RemoveByName(string name);
        void RemoveById(int id);
        bool HasPlayer(int id);
        bool HasPlayer(string name);
        IPlayer GetPlayer(int id);
        IPlayer GetPlayer(string name);
        string GetPlayerName(int id);
        void ApplyPlayerDataPackage(IPackage p);
        string GetMatchingPlayerName(string expression);
    }
}

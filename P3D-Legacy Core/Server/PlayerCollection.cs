using System.Collections.Generic;

namespace P3D.Legacy.Core.Server
{
    /// <summary>
    /// Contains all connected players.
    /// </summary>

    public class PlayerCollection : List<BaseOnlinePlayer>
    {
        /// <summary>
        /// Removes all players from the collection that have the specified name.
        /// </summary>
        public void RemoveByName(string name)
        {
            for (var i = 0; i <= Count - 1; i++)
            {
                if (i <= Count - 1)
                {
                    if (this[i].Name == name)
                    {
                        RemoveAt(i);
                        i -= 1;
                    }
                }
                else
                {
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
        }

        public void RemoveById(int id)
        {
            for (var i = 0; i <= Count - 1; i++)
            {
                if (i <= Count - 1)
                {
                    if (this[i].ServersID == id)
                    {
                        RemoveAt(i);
                        i -= 1;
                    }
                }
                else
                {
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
        }

        public bool HasPlayer(int id)
        {
            for (var i = 0; i <= Count - 1; i++)
            {
                if (i <= Count - 1)
                {
                    if (this[i].ServersID == id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HasPlayer(string name)
        {
            for (var i = 0; i <= Count - 1; i++)
            {
                if (i <= Count - 1)
                {
                    if (this[i].Name.ToLower() == name.ToLower())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public BaseOnlinePlayer GetPlayer(int id)
        {
            foreach (var p in this)
            {
                if (p.ServersID == id)
                {
                    return p;
                }
            }
            return null;
        }

        public BaseOnlinePlayer GetPlayer(string name)
        {
            for (var i = 0; i <= Count - 1; i++)
            {
                if (i <= Count - 1)
                {
                    if (this[i].Name == name)
                    {
                        return this[i];
                    }
                }
            }
            return null;
        }

        public string GetPlayerName(int id)
        {
            if (id == -1)
            {
                return "[SERVER]";
            }
            if (id == Core.ServersManager.ID)
            {
                return Core.Player.Name;
            }

            for (var i = 0; i <= Count - 1; i++)
            {
                if (i <= Count - 1)
                {
                    if (this[i].ServersID == id)
                    {
                        return this[i].Name;
                    }
                }
            }

            return "";
        }

        public void ApplyPlayerDataPackage(IPackage p)
        {
            if (p.DataItems.Count == BaseOnlinePlayer.PLAYERDATAITEMSCOUNT)
            {
                if (p.Origin != Core.ServersManager.ID)
                {
                    GetPlayer(p.Origin)?.ApplyNewData(p);
                }
            }
        }

        public string GetMatchingPlayerName(string expression)
        {
            for (var i = 0; i <= Count - 1; i++)
            {
                if (i <= Count - 1)
                {
                    if (this[i].Name.ToLower().StartsWith(expression.ToLower()))
                    {
                        return this[i].Name;
                    }
                }
            }

            //No matching player name, return input expression.
            return expression;
        }
    }
}

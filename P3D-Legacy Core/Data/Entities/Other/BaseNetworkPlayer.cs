using System;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using P3D.Legacy.Core.Interfaces;
using P3D.Legacy.Core.Resources.Models;
using P3D.Legacy.Core.Server;

namespace P3D.Legacy.Core.Entities.Other
{
    public abstract class BaseNetworkPlayer : Entity
    {
        public abstract class NetworkPlayerManager
        {
            public abstract void ScreenRegionChanged();
        }

        private static NetworkPlayerManager _npm;
        protected static NetworkPlayerManager NPM
        {
            get
            {
                if (_npm == null)
                {
                    var assembly = Assembly.GetEntryAssembly();
                    var type = assembly.GetTypes().SingleOrDefault(t => t.IsSubclassOf(typeof(NetworkPlayerManager)));
                    if (type != null)
                        _npm = Activator.CreateInstance(type) as NetworkPlayerManager;
                }

                return _npm;
            }
            set { _npm = value; }
        }
        public static void ScreenRegionChanged() => NPM.ScreenRegionChanged();


        /// <summary>
        /// The Network ID of the player
        /// </summary>
        public int NetworkID { get; set; }

        public BaseNetworkPlayer(float x, float y, float z, string entityID, Texture2D[] textures, int[] textureIndex, bool collision, int rotation, Vector3 scale, BaseModel model, int actionValue, string additionalValue, Vector3 shader)
            : base(x, y, z, entityID, textures, textureIndex, collision, rotation, scale, model, actionValue, additionalValue, shader) { }

        public abstract void ApplyPlayerData(BaseOnlinePlayer p0);
    }
}

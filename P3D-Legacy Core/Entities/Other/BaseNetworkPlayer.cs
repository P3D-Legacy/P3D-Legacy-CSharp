using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Resources.Models;

namespace P3D.Legacy.Core.Entities.Other
{
    public abstract class BaseNetworkPlayer : Entity
    {
        public BaseNetworkPlayer(float x, float y, float z, string entityID, Texture2D[] textures, int[] textureIndex, bool collision, int rotation, Vector3 scale, BaseModel model, int actionValue, string additionalValue, Vector3 shader)
            : base(x, y, z, entityID, textures, textureIndex, collision, rotation, scale, model, actionValue, additionalValue, shader) { }

        public static void ScreenRegionChanged()
        {

        }
    }
}

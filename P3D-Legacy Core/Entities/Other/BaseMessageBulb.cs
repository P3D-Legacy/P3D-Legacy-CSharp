using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Resources.Models;

namespace P3D.Legacy.Core.Entities.Other
{
    public abstract class BaseMessageBulb : Entity
    {
        public enum NotifcationTypes
        {
            Waiting = 0,
            Exclamation = 1,
            Shouting = 2,
            Question = 3,
            Note = 4,
            Heart = 5,
            Unhappy = 6,
            Happy = 7,
            Friendly = 8,
            Poisoned = 9,
            Battle = 10,
            Wink = 11,
            AFK = 12,
            Angry = 13,
            CatFace = 14,
            Unsure = 15
        }


        public BaseMessageBulb(float x, float y, float z, string entityID, Texture2D[] textures, int[] textureIndex, bool collision, int rotation, Vector3 scale, BaseModel model, int actionValue, string additionalValue, Vector3 shader)
            : base(x, y, z, entityID, textures, textureIndex, collision, rotation, scale, model, actionValue, additionalValue, shader) { }
    }
}

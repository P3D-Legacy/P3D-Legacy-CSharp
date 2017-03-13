using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Pokemon;
using P3D.Legacy.Core.Resources.Models;

namespace P3D.Legacy.Core.Entities.Other
{
    public abstract class BaseOverworldPokemon : Entity
    {
        public abstract bool warped { get; set; }
        public abstract float MoveSpeed { get; set; }
        public abstract BasePokemon PokemonReference { get; set; }
        public abstract int faceRotation { get; set; }


        public BaseOverworldPokemon(float x, float y, float z, string entityID, Texture2D[] textures, int[] textureIndex, bool collision, int rotation, Vector3 scale, BaseModel model, int actionValue, string additionalValue, Vector3 shader)
            : base(x, y, z, entityID, textures, textureIndex, collision, rotation, scale, model, actionValue, additionalValue, shader) { }


        public abstract bool IsVisible();

        public abstract void ApplyShaders();

        public abstract void ChangeRotation();

        public abstract void MakeVisible();

        public abstract void ForceTextureChange();
    }
}

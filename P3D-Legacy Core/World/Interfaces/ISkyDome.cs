using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace P3D.Legacy.Core.World
{
    public interface ISkyDome
    {
        Texture2D TextureUp { get; set; }
        Texture2D TextureDown { get; set; }

        Vector3 GetWeatherColorMultiplier(Vector3 vector);
        void Update();
        void Draw(float fov);
    }
}
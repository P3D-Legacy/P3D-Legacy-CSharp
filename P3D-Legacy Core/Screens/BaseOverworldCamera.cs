using Microsoft.Xna.Framework;

using P3D.Legacy.Core.Data;

namespace P3D.Legacy.Core.Screens
{
    public abstract class BaseOverworldCamera : Camera
    {
        public int OldX { get; set; }
        public int OldY { get; set; }

        public abstract bool ThirdPerson { get; }
        public Vector3 ThirdPersonOffset = new Vector3(0F, 0.3F, 1.5F);

        public abstract Vector3 CPosition { get; }


        public BaseOverworldCamera(string Name) : base(Name) { }


        public float GetAimYawFromDirection(int direction)
        {
            switch (direction)
            {
                case 0:
                    return 0f;
                case 1:
                    return MathHelper.Pi * 0.5f;
                case 2:
                    return MathHelper.Pi;
                case 3:
                    return MathHelper.Pi * 1.5f;
            }
            return 0f;
        }
    }
}

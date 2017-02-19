using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace P3D.Legacy.Core.Battle.BattleAnimations
{
    public class BASize : BattleAnimation3D
    {

        public enum Anchors
        {
            Top,
            Left,
            Right,
            Bottom
        }

        public bool Grow = false;
        public Vector3 EndSize;
        public float SizeSpeed = 0.01f;

        public Anchors[] Anchor = { Anchors.Bottom };

        public Vector3 Change = new Vector3(1);
        public BASize(Vector3 Position, Texture2D Texture, Vector3 Scale, bool Grow, Vector3 EndSize, float SizeSpeed, float startDelay, float endDelay) : base(Position, Texture, Scale, startDelay, endDelay)
        {

            this.Grow = Grow;
            this.EndSize = EndSize;
            this.SizeSpeed = SizeSpeed;

            this.AnimationType = AnimationTypes.Size;
        }

        public override void DoActionActive()
        {
            Vector3 saveScale = this.Scale;

            float changeX = SizeSpeed * Change.X;
            float changeY = SizeSpeed * Change.Y;
            float changeZ = SizeSpeed * Change.Z;

            if (Grow == true)
            {
                if (this.Scale.X < this.EndSize.X)
                {
                    Scale.X += changeX;

                    if (this.Scale.X >= this.EndSize.X)
                    {
                        Scale.X = this.EndSize.X;
                    }
                }
                if (this.Scale.Y < this.EndSize.Y)
                {
                    Scale.Y += changeY;

                    if (this.Scale.Y >= this.EndSize.Y)
                    {
                        Scale.Y = this.EndSize.Y;
                    }
                }
                if (this.Scale.Z < this.EndSize.Z)
                {
                    Scale.Z += changeZ;

                    if (this.Scale.Z >= this.EndSize.Z)
                    {
                        Scale.Z = this.EndSize.Z;
                    }
                }
            }
            else
            {
                if (this.Scale.X > this.EndSize.X)
                {
                    Scale.X -= changeX;

                    if (this.Scale.X <= this.EndSize.X)
                    {
                        Scale.X = this.EndSize.X;
                    }
                }
                if (this.Scale.Y > this.EndSize.Y)
                {
                    Scale.Y -= changeY;

                    if (this.Scale.Y <= this.EndSize.Y)
                    {
                        Scale.Y = this.EndSize.Y;
                    }
                }
                if (this.Scale.Z > this.EndSize.Z)
                {
                    Scale.Z -= changeZ;

                    if (this.Scale.Z <= this.EndSize.Z)
                    {
                        Scale.Z = this.EndSize.Z;
                    }
                }
            }

            if (Anchor.Contains(Anchors.Bottom) == true)
            {
                float diffY = saveScale.Y - this.Scale.Y;
                Position.Y -= diffY / 2;
            }
            if (Anchor.Contains(Anchors.Top) == true)
            {
                float diffY = saveScale.Y - this.Scale.Y;
                Position.Y += diffY / 2;
            }
            if (Anchor.Contains(Anchors.Left) == true)
            {
                float diffX = saveScale.X - this.Scale.X;
                Position.X -= diffX / 2;
            }
            if (Anchor.Contains(Anchors.Right) == true)
            {
                float diffX = saveScale.X - this.Scale.X;
                Position.X += diffX / 2;
            }

            if (this.EndSize == this.Scale)
            {
                this.Ready = true;
            }
        }

        public void SetChange(float changeX, float changeY, float changeZ)
        {
            this.Change = new Vector3(changeX, changeY, changeZ);
        }

    }
}

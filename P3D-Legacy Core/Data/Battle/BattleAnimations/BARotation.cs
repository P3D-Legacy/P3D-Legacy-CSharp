using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace P3D.Legacy.Core.Battle.BattleAnimations
{
    public class BARotation : BattleAnimation3D
    {

        Vector3 RotationVector;
        Vector3 EndRotation;
        bool DoReturn = false;
        Vector3 ReturnVector;
        bool hasReturned = false;

        Vector3 DoRotation = new Vector3(1f);
        public BARotation(Vector3 Position, Texture2D Texture, Vector3 Scale, Vector3 RotationVector, Vector3 EndRotation, float startDelay, float endDelay) : base(Position, Texture, Scale, startDelay, endDelay)
        {

            this.RotationVector = RotationVector;
            this.EndRotation = EndRotation;
            this.ReturnVector = this.Rotation;
        }

        public BARotation(Vector3 Position, Texture2D Texture, Vector3 Scale, Vector3 RotationVector, Vector3 EndRotation, float startDelay, float endDelay, bool DoXRotation, bool DoYRotation, bool DoZRotation) : this(Position, Texture, Scale, RotationVector, EndRotation, startDelay, endDelay)
        {

            if (DoXRotation == false)
            {
                DoRotation.X = 0f;
            }
            if (DoYRotation == false)
            {
                DoRotation.Y = 0f;
            }
            if (DoZRotation == false)
            {
                DoRotation.Z = 0f;
            }
        }

        public BARotation(Vector3 Position, Texture2D Texture, Vector3 Scale, Vector3 RotationVector, Vector3 EndRotation, float startDelay, float endDelay, bool DoXRotation, bool DoYRotation, bool DoZRotation,
        bool DoReturn) : this(Position, Texture, Scale, RotationVector, EndRotation, startDelay, endDelay, DoXRotation, DoYRotation, DoZRotation)
        {

            this.DoReturn = DoReturn;
        }

        public override void DoActionActive()
        {

            if (VectorReached() == false)
            {
                if (DoRotation.X == 1f)
                {
                    if (this.Rotation.X > this.EndRotation.X)
                    {
                        this.Rotation.X += this.RotationVector.X;

                        if (this.Rotation.X <= this.EndRotation.X)
                        {
                            this.Rotation.X = this.EndRotation.X;
                        }
                    }
                    else if (this.Rotation.X < this.EndRotation.X)
                    {
                        this.Rotation.X += this.RotationVector.X;

                        if (this.Rotation.X >= this.EndRotation.X)
                        {
                            this.Rotation.X = this.EndRotation.X;
                        }
                    }
                }

                if (DoRotation.Y == 1f)
                {
                    if (this.Rotation.Y > this.EndRotation.Y)
                    {
                        this.Rotation.Y += this.RotationVector.Y;

                        if (this.Rotation.Y <= this.EndRotation.Y)
                        {
                            this.Rotation.Y = this.EndRotation.Y;
                        }
                    }
                    else if (this.Rotation.Y < this.EndRotation.Y)
                    {
                        this.Rotation.Y += this.RotationVector.Y;

                        if (this.Rotation.Y >= this.EndRotation.Y)
                        {
                            this.Rotation.Y = this.EndRotation.Y;
                        }
                    }
                }

                if (DoRotation.Z == 1f)
                {
                    if (this.Rotation.Z > this.EndRotation.Z)
                    {
                        this.Rotation.Z += this.RotationVector.Z;

                        if (this.Rotation.Z <= this.EndRotation.Z)
                        {
                            this.Rotation.Z = this.EndRotation.Z;
                        }
                    }
                    else if (this.Rotation.Z < this.EndRotation.Z)
                    {
                        this.Rotation.Z += this.RotationVector.Z;

                        if (this.Rotation.Z >= this.EndRotation.Z)
                        {
                            this.Rotation.Z = this.EndRotation.Z;
                        }
                    }
                }

                if (VectorReached() == true)
                {
                    RotationReady();
                }
            }
            else
            {
                RotationReady();
            }
        }

        private void RotationReady()
        {
            if (this.DoReturn == true && this.hasReturned == false)
            {
                this.hasReturned = true;
                this.EndRotation = this.ReturnVector;
                this.RotationVector = new Vector3(this.RotationVector.X * -1, this.RotationVector.Y * -1, this.RotationVector.Z * -1);
            }
            else
            {
                this.Ready = true;
            }
        }

        private bool VectorReached()
        {
            if (DoRotation.X == 1f)
            {
                if (EndRotation.X != this.Rotation.X)
                {
                    return false;
                }
            }
            if (DoRotation.Y == 1f)
            {
                if (EndRotation.Y != this.Rotation.Y)
                {
                    return false;
                }
            }
            if (DoRotation.Z == 1f)
            {
                if (EndRotation.Z != this.Rotation.Z)
                {
                    return false;
                }
            }

            return true;
        }

    }
}

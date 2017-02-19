using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace P3D.Legacy.Core.Battle.BattleAnimations
{
    public class BAMove : BattleAnimation3D
    {
        public Vector3 Destination;
        public float MoveSpeed;
        public bool SpinX = false;

        public bool SpinZ = false;
        public float SpinSpeedX = 0.1f;

        public float SpinSpeedZ = 0.1f;
        public BAMove(Vector3 Position, Texture2D Texture, Vector3 Scale, Vector3 Destination, float Speed, bool SpinX, bool SpinZ, float startDelay, float endDelay) : base(Position, Texture, Scale, startDelay, endDelay)
        {

            this.Destination = Destination;
            this.MoveSpeed = Speed;
            this.Scale = Scale;

            this.SpinX = SpinX;
            this.SpinZ = SpinZ;

            this.AnimationType = AnimationTypes.Move;
        }

        public BAMove(Vector3 Position, Texture2D Texture, Vector3 Scale, Vector3 Destination, float Speed, float startDelay, float endDelay) : this(Position, Texture, Scale, Destination, Speed, false, false, startDelay, endDelay)
        {
        }

        public override void DoActionUpdate()
        {
            Spin();
        }

        public override void DoActionActive()
        {
            Move();
        }

        private void Spin()
        {
            if (this.SpinX == true)
            {
                this.Rotation.X += SpinSpeedX;
            }
            if (this.SpinZ == true)
            {
                this.Rotation.Z += SpinSpeedZ;
            }
        }

        private void Move()
        {
            if (this.Position.X < this.Destination.X)
            {
                this.Position.X += this.MoveSpeed;

                if (this.Position.X >= this.Destination.X)
                {
                    this.Position.X = this.Destination.X;
                }
            }
            else if (this.Position.X > this.Destination.X)
            {
                this.Position.X -= this.MoveSpeed;

                if (this.Position.X <= this.Destination.X)
                {
                    this.Position.X = this.Destination.X;
                }
            }
            if (this.Position.Y < this.Destination.Y)
            {
                this.Position.Y += this.MoveSpeed;

                if (this.Position.Y >= this.Destination.Y)
                {
                    this.Position.Y = this.Destination.Y;
                }
            }
            else if (this.Position.Y > this.Destination.Y)
            {
                this.Position.Y -= this.MoveSpeed;

                if (this.Position.Y <= this.Destination.Y)
                {
                    this.Position.Y = this.Destination.Y;
                }
            }
            if (this.Position.Z < this.Destination.Z)
            {
                this.Position.Z += this.MoveSpeed;

                if (this.Position.Z >= this.Destination.Z)
                {
                    this.Position.Z = this.Destination.Z;
                }
            }
            else if (this.Position.Z > this.Destination.Z)
            {
                this.Position.Z -= this.MoveSpeed;

                if (this.Position.Z <= this.Destination.Z)
                {
                    this.Position.Z = this.Destination.Z;
                }
            }

            //Dim x As Integer = 0
            //Dim y As Integer = 0
            //Dim z As Integer = 0

            //If Destination.X < Me.Position.X Then
            //    x = -1
            //ElseIf Destination.X > Me.Position.X Then
            //    x = 1
            //End If
            //If Destination.Y < Me.Position.Y Then
            //    y = -1
            //ElseIf Destination.X > Me.Position.Y Then
            //    y = 1
            //End If
            //If Destination.Z < Me.Position.Z Then
            //    z = -1
            //ElseIf Destination.Z > Me.Position.Z Then
            //    z = 1
            //End If

            //Dim v As Vector3 = New Vector3(x, y, z) * Speed
            //Position.X += v.X
            //Position.Z += v.Z
            //Position.Y += v.Y

            //If CInt(Destination.X) = CInt(Me.Position.X) Then
            //    If Functions.ToPositive(Me.Position.X - Destination.X) <= Me.Speed Then
            //        Me.Position.X = CInt(Me.Position.X)
            //    End If
            //End If
            //If CInt(Destination.Y) = CInt(Me.Position.Y) Then
            //    If Functions.ToPositive(Me.Position.Y - Destination.Y) <= Me.Speed + 0.01F Then
            //        Me.Position.Y = CInt(Me.Position.Y)
            //    End If
            //End If
            //If CInt(Destination.Z) = CInt(Me.Position.Z) Then
            //    If Functions.ToPositive(Me.Position.Z - Destination.Z) <= Me.Speed Then
            //        Me.Position.Z = CInt(Me.Position.Z)
            //    End If
            //End If

            if (this.Position == Destination)
            {
                this.Ready = true;
            }
        }

    }
}

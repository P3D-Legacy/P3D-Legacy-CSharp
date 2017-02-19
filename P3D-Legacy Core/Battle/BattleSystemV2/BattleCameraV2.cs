using System;

using Microsoft.Xna.Framework;

using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.Battle.BattleSystemV2
{
    public class BattleCamera : Camera
    {

        int oldX;

        int oldY;
        public Vector3 TargetPosition;

        public bool TargetMode = true;
        public float TargetYaw = 0f;
        public float TargetSpeed = 0.04f;
        public float TargetRotationSpeed = 0.04f;

        public float TargetPitch = 0f;
        public bool IsReady
        {
            get
            {
                if (this.Position == this.TargetPosition && this.TargetSpeed == this.Speed && this.TargetYaw == this.Yaw && this.TargetPitch == this.Pitch && this.TargetRotationSpeed == this.RotationSpeed)
                {
                    return true;
                }
                return false;
            }
        }

        public BattleCamera() : base("BattleV2")
        {

            this.Position = new Vector3(10, 10, 14);
            this.RotationSpeed = 0.008f;
            this.FOV = 60f;
            this.Speed = 0.04f;
            this.TargetSpeed = 0.04f;

            this.Yaw = Convert.ToSingle(Math.PI / 4);
            this.TargetYaw = Convert.ToSingle(Math.PI / 4);
            this.Pitch = -0.6f;
            this.TargetPitch = -0.6f;

            View = Matrix.CreateLookAt(Position, Vector3.Zero, Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(FOV), Core.GraphicsDevice.Viewport.AspectRatio, 0.01f, this.FarPlane);

            UpdateMatrices();
            UpdateFrustum();
            CreateRay();
        }

        public override void Update()
        {
            Ray = CreateRay();

            UpdateCamera();

            UpdateMatrices();
            UpdateFrustum();
        }

        public void UpdateCamera()
        {
            if (TargetMode == true)
            {
                if (this.Position != this.TargetPosition)
                {
                    float MoveX = 0f;
                    float MoveY = 0f;
                    float MoveZ = 0f;

                    if (this.Position.X < this.TargetPosition.X)
                    {
                        MoveX = this.Speed;
                        if (this.Position.X + MoveX > this.TargetPosition.X)
                        {
                            //Me.Position.X = Me.TargetPosition.X
                            Position = new Vector3(TargetPosition.X, Position.Y, Position.Z);
                            MoveX = 0f;
                        }
                    }

                    if (this.Position.X > this.TargetPosition.X)
                    {
                        MoveX = -this.Speed;
                        if (this.Position.X + MoveX < this.TargetPosition.X)
                        {
                            //Me.Position.X = Me.TargetPosition.X
                            Position = new Vector3(TargetPosition.X, Position.Y, Position.Z);
                            MoveX = 0f;
                        }
                    }

                    if (this.Position.Y < this.TargetPosition.Y)
                    {
                        MoveY = this.Speed;
                        if (this.Position.Y + MoveY > this.TargetPosition.Y)
                        {
                            //Me.Position.Y = Me.TargetPosition.Y
                            Position = new Vector3(Position.X, TargetPosition.Y, Position.Z);
                            MoveY = 0f;
                        }
                    }

                    if (this.Position.Y > this.TargetPosition.Y)
                    {
                        MoveY = -this.Speed;
                        if (this.Position.Y + MoveY < this.TargetPosition.Y)
                        {
                            //Me.Position.Y = Me.TargetPosition.Y
                            Position = new Vector3(Position.X, TargetPosition.Y, Position.Z);
                            MoveY = 0f;
                        }
                    }

                    if (this.Position.Z < this.TargetPosition.Z)
                    {
                        MoveZ = this.Speed;
                        if (this.Position.Z + MoveZ > this.TargetPosition.Z)
                        {
                            //Me.Position.Z = Me.TargetPosition.Z
                            Position = new Vector3(Position.X, Position.Y, TargetPosition.Z);
                            MoveZ = 0f;
                        }
                    }

                    if (this.Position.Z > this.TargetPosition.Z)
                    {
                        MoveZ = -this.Speed;
                        if (this.Position.Z + MoveZ < this.TargetPosition.Z)
                        {
                            //Me.Position.Z = Me.TargetPosition.Z
                            Position = new Vector3(Position.X, Position.Y, TargetPosition.Z);
                            MoveZ = 0f;
                        }
                    }

                    this.Position = new Vector3(this.Position.X + MoveX, this.Position.Y + MoveY, this.Position.Z + MoveZ);
                }

                if (this.TargetYaw != this.Yaw)
                {
                    if (this.Yaw < this.TargetYaw)
                    {
                        this.Yaw += this.RotationSpeed;
                        if (this.Yaw > this.TargetYaw)
                        {
                            this.Yaw = this.TargetYaw;
                        }
                    }
                    if (this.Yaw > this.TargetYaw)
                    {
                        this.Yaw -= this.RotationSpeed;
                        if (this.Yaw < this.TargetYaw)
                        {
                            this.Yaw = this.TargetYaw;
                        }
                    }
                }

                if (this.TargetPitch != this.Pitch)
                {
                    if (this.Pitch < this.TargetPitch)
                    {
                        this.Pitch += this.RotationSpeed;
                        if (this.Pitch > this.TargetPitch)
                        {
                            this.Pitch = this.TargetPitch;
                        }
                    }
                    if (this.Pitch > this.TargetPitch)
                    {
                        this.Pitch -= this.RotationSpeed;
                        if (this.Pitch < this.TargetPitch)
                        {
                            this.Pitch = this.TargetPitch;
                        }
                    }
                }

                if (this.TargetSpeed != this.Speed)
                {
                    if (this.Speed < this.TargetSpeed)
                    {
                        this.Speed += 0.005f;
                        if (this.Speed > this.TargetSpeed)
                        {
                            this.Speed = this.TargetSpeed;
                        }
                    }
                    if (this.Speed > this.TargetSpeed)
                    {
                        this.Speed -= 0.005f;
                        if (this.Speed < this.TargetSpeed)
                        {
                            this.Speed = this.TargetSpeed;
                        }
                    }
                }

                if (this.TargetRotationSpeed != this.RotationSpeed)
                {
                    if (this.RotationSpeed < this.TargetRotationSpeed)
                    {
                        this.RotationSpeed += 0.005f;
                        if (this.RotationSpeed > this.TargetRotationSpeed)
                        {
                            this.RotationSpeed = this.TargetRotationSpeed;
                        }
                    }
                    if (this.RotationSpeed > this.TargetRotationSpeed)
                    {
                        this.RotationSpeed -= 0.005f;
                        if (this.RotationSpeed < this.TargetRotationSpeed)
                        {
                            this.RotationSpeed = this.TargetRotationSpeed;
                        }
                    }
                }
            }
        }

        #region "CameraStuff"

        public void UpdateFrustum()
        {
            Matrix rotation = Matrix.CreateRotationX(Pitch) * Matrix.CreateRotationY(Yaw);

            Vector3 fPosition = new Vector3(this.Position.X, this.Position.Y, this.Position.Z);
            fPosition += GetBattleMapOffset();

            Vector3 transformed = Vector3.Transform(new Vector3(0, 0, -1), rotation);
            Vector3 lookAt = fPosition + transformed;

            this.BoundingFrustum = new BoundingFrustum(Matrix.CreateLookAt(fPosition, lookAt, Vector3.Up) * Projection);
        }

        public void UpdateMatrices()
        {
            Matrix rotation = Matrix.CreateRotationX(Pitch) * Matrix.CreateRotationY(Yaw);

            Vector3 transformed = Vector3.Transform(new Vector3(0, 0, -1), rotation);
            Vector3 lookAt = new Vector3(this.Position.X, this.Position.Y, this.Position.Z) + GetBattleMapOffset() + transformed;

            View = Matrix.CreateLookAt(Position + GetBattleMapOffset(), lookAt, Vector3.Up);
        }

        public Ray CreateRay()
        {
            int centerX = Convert.ToInt32(Core.WindowSize.Width / 2);
            int centerY = Convert.ToInt32(Core.WindowSize.Height / 2);

            Vector3 nearSource = new Vector3(centerX, centerY, 0);
            Vector3 farSource = new Vector3(centerX, centerY, 1);

            Vector3 nearPoint = Core.GraphicsDevice.Viewport.Unproject(nearSource, Projection, View, Matrix.Identity);
            Vector3 farPoint = Core.GraphicsDevice.Viewport.Unproject(farSource, Projection, View, Matrix.Identity);

            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            return new Ray(nearPoint, direction);
        }

        private Vector3 GetBattleMapOffset()
        {
            Vector3 v = new Vector3(0);
            Screen s = Core.CurrentScreen;
            while (s.Identification != Screen.Identifications.BattleScreen && (s.PreScreen != null))
            {
                s = s.PreScreen;
            }
            if (s.Identification == Screen.Identifications.BattleScreen)
            {
                v = ((BaseBattleScreen)s).BattleMapOffset;
            }
            return v;
        }

        public Vector3 CPosition
        {
            get { return this.Position + GetBattleMapOffset(); }
        }

        public override void Turn(int turns)
        {
            throw new NotImplementedException();
        }

        public override void InstantTurn(int turns)
        {
            throw new NotImplementedException();
        }

        public override Vector3 GetForwardMovedPosition()
        {
            throw new NotImplementedException();
        }

        public override Vector3 GetMoveDirection()
        {
            throw new NotImplementedException();
        }

        public override void Move(float Steps)
        {
            throw new NotImplementedException();
        }

        public override void StopMovement()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}

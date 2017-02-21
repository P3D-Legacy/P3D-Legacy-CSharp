using System;

using Microsoft.Xna.Framework;

namespace P3D.Legacy.Core
{
    public class Camera
    {
        public BoundingFrustum BoundingFrustum;
        public Matrix View;
        public Matrix Projection;

        public Vector3 Position;
        public float Yaw;

        public float Pitch;
        protected Vector3 _plannedMovement = new Vector3(0f);

        protected bool _setPlannedMovement = false;

        public Vector3 PlannedMovement
        {
            get { return _plannedMovement; }
            set
            {
                _plannedMovement = value;
                _setPlannedMovement = (value != Vector3.Zero);
            }
        }

        public void AddToPlannedMovement(Vector3 v)
        {
            _plannedMovement += v;
        }


        public Ray Ray = new Ray();

        public bool Turning = false;
        public float Speed = 0.04f;

        public float RotationSpeed = 0.003f;
        public float FarPlane = 30;

        public float FOV = 45.0f;

        public string Name = "INHERITS";

        public Camera(string Name)
        {
            this.Name = Name;
        }

        public virtual void Update()
        {
            throw new NotImplementedException();
        }

        public virtual void Turn(int turns)
        {
            throw new NotImplementedException();
        }

        public virtual void InstantTurn(int turns)
        {
            throw new NotImplementedException();
        }

        public int GetFacingDirection()
        {
            if (Yaw <= MathHelper.Pi * 0.25f || Yaw > MathHelper.Pi * 1.75f)
            {
                return 0;
            }
            if (Yaw <= MathHelper.Pi * 0.75f && Yaw > MathHelper.Pi * 0.25f)
            {
                return 1;
            }
            if (Yaw <= MathHelper.Pi * 1.25f && Yaw > MathHelper.Pi * 0.75f)
            {
                return 2;
            }
            if (Yaw <= MathHelper.Pi * 1.75f && Yaw > MathHelper.Pi * 1.25f)
            {
                return 3;
            }
            return 0;
        }

        public virtual int GetPlayerFacingDirection()
        {
            return this.GetFacingDirection();
        }

        public virtual Vector3 GetForwardMovedPosition()
        {
            throw new NotImplementedException();
        }

        public virtual Vector3 GetMoveDirection()
        {
            throw new NotImplementedException();
        }

        public virtual void Move(float Steps)
        {
            throw new NotImplementedException();
        }

        public virtual void StopMovement()
        {
            throw new NotImplementedException();
        }

        public virtual bool IsMoving
        {
            get { return false; }
        }

        public void CreateNewProjection(float newFOV)
        {
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(newFOV), Core.GraphicsDevice.Viewport.AspectRatio, 0.01f, this.FarPlane);
            this.FOV = newFOV;
        }

    }
}

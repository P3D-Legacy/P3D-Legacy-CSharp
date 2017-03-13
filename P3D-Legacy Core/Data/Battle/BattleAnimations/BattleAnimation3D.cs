using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using P3D.Legacy.Core.Entities;
using P3D.Legacy.Core.Resources.Models;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.Battle.BattleAnimations
{
    public class BattleAnimation3D : Entity
    {
        public enum AnimationTypes
        {
            Nothing,
            Move,
            Transition,
            Size,
            Opacity,
            Rotation,
            Texture,
            Wait,
            ViewPokeBill,
            BillMove,
            Sound
        }

        public AnimationTypes AnimationType = AnimationTypes.Nothing;

        public bool CanRemove = false;
        public bool Ready = false;
        public float startDelay;

        public float endDelay;

        public BattleAnimation3D(Vector3 Position, Texture2D Texture, Vector3 Scale, float startDelay, float endDelay) :
            base(Position.X, Position.Y, Position.Z, "BattleAnimation", new[] {Texture}, new[] {0, 0}, false, 0, Scale, BaseModel.BillModel, 0, "", new Vector3(1f))
        {
            this.Visible = Visible;
            this.startDelay = startDelay;
            this.endDelay = endDelay;

            this.CreateWorldEveryFrame = true;
            this.DropUpdateUnlessDrawn = false;
        }

        public override void Update()
        {
            if (CanRemove == false)
            {
                if (Ready == true)
                {
                    if (endDelay > 0f)
                    {
                        endDelay -= 0.1f;

                        if (endDelay <= 0f)
                        {
                            endDelay = 0f;
                        }
                    }
                    else
                    {
                        CanRemove = true;
                    }
                }
                else
                {
                    if (startDelay > 0f)
                    {
                        startDelay -= 0.1f;

                        if (startDelay <= 0f)
                        {
                            startDelay = 0f;
                        }
                    }
                    else
                    {
                        DoActionActive();
                    }
                }
            }

            base.Update();
        }

        public override void UpdateEntity()
        {
            if (this.Rotation.Y != Screen.Camera.Yaw)
            {
                this.Rotation.Y = Screen.Camera.Yaw;
            }

            DoActionUpdate();

            base.UpdateEntity();
        }

        public virtual void DoActionUpdate()
        {
            //Insert code in Inherits class for every update here.
        }

        public virtual void DoActionActive()
        {
            //Insert code in Inherits class here.
        }

        public override void Render()
        {
            if (this.startDelay <= 0f)
            {
                Draw(this.Model, this.Textures, true);
            }
        }

    }
}
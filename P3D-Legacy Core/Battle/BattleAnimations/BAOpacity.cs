using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace P3D.Legacy.Core.Battle.BattleAnimations
{
    public class BAOpacity : BattleAnimation3D
    {

        public float TransitionSpeed = 0.01f;
        public bool FadeIn = false;

        public float EndState = 0f;
        public BAOpacity(Vector3 Position, Texture2D Texture, Vector3 Scale, float TransitionSpeed, bool FadeIn, float EndState, float startDelay, float endDelay) : base(Position, Texture, Scale, startDelay, endDelay)
        {

            this.EndState = EndState;
            this.FadeIn = FadeIn;
            this.TransitionSpeed = TransitionSpeed;

            this.AnimationType = AnimationTypes.Transition;
        }

        public override void DoActionActive()
        {
            if (this.FadeIn == true)
            {
                if (this.EndState > this.Opacity)
                {
                    this.Opacity += this.TransitionSpeed;
                    if (this.Opacity >= this.EndState)
                    {
                        this.Opacity = this.EndState;
                    }
                }
            }
            else
            {
                if (this.EndState < this.Opacity)
                {
                    this.Opacity -= this.TransitionSpeed;
                    if (this.Opacity <= this.EndState)
                    {
                        this.Opacity = this.EndState;
                    }
                }
            }

            if (this.Opacity == this.EndState)
            {
                this.Ready = true;
            }
        }

    }
}

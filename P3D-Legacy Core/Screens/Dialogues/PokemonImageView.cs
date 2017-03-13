using System;
using System.Drawing;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Input;
using P3D.Legacy.Core.Pokemon;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Screens.GUI;

namespace P3D.Legacy.Core.Dialogues
{
    public class PokemonImageView
    {
        public float Delay = 0f;
        Texture2D Texture;

        bool front = true;
        public bool Showing { get; set; }

        public void Show(int ID, bool shiny, bool front)
        {
            var p = BasePokemon.GetPokemonByID(ID);
            p.PlayCry();

            this.Delay = 8f;
            this.Showing = true;

            p.IsShiny = shiny;

            this.Texture = p.GetTexture(front);
        }

        public void Show(BasePokemon Pokemon, bool front)
        {
            var p = Pokemon;
            p.PlayCry();

            this.Delay = 8f;
            this.Showing = true;

            this.Texture = p.GetTexture(front);
        }

        public void Show(Texture2D Texture)
        {
            this.Texture = Texture;

            this.Delay = 8f;
            this.Showing = true;
        }

        public void Update()
        {
            if (Delay > 0f)
            {
                Delay -= 0.1f;

                if (Delay <= 0f)
                {
                    Delay = 0f;
                }
            }
            else if (Delay == 0f)
            {
                if (Controls.Accept() == true || Controls.Dismiss() == true)
                {
                    this.Showing = false;
                }
            }
        }

        public void Draw()
        {
            if (this.Showing == true)
            {
                Vector2 p = Core.GetMiddlePosition(new Size(320, 320));

                Canvas.DrawImageBorder(TextureManager.GetTexture("GUI\\Menus\\Menu", new Microsoft.Xna.Framework.Rectangle(0, 0, 48, 48), ""), 2, new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(p.X), Convert.ToInt32(p.Y), 320, 320));

                Core.SpriteBatch.Draw(this.Texture, new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(p.X) + 36, Convert.ToInt32(p.Y) + 20, 280, 280), Microsoft.Xna.Framework.Color.White);

                if (this.Delay == 0f)
                {
                    Core.SpriteBatch.Draw(TextureManager.GetTexture("GUI\\Overworld\\TextBox"), new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(p.X) + 144 + 160, Convert.ToInt32(p.Y) + 144 + 160 + 32, 16, 16), new Microsoft.Xna.Framework.Rectangle(0, 48, 16, 16), Microsoft.Xna.Framework.Color.White);
                }
            }
        }
    }
}

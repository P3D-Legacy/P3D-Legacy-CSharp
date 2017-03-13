using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Data;
using P3D.Legacy.Core.Entities;
using P3D.Legacy.Core.Input;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Sound;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.Dialogues
{
    /*
    public class ChooseBox
    {
        public int Index { get; set; } = 0;
        public string[] Options { get; set; }


        float _positionY = 0;
        public bool ReadyForResult { get; set; }
        public int Result { get; set; }
        public int ResultId = 0;

        public bool ActionScript = false;
        public static int CancelIndex = -1;

        public FontContainer TextFont { get; set; }
        public bool Showing { get; set; }


        public bool DoDelegate = false;

        Action<Int32> _subs;

        public Entity[] UpdateEntities;

        public void Show(string[] options, Action<Int32> doSubs)
        {
            ResultId = 0;
            Options = options;
            Index = 0;
            ReadyForResult = false;
            Showing = true;
            _subs = doSubs;
            ActionScript = false;
            DoDelegate = true;
            TextFont = FontManager.GetFontContainer("textfont");

            SetupOptions();
        }
        public void Show(string[] options, int id, bool actionScript)
        {
            ResultId = id;
            Options = options;
            Index = 0;
            ReadyForResult = false;
            Showing = true;
            ActionScript = actionScript;
            DoDelegate = false;
            TextFont = FontManager.GetFontContainer("textfont");

            SetupOptions();
        }
        public void Show(string[] options, int id, Entity[] updateEntities)
        {
            ResultId = id;
            Options = options;
            Index = 0;
            ReadyForResult = false;
            Showing = true;
            UpdateEntities = updateEntities;
            ActionScript = false;
            DoDelegate = false;
            TextFont = FontManager.GetFontContainer("textfont");

            SetupOptions();
        }

        private void SetupOptions()
        {
            for (var i = 0; i <= Options.Length - 1; i++)
                Options[i] = Options[i].Replace("<playername>", Core.Player.Name);
        }

        public int GetResult(int id) => ReadyForResult ? (ResultId == id ? Result : -1) : -1;

        public void Update() => Update(true);

        public void Update(bool raiseClickEvent)
        {
            if (Showing)
            {
                if (Controls.Down(true, true, true))
                {
                    Index += 1;
                }
                if (Controls.Up(true, true, true))
                {
                    Index -= 1;
                }

                if (Index < 0)
                {
                    Index = Options.Length - 1;
                }
                if (Index == Options.Length)
                {
                    Index = 0;
                }
                if (raiseClickEvent)
                {
                    if (Controls.Accept())
                    {
                        PlayClickSound();
                        Result = Index;
                        HandleResult();
                    }
                    if (Controls.Dismiss() && CancelIndex > -1)
                    {
                        PlayClickSound();
                        Result = CancelIndex;
                        HandleResult();
                    }
                }
            }
        }

        private void PlayClickSound()
        {
            if (Screen.TextBox.Showing == false)
                SoundManager.PlaySound("select");
        }

        private void HandleResult()
        {
            CancelIndex = -1;
            ReadyForResult = true;
            Showing = false;
            if (DoDelegate)
            {
                _subs(Result);
            }
            else
            {
                if (Core.CurrentScreen.Identification == Screen.Identifications.OverworldScreen)
                {
                    if (ActionScript)
                    {
                        BaseOverworldScreen c = (BaseOverworldScreen)Core.CurrentScreen;
                        c.ActionScript.Switch(this.Options(Result));
                    }
                    else
                    {
                        foreach (Entity entity in UpdateEntities)
                        {
                            entity.ResultFunction(Result);
                        }
                    }
                }
            }
        }

        public void Draw(Vector2 position)
        {
            if (Showing)
            {
                var with1 = Core.SpriteBatch;
                with1.Draw(TextureManager.GetTexture("GUI\\Overworld\\ChooseBox"), new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), 288, 48), new Rectangle(0, 0, 96, 16), Color.White);
                for (var i = 0; i <= Options.Length - 2; i++)
                {
                    with1.Draw(TextureManager.GetTexture("GUI\\Overworld\\ChooseBox"), new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y) + 48 + i * 48, 288, 48), new Rectangle(0, 16, 96, 16), Color.White);
                }
                with1.Draw(TextureManager.GetTexture("GUI\\Overworld\\ChooseBox"), new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y) + 96 + (Options.Length - 2) * 48, 288, 48), new Rectangle(0, 32, 96, 16), Color.White);
                for (var i = 0; i <= Options.Length - 1; i++)
                {
                    float m = 1f;
                    switch (TextFont.FontName.ToLower())
                    {
                        case "textfont":
                        case "braille":
                            m = 2f;
                            break;
                    }

                    with1.DrawString(TextFont.SpriteFont, Options[i].Replace("[POKE]", "Poké"), new Vector2(Convert.ToInt32(position.X + 40), Convert.ToInt32(position.Y) + 32 + i * 48), Color.Black, 0f, Vector2.Zero, m, SpriteEffects.None, 0f);
                }
                with1.Draw(TextureManager.GetTexture("GUI\\Overworld\\ChooseBox"), new Rectangle(Convert.ToInt32(position.X + 20), Convert.ToInt32(position.Y) + 36 + Index * 48, 10, 20), new Rectangle(96, 0, 3, 6), Color.White);
            }
        }

        public void Draw()
        {
            if (Showing)
            {
                Vector2 position = new Vector2(Convert.ToInt32(Core.WindowSize.Width / 2) - 48, Core.WindowSize.Height - 160f - 96f - (Options.Length - 1) * 48);
                Draw(position);
            }
        }

    }
    */
}

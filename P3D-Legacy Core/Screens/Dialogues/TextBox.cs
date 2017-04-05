using System;
using System.Globalization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using P3D.Legacy.Core.Data;
using P3D.Legacy.Core.Entities;
using P3D.Legacy.Core.Input;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Managers;
using P3D.Legacy.Core.Resources.Managers.Sound;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.Dialogues
{
    public class TextBox
    {
        public static readonly Color DefaultColor = new Color(16, 24, 32);

        public static readonly Color PlayerColor = new Color(0, 0, 180);
        public static int TextSpeed = 0;

        int _currentChar = 0;
        int _currentLine = 0;
        int _fullLines = 0;
        bool _through = false;

        bool _clearNextLine = false;

        string[] _showText = new string[3];
        float _delay = 0.2f;

        bool _doReDelay = true;

        Entity[] _entities;

        Action<Int32> _resultFunction = null;

        public FontContainer TextFont { get; set; } = FontManager.GetFontContainer("textfont");

        public float PositionY { get; set; } = 0;
        public Color TextColor { get; set; } = new Color(16, 24, 33);
        public bool Showing { get; set; }
        public float ReDelay { get; set; } = 1.5f;
        public string Text { get; set; }
        public bool CanProceed { get; set; } = true;
        public Action FollowUp { get; set; }

        public void Show(string text, Action<Int32> resultFunction, bool doReDelay, bool checkDelay, Color textColor)
        {
            if (ReDelay == 0f || !checkDelay)
            {
                if (!Showing)
                {
                    PositionY = Core.WindowSize.Height;
                    Showing = true;
                }
                this._doReDelay = doReDelay;
                this.Text = text;
                this._resultFunction = resultFunction;
                this.TextColor = textColor;
                _showText[0] = "";
                _showText[1] = "";
                _through = false;
                _currentLine = 0;
                _currentChar = 0;
                _delay = 0.2f;
                _clearNextLine = false;

                FormatText();
            }
        }
        public void Show(string text, Entity[] entities, bool doReDelay, bool checkDelay, Color textColor)
        {
            if (ReDelay == 0f || !checkDelay)
            {
                if (!Showing)
                {
                    PositionY = Core.WindowSize.Height;
                    Showing = true;
                }
                this._doReDelay = doReDelay;
                this.Text = text;
                this._entities = entities;
                this.TextColor = textColor;
                _showText[0] = "";
                _showText[1] = "";
                _through = false;
                _currentLine = 0;
                _currentChar = 0;
                _delay = 0.2f;
                _clearNextLine = false;

                FormatText();
            }
        }
        public void Show(string text, Entity[] entities, bool doReDelay, bool checkDelay)
        {
            Show(text, entities, doReDelay, checkDelay, TextColor);
        }
        public void Show(string text, Entity[] entities, bool doReDelay)
        {
            Show(text, entities, doReDelay, true);
        }
        public void Show(string text, Entity[] entities)
        {
            Show(text, entities, true);
        }
        public void Show(string text)
        {
            Show(text, new Entity[] { }, false, false);
        }

        public void Hide()
        {
            Showing = false;
            if (_doReDelay)
            {
                ReDelay = 1f;
            }
        }

        private void FormatText()
        {
            string[] tokenSearchBuffer = Text.Split(Convert.ToChar("<"));
            int tokenEndIdx = 0;
            string validToken = "";
            Localization.Token token = null;
            foreach (string possibleToken in tokenSearchBuffer)
            {
                tokenEndIdx = possibleToken.IndexOf(">", StringComparison.Ordinal);
                if (tokenEndIdx != -1)
                {
                    validToken = possibleToken.Substring(0, tokenEndIdx);
                    if (Localization.LocalizationTokens.ContainsKey(validToken))
                    {
                        if (Localization.LocalizationTokens.TryGetValue(validToken, out token))
                        {
                            Text = Text.Replace("<" + validToken + ">", token.TokenContent);
                        }
                    }
                }
            }

            Text = Text.Replace("<playername>", Core.Player.Name);
            Text = Text.Replace("<rivalname>", Core.Player.RivalName);

            Text = Text.Replace("[POKE]", "Poké");
            Text = Text.Replace("[POKEMON]", "Pokémon");
        }

        public void Update()
        {
            if (Showing)
            {
                ResetCursor();
                if (PositionY <= Core.WindowSize.Height - 160f)
                {
                    if (!_through)
                    {
                        if (Text.Length > _currentChar)
                        {
                            if (_delay <= 0f)
                            {
                                if (Text[_currentChar].ToString(NumberFormatInfo.InvariantInfo) == "\\")
                                {
                                    if (Text.Length > _currentChar + 1)
                                    {
                                        _showText[_currentLine] += Text[_currentChar + 1];

                                        _currentChar += 2;
                                    }
                                    else
                                    {
                                        _currentChar += 1;
                                    }
                                }
                                else
                                {
                                    switch (Text[_currentChar])
                                    {
                                        case '~':
                                            if (_currentLine == 1)
                                            {
                                                _through = true;
                                            }
                                            else
                                            {
                                                _currentLine += 1;
                                            }
                                            break;
                                        case '*':
                                            _currentLine = 0;
                                            _clearNextLine = true;
                                            _through = true;
                                            break;
                                        case '%':
                                            ProcessChooseBox();
                                            break;
                                        default:
                                            _showText[_currentLine] += Text[_currentChar];
                                            break;
                                    }

                                    _currentChar += 1;
                                }

                                if (KeyBoardHandler.KeyDown(Core.KeyBindings.Enter1) || KeyBoardHandler.KeyDown(Core.KeyBindings.Enter2) || MouseHandler.ButtonDown(MouseHandler.MouseButtons.LeftButton) == true || ControllerHandler.ButtonDown(Buttons.A) == true || ControllerHandler.ButtonDown(Buttons.B) == true)
                                {
                                    _delay = 0f;
                                }
                                else
                                {
                                    _delay = GetTextSpeed();
                                }
                            }
                            else
                            {
                                _delay -= 0.1f;
                            }
                        }
                        else
                        {
                            _through = true;
                        }
                    }
                    else
                    {
                        if (Controls.Accept() || Controls.Dismiss())
                        {
                            SoundEffectManager.PlaySound("select");
                            if (Text.Length <= _currentChar)
                            {
                                if (CanProceed == true)
                                {
                                    Showing = false;
                                    ResetCursor();

                                    if ((FollowUp != null))
                                    {
                                        FollowUp.Invoke();
                                        FollowUp = null;
                                    }

                                    TextFont = FontManager.GetFontContainer("textfont");
                                    TextColor = DefaultColor;
                                    if (_doReDelay == true)
                                    {
                                        ReDelay = 1f;
                                    }
                                }
                            }
                            else
                            {
                                if (_clearNextLine == true)
                                {
                                    _showText[0] = "";
                                }
                                else
                                {
                                    _showText[0] = _showText[1];
                                }
                                _showText[1] = "";
                                _through = false;
                                _clearNextLine = false;
                            }
                        }
                    }
                }
                else
                {
                    float ySpeed = 3.5f;
                    switch (TextSpeed)
                    {
                        case 1:
                            ySpeed = 3.5f;
                            break;
                        case 2:
                            ySpeed = 4.5f;
                            break;
                        case 3:
                            ySpeed = 6.5f;
                            break;
                    }
                    PositionY -= ySpeed;
                }
            }
            else
            {
                if (ReDelay > 0f)
                {
                    ReDelay -= 0.1f;
                    if (ReDelay <= 0f)
                    {
                        ReDelay = 0f;
                    }
                }
            }
        }

        private void ResetCursor()
        {
            if (Core.CurrentScreen.Identification == Screen.Identifications.OverworldScreen)
            {
                BaseOverworldCamera c = (BaseOverworldCamera)Screen.Camera;
                Mouse.SetPosition(Convert.ToInt32(Core.WindowSize.Width / 2), Convert.ToInt32(Core.WindowSize.Height / 2));
                c.OldX = Convert.ToInt32(Core.WindowSize.Width / 2);
                c.OldY = Convert.ToInt32(Core.WindowSize.Height / 2);
            }
        }

        public void Draw()
        {
            if (Showing == true)
            {
                var with1 = Core.SpriteBatch;
                with1.Draw(TextureManager.GetTexture("GUI|Overworld|TextBox"), new Rectangle(Convert.ToInt32(Core.WindowSize.Width / 2) - 240, Convert.ToInt32(PositionY), 480, 144), new Rectangle(0, 0, 160, 48), Color.White);

                float m = 1f;
                switch (TextFont.FontName.ToLower())
                {
                    case "textfont":
                    case "braille":
                        m = 2f;
                        break;
                }

                with1.DrawString(TextFont.SpriteFont, this._showText[0], new Vector2(Convert.ToInt32(Core.WindowSize.Width / 2) - 210, Convert.ToInt32(PositionY) + 40), TextColor, 0f, Vector2.Zero, m, SpriteEffects.None, 0f);
                with1.DrawString(TextFont.SpriteFont, this._showText[1], new Vector2(Convert.ToInt32(Core.WindowSize.Width / 2) - 210, Convert.ToInt32(PositionY) + 75), TextColor, 0f, Vector2.Zero, m, SpriteEffects.None, 0f);

                if (CanProceed == true && _through == true)
                {
                    with1.Draw(TextureManager.GetTexture("GUI|Overworld|TextBox"), new Rectangle(Convert.ToInt32(Core.WindowSize.Width / 2) + 192, Convert.ToInt32(PositionY) + 128, 16, 16), new Rectangle(0, 48, 16, 16), Color.White);
                }
            }
        }

        private void ProcessChooseBox()
        {
            string splitText = Text.Remove(0, _currentChar + 1);
            splitText = splitText.Remove(splitText.IndexOf("%", StringComparison.Ordinal));
            _through = true;
            string[] options = splitText.Split(Convert.ToChar("|"));
            Text = Text.Remove(_currentChar, splitText.Length + 1);
            if (_entities == null && (_resultFunction != null) || _entities.Length == 0 && (_resultFunction != null))
            {
                Screen.ChooseBox.Show(options, _resultFunction);
            }
            else
            {
                Screen.ChooseBox.Show(options, 0, _entities);
            }
            Screen.ChooseBox.TextFont = TextFont;
        }

        private float GetTextSpeed()
        {
            switch (TextSpeed)
            {
                case 1:
                    return 0.3f;
                case 2:
                    return 0.2f;
                case 3:
                    return 0.1f;
            }
            return 0.2f;
        }

    }
}

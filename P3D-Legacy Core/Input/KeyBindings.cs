using System;
using System.Linq;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace P3D.Legacy.Core.Input
{
    public class KeyBindings
    {
        private static float _holdDelay = 3f;
        private static Keys _holdKey = Keys.A;

        /// <summary>
        /// Converts the name of a key to the actual Key class.
        /// </summary>
        /// <param name="keyStr">The key name to convert.</param>
        /// <remarks>The default is Keys.None.</remarks>
        public static Keys GetKey(string keyStr) => Enum.GetValues(typeof(Keys)).Cast<Keys>().FirstOrDefault(k => string.Equals(k.ToString(), keyStr, StringComparison.InvariantCultureIgnoreCase));

        /// <summary>
        /// Returns the name of a key.
        /// </summary>
        /// <param name="key">The key to get the name for.</param>
        /// <remarks>Returns String.Empty by default.</remarks>
        public static string GetKeyName(Keys key) => key.ToString();


        /// <summary>
        /// Gets keyboard inputs.
        /// </summary>
        /// <param name="whiteKeys">All keys that allow a valid return. If the list is empty, all keys are allowed by default.</param>
        /// <param name="blackKeys">All keys that are not allowed for a valid return. If this list is empty, all keys are allowed by default.</param>
        /// <param name="text">The current text that this function adds text to.</param>
        /// <param name="maxLength">The maximum length of the text. -1 means infinite length.</param>
        /// <param name="triggerShift">Checks if the Shift variant of a key gets considered.</param>
        /// <param name="triggerAlt">Checks if the Alt variant of a key gets considered.</param>
        public static string GetInput(Keys[] whiteKeys, Keys[] blackKeys, ref string text, int maxLength = -1, bool triggerShift = true, bool triggerAlt = true)
        {
            var keys = KeyBoardHandler.GetPressedKeys;
            foreach (var key in keys)
            {
                if (key == Keys.V && KeyBoardHandler.KeyPressed(Keys.V) && (KeyBoardHandler.KeyDown(Keys.LeftControl) || KeyBoardHandler.KeyDown(Keys.RightControl)))
                {
                    // TODO
                    // OSX clipboard http://stackoverflow.com/questions/28611112/mono-clipboard-fix
                    if (Clipboard.ContainsText())
                        text += Clipboard.GetText();
                }
                else
                {
                    if (key != Keys.Back)
                    {
                        if (!KeyBlocked(whiteKeys, blackKeys, key))
                        {
                            char c;
                            if (TryConvertKeyboardInput(key, KeyBoardHandler.KeyDown(Keys.LeftShift) || KeyBoardHandler.KeyDown(Keys.RightShift), out c))
                            {
                                if (_holdDelay <= 0f && _holdKey == key)
                                    text += c.ToString();
                                else
                                {
                                    if (KeyBoardHandler.KeyPressed(key))
                                    {
                                        text += c.ToString();

                                        _holdKey = key;
                                        _holdDelay = 3f;
                                    }
                                }
                            }
                        }
                    }
                    if (key == Keys.Back && !KeyBlocked(whiteKeys, blackKeys, Keys.Back))
                    {
                        if (_holdDelay <= 0f && _holdKey == key)
                        {
                            if (text.Length > 0)
                                text = text.Remove(text.Length - 1, 1);
                        }
                        else
                        {
                            if (KeyBoardHandler.KeyPressed(key))
                            {
                                if (text.Length > 0)
                                    text = text.Remove(text.Length - 1, 1);

                                _holdKey = key;
                                _holdDelay = 3f;
                            }
                        }
                    }
                }
            }

            if (KeyBoardHandler.KeyUp(_holdKey))
                _holdDelay = 3f;
            else
            {
                _holdDelay -= 0.1f;
                if (_holdDelay <= 0f)
                    _holdDelay = 0f;
            }

            if (maxLength > -1)
                while (text.Length > maxLength)
                    text = text.Remove(text.Length - 1, 1);

            return text;
        }

        /// <summary>
        /// Checks if the Key used is blocked by either the key whitelist or key blacklist.
        /// </summary>
        /// <param name="whiteKeys">The key whitelist.</param>
        /// <param name="blackKeys">The key blacklist.</param>
        /// <param name="key">The key to be checked.</param>
        private static bool KeyBlocked(Keys[] whiteKeys, Keys[] blackKeys, Keys key)
        {
            var mapWhite = whiteKeys.Length > 0;
            var mapBlack = blackKeys.Length > 0;

            //Check if key is whitelisted:
            return !(!mapWhite || (mapWhite && whiteKeys.Contains(key))) || !(!mapBlack || (mapBlack && !blackKeys.Contains(key)));
        }

        /// <summary>
        /// Gets default text input.
        /// </summary>
        /// <param name="text">The text to modify.</param>
        /// <param name="maxLength">The maximum length of the text. -1 means infinite length.</param>
        /// <param name="triggerShift">Checks if the Shift variant of a key gets considered.</param>
        /// <param name="triggerAlt">Checks if the Alt variant of a key gets considered.</param>
        public static string GetInput(ref string text, int maxLength = -1, bool triggerShift = true, bool triggerAlt = true)
        {
            return GetInput(new Keys[0], new[]
            {
                Keys.Enter,
                Keys.Up,
                Keys.Down,
                Keys.Left,
                Keys.Right,
                Keys.Tab,
                Keys.Delete,
                Keys.Home,
                Keys.End,
                Keys.Escape
            }, ref text, maxLength, triggerShift, triggerAlt);
        }

        /// <summary>
        /// Gets input for names.
        /// </summary>
        /// <param name="text">The text to modify.</param>
        /// <param name="maxLength">The maximum length of the text. -1 means infinite length.</param>
        /// <param name="triggerShift">Checks if the Shift variant of a key gets considered.</param>
        /// <param name="triggerAlt">Checks if the Alt variant of a key gets considered.</param>
        public static string GetNameInput(ref string text, int maxLength = -1, bool triggerShift = true, bool triggerAlt = true)
        {
            return GetInput(new[]
            {
                Keys.NumPad0,
                Keys.NumPad1,
                Keys.NumPad2,
                Keys.NumPad3,
                Keys.NumPad4,
                Keys.NumPad5,
                Keys.NumPad6,
                Keys.NumPad7,
                Keys.NumPad8,
                Keys.NumPad9,
                Keys.Space,
                Keys.D1,
                Keys.D2,
                Keys.D3,
                Keys.D4,
                Keys.D5,
                Keys.D6,
                Keys.D7,
                Keys.D8,
                Keys.D9,
                Keys.D0,
                Keys.Back,
                Keys.A,
                Keys.B,
                Keys.C,
                Keys.D,
                Keys.E,
                Keys.F,
                Keys.G,
                Keys.H,
                Keys.I,
                Keys.J,
                Keys.K,
                Keys.L,
                Keys.M,
                Keys.N,
                Keys.O,
                Keys.P,
                Keys.Q,
                Keys.R,
                Keys.S,
                Keys.T,
                Keys.U,
                Keys.V,
                Keys.W,
                Keys.X,
                Keys.Y,
                Keys.Z
            }, new Keys[0], ref text, maxLength, triggerShift, triggerAlt);
        }



         /// <summary>
        /// Tries to convert keyboard input to characters and prevents repeatedly returning the 
        /// same character if a key was pressed last frame, but not yet unpressed this frame.
        /// </summary>
        /// <param name="keyboard">The current KeyboardState</param>
        /// <param name="oldKeyboard">The KeyboardState of the previous frame</param>
        /// <param name="key">When this method returns, contains the correct character if conversion succeeded.
        /// Else contains the null, (000), character.</param>
        /// <returns>True if conversion was successful</returns>
        public static bool TryConvertKeyboardInput(Keys keys, bool shift, out char key)
        {
            switch (keys)
            {
                //Alphabet keys
                case Keys.A: if (shift) { key = 'A'; } else { key = 'a'; } return true;
                case Keys.B: if (shift) { key = 'B'; } else { key = 'b'; } return true;
                case Keys.C: if (shift) { key = 'C'; } else { key = 'c'; } return true;
                case Keys.D: if (shift) { key = 'D'; } else { key = 'd'; } return true;
                case Keys.E: if (shift) { key = 'E'; } else { key = 'e'; } return true;
                case Keys.F: if (shift) { key = 'F'; } else { key = 'f'; } return true;
                case Keys.G: if (shift) { key = 'G'; } else { key = 'g'; } return true;
                case Keys.H: if (shift) { key = 'H'; } else { key = 'h'; } return true;
                case Keys.I: if (shift) { key = 'I'; } else { key = 'i'; } return true;
                case Keys.J: if (shift) { key = 'J'; } else { key = 'j'; } return true;
                case Keys.K: if (shift) { key = 'K'; } else { key = 'k'; } return true;
                case Keys.L: if (shift) { key = 'L'; } else { key = 'l'; } return true;
                case Keys.M: if (shift) { key = 'M'; } else { key = 'm'; } return true;
                case Keys.N: if (shift) { key = 'N'; } else { key = 'n'; } return true;
                case Keys.O: if (shift) { key = 'O'; } else { key = 'o'; } return true;
                case Keys.P: if (shift) { key = 'P'; } else { key = 'p'; } return true;
                case Keys.Q: if (shift) { key = 'Q'; } else { key = 'q'; } return true;
                case Keys.R: if (shift) { key = 'R'; } else { key = 'r'; } return true;
                case Keys.S: if (shift) { key = 'S'; } else { key = 's'; } return true;
                case Keys.T: if (shift) { key = 'T'; } else { key = 't'; } return true;
                case Keys.U: if (shift) { key = 'U'; } else { key = 'u'; } return true;
                case Keys.V: if (shift) { key = 'V'; } else { key = 'v'; } return true;
                case Keys.W: if (shift) { key = 'W'; } else { key = 'w'; } return true;
                case Keys.X: if (shift) { key = 'X'; } else { key = 'x'; } return true;
                case Keys.Y: if (shift) { key = 'Y'; } else { key = 'y'; } return true;
                case Keys.Z: if (shift) { key = 'Z'; } else { key = 'z'; } return true;

                //Decimal keys
                case Keys.D0: if (shift) { key = ')'; } else { key = '0'; } return true;
                case Keys.D1: if (shift) { key = '!'; } else { key = '1'; } return true;
                case Keys.D2: if (shift) { key = '@'; } else { key = '2'; } return true;
                case Keys.D3: if (shift) { key = '#'; } else { key = '3'; } return true;
                case Keys.D4: if (shift) { key = '$'; } else { key = '4'; } return true;
                case Keys.D5: if (shift) { key = '%'; } else { key = '5'; } return true;
                case Keys.D6: if (shift) { key = '^'; } else { key = '6'; } return true;
                case Keys.D7: if (shift) { key = '&'; } else { key = '7'; } return true;
                case Keys.D8: if (shift) { key = '*'; } else { key = '8'; } return true;
                case Keys.D9: if (shift) { key = '('; } else { key = '9'; } return true;

                //Decimal numpad keys
                case Keys.NumPad0: key = '0'; return true;
                case Keys.NumPad1: key = '1'; return true;
                case Keys.NumPad2: key = '2'; return true;
                case Keys.NumPad3: key = '3'; return true;
                case Keys.NumPad4: key = '4'; return true;
                case Keys.NumPad5: key = '5'; return true;
                case Keys.NumPad6: key = '6'; return true;
                case Keys.NumPad7: key = '7'; return true;
                case Keys.NumPad8: key = '8'; return true;
                case Keys.NumPad9: key = '9'; return true;

                //Special keys
                case Keys.OemTilde: if (shift) { key = '~'; } else { key = '`'; } return true;
                case Keys.OemSemicolon: if (shift) { key = ':'; } else { key = ';'; } return true;
                case Keys.OemQuotes: if (shift) { key = '"'; } else { key = '\''; } return true;
                case Keys.OemQuestion: if (shift) { key = '?'; } else { key = '/'; } return true;
                case Keys.OemPlus: if (shift) { key = '+'; } else { key = '='; } return true;
                case Keys.OemPipe: if (shift) { key = '|'; } else { key = '\\'; } return true;
                case Keys.OemPeriod: if (shift) { key = '>'; } else { key = '.'; } return true;
                case Keys.OemOpenBrackets: if (shift) { key = '{'; } else { key = '['; } return true;
                case Keys.OemCloseBrackets: if (shift) { key = '}'; } else { key = ']'; } return true;
                case Keys.OemMinus: if (shift) { key = '_'; } else { key = '-'; } return true;
                case Keys.OemComma: if (shift) { key = '<'; } else { key = ','; } return true;
                case Keys.Space: key = ' '; return true;
            }

            key = (char)0;
            return false;           
        }
    }
}
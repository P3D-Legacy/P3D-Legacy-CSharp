using System;
using System.IO;
using Microsoft.Xna.Framework;

using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Managers;

namespace P3D.Legacy.Core.World
{
    /// <summary>
    /// The sign displaying the current location in the world.
    /// </summary>
    public class RouteSign
    {
        private float _positionY = -60;
        private bool _show;
        private float _delay;
        private string _text = "";

        /// <summary>
        /// Sets the values of the RouteSign and displays it on the screen.
        /// </summary>
        public void Setup(string newText)
        {
            //Only if the text is different from last time the RouteSign showed up, display the RouteSign.
            if (!string.Equals(newText, _text, StringComparison.CurrentCultureIgnoreCase))
            {
                _show = true;
                _delay = 13f;
                _text = newText;
            }
        }

        /// <summary>
        /// Hides the RouteSign.
        /// </summary>
        public void Hide() => _show = false;

        /// <summary>
        /// Update the RouteSign.
        /// </summary>
        public void Update()
        {
            if (_delay > 0f)
            {
                if (_positionY < 5f)
                    _positionY += 1.2f;

                _delay -= 0.1f;
                if (_delay <= 0f)
                    _delay = 0f;
            }
            else
            {
                if (_positionY > -60)
                {
                    _positionY -= 1.2f;
                    if (_positionY <= -60)
                        _show = false;
                }
            }
        }

        /// <summary>
        /// Renders the RouteSign.
        /// </summary>
        public void Draw()
        {
            if (_show)
            {
                string placeString = Localization.GetString("Places_" + _text, _text);

                //Get the point to render the text to.
                int pX = Convert.ToInt32(316 / 2) - Convert.ToInt32(FontManager.InGameFont.MeasureString(placeString).X / 2);

                Core.SpriteBatch.DrawInterface(TextureManager.GetTexture(Path.Combine("GUI", "Overworld", "Sign")), new Rectangle(5, Convert.ToInt32(_positionY), 316, 60), Color.White);
                //Draw the sign image.
                Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, placeString, new Vector2(pX, Convert.ToInt32(_positionY) + 13), Color.Black);
                //Draw the text on the sign.
            }
        }

    }
}

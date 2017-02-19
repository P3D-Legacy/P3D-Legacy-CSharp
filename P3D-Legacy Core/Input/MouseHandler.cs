using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace P3D.Legacy.Core.Input
{
    public static class MouseHandler
    {
        public enum MouseButtons
        {
            LeftButton,
            MiddleButton,
            RightButton
        }

        private static MouseState _oldState;
        private static MouseState _newState;

        public static MouseState MouseState
        {
            get { return _newState; }
            set { _newState = value; }
        }

        public static void Update()
        {
            _oldState = _newState;
            _newState = Mouse.GetState();
        }

        public static bool ButtonPressed(MouseButtons button)
        {
            if (WindowContainsMouse && Core.GameInstance.IsActive)
            {
                switch (button)
                {
                    case MouseButtons.LeftButton:
                        if (_oldState.LeftButton == ButtonState.Released && _newState.LeftButton == ButtonState.Pressed)
                            return true;
                        break;

                    case MouseButtons.MiddleButton:
                        if (_oldState.MiddleButton == ButtonState.Released && _newState.MiddleButton == ButtonState.Pressed)
                            return true;
                        break;

                    case MouseButtons.RightButton:
                        if (_oldState.RightButton == ButtonState.Released && _newState.RightButton == ButtonState.Pressed)
                            return true;
                        break;
                }
            }
            return false;
        }

        public static bool ButtonDown(MouseButtons button)
        {
            if (WindowContainsMouse && Core.GameInstance.IsActive)
            {
                switch (button)
                {
                    case MouseButtons.LeftButton:
                        return _newState.LeftButton == ButtonState.Pressed;

                    case MouseButtons.MiddleButton:
                        return _newState.MiddleButton == ButtonState.Pressed;

                    case MouseButtons.RightButton:
                        return _newState.RightButton == ButtonState.Pressed;
                }
            }
            return false;
        }

        public static bool ButtonUp(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LeftButton:
                    return _newState.LeftButton == ButtonState.Released;
                case MouseButtons.MiddleButton:
                    return _newState.MiddleButton == ButtonState.Released;
                case MouseButtons.RightButton:
                    return _newState.RightButton == ButtonState.Released;
            }
            return false;
        }

        public static bool IsInRectangle(Rectangle rec) => rec.Contains(_newState.X, _newState.Y);

        public static int GetScrollWheelChange() => _newState.ScrollWheelValue - _oldState.ScrollWheelValue;

        public static bool HasMouseInput => (ButtonDown(MouseButtons.LeftButton) || ButtonDown(MouseButtons.RightButton) || ButtonDown(MouseButtons.MiddleButton) || GetScrollWheelChange() != 0) && WindowContainsMouse && Core.GameInstance.IsActive;

        public static bool WindowContainsMouse => IsInRectangle(Core.GraphicsDevice.Viewport.Bounds);

        public static Point MousePosition => new Point(_newState.X, _newState.Y);
    }
}

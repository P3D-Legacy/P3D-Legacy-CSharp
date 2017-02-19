using Microsoft.Xna.Framework.Input;

namespace P3D.Legacy.Core.Input
{
    public static class KeyBoardHandler
    {
        private static KeyboardState _oldState;
        private static KeyboardState _newState;

        public static KeyboardState KeyBoardState { get { return _newState; } set { _newState = value; } }

        public static void Update()
        {
            _oldState = _newState;
            _newState = Keyboard.GetState();
        }

        public static bool KeyPressed(Keys key) => !_oldState.IsKeyDown(key) && _newState.IsKeyDown(key);

        public static bool KeyDown(Keys key) => _newState.IsKeyDown(key);

        public static bool KeyUp(Keys key) => _newState.IsKeyUp(key);

        public static bool HasKeyboardInput() => _newState.GetPressedKeys().Length > 0;

        public static Keys[] GetPressedKeys => _newState.GetPressedKeys();
    }
}

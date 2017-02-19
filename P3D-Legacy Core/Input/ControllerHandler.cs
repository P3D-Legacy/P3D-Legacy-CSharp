using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace P3D.Legacy.Core.Input
{
    public static class ControllerHandler
    {
        private static GamePadState _oldState;
        private static GamePadState _newState;

        public static GamePadState GamePadState
        {
            get { return _newState; }
            set { _newState = value; }
        }

        public static void Update()
        {
            _oldState = _newState;
            _newState = GamePad.GetState(PlayerIndex.One);
        }

        public static bool ButtonPressed(Buttons button) => ButtonPressed(button, Core.GameOptions.GamePadEnabled);

        public static bool ButtonPressed(Buttons button, bool gamePadEnabled) => gamePadEnabled && !_oldState.IsButtonDown(button) && _newState.IsButtonDown(button);

        public static bool ButtonDown(Buttons button) => ButtonDown(button, Core.GameOptions.GamePadEnabled);

        public static bool ButtonDown(Buttons button, bool gamePadEnabled) => gamePadEnabled && _newState.IsButtonDown(button);

        public static bool IsConnected(int index = 0) => GamePad.GetState((PlayerIndex) index).IsConnected && Core.GameOptions.GamePadEnabled;

        public static bool HasControlerInput(int index = 0)
        {
            if (!IsConnected())
                return false;

            var gPadState = GamePad.GetState((PlayerIndex) index);

            Buttons[] bArr =
            {
                Buttons.A,
                Buttons.B,
                Buttons.Back,
                Buttons.BigButton,
                Buttons.DPadDown,
                Buttons.DPadLeft,
                Buttons.DPadRight,
                Buttons.DPadUp,
                Buttons.LeftShoulder,
                Buttons.LeftStick,
                Buttons.LeftThumbstickDown,
                Buttons.LeftThumbstickLeft,
                Buttons.LeftThumbstickRight,
                Buttons.LeftThumbstickUp,
                Buttons.LeftTrigger,
                Buttons.RightShoulder,
                Buttons.RightStick,
                Buttons.RightThumbstickDown,
                Buttons.RightThumbstickLeft,
                Buttons.RightThumbstickRight,
                Buttons.RightThumbstickUp,
                Buttons.RightTrigger,
                Buttons.Start,
                Buttons.X,
                Buttons.Y
            };

            return bArr.Any(b => gPadState.IsButtonDown(b));
        }
    }
}

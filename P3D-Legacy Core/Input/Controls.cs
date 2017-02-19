using Microsoft.Xna.Framework.Input;

namespace P3D.Legacy.Core.Input
{
    public static class Controls
    {
        private enum PressDirections
        {
            Up,
            Left,
            Down,
            Right,
            None
        }

        private static PressDirections _lastPressedDirection = PressDirections.None;
        private static float _pressedKeyDelay;

        private static void ResetDirectionPressed(PressDirections direction)
        {
            if (_lastPressedDirection == direction)
            {
                _pressedKeyDelay = 4f;
                _lastPressedDirection = PressDirections.None;
            }
        }

        private static void ChangeDirectionPressed(PressDirections direction)
        {
            if (_lastPressedDirection != direction)
            {
                _pressedKeyDelay = 4f;
                _lastPressedDirection = direction;
            }
        }

        private static bool HoldDownPress(PressDirections direction)
        {
            if (_lastPressedDirection == direction)
            {
                _pressedKeyDelay -= 0.1f;
                if (_pressedKeyDelay <= 0f)
                {
                    _pressedKeyDelay = 0.3f;
                    return true;
                }
            }
            return false;
        }

        private static bool CheckDirectionalPress(PressDirections direction, Keys wasdKey, Keys directionalKey, Buttons thumbStickDirection, Buttons dPadDirecion, bool arrowKeys, bool wasd, bool thumbStick, bool dPad)
        {
            bool command = false;
            if (wasd)
            {
                if (KeyBoardHandler.KeyDown(wasdKey))
                {
                    command = true;
                    if (HoldDownPress(direction))
                    {
                        return true;
                    }
                    if (KeyBoardHandler.KeyPressed(wasdKey))
                    {
                        ChangeDirectionPressed(direction);
                        return true;
                    }
                }
            }
            if (arrowKeys)
            {
                if (KeyBoardHandler.KeyDown(directionalKey))
                {
                    command = true;
                    if (HoldDownPress(direction))
                    {
                        return true;
                    }
                    if (KeyBoardHandler.KeyPressed(directionalKey))
                    {
                        ChangeDirectionPressed(direction);
                        return true;
                    }
                }
            }
            if (thumbStick)
            {
                if (ControllerHandler.ButtonDown(thumbStickDirection))
                {
                    command = true;
                    if (HoldDownPress(direction))
                    {
                        return true;
                    }
                    if (ControllerHandler.ButtonPressed(thumbStickDirection))
                    {
                        ChangeDirectionPressed(direction);
                        return true;
                    }
                }
            }
            if (dPad)
            {
                if (ControllerHandler.ButtonDown(dPadDirecion))
                {
                    command = true;
                    if (HoldDownPress(direction))
                    {
                        return true;
                    }
                    if (ControllerHandler.ButtonPressed(dPadDirecion))
                    {
                        ChangeDirectionPressed(direction);
                        return true;
                    }
                }
            }

            if (command == false)
            {
                ResetDirectionPressed(direction);
            }

            return false;
        }

        public static bool Left(bool pressed, bool arrowKeys = true, bool scroll = true, bool wasd = true, bool thumbStick = true, bool dPad = true)
        {
            if (Core.GameInstance.IsActive)
            {
                if (MouseHandler.WindowContainsMouse && scroll)
                {
                    if (MouseHandler.GetScrollWheelChange() > 0)
                    {
                        return true;
                    }
                }
                if (pressed)
                {
                    return CheckDirectionalPress(PressDirections.Left, Core.KeyBindings.LeftMove, Core.KeyBindings.Left, Buttons.LeftThumbstickLeft, Buttons.DPadLeft, arrowKeys, wasd, thumbStick, dPad);
                }
                if (wasd)
                {
                    if (KeyBoardHandler.KeyDown(Core.KeyBindings.LeftMove))
                    {
                        return true;
                    }
                }
                if (arrowKeys)
                {
                    if (KeyBoardHandler.KeyDown(Core.KeyBindings.Left))
                    {
                        return true;
                    }
                }
                if (thumbStick)
                {
                    if (ControllerHandler.ButtonDown(Buttons.LeftThumbstickLeft))
                    {
                        return true;
                    }
                }
                if (dPad)
                {
                    if (ControllerHandler.ButtonDown(Buttons.DPadLeft))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool Right(bool pressed, bool arrowKeys = true, bool scroll = true, bool wasd = true, bool thumbStick = true, bool dPad = true)
        {
            if (Core.GameInstance.IsActive)
            {
                if (MouseHandler.WindowContainsMouse && scroll)
                {
                    if (MouseHandler.GetScrollWheelChange() < 0)
                    {
                        return true;
                    }
                }
                if (pressed)
                {
                    return CheckDirectionalPress(PressDirections.Right, Core.KeyBindings.RightMove, Core.KeyBindings.Right, Buttons.LeftThumbstickRight, Buttons.DPadRight, arrowKeys, wasd, thumbStick, dPad);
                }
                if (wasd)
                {
                    if (KeyBoardHandler.KeyDown(Core.KeyBindings.RightMove))
                    {
                        return true;
                    }
                }
                if (arrowKeys)
                {
                    if (KeyBoardHandler.KeyDown(Core.KeyBindings.Right))
                    {
                        return true;
                    }
                }
                if (thumbStick)
                {
                    if (ControllerHandler.ButtonDown(Buttons.LeftThumbstickRight))
                    {
                        return true;
                    }
                }
                if (dPad)
                {
                    if (ControllerHandler.ButtonDown(Buttons.DPadRight))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool Up(bool pressed, bool arrowKeys = true, bool scroll = true, bool wasd = true, bool thumbStick = true, bool dPad = true)
        {
            if (Core.GameInstance.IsActive)
            {
                if (MouseHandler.WindowContainsMouse && scroll)
                {
                    if (MouseHandler.GetScrollWheelChange() > 0)
                    {
                        return true;
                    }
                }
                if (pressed)
                {
                    return CheckDirectionalPress(PressDirections.Up, Core.KeyBindings.ForwardMove, Core.KeyBindings.Up, Buttons.LeftThumbstickUp, Buttons.DPadUp, arrowKeys, wasd, thumbStick, dPad);
                }
                if (wasd)
                {
                    if (KeyBoardHandler.KeyDown(Core.KeyBindings.ForwardMove))
                    {
                        return true;
                    }
                }
                if (arrowKeys)
                {
                    if (KeyBoardHandler.KeyDown(Core.KeyBindings.Up))
                    {
                        return true;
                    }
                }
                if (thumbStick)
                {
                    if (ControllerHandler.ButtonDown(Buttons.LeftThumbstickUp))
                    {
                        return true;
                    }
                }
                if (dPad)
                {
                    if (ControllerHandler.ButtonDown(Buttons.DPadUp))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool Down(bool pressed, bool arrowKeys = true, bool scroll = true, bool wasd = true, bool thumbStick = true, bool dPad = true)
        {
            if (Core.GameInstance.IsActive)
            {
                if (MouseHandler.WindowContainsMouse && scroll)
                {
                    if (MouseHandler.GetScrollWheelChange() < 0)
                    {
                        return true;
                    }
                }
                if (pressed)
                {
                    return CheckDirectionalPress(PressDirections.Down, Core.KeyBindings.BackwardMove, Core.KeyBindings.Down, Buttons.LeftThumbstickDown, Buttons.DPadDown, arrowKeys, wasd, thumbStick, dPad);
                }
                if (wasd)
                {
                    if (KeyBoardHandler.KeyDown(Core.KeyBindings.BackwardMove))
                    {
                        return true;
                    }
                }
                if (arrowKeys)
                {
                    if (KeyBoardHandler.KeyDown(Core.KeyBindings.Down))
                    {
                        return true;
                    }
                }
                if (thumbStick)
                {
                    if (ControllerHandler.ButtonDown(Buttons.LeftThumbstickDown))
                    {
                        return true;
                    }
                }
                if (dPad)
                {
                    if (ControllerHandler.ButtonDown(Buttons.DPadDown))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool ShiftDown(string pressedFlag = "LR", bool triggerButtons = true)
        {
            if (pressedFlag.Contains("L") && KeyBoardHandler.KeyDown(Keys.LeftShift))
            {
                return true;
            }
            if (pressedFlag.Contains("L") && ControllerHandler.ButtonDown(Buttons.LeftTrigger) && triggerButtons)
            {
                return true;
            }
            if (pressedFlag.Contains("R") && KeyBoardHandler.KeyDown(Keys.RightShift))
            {
                return true;
            }
            if (pressedFlag.Contains("R") && ControllerHandler.ButtonDown(Buttons.RightTrigger) && triggerButtons)
            {
                return true;
            }
            return false;
        }

        public static bool ShiftPressed(string pressedFlag = "LR", bool triggerButtons = true)
        {
            if (pressedFlag.Contains("L") && KeyBoardHandler.KeyPressed(Keys.LeftShift))
            {
                return true;
            }
            if (pressedFlag.Contains("L") && ControllerHandler.ButtonPressed(Buttons.LeftTrigger) && triggerButtons)
            {
                return true;
            }
            if (pressedFlag.Contains("R") && KeyBoardHandler.KeyPressed(Keys.RightShift))
            {
                return true;
            }
            if (pressedFlag.Contains("R") && ControllerHandler.ButtonPressed(Buttons.RightTrigger) && triggerButtons)
            {
                return true;
            }
            return false;
        }

        public static bool CtrlPressed(string pressedFlag = "LR", bool triggerButtons = true)
        {
            // TODO
            //if (System.Windows.Forms.Control.ModifierKeys == (System.Windows.Forms.Keys.Control || System.Windows.Forms.Keys.Alt))
            //{
            //    return false;
            //}
            if (pressedFlag.Contains("L") && KeyBoardHandler.KeyDown(Keys.LeftControl))
            {
                return true;
            }
            if (pressedFlag.Contains("L") && ControllerHandler.ButtonDown(Buttons.LeftTrigger) && triggerButtons)
            {
                return true;
            }
            if (pressedFlag.Contains("R") && KeyBoardHandler.KeyDown(Keys.RightControl))
            {
                return true;
            }
            if (pressedFlag.Contains("R") && ControllerHandler.ButtonDown(Buttons.RightTrigger) && triggerButtons)
            {
                return true;
            }

            return false;
        }

        public static bool HasKeyboardInput()
        {
            Keys[] keyArr = {
                Core.KeyBindings.ForwardMove,
                Core.KeyBindings.LeftMove,
                Core.KeyBindings.BackwardMove,
                Core.KeyBindings.RightMove,
                Core.KeyBindings.Up,
                Core.KeyBindings.Left,
                Core.KeyBindings.Down,
                Core.KeyBindings.Right
            };
            foreach (Keys key in keyArr)
            {
                if (KeyBoardHandler.KeyPressed(key))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Accept(bool doMouse = true, bool doKeyBoard = true, bool doGamePad = true)
        {
            if (Core.GameInstance.IsActive)
            {
                if (doKeyBoard)
                {
                    if (KeyBoardHandler.KeyPressed(Core.KeyBindings.Enter1) || KeyBoardHandler.KeyPressed(Core.KeyBindings.Enter2))
                    {
                        return true;
                    }
                }
                if (doMouse)
                {
                    if (MouseHandler.ButtonPressed(MouseHandler.MouseButtons.LeftButton))
                    {
                        return true;
                    }
                }
                if (doGamePad)
                {
                    if (ControllerHandler.ButtonPressed(Buttons.A))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool Dismiss(bool doMouse = true, bool doKeyBoard = true, bool doGamePad = true)
        {
            if (Core.GameInstance.IsActive)
            {
                if (doKeyBoard)
                {
                    if (KeyBoardHandler.KeyPressed(Core.KeyBindings.Back1) || KeyBoardHandler.KeyPressed(Core.KeyBindings.Back2))
                    {
                        return true;
                    }
                }
                if (doMouse)
                {
                    if (MouseHandler.ButtonPressed(MouseHandler.MouseButtons.RightButton))
                    {
                        return true;
                    }
                }
                if (doGamePad)
                {
                    if (ControllerHandler.ButtonPressed(Buttons.B))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void MakeMouseVisible()
        {
            if (Core.GameInstance.IsMouseVisible == false && Core.CurrentScreen.MouseVisible)
            {
                MouseState mState = Mouse.GetState();
                if (mState.X != MouseHandler.MousePosition.X || mState.Y != MouseHandler.MousePosition.Y)
                {
                    Core.GameInstance.IsMouseVisible = true;
                }
            }
            else if (Core.GameInstance.IsMouseVisible)
            {
                if (ControllerHandler.HasControlerInput() || HasKeyboardInput())
                {
                    Core.GameInstance.IsMouseVisible = false;
                }
            }
        }
    }
}

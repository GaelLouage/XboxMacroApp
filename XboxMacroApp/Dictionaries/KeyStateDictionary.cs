using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace XboxMacroApp.Dictionaries
{
    public class KeyStateDictionary
    {
        public static Dictionary<GamepadButtonFlags,bool>  Get(State state) =>
              new Dictionary<GamepadButtonFlags, bool>(){ 
                       {GamepadButtonFlags.A , state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.A) },
                        {GamepadButtonFlags.B , state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.B) },
                        {GamepadButtonFlags.X ,state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.X) },
                        {GamepadButtonFlags.Y , state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Y) },
                        {GamepadButtonFlags.Start ,  state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Start) },
                        {GamepadButtonFlags.Back ,  state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Back) },
                        {GamepadButtonFlags.DPadDown ,  state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadDown) },
                        {GamepadButtonFlags.DPadUp ,  state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadUp) },
                        {GamepadButtonFlags.DPadLeft ,  state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadLeft) },
                        {GamepadButtonFlags.DPadRight,   state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadRight) },
                        {GamepadButtonFlags.LeftShoulder , state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.LeftShoulder) },
                        {GamepadButtonFlags.RightShoulder , state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.RightShoulder) },
                        {GamepadButtonFlags.LeftThumb ,  state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.LeftThumb) },
                        {GamepadButtonFlags.RightThumb,  state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.RightThumb) },
                        {GamepadButtonFlags.None,  state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.None) },
                        };
    }
}

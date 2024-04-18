using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using XboxMacroApp.Helpers;

namespace XboxMacroApp.Models
{
    public class ProgramModel
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public GamepadButtonFlags AssignedKey { get; set; } = GamepadButtonFlags.None;
        public BitmapImage ButtonImage => ButtonImageHelper.GetButtonImage(AssignedKey);
    }
}

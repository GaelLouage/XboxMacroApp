using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using XboxMacroApp.Constants;

namespace XboxMacroApp.Helpers
{
    public static class ButtonImageHelper
    {
        public static BitmapImage GetButtonImage(GamepadButtonFlags btn)
        {
            switch (btn)
            {
                case GamepadButtonFlags.A:
                    return new BitmapImage(new Uri(Path(ImageConstant.BUTTON_A)));
                case GamepadButtonFlags.B:
                    return new BitmapImage(new Uri(Path(ImageConstant.BUTTON_B)));
                case GamepadButtonFlags.X:
                    return new BitmapImage(new Uri(Path(ImageConstant.BUTTON_X)));
                case GamepadButtonFlags.Y:
                    return new BitmapImage(new Uri(Path(ImageConstant.BUTTON_Y)));
                case GamepadButtonFlags.Start:
                    return new BitmapImage(new Uri(Path(ImageConstant.BUTTON_START)));
                case GamepadButtonFlags.Back:
                    return new BitmapImage(new Uri(Path(ImageConstant.BUTTON_BACK)));
                default:
                    return new BitmapImage(new Uri(Path(ImageConstant.N_A)));
            }
        }
        public static string Path(string fileName) => 
            System.IO.Path.Combine(Environment.CurrentDirectory, fileName); 
      
    }
}

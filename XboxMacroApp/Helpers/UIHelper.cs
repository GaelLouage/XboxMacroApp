using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using XboxMacroApp.Extensions;

namespace XboxMacroApp.Helpers
{
    public static class UIHelper
    {
        private static int counter = 0;
        public static void UIUpdate(ListView lv, Image imgApp, Image imgAppControllerON, Image imgKey, Image imgTrash, Image imgPlus)
        {
            lv.RemoveListViewBackgroundBorderOnFocus();
            imgKey.IsEnabled = false;
            imgTrash.IsEnabled = false;
            imgApp.Source = FileHelper.CombineCurrentDirectoryWithPath("imgApp.png");
            //imgAppControllerON.Source = FileHelper.CombineCurrentDirectoryWithPath("imgApp.png");
            imgTrash.Source = FileHelper.CombineCurrentDirectoryWithPath("trash.png");
            imgKey.Source = FileHelper.CombineCurrentDirectoryWithPath("key.png");
            imgPlus.Source = FileHelper.CombineCurrentDirectoryWithPath("plus.png");
        }
        public static void UiVisibilityOnConnectedCheck(Dispatcher dispatcher, Image imgPlus, Image imgAppControllerOn, ListView lvPrograms, bool controllerConnected)
        {
            dispatcher?.Invoke(async () =>
            {
                imgPlus.IsEnabled = false;
                imgAppControllerOn.Visibility = Visibility.Visible;
                imgAppControllerOn.Source = FileHelper.CombineCurrentDirectoryWithPath("imgApp.png");
                if (counter % 2 == 0)
                {
                    imgAppControllerOn.Source = FileHelper.CombineCurrentDirectoryWithPath("xboxRed.png");
                }
                counter++;
                lvPrograms.Visibility = Visibility.Hidden;
                if (controllerConnected)
                {
                    imgAppControllerOn.Visibility = Visibility.Hidden;
                    lvPrograms.Visibility = Visibility.Visible;
                    imgPlus.IsEnabled = true;
                }
            });
        }

    }
}

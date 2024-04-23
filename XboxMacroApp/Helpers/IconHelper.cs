using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;

namespace XboxMacroApp.Helpers
{
    public static class IconHelper
    {
        public static BitmapImage ConvertToBitmapImage(Icon icon)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                icon.Save(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
                bitmapImage.Freeze(); // Freeze the bitmap to prevent modifications
                return bitmapImage;
            }
        }

        public static BitmapImage GetIcon(string filePath)
        {
            try
            {
                Icon icon = Icon.ExtractAssociatedIcon(filePath);
                return ConvertToBitmapImage(icon);
            }
            catch {}

            return null;
        }
    }
}

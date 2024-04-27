using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using XboxMacroApp.Models;

namespace XboxMacroApp.Helpers
{
    public static class FileHelper
    {
        public static string OpenFileDialogWithPath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                openFileDialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
                return openFileDialog.FileName;
            }

            return string.Empty;
        }

        public static async Task WriteListToJsonFileAsync(string fileName, List<ProgramModel>? programs)
        {
            var seria = JsonConvert.SerializeObject(programs);
            await File.WriteAllTextAsync(fileName, seria);
        }
        

        public static BitmapImage CombineCurrentDirectoryWithPath(string fileName)
        {
            return new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, fileName)));
        }

        public static bool IsSupportedFileExtension(string filePath)
        {
            var fileExtension = Path.GetExtension(filePath).ToLower();
            if (fileExtension == ".exe" || fileExtension == ".txt" || fileExtension == ".png")
            {
                return true;
            }
            return false;
        }
    }
}

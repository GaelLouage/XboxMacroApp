using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
    }
}

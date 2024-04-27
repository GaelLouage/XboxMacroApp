using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XboxMacroApp.Models;

namespace XboxMacroApp.Helpers
{
    public static class ProcessHelper
    {
        public static void OpenFileWithAssociatedProgram(ProgramModel? getProgramWithKeyPresseValue)
        {
            //get last string of file
            var fileExtension = getProgramWithKeyPresseValue.FilePath.Split('.')[^1];
            switch (fileExtension)
            {
                case "exe":
                    Process.Start(getProgramWithKeyPresseValue.FilePath);
                    break;
                case "txt":
                    Process.Start("Notepad.exe", getProgramWithKeyPresseValue.FilePath);
                    break;
                case "png":
                    Process.Start("explorer.exe", getProgramWithKeyPresseValue.FilePath);
                    break;
                default:
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XboxMacroApp.Extensions
{
    public static class StringExtensions
    {
        public static string GetFileName(this string filePath)
        {
            if(filePath.Contains("\\"))
            {
                return filePath.Split("\\")[^1];
            }
            return "";
        }
    }
}

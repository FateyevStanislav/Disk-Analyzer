using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskAnalyzer.Library.Infrastructure
{
    public static class StringExtensions
    {
        public static string EscapeSlashes(this string str)
        {
            return str.Replace("\\", "\\\\");
        }
    }
}

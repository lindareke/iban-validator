using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IbanValidatorConsoleApp
{
    public static class Utilities
    {
        public static string TrimQuotes(string path)
        {
            if (path.Length > 2 && path[0] == '"' && path[path.Length - 1] == '"')
            {
                path = path.Substring(1, path.Length - 2);
            }
            return path;
        }
    }
}

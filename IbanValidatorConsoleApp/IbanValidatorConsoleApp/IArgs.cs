using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IbanValidatorConsoleApp
{
    public interface IArgs
    {
        string Type { get; set; }
    }

    public class FileArgs : IArgs
    {
        public string Type { get; set; }
        public string FileName { get; set; }

        public FileArgs(string type, string fileName)
        {
            Type = type;
            FileName = fileName;
        }
    }

    public class ConsoleArgs : IArgs
    {
        public string Type { get; set; }
        public string IbanNumber { get; set; }

        public ConsoleArgs(string type, string iban)
        {
            Type = type;
            IbanNumber = iban;
        }
    }
}
 
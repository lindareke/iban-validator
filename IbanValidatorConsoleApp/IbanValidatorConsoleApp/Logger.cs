using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IbanValidatorConsoleApp
{
    public class Logger
    {
        public static bool Quit { get; internal set; }

        public static void WriteLine(string message)
        {
            if (Quit)
            {
                return;
            }

            Console.WriteLine(message);
        }

        public static void Write(string message)
        {
            if (Quit)
            {
                return;
            }

            Console.Write(message);
        }

        public static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(message);
            Console.ResetColor();
        }
    }
}

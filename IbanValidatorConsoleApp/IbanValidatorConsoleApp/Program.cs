using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace IbanValidatorConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Arguments arguments = new Arguments(args);

                if (arguments.Help || args.Length == 0)
                {
                    PrintUsage();
                //    return 0;
                }

                if (!string.IsNullOrEmpty(arguments.Error))
                {
                    Logger.WriteLine($"Error: Invalid arguments: {arguments.Error}");
                    PrintUsage();
                    //return 1;
                }

                var validator = new ValidatorFactory(arguments.Command, arguments.Parameter).GetValidator();

                if (validator != null)
                {
                    validator.Process();
                }
            //    return 0;
            }
            catch (Exception ex)
            {
                Logger.WriteLine($"Error: Unrecoverable error: {ex.Message}");
            //    return 1;
            }

            while (true)
            {
                Console.ReadLine();
            }
        }

        private static void PrintUsage()
        {
            Logger.WriteLine(@" Usage:
IbanValidatorConsoleApp.exe -h
IbanValidatorConsoleApp.exe -q
IbanValidatorConsoleApp.exe -i <IBAN>
IbanValidatorConsoleApp.exe -f <file_path>

Options:
-h      Show this screen
-q      Quit the application
-i      Single IBAN validation
-f      File validation

Project page: https://github.com/lindareke/iban-validator
            ");
        }
    }
}

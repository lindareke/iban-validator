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
        static int Main(string[] args)
        {
            try
            {
                Arguments arguments = new Arguments(args);

                if (arguments.Help || args.Length == 0)
                {
                    PrintUsage();
                   return 0;
                }

                if (!string.IsNullOrEmpty(arguments.Error))
                {
                    Logger.WriteError($"Error: Invalid arguments: {arguments.Error} \n");
                    PrintUsage();
                    return 1;
                }

                var validator = new ValidatorFactory(arguments.Command, arguments.Parameter).GetValidator();

                if (validator != null)
                {
                    validator.Process();                  
                }
                else
                {
                    Logger.WriteError($"Error: Errord during app execution\n");
                    return 1;
                }

                //    return 0;
            }
            catch (Exception ex)
            {
                Logger.WriteError($"Error: Unrecoverable error: {ex.Message}\n");
                return 1;
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

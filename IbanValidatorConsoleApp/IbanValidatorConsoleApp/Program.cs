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
                    Logger.WriteLine($"Error: Invalid arguments: {arguments.Error}");
                    PrintUsage();
                    return 1;
                }

                string command = arguments.Command;
                string parameter = arguments.Parameter;

                if (arguments.Command == "f")
                {
                    if (File.Exists(parameter))
                    {
                        ProcessFile(parameter);
                        return 0;
                    }
                    else
                    {
                        Logger.WriteLine($"Error: File {parameter} does not exist");
                        return 1;
                    }
                }
                else
                {
                    CheckIban(arguments.Parameter);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine($"Error: Unrecoverable error: {ex.Message}");
                return 1;
            }
        }

        private static void CheckIban(string iban)
        {
            Logger.WriteLine($"{iban} number is{(IbanValidator.IsValidIban(iban) ? "" : "not")} a valid IBAN");
        }

        private static void ProcessFile(string fileName)
        {
            FileProcessor proc = new FileProcessor(fileName);
            proc.RunProcess();
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

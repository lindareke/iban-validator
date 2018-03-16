using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IbanValidatorConsoleApp
{
    public class FileValidator
    {
        public static void ValidateFile(string inputFileName)
        {
            try
            {
                string outputFileName = OutputFileName(inputFileName);

                if (File.Exists(outputFileName))
                {
                    File.Delete(outputFileName);
                }

                using (StreamWriter streamWriter = File.CreateText(outputFileName))
                {
                    using (StreamReader streamReader = new StreamReader(inputFileName))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            line = $"{line},{IbanValidator.IsValidIban(line).ToString().ToLower()}";
                            streamWriter.WriteLine(line);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Logger.WriteLine($"Error: Can not create output file for {inputFileName}. Error message: {ex.Message}");
            }
        }

        public static string OutputFileName(string inputFileName)
        {
            string directoryName = Path.GetDirectoryName(inputFileName);
            string filenameWoDir = Path.GetFileNameWithoutExtension(inputFileName);

            return  directoryName + "\\" + filenameWoDir + ".out";
        }
    }
}

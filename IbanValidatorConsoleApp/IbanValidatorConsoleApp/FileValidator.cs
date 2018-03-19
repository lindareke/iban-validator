using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace IbanValidatorConsoleApp
{
    public class FileValidator : IValidator
    {
        private FileArgs _args;

        private static List<string> _filesInProgress;
        static FileValidator()
        {
            _filesInProgress = new List<string>();
        }

        public FileValidator(FileArgs args)
        {
            _args = args;
        }

        public void Process()
        {
                string inputFile = _args.FileName;

            if (_filesInProgress.Contains(inputFile))
            {
                Logger.WriteError($"Error: {inputFile} file processing in progress\n");
                return;
            }

            if (!File.Exists(inputFile))
            {
                Logger.WriteError($"Error: {inputFile} file does not exist\n");
                return;
            }
                
            if (FileHasValidExtention(inputFile))
            {
                StartProcessingTask(inputFile);
            }
            else
            {
                Logger.WriteError($"Error: {inputFile} file type is not allowed\n");
            }
        }

        private async void StartProcessingTask(string inputFile)
        {
            Logger.WriteLine($"{inputFile} file processing started\n");

            _filesInProgress.Add(inputFile);

            string outputFile = OutputFileName(inputFile);

            await Task.Run(() => ProcessFile(inputFile, outputFile));

            _filesInProgress.Remove(inputFile);

            Logger.WriteLine($"{inputFile} file processing finished\n");
        }

        public static void ProcessFile(string inputFileName, string outputFileName)
        {
            try
            {
                if (File.Exists(outputFileName))
                {
                    File.Delete(outputFileName);
                }

                using (FileStream fs = File.Create(outputFileName))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fs))
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
            }
            catch (IOException ex)
            {
                Logger.WriteError($"Error: Can not create output file for {inputFileName}. Error message: {ex.Message}\n");
            }

            //Thread.Sleep(10000);
        }

        public static string OutputFileName(string inputFile)
        {
            string directoryName = Path.GetDirectoryName(inputFile);
            string filenameWoDir = Path.GetFileNameWithoutExtension(inputFile);

            return directoryName + "\\" + filenameWoDir + ".out";
        }

        private static bool FileHasValidExtention(string fullPath)
        {
            string ext = Path.GetExtension(fullPath).ToLower();

            if (ext == ".in")
            {
                return true;
            }
            return false;
        }
    }
}

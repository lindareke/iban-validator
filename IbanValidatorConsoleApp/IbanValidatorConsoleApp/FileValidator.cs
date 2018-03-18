using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IbanValidatorConsoleApp
{
    public class FileValidator : IValidator
    {
        private FileArgs _args;
        public FileValidator(FileArgs args)
        {
            _args = args;
        }

        public void Process ()
        {
            string inputFile = _args.Filename;
            if (FileHasValidExtention(inputFile))
            {
                string outputFile = OutputFileName(inputFile);

                ValidateFile(inputFile, outputFile);
            }
            else
            {
                Logger.WriteLine($"Error: {inputFile} file type is not allowed");
            }
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

        public static void ValidateFile(string inputFileName, string outputFileName)
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

                Logger.WriteLine($"Process complete for file {inputFileName}");
            }
            catch (IOException ex)
            {
                Logger.WriteLine($"Error: Can not create output file for {inputFileName}. Error message: {ex.Message}");
            }
        }

        //private static List<string> _filesInProgress;
        //static FileProcessor()
        //{
        //    _filesInProgress = new List<string>();
        //}
        //public bool ProcessFile()
        //{
        //    FileValidator.ValidateFile(_inputFile);
        //}

        //public async void DoTask()
        //{
        //    Task<bool> task = new Task<bool>(ProcessFile);
        //    task.Start();

        //    _filesInProgress.Add(_inputFile);
        //    Console.WriteLine("File {0} processing started ...", _inputFile);

        //    bool processed = await task;
        //    if (processed)
        //    {              
        //        Console.WriteLine("File {0} processed", _inputFile);
        //    }
        //    _filesInProgress.Remove(_inputFile);
        //}
    }
}

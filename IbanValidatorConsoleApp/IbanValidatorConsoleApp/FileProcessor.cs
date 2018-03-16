using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IbanValidatorConsoleApp
{
    public class FileProcessor
    {

        private static List<string> _filesInProgress;
        string _inputFile;

        static FileProcessor()
        {
            _filesInProgress = new List<string>();
        }

        public FileProcessor(string inputFile)
        {
            _inputFile = inputFile;
        }

        public void Process()
        {
            if (FileHasValidExtention(_inputFile))
            {
                string result = _filesInProgress.FirstOrDefault(x => x == _inputFile);
                if (result == null)
                {                   
                    DoTask(); 
                }
                else
                {
                    Logger.WriteLine($"Error: {_inputFile} processing already in progress");
                }
            }
            else
            {
                Logger.WriteLine($"Error: {_inputFile} file type is not allowed");
            }
        }

        public bool ProcessFile()
        {
            FileValidator.ValidateFile(_inputFile);
            Thread.Sleep(10000);
            return true;
        }

        public async void DoTask()
        {
            Task<bool> task = new Task<bool>(ProcessFile);
            task.Start();

            _filesInProgress.Add(_inputFile);
            Console.WriteLine("File {0} processing started ...", _inputFile);

            bool processed = await task;
            if (processed)
            {
                _filesInProgress.Remove(_inputFile);
                Console.WriteLine("File {0} processed", _inputFile);
            }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IbanValidatorConsoleApp
{
    public class Arguments
    {
        private string[] _arguments;

        public string Command { get; set; }

        public string Parameter { get; set; }

        public bool Help { get; set; }

        public string Error { get; set; } = "";

        public Arguments(params string[] args)
        {
            _arguments = args;
            ParseArguments();
        }

        private void ParseArguments()
        {
            List<string> parameters = new List<string>();
            bool parameterRequired = false;

            foreach (string arg in _arguments)
            {
                if (arg.StartsWith("/") || arg.StartsWith("-"))
                {
                    Command = arg.Substring(1).ToLower();
                }
                else
                {
                    parameters.Add(Utilities.TrimQuotes(arg));
                }
            }

            switch (Command)
            {
                case "f":
                case "i":
                    parameterRequired = true;
                    break;
                case "q":
                    Logger.Quit = true;
                    break;
                case "h":
                    Help = true;
                    return;
                default:
                    Error = "Unrecognized argument: " + Command;
                    return;
            }

            if (parameterRequired && parameters.Count != 1)
            {
                Error = "Invalid number of arguments";
                return;
            }

            Parameter = parameters[0];
        }
    }
}

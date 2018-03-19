using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IbanValidatorConsoleApp
{
    public class ConsoleValidator : IValidator
    {
        private ConsoleArgs _iargs;
        public ConsoleValidator(ConsoleArgs args)
        {
            _iargs = args;
        }

        public void Process()
        { 
            Logger.WriteLine($"{_iargs.IbanNumber} number is{(IbanValidator.IsValidIban(_iargs.IbanNumber) ? "" : " not")} a valid IBAN\n");
        }
    }
}

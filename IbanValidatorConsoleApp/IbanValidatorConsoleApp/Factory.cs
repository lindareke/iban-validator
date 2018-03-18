using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IbanValidatorConsoleApp
{
    public class ValidatorFactory
    {
        string _type;
        string _parameter;

        public ValidatorFactory(string type, string parameter)
        {
            _type = type;
            _parameter = parameter;
        }

        public IValidator GetValidator()
        {
            switch (_type)
            {
                case "i":
                    return new ConsoleValidator(new ConsoleArgs(_type, _parameter));
                case "f":
                    return new FileValidator(new FileArgs(_type, _parameter));
                default:
                    return null;
            }
        }
    }
}

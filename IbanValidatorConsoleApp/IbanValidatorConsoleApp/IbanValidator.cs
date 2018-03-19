using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IbanValidatorConsoleApp
{
    public class IbanValidator
    {
        public static bool IsValidIban(string iban)
        {
            iban = Utilities.RemoveWhiteSpaces(iban).ToUpper();

            if (!ValidLength(iban))
            {
                return false;
            }

            StringBuilder sb = new StringBuilder();

            char[] data = (iban.Substring(4) + iban.Substring(0, 2)).ToCharArray();

            foreach (char c in data)
            {
                if (c >= 'A')
                {
                    sb.Append(c - 'A' + 10);
                }
                else
                {
                    sb.Append(c);
                }
            }

            sb.Append("00");

            data = sb.ToString().ToCharArray();

            int checksum = 0;

            foreach (char c in data)
            {
                checksum = ((c - '0') + checksum * 10) % 97;
            }

            checksum = 98 - checksum;

            return iban.Substring(2, 2).Equals(checksum < 10 ? ("0" + checksum) : ("" + checksum));
        }

        private static bool ValidLength(string iban)
        {
            Dictionary<string, int> countryLenghts = new Dictionary<string, int>()
            {
                {"AL",28},{"AD",24},{"AT",20},{"AZ",28},{"BH",22},{"BY",28},{"BE",16},
                {"BA",20},{"BR",29},{"BG",22},{"CR",22},{"HR",21},{"CY",28},{"CZ",24},
                {"DK",18},{"DO",28},{"SV",28},{"EE",20},{"FO",18},{"FI",18},{"FR",27},
                {"GE",22},{"DE",22},{"GI",23},{"GR",27},{"GL",18},{"GT",28},{"HU",28},
                {"IS",26},{"IQ",23},{"IE",22},{"IL",23},{"IT",27},{"JO",30},{"KZ",20},
                {"XK",20},{"KW",30},{"LV",21},{"LB",28},{"LI",21},{"LT",20},{"LU",20},
                {"MK",19},{"MT",31},{"MR",27},{"MU",30},{"MD",24},{"MC",27},{"ME",22},
                { "NL",18},{"NO",15},{"PK",24},{"PS",29},{"PL",28},{"PT",25},{"QA",29},
                { "RO",24},{"LC",32},{"SM",27},{"ST",25},{"RS",22},{"SC",31},{"SK",24},
                {"SI",19},{"ES",24},{"SE",24},{"CH",21},{"TL",23},{"TN",24},{"TR",26},
                {"UA",29},{"AE",23},{"GB",22},{"VG",24}
            };

            if (iban.Length < 4)
            {
                return false;
            }

            //checks length for countries specifized in list, otherwise only checksum
            string countryCode = iban.Substring(0, 2);
            if (countryLenghts.ContainsKey(countryCode) && iban.Length != countryLenghts[countryCode])
            {
                return false;
            }
            return true;
        }
    }
}

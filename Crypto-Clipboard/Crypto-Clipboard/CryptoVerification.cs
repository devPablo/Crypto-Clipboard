using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crypto_Clipboard
{
    class CryptoVerification
    {
        private readonly string BTC = "^[13][a-km-zA-HJ-NP-Z1-9]{25,34}$";
        private readonly string ETH = "^0x[a-fA-F0-9]{40}$";

        public CryptoVerification() { }

        public bool Verify(string value, CryptoTypes type)
        {
            Regex regex;
            switch (type)
            {
                case CryptoTypes.BTC:
                    regex = new Regex(BTC);
                    return regex.Match(value).Success;
                case CryptoTypes.ETH:
                    regex = new Regex(ETH);
                    return regex.Match(value).Success;
            }
            return false;
        }
    }
}

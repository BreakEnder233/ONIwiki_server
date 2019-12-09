using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.DataSafety
{
    public class HashEncrypter
    {

        public static string GetHash(string data)
        {
            return data.GetHashCode().ToString();
        }
    }
}

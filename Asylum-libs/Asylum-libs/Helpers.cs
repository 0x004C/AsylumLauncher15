using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;

namespace AsylumLibs
{
    public static class Helpers
    {
        public static string sha256_hash(string value)
        {
            StringBuilder s = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    s.Append(b.ToString("x2"));
            }

            return s.ToString();
        }
    }
}

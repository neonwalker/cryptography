using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SHACollision
{
    public class HashGenerator
    {
        public Dictionary<ulong, string> CalculatedHashes { get; set; }

        public (string, string) Calculate(uint count)
        {
            Clear();
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                string key = "";
                byte[] hash;
                while (CalculatedHashes.Count != count)
                {
                    key = KeyGenerator.GetUniqueKey(15);
                    hash = sha1.ComputeHash(Encoding.ASCII.GetBytes(key));
                    var sb = new StringBuilder(hash.Length * 2);

                    foreach (byte b in hash)
                    {
                        sb.Append(b.ToString("X2"));
                    }
                    string firstPart = sb.ToString().Substring(0, 6);
                    ulong value = Convert.ToUInt64(firstPart, 16);
                    if (!CalculatedHashes.TryAdd(value, key))
                    {
                        return (key, CalculatedHashes[value]);
                    }
                }
            }
            return (null, null);
        }

        private void Clear()
        {
            CalculatedHashes.Clear();
            GC.Collect();
        }

        public HashGenerator()
        {
            CalculatedHashes = new Dictionary<ulong, string>();
        }
    }
}

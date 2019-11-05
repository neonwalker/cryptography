using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SHACollision
{
    class Program
    {
        static string ByteToString(byte[] value)
        {
            var sb = new StringBuilder(value.Length * 2);

            foreach (byte b in value)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        static void Main(string[] args)
        {
            HashGenerator generator = new HashGenerator();
            while (true)
            {
                var values = generator.Calculate(180000000);
                if (values.Item1 != null)
                {
                    Console.WriteLine(values.Item1);
                    Console.WriteLine(values.Item2);
                    return;
                }
            }
        }
    }
}

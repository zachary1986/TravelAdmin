using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class MD5Opr
    {
        public static string getMd5Hash(string args)
        {
            MD5 md = MD5.Create();

            byte[] data = md.ComputeHash(Encoding.Default.GetBytes(args));

            StringBuilder sb = new StringBuilder();

            foreach (byte b in data)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
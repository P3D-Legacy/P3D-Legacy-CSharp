using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3D.Legacy.Core.Security
{
    public class StringObfuscation
    {
        public static string Obfuscate(string s) => Convert.ToBase64String(Encoding.UTF8.GetBytes(s));

        public static string DeObfuscate(string s) => Encoding.UTF8.GetString(Convert.FromBase64String(s));
    }
}

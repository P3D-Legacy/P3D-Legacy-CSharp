using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace P3D.Legacy.Core.Security
{
    public class Encryption
    {
        public static string EncryptString(string s, string password)
        {
            RijndaelManaged rd = new RijndaelManaged();
            string @out = "";

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] key = md5.ComputeHash(Encoding.UTF8.GetBytes(StringObfuscation.DeObfuscate(password)));

            md5.Clear();
            rd.Key = key;
            rd.GenerateIV();

            byte[] iv = rd.IV;
            MemoryStream ms = new MemoryStream();

            ms.Write(iv, 0, iv.Length);

            CryptoStream cs = new CryptoStream(ms, rd.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(s);

            cs.Write(data, 0, data.Length);
            cs.FlushFinalBlock();

            byte[] encdata = ms.ToArray();
            @out = Convert.ToBase64String(encdata);
            cs.Close();
            rd.Clear();

            return @out;
        }

        public static string DecryptString(string s, string password)
        {
            RijndaelManaged rd = new RijndaelManaged();
            int rijndaelIvLength = 16;
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] key = md5.ComputeHash(Encoding.UTF8.GetBytes(StringObfuscation.DeObfuscate(password)));

            md5.Clear();

            byte[] encdata = Convert.FromBase64String(s);
            MemoryStream ms = new MemoryStream(encdata);
            byte[] iv = new byte[16];

            ms.Read(iv, 0, rijndaelIvLength);
            rd.IV = iv;
            rd.Key = key;

            CryptoStream cs = new CryptoStream(ms, rd.CreateDecryptor(), CryptoStreamMode.Read);

            byte[] data = new byte[Convert.ToInt32(ms.Length - rijndaelIvLength) + 1];
            int i = cs.Read(data, 0, data.Length);

            cs.Close();
            rd.Clear();
            return System.Text.Encoding.UTF8.GetString(data, 0, i);
        }

    }
}

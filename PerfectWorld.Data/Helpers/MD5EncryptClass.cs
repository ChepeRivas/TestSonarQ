using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWorld.Data.Helpers
{
    public class MD5EncryptClass
    {

        public static String MD5Hash(String text)
        {
            MD5CryptoServiceProvider crypto = new MD5CryptoServiceProvider();

            byte[] arr = System.Text.Encoding.UTF8.GetBytes(text);
            arr = crypto.ComputeHash(arr);
            System.Text.StringBuilder Sb = new System.Text.StringBuilder();

            foreach (byte bit in arr)
            {

                Sb.Append(bit.ToString("x2").ToLower());

            }
            String hash = Sb.ToString();
            return hash;
        }

        public static byte[] MD5Hash2(string text)
        {
            MD5CryptoServiceProvider crypto = new MD5CryptoServiceProvider();
            byte[] arr = System.Text.Encoding.UTF8.GetBytes(text);
            arr = crypto.ComputeHash(arr);
            return arr;
        }

        public static String GetEncrypt(string user, string pwd)
        {
            return MD5Hash(user + pwd);
        }

        public static String GetEncrypt_64(string user, string pwd)
        {
            return Convert.ToBase64String(MD5Hash2(user + pwd));
        }


        public static string RandomKey(string Planetext = "")
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var Charsarr = new char[10];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            var resultString = new String(Charsarr);
            resultString = Planetext == "" ? resultString : Planetext + resultString;
            return resultString;
        }
    }
}

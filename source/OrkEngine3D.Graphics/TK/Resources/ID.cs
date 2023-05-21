using System.Security.Cryptography;
using System.Text;
using System;

namespace OrkEngine3D.Graphics.TK.Resources
{
    public struct ID
    {
        public string Hash { get; set; }

        public ID()
        {
            Hash = "";
            Hash = CreateMD5Hash(Guid.NewGuid().ToString());
        }

        private string CreateMD5Hash(string input)
        {
            // Step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
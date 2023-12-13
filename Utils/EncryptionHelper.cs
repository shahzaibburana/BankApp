using System.Security.Cryptography;
using System.Text;

namespace BankApp.Utils;
internal static class EncryptionHelper
{
    public static string CalculateMD5Hash(string input)
    {
        // Create a new instance of the MD5CryptoServiceProvider object.
        using (MD5 md5 = MD5.Create())
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new StringBuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}


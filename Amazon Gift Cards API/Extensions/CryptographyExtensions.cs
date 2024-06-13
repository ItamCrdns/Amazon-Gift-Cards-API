using Amazon.Runtime;
using System.Security.Cryptography;
using System.Text;

namespace Amazon_Gift_Cards_API.Extensions
{
    public static class CryptographyExtensions
    {
        public static byte[] Sha256(this string data)
        {
            return HashAlgorithm.Create("SHA-256").ComputeHash(Encoding.UTF8.GetBytes(data));
        }

        public static byte[] HmacSha256(this string data, byte[] key)
        {
            var algorithm = KeyedHashAlgorithm.Create(SigningAlgorithm.HmacSHA256.ToString().ToUpper());
            algorithm.Key = key;

            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(data));
        }

        public static string ToHex(this byte[] data)
        {
            var builder = new StringBuilder();

            for (var i = 0; i < data.Length; i++)
                builder.Append(data[i].ToString("x2"));

            return builder.ToString();
        }
    }
}

using System.Security.Cryptography;
using System.Text;

namespace MusicManagementsMinimalAPI.Common
{
    public class DataEncryption
    {
        public static string ToMD5(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] encryptdata = md5.ComputeHash(bytes);
            return Convert.ToBase64String(encryptdata);
        }
    }
}

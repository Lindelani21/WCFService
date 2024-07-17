using System.Security.Cryptography;
using System.Text;

namespace AssistantGenesis_Web_App
{

    public static class Secrecy
    {
        private static byte[]? Key = null;
        private static byte[]? IV = null;

        public static string HashPassword(string password)
        {
            SHA1 algorithm = SHA1.Create();
            byte[] byteArray = null;
            byteArray = algorithm.ComputeHash(Encoding.Default.GetBytes(password));
            string hashedPassword = "";
            for (int i = 0; i < byteArray.Length - 1; i++)
            {
                hashedPassword += byteArray[i].ToString("x2");
            }
            return hashedPassword;
        }

        public static string EncodeAES(string text)
        {
            string encoded = string.Empty;
            byte[] bytes;

            Aes aes = Aes.Create();

            if(Key == null || IV == null)
            {
                aes.GenerateKey();
                aes.GenerateIV();

                Key = aes.Key;
                IV = aes.IV;
            }
            
            ICryptoTransform encryptor = aes.CreateEncryptor();

            using (MemoryStream mStream = new MemoryStream())
            using (CryptoStream cStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write))
            using (StreamWriter writer = new StreamWriter(cStream))
            {
                writer.Write(text);

                cStream.FlushFinalBlock();

                bytes = mStream.ToArray();
            }

            encoded = Encoding.UTF8.GetString(bytes);

            return encoded;
        }

        public static string? DecodeAES(string text)
        {
            string decoded = string.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            Aes aes = Aes.Create();

            if(Key == null || IV == null)
            {
                return null;
            }

            aes.Key = Key;
            aes.IV = IV;

            ICryptoTransform decryptor = aes.CreateDecryptor();

            using (MemoryStream mStream = new MemoryStream(bytes))
            using (CryptoStream cStream = new CryptoStream(mStream, decryptor, CryptoStreamMode.Read))
            using (StreamReader reader = new StreamReader(cStream))
            {
                decoded = reader.ReadToEnd();
            }

            return decoded;
        }
    }
}

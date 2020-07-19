using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Tropical.Infrastructure.Util
{
    public static class Cryptography
    {
        private static string str_rgbKey = "sa-l2Oi6";//kS-3ng14
        private static string str_rgbIV = "tr0p1c4L";

        public static string EncryptIt(string encryptData)
        {
            try
            {
                byte[] data = ASCIIEncoding.ASCII.GetBytes(encryptData);
                byte[] rgbKey = ASCIIEncoding.ASCII.GetBytes(str_rgbKey);
                byte[] rgbIV = ASCIIEncoding.ASCII.GetBytes(str_rgbIV);
                //1024-bit encryption
                MemoryStream memoryStream = new MemoryStream(1024);
                DESCryptoServiceProvider desCryptoServiceProvider = new DESCryptoServiceProvider();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, desCryptoServiceProvider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();
                byte[] result = new byte[(int)memoryStream.Position];
                memoryStream.Position = 0;
                memoryStream.Read(result, 0, result.Length);
                cryptoStream.Close();
                memoryStream.Dispose();
                return Convert.ToBase64String(result);
            }
            catch 
            {
                return null;
            }
        }

        public static string DecryptIt(string toDecrypt)
        {
            string decrypted = string.Empty;
            try
            {
                    byte[] data = System.Convert.FromBase64String(toDecrypt);
                    byte[] rgbKey = System.Text.ASCIIEncoding.ASCII.GetBytes(str_rgbKey);
                    byte[] rgbIV = System.Text.ASCIIEncoding.ASCII.GetBytes(str_rgbIV);
                    //1024-bit decryption
                    MemoryStream memoryStream = new MemoryStream(data.Length);
                    DESCryptoServiceProvider desCryptoServiceProvider = new DESCryptoServiceProvider();
                    ICryptoTransform x = desCryptoServiceProvider.CreateDecryptor(rgbKey, rgbIV);
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, x, CryptoStreamMode.Read);
                    memoryStream.Write(data, 0, data.Length);
                    memoryStream.Position = 0;
                    decrypted = new StreamReader(cryptoStream).ReadToEnd();
                    cryptoStream.Close();
                    memoryStream.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return decrypted;
        }
    }    
}

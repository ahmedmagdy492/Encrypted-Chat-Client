using EncChatCommonLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EncChatCommonLib.Services
{
    public class EncryptionService
    {
        public byte[] Encrypt(byte[] data, string publicKeyXml)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKeyXml);

                byte[] encryptedData = rsa.Encrypt(data, true);

                return encryptedData;
            }
        }

        public string Decrypt(byte[] encryptedData, string privateKeyXml)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKeyXml);

                byte[] decryptedData = rsa.Decrypt(encryptedData, true);

                string originalData = Encoding.UTF8.GetString(decryptedData);

                return originalData;
            }
        }

        public byte[] EncryptSymmetericlly(string data, byte[] key)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = key;
                aesAlgorithm.IV = SharedConstants.IV;

                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor();

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(data);
                        }
                        return ms.ToArray();
                    }
                }
            }
        }

        public string DecryptSymmetericlly(byte[] data, byte[] key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = SharedConstants.IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(data))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}

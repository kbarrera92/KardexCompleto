using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    public class Aes256Helper
    {
        public static string Encriptar(string plainText, string key, string iv)
        {
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] ivBytes = Convert.FromBase64String(iv);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                        cs.Write(plainBytes, 0, plainBytes.Length);
                        cs.FlushFinalBlock();
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public static string Desencriptar(string encryptedText, string key, string iv)
        {
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] ivBytes = Convert.FromBase64String(iv);
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream ms = new MemoryStream(cipherTextBytes))
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        byte[] plainBytes = new byte[cipherTextBytes.Length];
                        int decryptedCount = cs.Read(plainBytes, 0, plainBytes.Length);
                        return Encoding.UTF8.GetString(plainBytes, 0, decryptedCount);
                    }
                }
            }
        }

        public void EncryptFile(string inputFilePath, string outputFilePath, string key, string iv)
        {
            try
            {
                // Leer el contenido del archivo CSV
                string plainText = File.ReadAllText(inputFilePath);

                // Encriptar el contenido
                string encryptedText = Encriptar(plainText, key, iv);

                // Guardar el contenido encriptado en un nuevo archivo
                File.WriteAllText(outputFilePath, encryptedText);

                Console.WriteLine("Archivo encriptado guardado en: " + outputFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al encriptar el archivo: " + ex.Message);
            }
        }

        // Método para desencriptar un archivo
        public void DecryptFile(string inputFilePath, string outputFilePath, string key, string iv)
        {
            try
            {
                // Leer el contenido encriptado del archivo
                string encryptedText = File.ReadAllText(inputFilePath);

                // Desencriptar el contenido
                string decryptedText = Desencriptar(encryptedText, key, iv);

                // Guardar el contenido desencriptado en un nuevo archivo
                File.WriteAllText(outputFilePath, decryptedText);

                Console.WriteLine("Archivo desencriptado guardado en: " + outputFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al desencriptar el archivo: " + ex.Message);
            }
        }
    }
}

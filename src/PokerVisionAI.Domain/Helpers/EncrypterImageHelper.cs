using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PokerVisionAI.Domain.Helpers
{
    public static class EncrypterImageHelper
    {
        private static readonly Encoding encoding = Encoding.UTF8;

        public static string Encrypt(string plainText, byte[] key, byte[] iv)
        {
            // Instantiate a new Aes object to perform string symmetric encryption
            Aes encryptor = Aes.Create();

            encryptor.Mode = CipherMode.CBC;

            // Set key and IV
            byte[] aesKey = new byte[32];
            Array.Copy(key, 0, aesKey, 0, 32);
            encryptor.Key = aesKey;
            encryptor.IV = iv;

            // Instantiate a new MemoryStream object to contain the encrypted bytes
            MemoryStream memoryStream = new MemoryStream();

            // Instantiate a new encryptor from our Aes object
            ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();

            // Instantiate a new CryptoStream object to process the data and write it to the 
            // memory stream
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);

            // Convert the plainText string into a byte array
            byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);

            // Encrypt the input plaintext string
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);

            // Complete the encryption process
            cryptoStream.FlushFinalBlock();

            // Convert the encrypted data from a MemoryStream to a byte array
            byte[] cipherBytes = memoryStream.ToArray();

            // Close both the MemoryStream and the CryptoStream
            memoryStream.Close();
            cryptoStream.Close();

            // Convert the encrypted byte array to a base64 encoded string
            string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);

            // Return the encrypted data as a string
            return cipherText;
        }

        public static string Decrypt(string cipherText, byte[] key, byte[] iv)
        {
            // Instantiate a new Aes object to perform string symmetric encryption
            Aes encryptor = Aes.Create();

            encryptor.Mode = CipherMode.CBC;

            // Set key and IV
            byte[] aesKey = new byte[32];
            Array.Copy(key, 0, aesKey, 0, 32);
            encryptor.Key = aesKey;
            encryptor.IV = iv;

            // Instantiate a new MemoryStream object to contain the encrypted bytes
            MemoryStream memoryStream = new MemoryStream();

            // Instantiate a new encryptor from our Aes object
            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();

            // Instantiate a new CryptoStream object to process the data and write it to the 
            // memory stream
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);

            // Will contain decrypted plaintext
            string plainText = String.Empty;

            try
            {
                // Convert the ciphertext string into a byte array
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                // Decrypt the input ciphertext string
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);

                // Complete the decryption process
                cryptoStream.FlushFinalBlock();

                // Convert the decrypted data from a MemoryStream to a byte array
                byte[] plainBytes = memoryStream.ToArray();

                // Convert the decrypted byte array to string
                plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
            }
            finally
            {
                // Close both the MemoryStream and the CryptoStream
                memoryStream.Close();
                cryptoStream.Close();
            }

            // Return the decrypted data as a string
            return plainText;
        }

        public static async Task<string> GetImageEncrypted(Image imagen, string secret)
        {
            // Verificamos que la imagen tenga una fuente
            if (imagen?.Source == null)
                throw new ArgumentNullException(nameof(imagen));

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Obtenemos el stream de la imagen según su tipo de fuente
                    if (imagen.Source is FileImageSource fileImageSource)
                    {
                        using (var fileStream = File.OpenRead(fileImageSource.File))
                        {
                            await fileStream.CopyToAsync(ms);
                        }
                    }
                    else if (imagen.Source is StreamImageSource streamImageSource)
                    {
                        using (var stream = await streamImageSource.Stream(CancellationToken.None))
                        {
                            if (stream != null)
                                await stream.CopyToAsync(ms);
                            else
                                throw new Exception("No se pudo obtener el stream de la imagen");
                        }
                    }
                    else if (imagen.Source is UriImageSource uriImageSource)
                    {
                        using (var client = new HttpClient())
                        {
                            var imageByte = await client.GetByteArrayAsync(uriImageSource.Uri);
                            await ms.WriteAsync(imageByte, 0, imageByte.Length);
                        }
                    }
                    else
                    {
                        throw new Exception("Tipo de fuente de imagen no soportado");
                    }

                    byte[] imageBytes = ms.ToArray();
                    var base64String = Convert.ToBase64String(imageBytes);

                    using (SHA256 mySHA256 = SHA256.Create())
                    {
                        byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(secret));
                        byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
                        return Encrypt(base64String, key, iv);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al procesar la imagen: {ex.Message}", ex);
            }
        }

        public static Image GetImageDecrypted(string base64String, string secret)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(secret));
                byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
                string decrypted = Decrypt(base64String, key, iv);
                byte[] byteImage = Convert.FromBase64String(decrypted);

                return new Image
                {
                    Source = ImageSource.FromStream(() => new MemoryStream(byteImage))
                };
            }
        }
    }
}

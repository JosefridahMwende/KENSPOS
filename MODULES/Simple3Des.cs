using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic; // Install-Package Microsoft.VisualBasic

namespace TIMS.MODULES
{
    public partial class Simple3Des
    {
        private TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();

        private byte[] TruncateHash(string key, int length)
        {

            var sha1 = new SHA1CryptoServiceProvider();

            // Hash the key.
            var keyBytes = Encoding.Unicode.GetBytes(key);
            var hash = sha1.ComputeHash(keyBytes);

            // Truncate or pad the hash.
            Array.Resize(ref hash, length);
            return hash;
        }

        public Simple3Des(string key)
        {
            // Initialize the crypto provider.
            TripleDes.Key = TruncateHash(key, TripleDes.KeySize / 8);
            TripleDes.IV = TruncateHash("", TripleDes.BlockSize / 8);
        }

        public string EncryptData(string plaintext)
        {

            // Convert the plaintext string to a byte array.
            var plaintextBytes = Encoding.Unicode.GetBytes(plaintext);

            // Create the stream.
            var ms = new MemoryStream();
            // Create the encoder to write to the stream.
            var encStream = new CryptoStream(ms, TripleDes.CreateEncryptor(), CryptoStreamMode.Write);

            // Use the crypto stream to write the byte array to the stream.
            encStream.Write(plaintextBytes, 0, plaintextBytes.Length);
            encStream.FlushFinalBlock();

            // Convert the encrypted stream to a printable string.
            return Convert.ToBase64String(ms.ToArray());
        }

        public string DecryptData(string encryptedtext)
        {
            string DecryptDataRet = "";

            try
            {
                // Convert the encrypted text string to a byte array.
                var encryptedBytes = Convert.FromBase64String(encryptedtext);

                // Create the stream.
                var ms = new MemoryStream();
                // Create the decoder to write to the stream.
                var decStream = new CryptoStream(ms, TripleDes.CreateDecryptor(), CryptoStreamMode.Write);

                // Use the crypto stream to write the byte array to the stream.
                decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                decStream.FlushFinalBlock();

                // Convert the plaintext stream to a string.
                DecryptDataRet = Encoding.Unicode.GetString(ms.ToArray());
                return DecryptDataRet;
            }

            catch (Exception ex)
            {
                MessageBox.Show("The encrypted data has been corrupted");
                DecryptDataRet = "ERROR";
                return DecryptDataRet;
            }

        }

    }
}



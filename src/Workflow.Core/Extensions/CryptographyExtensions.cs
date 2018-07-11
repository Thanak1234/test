using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Workflow {

    public static class CryptographyExtensions {

        public static string Encrypt(this string text) {

            if (string.IsNullOrEmpty(text))
                throw new InvalidDataException("String is empty.");

            string passwordKey = GetSecretKey();

            if (string.IsNullOrEmpty(passwordKey))
                throw new InvalidDataException("Encrypt Key is empty.");

            byte[] baPwd = Encoding.UTF8.GetBytes(passwordKey);
            byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);
            byte[] baText = Encoding.UTF8.GetBytes(text);
            byte[] baSalt = GetRandomBytes();
            byte[] baEncrypted = new byte[baSalt.Length + baText.Length];
            for (int i = 0; i < baSalt.Length; i++)
                baEncrypted[i] = baSalt[i];
            for (int i = 0; i < baText.Length; i++)
                baEncrypted[i + baSalt.Length] = baText[i];
            baEncrypted = AESEncrypt(baEncrypted, baPwdHash);
            string result = Convert.ToBase64String(baEncrypted);
            return result;
        }

        public static string Decrypt(this string text) {

            if (string.IsNullOrEmpty(text))
                throw new InvalidDataException("String is empty.");

            string passwordKey = GetSecretKey();
            if (string.IsNullOrEmpty(passwordKey))
                throw new InvalidDataException("Encrypt Key is empty.");

            byte[] baPwd = Encoding.UTF8.GetBytes(passwordKey);
            byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);
            byte[] baText = Convert.FromBase64String(text);
            byte[] baDecrypted = AESDecrypt(baText, baPwdHash);
            int saltLength = GetSaltLength();
            byte[] baResult = new byte[baDecrypted.Length - saltLength];
            for (int i = 0; i < baResult.Length; i++)
                baResult[i] = baDecrypted[i + saltLength];
            string result = Encoding.UTF8.GetString(baResult);
            return result;
        }

        private static byte[] AESEncrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes) {
            byte[] encryptedBytes = null;
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (MemoryStream ms = new MemoryStream()) {
                using (RijndaelManaged AES = new RijndaelManaged()) {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write)) {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }

        private static byte[] AESDecrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes) {
            byte[] decryptedBytes = null;
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (MemoryStream ms = new MemoryStream()) {
                using (RijndaelManaged AES = new RijndaelManaged()) {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write)) {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }
            return decryptedBytes;
        }

        public static byte[] GetRandomBytes() {
            int saltLength = GetSaltLength();
            byte[] ba = new byte[saltLength];
            RNGCryptoServiceProvider.Create().GetBytes(ba);
            return ba;
        }

        public static int GetSaltLength() {
            return 8;
        }

        private static string GetSecretKey() {
            //StringBuilder builder = new StringBuilder();
            //String query = "SELECT * FROM Win32_BIOS";
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            //foreach (ManagementObject item in searcher.Get()) {
            //    Object obj = item["Manufacturer"];
            //    builder.Append(Convert.ToString(obj));
            //    builder.Append(':');
            //    obj = item["SerialNumber"];
            //    builder.Append(Convert.ToString(obj));
            //}
            //return builder.ToString();

            return "35283ee6fa843b6c2412bb7ec7afed79";
        }
    }
}

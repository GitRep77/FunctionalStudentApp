using System;
using System.Security.Cryptography;
using System.Text;
using LanguageExt;
using static LanguageExt.Prelude;
using static StudentApp.Prelude;

namespace StudentApp
{
    public static class Cryptography
    {
        #region AES Library
        private const string IV = "qo1lc3sjd8zpt9cx";  // 16 chars = 128 bytes
        private const string Key = "ow7dxys8glfor9tnc2ansdfo1etkfjcv";   // 32 chars = 256 bytes

        public static class AES
        {
            private static Func<string, string, AesCryptoServiceProvider> EncdecBase = (IV, Key) => new AesCryptoServiceProvider().With(o => {
                o.BlockSize = 128;
                o.KeySize = 256;
                o.Key = Encoding.ASCII.GetBytes(Key);
                o.IV = Encoding.ASCII.GetBytes(IV);
                o.Padding = PaddingMode.PKCS7;
                o.Mode = CipherMode.CBC;
            });

            private static Either<string, string> Encrypt(AesCryptoServiceProvider encdec, string decrypted)
            {
                return TryEitherLog<string>(() => {
                    var textbytes = Encoding.ASCII.GetBytes(decrypted);
                    var icrypt = encdec.CreateEncryptor(encdec.Key, encdec.IV);
                    var enc = icrypt.TransformFinalBlock(textbytes, 0, textbytes.Length);
                    icrypt.Dispose();
                    return Right(Convert.ToBase64String(enc));
                });
            }

            private static Either<string, string> Decrypt(AesCryptoServiceProvider encdec, string encrypted)
            {
                return TryEitherLog<string>(() => {
                    var encbytes = Convert.FromBase64String(encrypted);
                    var icrypt = encdec.CreateDecryptor(encdec.Key, encdec.IV);
                    var dec = icrypt.TransformFinalBlock(encbytes, 0, encbytes.Length);
                    icrypt.Dispose();
                    return Right(Encoding.ASCII.GetString(dec));
                });
            }

            public static Either<string, string> Encrypt(string decrypted, string iv = IV, string key = Key)
            {
                return Encrypt(AES.EncdecBase(IV, Key), decrypted);
            }

            public static Either<string, string> Decrypt(string encrypted, string iv = IV, string key = Key)
            {
                return Decrypt(AES.EncdecBase(IV, Key), encrypted);
            }
        }

        public static Either<string, string> EncryptAES(this string value, string iv = IV, string key = Key)
        {
            return AES.Encrypt(value, iv, key);
        }

        public static Either<string, string> DecryptAES(this string value, string iv = IV, string key = Key)
        {
            return AES.Decrypt(value, iv, key);
        }
        #endregion
    }
}

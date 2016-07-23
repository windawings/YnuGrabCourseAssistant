using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ICSharpCode.SharpZipLib.BZip2;
namespace 云大选课辅助工具
{
    internal static class ClassEncrypt
    {
        /// <summary> AES密钥.</summary>
        private const string Key = "32 bytes key";
        /// <summary> 密码模式初始向量.</summary>
        private const string Iv = "16 bytes IV";
        /// <summary> 解密器.</summary>
        private static readonly ICryptoTransform Decrptor;
        /// <summary> 加密器.</summary>
        private static readonly ICryptoTransform Encryptor;
        /// <summary> 加密管理.</summary>
        private static readonly RijndaelManaged RijndaelManaged = new RijndaelManaged();

        /// <summary> 静态初始化构造函数.</summary>
        /// <remarks> windawings, 11/30/2015.</remarks>
        static ClassEncrypt()
        {
            RijndaelManaged.KeySize = 256;
            RijndaelManaged.Key = Encoding.UTF8.GetBytes(Key);
            RijndaelManaged.IV = Encoding.UTF8.GetBytes(Iv);
            RijndaelManaged.Mode = CipherMode.CBC;
            RijndaelManaged.Padding = PaddingMode.ISO10126;
            Encryptor = RijndaelManaged.CreateEncryptor();
            Decrptor = RijndaelManaged.CreateDecryptor();
        }

        /// <summary> 解压解密.</summary>
        /// <remarks> windawings, 11/29/2015.</remarks>
        /// <param name="compressBytes"> The compress in bytes.</param>
        /// <returns> A string.</returns>
        public static string Decrypt(byte[] compressBytes)
        {
            try
            {
                var base64Str = "";
                using (var inStream = new MemoryStream(compressBytes))
                {
                    using (var outStream = new MemoryStream())
                    {
                        BZip2.Decompress(inStream, outStream, false);
                        var base64Bytes = outStream.ToArray();
                        base64Str = Encoding.UTF8.GetString(base64Bytes);
                    }
                }
                var encodeBytes = Convert.FromBase64String(base64Str);
                var decodeBytes = Decrptor.TransformFinalBlock(encodeBytes, 0, encodeBytes.Length);
                return Encoding.UTF8.GetString(decodeBytes);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> 文件解压解密.</summary>
        /// <remarks> windawings, 11/29/2015.</remarks>
        /// <param name="fileName"> 待解密文件全路径.</param>
        /// <returns> 解密字串.</returns>
        public static string DecryptReturn(string fileName)
        {
            try
            {
                using (var fileStream = new FileStream(fileName, FileMode.Open))
                {
                    using (var binaryReader = new BinaryReader(fileStream, Encoding.UTF8))
                    {
                        var encodeBytes = new byte[binaryReader.BaseStream.Length];
                        binaryReader.Read(encodeBytes, 0, encodeBytes.Length);
                        return Decrypt(encodeBytes);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> 加密压缩.</summary>
        /// <remarks> windawings, 11/29/2015.</remarks>
        /// <param name="plainStr"> The encrypt string.</param>
        /// <returns> A byte[].</returns>
        public static byte[] Encrypt(string plainStr)
        {
            try
            {
                var plainBytes = Encoding.UTF8.GetBytes(plainStr);
                var base64Str = Convert.ToBase64String(Encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length));
                var base64Bytes = Encoding.UTF8.GetBytes(base64Str);
                using (var inStream = new MemoryStream())
                {
                    using (var outStream = new MemoryStream(base64Bytes))
                    {
                        BZip2.Compress(outStream, inStream, false, 9);
                        return inStream.ToArray();
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> 读取公钥弓箭.</summary>
        /// <remarks> windawings, 12/1/2015.</remarks>
        /// <param name="keyFileName"> Filename of the key file.</param>
        /// <returns> An array of byte.</returns>
        public static byte[] ReadKeyPair(string keyFileName)
        {
            try
            {
                using (var fileReader = new FileStream(keyFileName, FileMode.Open))
                {
                    using (var reader = new BinaryReader(fileReader, Encoding.UTF8))
                    {
                        var keyBytes = new byte[reader.BaseStream.Length];
                        reader.Read(keyBytes, 0, keyBytes.Length);

                        return keyBytes;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary> 加密压缩存储.</summary>
        /// <remarks> windawings, 11/29/2015.</remarks>
        /// <param name="plainStr"> 明文字串.</param>
        /// <param name="fileName">   存储路径.</param>
        /// <returns> 是否加密存储成功.</returns>
        public static bool EncryptSave(string plainStr, string fileName)
        {
            try
            {
                using (var fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    using (var binaryWriter = new BinaryWriter(fileStream, Encoding.UTF8))
                    {
                        binaryWriter.Write(Encrypt(plainStr));
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}


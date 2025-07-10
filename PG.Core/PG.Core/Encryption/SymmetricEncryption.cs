using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace PG.Core.Encryption
{
    public class SymmetricEncryption
    {
        private static bool m_ProtectKey = false;
        private static string m_AlgorithmName = "Rijindael";
        private static bool m_AutoKey = false;
        private static bool m_IsPassword = false;
        private static string m_Password;
        private static byte[] m_Key;

        public static bool ProtectKey
        {
            get { return m_ProtectKey; }
            set { m_ProtectKey = value; }
        }
        public static string AlgorithmName
        {
            /////
            ///valid name: DES,TripleDES,Rijndael ,RC2 
            get { return m_AlgorithmName; }
            set { m_AlgorithmName = value; }
        }

        public static byte[] Key
        {
            get { return m_Key; }
            set { m_Key = value; }
        }

        public static byte[] IV
        {
            get { return m_Key; }
            set { m_Key = value; }
        }

        public static bool AutoKey
        {
            get { return m_AutoKey; }
            set { m_AutoKey = value; }
        }

        private static Rfc2898DeriveBytes GetPasswordBytes(string password)
        {
            return new Rfc2898DeriveBytes(password, new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 0x3e8);
        }

        public static byte[] GenerateKey()
        {
            byte[] vKey;
            SymmetricAlgorithm SymAlg = SymmetricAlgorithm.Create(AlgorithmName);

            if (AutoKey)
            {
                if (m_IsPassword)
                {
                    //if password based, generate specific key for every time
                    Rfc2898DeriveBytes passwordBytes = GetPasswordBytes(m_Password);
                    vKey = passwordBytes.GetBytes(0x10);
                }
                else
                {
                    ///
                    vKey = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
                }
            }
            else
            {

                SymAlg.GenerateKey();
                vKey = SymAlg.Key;

                if (ProtectKey)
                {
                    ///encrypt key with machine key
                    vKey = ProtectedData.Protect(vKey, null, DataProtectionScope.LocalMachine);
                }

            }

            return vKey;
        }

        public static void WriteKey(byte[] key, string targetFile)
        {
            //saving key to file: i.e. key.config
            try
            {
                FileStream fs = new FileStream(targetFile, FileMode.Create);
                fs.Write(key, 0, key.Length);
                fs.Close();
            }
            finally
            {

            }
        }

        public static byte[] ReadKey(string keyFile)
        {
            /////read key from file: i.e. key.config

            byte[] key;

            FileStream fs = new FileStream(keyFile, FileMode.Open);

            key = new byte[fs.Length];
            fs.Read(key, 0, (int)fs.Length);

            if (ProtectKey)
            {
                key = ProtectedData.Unprotect(key, null, DataProtectionScope.LocalMachine);
            }
            fs.Close();

            return key;
        }

        public static byte[] EncryptData(byte[] data)
        {

            SymmetricAlgorithm SymAlg = SymmetricAlgorithm.Create(AlgorithmName);

            if (AutoKey)
            {
                SymAlg.Key = GenerateKey();
            }
            else
            {
                SymAlg.Key = Key;
            }


            MemoryStream Target = new MemoryStream();


            ///now generate IV(initialize vector)
            ///if password base then fixed IV else random IV
            if (m_IsPassword)
            {
                SymAlg.IV = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
            }
            else
            {
                SymAlg.GenerateIV();
            }

            ///Write IV To Memory Stream
            Target.Write(SymAlg.IV, 0, SymAlg.IV.Length);

            CryptoStream cs = new CryptoStream(Target, SymAlg.CreateEncryptor(), CryptoStreamMode.Write);


            cs.Write(data, 0, data.Length);

            cs.FlushFinalBlock();
            cs.Close();

            return Target.ToArray();
        }

        public static string EncryptData(string data)
        {
            m_IsPassword = false;
            return Convert.ToBase64String(EncryptData(Encoding.UTF8.GetBytes(data)));
        }

        public static string EncryptData(string data, string password)
        {
            m_IsPassword = true;
            m_Password = password;
            return Convert.ToBase64String(EncryptData(Encoding.UTF8.GetBytes(data)));
        }

        public static byte[] DecryptData(byte[] data)
        {
            SymmetricAlgorithm SymAlg = SymmetricAlgorithm.Create(AlgorithmName);
            //SymAlg.Key= Key;

            if (AutoKey)
            {
                SymAlg.Key = GenerateKey();
            }
            else
            {
                SymAlg.Key = Key;
            }

            MemoryStream Target = new MemoryStream();
            int ReadPos = 0;

            //SymAlg.GenerateIV();
            byte[] IV = new byte[SymAlg.IV.Length];

            Array.Copy(data, IV, IV.Length);
            SymAlg.IV = IV;

            ReadPos += SymAlg.IV.Length;

            CryptoStream cs = new CryptoStream(Target, SymAlg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(data, ReadPos, data.Length - ReadPos);
            cs.FlushFinalBlock();

            //return Encoding.UTF8.GetString(Target.ToArray());
            return Target.ToArray();
        }
        public static string DecryptData(string data)
        {
            m_IsPassword = false;
            return Encoding.UTF8.GetString(DecryptData(Convert.FromBase64String(data)));
        }
        public static string DecryptData(string data, string password)
        {
            m_IsPassword = true;
            m_Password = password;
            return Encoding.UTF8.GetString(DecryptData(Convert.FromBase64String(data)));
        }
    }
}

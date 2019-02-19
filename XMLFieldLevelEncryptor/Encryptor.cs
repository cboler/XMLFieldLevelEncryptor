using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.DataProtection;

namespace XMLFieldLevelEncryptor
{
    public enum EncryptionType
    {
        AES,
        DES
    };

    [DataContract]
    public partial class Configuration
    {
        [DataMember]
        public XmlDocument XMLDocument { get; set; }
        [DataMember]
        public List<string> FieldsToEncrypt { get; set; }
        [DataMember]
        public bool UseMultipleKeys { get; set; }
        [DataMember]
        public EncryptionType EncryptionType { get; set; }

        public Configuration() { }
    }

    [DataContract]
    public partial class XMLTransmission
    {
        [DataMember]
        public XmlDocument XMLDocument { get; set; }
        [DataMember]
        public Dictionary<string, string> EncryptionKeys { get; set; }

        public XMLTransmission() { }
    }

    public partial class Encryptor : IDisposable, IDataProtector
    {
        public Configuration Configuration { get; set; }
        public string Purpose { get; set; }

        public async Task<XMLTransmission> EncryptAsync(Configuration configuration)
        {
            return Encrypt(configuration);
        }

        public static XMLTransmission Encrypt(Configuration configuration)
        {
            XmlDocument xmlDocument = configuration.XMLDocument;
            Dictionary<string, string> encryptionKeys = new Dictionary<string, string>();

            foreach (var entry in configuration.FieldsToEncrypt)
            {

            }

            return new XMLTransmission();
        }

        public static XmlDocument Decrypt(XMLTransmission xmlTransmission)
        {
            foreach (var entry in xmlTransmission.EncryptionKeys)
            {

            }

            return new XmlDocument();
        }

        public async Task<XmlDocument> DecryptAsync(XMLTransmission xmlTransmission)
        {
            return Decrypt(xmlTransmission);
        }

        public void Dispose()
        {
            Configuration = null;
        }

        public byte[] Protect(byte[] plaintext)
        {
            byte[] encrypted = null;

            if (Configuration.EncryptionType.Equals(EncryptionType.AES))
            {
                using (var aes = Aes.Create())
                {
                    aes.GenerateKey();
                    aes.GenerateIV();

                    // Create an encryptor to perform the stream transform.
                    var encryptor = aes.CreateEncryptor();

                    // Create the streams used for encryption.
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                //Write all data to the stream.
                                swEncrypt.Write(plaintext);
                            }
                            encrypted = msEncrypt.ToArray();
                        }

                    }
                }
            }
            else
            {
                using (var des = TripleDES.Create())
                {
                    des.GenerateKey();
                    des.GenerateIV();

                    // Create an encryptor to perform the stream transform.
                    var encryptor = des.CreateEncryptor();

                    // Create the streams used for encryption.
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                //Write all data to the stream.
                                swEncrypt.Write(plaintext);
                            }
                            encrypted = msEncrypt.ToArray();
                        }

                    }
                }
            }

            return encrypted;
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            var plaintext = "";

            if (Configuration.EncryptionType.Equals(EncryptionType.AES))
            {
                using (var aes = Aes.Create())
                {
                    aes.GenerateKey();
                    aes.GenerateIV();

                    // Create a decryptor to perform the stream transform.
                    var decryptor = aes.CreateDecryptor();

                    // Create the streams used for decryption.
                    using (MemoryStream msDecrypt = new MemoryStream(protectedData))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {

                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            else
            {
                using (var des = TripleDES.Create())
                {
                    des.GenerateKey();
                    des.GenerateIV();

                    // Create a decryptor to perform the stream transform.
                    var decryptor = des.CreateDecryptor();

                    // Create the streams used for decryption.
                    using (MemoryStream msDecrypt = new MemoryStream(protectedData))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {

                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }

            return System.Text.Encoding.UTF8.GetBytes(plaintext);
        }

        public IDataProtector CreateProtector(string purpose)
        {
            return new Encryptor(purpose);
        }

        Encryptor()
        {

        }

        Encryptor(string purpose)
        {
            Purpose = purpose;
        }

        ~Encryptor()
        {
            Dispose();
        }
    }
}

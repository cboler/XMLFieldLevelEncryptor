using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
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
            if (Configuration.EncryptionType.Equals(EncryptionType.AES))
            {
                using (var aes = System.Security.Cryptography.Aes.Create())
                {
                    
                }
            }
            else
            {
                using (var des = System.Security.Cryptography.TripleDES.Create())
                {

                }
            }

            return null;
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            if (Configuration.EncryptionType.Equals(EncryptionType.AES))
            {
                using (var aes = System.Security.Cryptography.Aes.Create())
                {

                }
            }
            else
            {
                using (var des = System.Security.Cryptography.TripleDES.Create())
                {

                }
            }

            return null;
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

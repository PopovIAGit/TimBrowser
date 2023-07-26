using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Security.Cryptography;

namespace TpeMaker.Services
{
    public class Cryptographer
    {
        private delegate bool CryptXmlDocumentAsyncCaller(string inFilePath, string outFilePath);
        
        public IAsyncResult BeginCryptXmlDocument(string inFilePath, string outFilePath, 
            AsyncCallback asyncCallback)
        {
            CryptXmlDocumentAsyncCaller caller = new CryptXmlDocumentAsyncCaller(CryptXmlDocument);

            return caller.BeginInvoke(inFilePath, outFilePath, asyncCallback, null);
        }

        public bool EndCryptXmlDocument(IAsyncResult result)
        {
            System.Runtime.Remoting.Messaging.AsyncResult r =
                (System.Runtime.Remoting.Messaging.AsyncResult)result;

            CryptXmlDocumentAsyncCaller caller = (CryptXmlDocumentAsyncCaller)r.AsyncDelegate;

            return caller.EndInvoke(result);
        }

        /*
        public bool TestCryptXmlDocument(string inFilePath, string outFilePath)
        {
            CryptXmlDocumentAsyncCaller caller = new CryptXmlDocumentAsyncCaller(CryptXmlDocument);

            IAsyncResult result = caller.BeginInvoke(inFilePath, outFilePath, null, null);

            result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(20));

            return caller.EndInvoke(result);
        }        
         * */
        
        public bool CryptXmlDocument(string inFilePath, string outFilePath)
        {
            FileStream file = null;
            CryptoStream cs = null;
            TripleDESCryptoServiceProvider tdes = null;

            bool error = false;

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(inFilePath);

                file = File.Open(outFilePath, FileMode.Create, FileAccess.Write);

                try
                {
                    tdes = new TripleDESCryptoServiceProvider()
                    {
                        IV = Helper.Key.CS_IV,
                        Key = Helper.Key.CS_KEY
                    };
                    cs = new CryptoStream(file, tdes.CreateEncryptor(), CryptoStreamMode.Write);

                    xmlDoc.Save(cs);
                }
                catch 
                {
                    error = true;
                }
                finally
                {
                    if (tdes != null)
                        tdes.Clear();
                    if (cs != null)
                        cs.Close();
                }
            }
            catch 
            {
                error = true;
            }
            finally
            {
                if (file != null)
                    file.Close();
            }

            return error;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Xml;

namespace TpeParameters.File
{
    public class TpeCryptor
    {
        public XmlDocument DecryptTpeFromTpeFile(FileStream file)
        {
            XmlDocument xmlDocument = null;
            CryptoStream cs = null;
            TripleDESCryptoServiceProvider tdes = null;

            try
            {
                tdes = new TripleDESCryptoServiceProvider()
                {
                    IV = Helpers.Constants.CsIV,
                    Key = Helpers.Constants.CsKey
                };

                cs = new CryptoStream(file, tdes.CreateDecryptor(), CryptoStreamMode.Read);

                xmlDocument = new XmlDocument();
                xmlDocument.Load(cs);
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                //SendTpeDecryptorMessage("Ошибка 1", true);
                //SendTpeDecryptorMessage(DataManagerRes.TpeDecodeErr, true);
            }
            finally
            {
                if (tdes != null)
                    tdes.Clear();

                if (cs != null)
                    cs.Close();

                if (file != null)
                    file.Close();
            }

            return xmlDocument;
        }

        public void EncryptXmlDocumentToStream(XmlDocument xmlDoc, string path)
        {
            FileStream file = null;
            CryptoStream cs = null;
            TripleDESCryptoServiceProvider tdes = null;

            bool error = false;

            try
            {
                try
                {
                    tdes = new TripleDESCryptoServiceProvider()
                    {
                        IV = Helpers.Constants.CsIV,
                        Key = Helpers.Constants.CsKey
                    };

                    file = System.IO.File.Open(path, FileMode.Create, FileAccess.Write);

                    cs = new CryptoStream(file, tdes.CreateEncryptor(), CryptoStreamMode.Write);                    

                    //!!!
                    xmlDoc.Save(cs);
                }
                catch (Exception ex)
                {
                    string mes = ex.Message;
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
        }

        /*
        public bool EncryptXmlDocumentDebug(string inFilePath, string outFilePath)
        {
            FileStream file = null;
            CryptoStream cs = null;
            TripleDESCryptoServiceProvider tdes = null;

            bool error = false;

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(inFilePath);

                file = System.IO.File.Open(outFilePath, FileMode.Create, FileAccess.Write);

                try
                {
                    tdes = new TripleDESCryptoServiceProvider()
                    {
                        IV = Helpers.Constants.CsIV,
                        Key = Helpers.Constants.CsKey
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
        */
    }
}

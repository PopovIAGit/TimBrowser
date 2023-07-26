using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeBuilder.Model;
using TpeParameters.Model;
using TpeParameters.Parser;
using TpeParameters.Helpers;
using TpeParameters.File;
using System.Xml;

namespace TpeBuilder.Services
{
    public class FileService
    {

        public TpeTable LoadTpe(string path)
        {
            FileOperations fileOp = new FileOperations();
            var stream = fileOp.LoadFileStream(path);
            TpeCryptor cryptor = new TpeCryptor();
            var xmlDoc = cryptor.DecryptTpeFromTpeFile(stream);
            TpeXmlParser xmlParser = new TpeXmlParser();
            TableItem table = xmlParser.Parse(xmlDoc);

            TpeTable tpeTable = Services.Mapper.MapTableFromTpe(table);

            return tpeTable;
        }

        public void SaveTableToXml(TpeTable tpeTable)
        {
            TableItem tableItem = Services.Mapper.MapTableToTpe(tpeTable);

            if (tableItem == null)
                return;

            XmlBuilder xmlBuilder = new XmlBuilder();

            var xmlDoc = xmlBuilder.BuildTpeXmlTable(tableItem);

            string filePath = Constants.FilePath + "//" + GetFileName(tableItem) + ".xml";

            xmlDoc.Save(filePath);
        }

        public void SaveTableToTpe(TpeTable tpeTable)
        {
            TableItem tableItem = Services.Mapper.MapTableToTpe(tpeTable);

            if (tableItem == null)
                return;

            XmlBuilder xmlBuilder = new XmlBuilder();

            var xmlDoc = xmlBuilder.BuildTpeXmlTable(tableItem);

            TpeCryptor cryptor = new TpeCryptor();
            string filePath = Constants.FilePath + "//" + GetFileName(tableItem) + Constants.TpeFileExtention;

            cryptor.EncryptXmlDocumentToStream(xmlDoc, filePath);

        }

        private string GetFileName(TableItem tableItem)
        {
            string fileName = tableItem.DeviceName + " " + tableItem.DeviceId + " (" + tableItem.FirmwareVersion + ")";

            return fileName;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeParameters.File;
using TpeParameters.Model;
using TpeParameters.Parser;

namespace TimBrowser.DataCore.Services
{
    public class ParametersTemplateService
    {
        public ParametersTemplateService()
        {
            _fileOperations = new FileOperations();
            _tpeCryptor = new TpeCryptor();
            _xmlParser = new TpeXmlParser();
        }

        private readonly FileOperations _fileOperations;
        private readonly TpeCryptor _tpeCryptor;
        private readonly TpeXmlParser _xmlParser;


        public TableItem GetParametersTemplate(int deviceId)
        {
            string filePath = GetTpeFilePath(deviceId);

            var fileStream = _fileOperations.LoadFileStream(filePath); //_tpeFile.GetTpeFileStream(filePath);D:\Work\TimBrowser\TimBrowser\bin\Debug\Templates
            var xmlDoc = _tpeCryptor.DecryptTpeFromTpeFile(fileStream);
            var table = _xmlParser.Parse(xmlDoc);

            return table;
        }



        private string GetTpeFilePath(int deviceId)
        {
            string filePath = String.Empty;

            filePath = TimBrowser.DataCore.Helpers.Constants.ApplicationDirectory +
                TimBrowser.DataCore.Helpers.Constants.TemplatesDirectoryName + "\\" + deviceId.ToString() +
                TpeParameters.Helpers.Constants.TpeFileExtention;

            return filePath;

        }
    }
}

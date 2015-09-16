using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;
using System.Data;
using System.Xml.Linq;
using TimBrowser.DataCore.Download.Model;
using System.Text.RegularExpressions;

namespace TimBrowser.DataCore.File
{
    public class DesktopFileOperation : IFileOperation
    {
        public IFileItem LoadFile(string filePath)
        {
            IFileItem desktopFileTime = null;

            BinaryFormatter binnaryFormatter = new BinaryFormatter();

            try
            {
                using (Stream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    desktopFileTime = (DesktopFileItem)binnaryFormatter.Deserialize(fileStream);
                }
            }
            catch (FileNotFoundException)
            {
                //SendFileOperationsMessageMessage(DataManagerRes.FoLoadFileNotFound, true);
            }
            catch (Exception)
            {
                //SendFileOperationsMessageMessage(DataManagerRes.FoLoadFileErr, true);
            }

            return desktopFileTime;
        }

        public IFileItem LoadFileA(string filePath)
        {
            IFileItem desktopFileTime = null;

            FuncDownloadData funcDownloadData = null;

            FuncOneData funcOneData = null;
            FuncTwoData funcTwoData = null;
            FuncThreeData funcThreeData = null;
            FuncFourData funcFourData = null;
            FuncFiveData funcFiveData = null;
            FuncSixData funcSixData = null;

            DateTime _dateTimeDownload = new DateTime();

            byte[] LogsTypes = null;
            int IdOfDevice = 0;
            int LogsNumber = 0;
            int FirmwareVersion = 0;
            int ParametersNumber = 0;
            int xmlDateTime = 0;

            XmlDocument xmlDocument = null;
            xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XElement mainXmlEl = XElement.Load(new XmlNodeReader(xmlDocument));
            var xmlTable = mainXmlEl;

            var dateTimeDownload = from el in xmlTable.Descendants("_dateTimeDownload") select el;
            foreach (var gr in dateTimeDownload)
            {

                _dateTimeDownload = new DateTime(Convert.ToInt32(gr.Value.Substring(0, 4)),
                                                 Convert.ToInt32(gr.Value.Substring(5, 2)),
                                                 Convert.ToInt32(gr.Value.Substring(8, 2)),
                                                 Convert.ToInt32(gr.Value.Substring(11, 2)),
                                                 Convert.ToInt32(gr.Value.Substring(14, 2)),
                                                 Convert.ToInt32(gr.Value.Substring(17, 2)));
            }

            var xmlFuncOneData = from el in xmlTable.Descendants("_funcOneData") select el;

            foreach (var pr in xmlFuncOneData)
            {
                var xmlLogTypes = from el in pr.Descendants("_logsTypes") select el;
                foreach (var gr in xmlLogTypes)
                {
                    string Length = gr.Attribute("length").Value;
                    string Value = gr.Value;

                    //byte[] logsTypes = new byte[Convert.ToInt32(Length)];

                    LogsTypes = Regex.Matches(Value, "\\d+")
                        .Cast<Match>()
                        .Select(x => byte.Parse(x.Value))
                        .ToArray();
                }
            }
            foreach (var pr in xmlFuncOneData)
            {
                var xmlOne = from el in pr.Descendants("_idOfDevice") select el;
                foreach (var gr in xmlOne)
                {
                    IdOfDevice = Convert.ToInt32(gr.Value);
                }
            }
            foreach (var pr in xmlFuncOneData)
            {
                var xmlOne = from el in pr.Descendants("_logsNumber") select el;
                foreach (var gr in xmlOne)
                {
                    LogsNumber = Convert.ToInt32(gr.Value);
                }
            }
            foreach (var pr in xmlFuncOneData)
            {
                var xmlOne = from el in pr.Descendants("_firmwareVersion") select el;
                foreach (var gr in xmlOne)
                {
                    FirmwareVersion = Convert.ToInt32(gr.Value);
                }
            }
            foreach (var pr in xmlFuncOneData)
            {
                var xmlOne = from el in pr.Descendants("_parametersNumber") select el;
                foreach (var gr in xmlOne)
                {
                    ParametersNumber = Convert.ToInt32(gr.Value);
                }
            }

            funcOneData = new FuncOneData(IdOfDevice, ParametersNumber,                                // Возвращаем объект первой функции
                          LogsNumber, LogsTypes, FirmwareVersion);

            //------------------------------------------------------------------------------------------------------------------------------
            //------------------------------------------------------------------------------------------------------------------------------

            int[] LogCurrentAddress = new int[5];
            int LogRecordsNumber = 0;

            List<FuncTwoItem> funcTwoList = new List<FuncTwoItem>();

            var xmlFuncTwoData = from el in xmlTable.Descendants("_funcTwoData") select el;
            foreach (var pr in xmlFuncTwoData)
            {
                var xmlOne = from el in pr.Descendants("_logsData") select el;

                //funcTwoData.LogsData[logTypeIndex].LogRecordsNumber;


                foreach (var gr in xmlOne)
                {
                    int i = 0;
                    var xmlPar1 = from el in gr.Descendants("_logCurrentAddress") select el;
                    foreach (var v in xmlPar1)
                    {
                        LogCurrentAddress[i++] = Convert.ToInt32(v.Value);
                    }

                    i = 0;
                    var xmlPar2 = from el in gr.Descendants("_logRecordsNumber") select el;
                    foreach (var v in xmlPar2)
                    {
                        LogRecordsNumber = Convert.ToInt32(v.Value);
                        FuncTwoItem funcTwoSubData = new FuncTwoItem(LogCurrentAddress[i++], LogRecordsNumber);
                        funcTwoList.Add(funcTwoSubData);
                    }
                }
            }

            funcTwoData = new FuncTwoData(funcTwoList);
            //-----------------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------------

            List<FuncThreeItem> funcThreeList = new List<FuncThreeItem>();

            byte _logCellNumber = 0;

            var xmlFuncThreeData = from el in xmlTable.Descendants("_funcThreeData") select el;
            foreach (var lg in xmlFuncThreeData)
            {
                var xmlThree = from el in lg.Descendants("_logsData") select el;
                foreach (var gr in xmlThree)
                {
                    var xmlPar1 = from el in gr.Descendants("funcThreeItem") select el;
                    foreach (var v in xmlPar1)
                    {
                        var xmlPar2 = from el in v.Descendants("_logCellFieldNumber") select el;
                        foreach (var v2 in xmlPar2)
                        {
                            List<int> _logCellFieldNumber = new List<int>();
                            _logCellNumber = 0;
                            var xmlPar3 = from el in v2.Descendants("integer") select el;
                            foreach (var v3 in xmlPar3)
                            {
                                _logCellNumber++;
                                _logCellFieldNumber.Add(Convert.ToInt32(v3.Value));
                            }

                            FuncThreeItem funcThreeSubData = new FuncThreeItem(_logCellNumber, _logCellFieldNumber);
                            funcThreeList.Add(funcThreeSubData);
                        }
                    }
                }
            }
            funcThreeData = new FuncThreeData(funcThreeList);


            //------------------------------------------------------------------------------------------------------------------------------
            //------------------------------------------------------------------------------------------------------------------------------
            //------------------------------------------------------------------------------------------------------------------------------
            //------------------------------------------------------------------------------------------------------------------------------

            List<FuncFourCells> funcFourCells = new List<FuncFourCells>();

            var xmlFuncFourData = from el in xmlTable.Descendants("_funcFourData") select el;
            foreach (var lg in xmlFuncFourData)
            {
                var xmlThree = from el in lg.Descendants("_logsData") select el;
                foreach (var gr in xmlThree)
                {

                    var xmlP = from el in gr.Descendants("funcFourCells") select el;
                    foreach (var p in xmlP)
                    {
                        List<FuncFourFieldsAddress> _logsCells = new List<FuncFourFieldsAddress>();
                        var xmlP2 = from el in p.Descendants("_logsCells") select el;
                        foreach (var p2 in xmlP2)
                        {
                            var xmlP3 = from el in p2.Descendants("funcFourFieldsAddress") select el;
                            foreach (var p3 in xmlP3)
                            {
                                List<int> _logFieldsAddress = new List<int>();
                                var xmlP4 = from el in p3.Descendants("_logFieldsAddress") select el;
                                foreach (var p4 in xmlP4)
                                {
                                    var xmlP5 = from el in p4.Descendants("integer") select el;
                                    foreach (var p5 in xmlP5)
                                    {
                                        _logFieldsAddress.Add(Convert.ToInt32(p5.Value));
                                    }

                                }
                                FuncFourFieldsAddress f = new FuncFourFieldsAddress(_logFieldsAddress);
                                _logsCells.Add(f);

                            }

                        }
                        FuncFourCells fc = new FuncFourCells(_logsCells);
                        funcFourCells.Add(fc);
                    }

                }
            }
            funcFourData = new FuncFourData(funcFourCells);
            //--------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------
            //-------------------------------------------------------------------------------


            List<FuncFiveRecords> _logsData = new List<FuncFiveRecords>();

            var xmlFuncFiveData = from el in xmlTable.Descendants("_funcFiveData") select el;
            foreach (var lg in xmlFuncFiveData)
            {
                //List<FuncFiveCells> _logFieldRecords = new List<FuncFiveCells>();
                var xmlThree = from el in lg.Descendants("_logsData") select el;
                foreach (var gr in xmlThree)
                {
                    var xmlP = from el in gr.Descendants("funcFiveRecords") select el;
                    foreach (var p in xmlP)
                    {
                        List<FuncFiveCells> _logFieldRecords = new List<FuncFiveCells>();
                        var xmlP2 = from el in p.Descendants("_logFieldRecords") select el;
                        foreach (var p2 in xmlP2)
                        {
                            
                            var xmlP3 = from el in p2.Descendants("funcFiveCells") select el;
                            foreach (var p3 in xmlP3)
                            {
                                List<FuncFiveValues> _logFieldCells = new List<FuncFiveValues>();
                                var xmlP4 = from el in p3.Descendants("_logFieldCells") select el;
                                foreach (var p4 in xmlP4)
                                {
                                    var xmlP5 = from el in p4.Descendants("funcFiveValues") select el;
                                    foreach (var p5 in xmlP5)
                                    {
                                        List<int> LogFieldValues = new List<int>();
                                        var xmlP6 = from el in p5.Descendants("_logFieldValues") select el;
                                        foreach (var p6 in xmlP6)
                                        {
                                            var xmlP7 = from el in p6.Descendants("integer") select el;
                                            foreach (var p7 in xmlP7)
                                            {
                                                LogFieldValues.Add((Convert.ToInt32(p7.Value)));
                                            }
                                        }
                                        FuncFiveValues f = new FuncFiveValues(LogFieldValues);
                                        _logFieldCells.Add(f);
                                    }
                                }
                                FuncFiveCells ffc = new FuncFiveCells(_logFieldCells);
                                _logFieldRecords.Add(ffc);
                            }
                           
                        }

                        FuncFiveRecords ffr = new FuncFiveRecords(_logFieldRecords);
                        _logsData.Add(ffr);
                    }
                }
            }

            funcFiveData = new FuncFiveData(_logsData);
            //--------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------
            List<int> _parametersValues = new List<int>();
            var xmlFuncSixData = from el in xmlTable.Descendants("_funcSixData") select el;
            foreach (var lg in xmlFuncSixData)
            {
                var xmlThree = from el in lg.Descendants("_parametersValues") select el;
                foreach (var gr in xmlThree)
                {
                    var xmlPar1 = from el in gr.Descendants("integer") select el;
                    foreach (var v in xmlPar1)
                    {
                        _parametersValues.Add(Convert.ToInt32(v.Value));
                    }
                }
            }
            funcSixData = new FuncSixData(_parametersValues);



            if (funcOneData == null || funcTwoData == null || funcThreeData == null ||
                funcFourData == null || funcFiveData == null || funcSixData == null)
                return null;

            funcDownloadData = new FuncDownloadData(funcOneData, funcTwoData, funcThreeData,
                funcFourData, funcFiveData, funcSixData, _dateTimeDownload);
            desktopFileTime = new DesktopFileItem("Какоето имя", "Версия которуя я не знаю", funcDownloadData);
            //desktopFileTime.FuncDownloadData = new FuncDownloadData(funcOneData, funcTwoData, funcThreeData,
            //     funcFourData, funcFiveData, funcSixData, _dateTimeDownload);

            int a = 0;

            /*FuncDownloadData funcDownloadData = null;

            FuncOneData funcOneData = FuncOneDownload();
            FuncTwoData funcTwoData = FuncTwoDownload(funcOneData);
            FuncThreeData funcThreeData = FuncThreeDownload(funcOneData);
            FuncFourData funcFourData = FuncFourDownload(funcOneData, funcThreeData);
            FuncFiveData funcFiveData = FuncFiveDownload(funcOneData, funcTwoData, funcThreeData);
            FuncSixData funcSixData = FuncSixDownload(funcOneData);

            if (funcOneData == null || funcTwoData == null || funcThreeData == null ||
                funcFourData == null || funcFiveData == null || funcSixData == null)
            return null;

            funcDownloadData = new FuncDownloadData(funcOneData, funcTwoData, funcThreeData,
                funcFourData, funcFiveData, funcSixData, DateTime.Now);
            */
            //var _funcTwoData = xmlTable.FirstNode.Parent.Elements;
            //string deviceIdStr = xmlTable.Attribute(DeviceIdAttribute).Value;
            //string deviceName = xmlTable.Attribute(DeviceNameAttribute).Value;

            //парсим файл
            //string Name { get; }
            //string TimBrowserVersion { get; }
            //FuncDownloadData FuncDownloadData { get; } 

            //BinaryFormatter binnaryFormatter = new BinaryFormatter();
            /*try
            {
                using (Stream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    //desktopFileTime = (DesktopFileItem)binnaryFormatter.Deserialize(fileStream);
                }
            }
            catch (FileNotFoundException)
            {
                //SendFileOperationsMessageMessage(DataManagerRes.FoLoadFileNotFound, true);
            }
            catch (Exception)
            {
                //SendFileOperationsMessageMessage(DataManagerRes.FoLoadFileErr, true);
            }*/

            return desktopFileTime;
        }

        public bool SaveFile(IFileItem fileItem, string filePath)
        {
            BinaryFormatter binnaryFormatter = new BinaryFormatter();

            DesktopFileItem desktopFileItem = (DesktopFileItem)fileItem;

            bool operationSucceed = false;

            try
            {
                using (Stream fileStream =
                    new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    binnaryFormatter.Serialize(fileStream, fileItem);
                    operationSucceed = true;
                }
            }
            catch (Exception ex)
            {
                //SendFileOperationsMessageMessage(DataManagerRes.FoSaveFileErr, true);
                operationSucceed = false;
            }

            return operationSucceed;
        }
    }
}

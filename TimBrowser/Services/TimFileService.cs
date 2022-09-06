using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Download.Model;
using TimBrowser.DataCore.File;

namespace TimBrowser.Services
{
    public class TimFileService
    {
        public TimFileService(TimDataServiceM timDataServiceM, TimDataService timDataService)
        {
            _timDataService = timDataService;
            _timDataService.OnTimDataChanged += TimDataChanged;

            _timDataServiceM = timDataServiceM;
            _timDataServiceM.OnTimDataChangedM += TimDataChanged;

            // по хорошему нужно как-то передавать реализацию, а не создавать новую в этом классе
            _fileOperation = new DesktopFileOperation();
            _fileOperationM = new DesktopFileOperationM();
        }

        #region Fields

        private  TimDataService _timDataService;
        private  TimDataServiceM _timDataServiceM;
        private  IFileOperation _fileOperation;
        private  IFileOperationM _fileOperationM;
        private IFileItem _currentFileItem;
        //readonly

        private bool _canSaveFile;
        private string _typeInputSignal = "";

        public Action<bool> CanSaveFileAction;
        
        #endregion

        #region Methods

        private void TimDataChanged(object sender, EventArgs e)
        {
            _canSaveFile = (_timDataService.CurrentInformationModule != null);

            if (_canSaveFile==false) _canSaveFile = (_timDataServiceM.CurrentInformationModule != null);

            if (CanSaveFileAction != null)
                CanSaveFileAction(_canSaveFile);
        }

        public void LoadFile(string loadPath)
        {
            // загружаем файл
            if (loadPath.Contains(".tima"))
            {
                _currentFileItem = _fileOperation.LoadFileA(loadPath);
            }
            else _currentFileItem = _fileOperation.LoadFile(loadPath);

            // запускаем преобразование считанных данных
            if (_currentFileItem != null)
            {
                var funcDownloadData = _currentFileItem.FuncDownloadData;
                
                _timDataService.PutFuncDownloadData(ServiceSources.FileService, funcDownloadData);
            }
        }

        public void SaveFile(string fileName, string savePath)
        {
            string appVer = Helper.Constants.AppVersion;

            if (!savePath.Contains(Helper.Constants.FILE_SAVE_EXT))
                savePath += Helper.Constants.FILE_SAVE_EXT;


            bool isSaveSuccess = false;
            FuncOneData funcOneData = null;
            FuncTwoData funcTwoData = null;
            FuncThreeData funcThreeData = null;
            FuncFourData funcFourData = null;
            FuncFiveData funcFiveData = null;
            FuncSixData funcSixData = null;


            if (_timDataService.CurrentFuncDownloadData == null)
            {
                /*byte[] _logsTypes = new byte[3];
                for (int i = 0; i < 3; i++)
                {
                    _logsTypes[i] = _timDataServiceM.CurrentFuncDownloadData.FuncOne.LogsTypes[i];
                }*/

                funcOneData = new FuncOneData(_timDataServiceM.CurrentFuncDownloadData.FuncOne.IdOfDevice,
                                                _timDataServiceM.CurrentFuncDownloadData.FuncOne.ParametersNumber,
                                                _timDataServiceM.CurrentFuncDownloadData.FuncOne.LogsNumber,
                                                _timDataServiceM.CurrentFuncDownloadData.FuncOne.LogsTypes,
                                                _timDataServiceM.CurrentFuncDownloadData.FuncOne.FirmwareVersion);
                
                List<FuncTwoItem> _logsData;
                _logsData = new List<FuncTwoItem>();
                for(int i=0; i<_timDataServiceM.CurrentFuncDownloadData.FuncTwo.LogsData.Count; i++)
                {
                    _logsData.Add(new FuncTwoItem(_timDataServiceM.CurrentFuncDownloadData.FuncTwo.LogsData[i].LogCurrentAddress, _timDataServiceM.CurrentFuncDownloadData.FuncTwo.LogsData[i].LogRecordsNumber));
                }
                funcTwoData = new FuncTwoData(_logsData);

                List<FuncThreeItem> _list = new List<FuncThreeItem>();
                for (int i = 0; i < _timDataServiceM.CurrentFuncDownloadData.FuncThree.LogsData.Count; i++)
                {
                    _list.Add(new FuncThreeItem(_timDataServiceM.CurrentFuncDownloadData.FuncThree.LogsData[i].LogCellNumber,
                                                _timDataServiceM.CurrentFuncDownloadData.FuncThree.LogsData[i].LogCellFieldNumber));
                }

                funcThreeData = new FuncThreeData(_list);


                List<FuncFourCells> _logsData4 = new List<FuncFourCells>();
                for (int i = 0; i < _timDataServiceM.CurrentFuncDownloadData.FuncFour.LogsData.Count; i++)
                {

                    List<FuncFourFieldsAddress> _logsDataCells4 = new List<FuncFourFieldsAddress>();
                    for (int x = 0; x < _timDataServiceM.CurrentFuncDownloadData.FuncFour.LogsData[i].LogsCells.Count; x++)
                    {
                        /*List<int> LogFieldsAddress4 = new List<int>();
                        for (int y = 0; y < _timDataServiceM.CurrentFuncDownloadData.FuncFour.LogsData[i].LogsCells[x].LogFieldsAddress.Count; y++)
                        {
                            LogFieldsAddress4.Add(new int(_timDataServiceM.CurrentFuncDownloadData.FuncFour.LogsData[i].LogsCells[x].LogFieldsAddress));
                        }*/
                        _logsDataCells4.Add(new FuncFourFieldsAddress(_timDataServiceM.CurrentFuncDownloadData.FuncFour.LogsData[i].LogsCells[x].LogFieldsAddress));
                    }

                    _logsData4.Add(new FuncFourCells(_logsDataCells4));
                }

                funcFourData = new FuncFourData(_logsData4);

                List<FuncFiveRecords> _logsData5 = new List<FuncFiveRecords>();
                for (int i = 0; i < _timDataServiceM.CurrentFuncDownloadData.FuncFive.LogsData.Count; i++)
                {

                    List<FuncFiveCells> _logFieldRecords5 = new List<FuncFiveCells>();
                    for (int x = 0; x < _timDataServiceM.CurrentFuncDownloadData.FuncFive.LogsData[i].LogFieldRecords.Count; x++)
                    {
                        List<FuncFiveValues> _logFieldCells5 = new List<FuncFiveValues>();
                        for (int y = 0; y < _timDataServiceM.CurrentFuncDownloadData.FuncFive.LogsData[i].LogFieldRecords[x].LogFieldCells.Count; y++)
                        {
                            _logFieldCells5.Add(new FuncFiveValues(_timDataServiceM.CurrentFuncDownloadData.FuncFive.LogsData[i].LogFieldRecords[x].LogFieldCells[y].LogFieldValues));
                        }

                        _logFieldRecords5.Add(new FuncFiveCells(_logFieldCells5));
                    }

                    _logsData5.Add(new FuncFiveRecords(_logFieldRecords5));
                }

                funcFiveData = new FuncFiveData(_logsData5);


                /*List<int> ParametersValues = new List<int>();
                for (int i = 0; i < _timDataServiceM.CurrentFuncDownloadData.FuncSix.ParametersValues.Count; i++)
                {
                    _logsData.Add(new FuncTwoItem(_timDataServiceM.CurrentFuncDownloadData.FuncTwo.LogsData[i].LogCurrentAddress, _timDataServiceM.CurrentFuncDownloadData.FuncTwo.LogsData[i].LogRecordsNumber));
                }*/

                funcSixData = new FuncSixData(_timDataServiceM.CurrentFuncDownloadData.FuncSix.ParametersValues);

                /*
                funcSixData = new FuncSixData;

                _timDataServiceM.CurrentFuncDownloadData.FuncFive,
                _timDataServiceM.CurrentFuncDownloadData.FuncSix,
                _timDataServiceM.CurrentFuncDownloadData.DateTimeDownload);
                 */

                _timDataService.CurrentFuncDownloadData = new FuncDownloadData(funcOneData,funcTwoData,funcThreeData,funcFourData,funcFiveData,funcSixData,
                                                                               _timDataServiceM.CurrentFuncDownloadData.DateTimeDownload);
            
            }

            if (_timDataService.CurrentFuncDownloadData != null)
            {
                IFileItem fileItem = new DesktopFileItem(fileName, appVer, _timDataService.CurrentFuncDownloadData);
                isSaveSuccess = _fileOperation.SaveFile(fileItem, savePath);
            }
            else if (_timDataServiceM.CurrentFuncDownloadData != null)
            {
                IFileItemM fileItem = new DesktopFileItemM(fileName, appVer, _timDataServiceM.CurrentFuncDownloadData);
                isSaveSuccess = _fileOperationM.SaveFile(fileItem, savePath);
            }
            
        }

        #endregion


        public string CurrentFactoryNumber
        {
            get
            {
                
                string Number;

                if (_timDataService.CurrentInformationModule != null)
                {
                    Number = "Заводской номер:         " + _timDataService.CurrentInformationModule.DeviceInfo.DeviceNumberString;
                }

                else if (_timDataServiceM.CurrentInformationModule != null)
                {
                    Number = "Заводской номер:         " + _timDataServiceM.CurrentInformationModule.DeviceInfo.DeviceNumberString;
                    
                }else return String.Empty;
                return Number;
            }
        }

        public string CurrentFileName
        {
            get 
            {
                string fileName="не выбран";

                if (_timDataService.CurrentInformationModule != null)
                {
                    fileName = _timDataService.CurrentInformationModule.DeviceInfo.DeviceName +
                    "-" + _timDataService.CurrentInformationModule.DeviceInfo.DeviceNumberString +
                    " (" + _timDataService.CurrentFuncDownloadData.DateTimeDownload
                    .ToString(Helper.Constants.DATE_TIME_FILE_FORMAT_STRING) + ")";
                }

                else if (_timDataServiceM.CurrentInformationModule != null)
                {
                    fileName = _timDataServiceM.CurrentInformationModule.DeviceInfo.DeviceName +
                    "-" + _timDataServiceM.CurrentInformationModule.DeviceInfo.DeviceNumberString +
                    " (" + _timDataServiceM.CurrentFuncDownloadData.DateTimeDownload
                    .ToString(Helper.Constants.DATE_TIME_FILE_FORMAT_STRING) + ")";
                }
                //else return String.Empty;

                return fileName;
            }
        }

        public string CurrentTypeDrive
        {
            get
            {
                string typeDrive;
                string typeBlockString = "";

                if (_timDataService.CurrentInformationModule != null)
                {
                    if (_timDataService.CurrentInformationModule.CurrentParameters.DeviceId<8001){
                        int typeBlockValue = (int)(_timDataService.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[1].DValue);
                        typeBlockString = _timDataService.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[1].ValueDescription.Fields[typeBlockValue].Description;
                    } 

                        string typeTU = "M220";

                    foreach (var item in _timDataService.CurrentInformationModule.CurrentParameters.Groups[1].Parameters)
                    {
                        if (item.Name.Contains("Тип входного сигнала"))
                        {
                            typeTU = _typeInputSignal + item.ValueDescription.Fields[(int)item.DValue].Description.Replace("В", "");
                        }
                    }

                    typeDrive = "Электропривод:             " + typeBlockString;// + "." + typeTU;
                }

                else if (_timDataServiceM.CurrentInformationModule != null)
                {
                    if (_timDataServiceM.CurrentInformationModule.CurrentParameters.DeviceId<8001){
                        int typeBlockValue = (int)(_timDataServiceM.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[1].DValue);
                        typeBlockString = _timDataServiceM.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[1].ValueDescription.Fields[typeBlockValue].Description;
                    }else{typeBlockString="";}

                    string typeTU = "M220";
                    foreach (var item in _timDataServiceM.CurrentInformationModule.CurrentParameters.Groups[2].Parameters)
                    {
                        if (item.Name.Contains("Тип входного сигнала"))
                        {
                            typeTU = "T" + item.ValueDescription.Fields[(int)item.DValue].Description.Replace("В","");
                        }
                    }

                    typeDrive = "Электропривод:             " + typeBlockString;// + "." + typeTU;
                }
                else return String.Empty;

                return typeDrive;
            }
        }

        public string CurrentVersion
        {
            get
            {
                string version="";

                if (_timDataService.CurrentInformationModule != null)
                {
                    if (_timDataService.CurrentInformationModule.CurrentParameters.DeviceId<8001){
                                                                                                //Parameters[57] для старых версий
                        if (_timDataService.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[10].Name.Contains("Подверсия ПО"))
                        {
                            version = "Версия ПО:                      v_" + (_timDataService.CurrentInformationModule.DeviceInfo.FirmwareVersion / 1000).ToString() + "," +
                            _timDataService.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[10].sValue.ToString();
                        }
                    }
                    else
                    {
                        version = "Версия ПО:                      v_" + (_timDataService.CurrentInformationModule.DeviceInfo.FirmwareVersion / 1000).ToString() + "," + 0;
                    }
                }

                else if (_timDataServiceM.CurrentInformationModule != null)
                {
                    if (_timDataServiceM.CurrentInformationModule.CurrentParameters.DeviceId < 8001)
                    {
                        if (
                            _timDataServiceM.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[10].Name
                                .Contains("Подверсия ПО"))
                        {
                            version = "Версия ПО:                      v_" +
                                      (_timDataServiceM.CurrentInformationModule.DeviceInfo.FirmwareVersion/1000).ToString() + "," +
                                      _timDataServiceM.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[10].sValue.ToString();
                        }
                    }
                    else
                    {
                        version = "Версия ПО:                      v_" + (_timDataServiceM.CurrentInformationModule.DeviceInfo.FirmwareVersion / 1000).ToString() + "," + 0;
                    }
                }
                else return String.Empty;

                return version;
            }
        }


        public string CurrentTypeBlock
        {
            get
            {
                string typeBlock;
                string mode="";
                string mode2 = "";                 

                typeBlock = "БУР-";

                if (_timDataService.CurrentInformationModule != null)
                {
                    if (_timDataService.CurrentInformationModule.DeviceInfo.DeviceName == "БУР-М" || _timDataService.CurrentInformationModule.DeviceInfo.DeviceName == "БУР-M")
                    {
                        mode = "М";
                    }
                    else if (_timDataService.CurrentInformationModule.DeviceInfo.DeviceName == "БУР-Т" || _timDataService.CurrentInformationModule.DeviceInfo.DeviceName == "БУР-T" || _timDataService.CurrentInformationModule.DeviceInfo.DeviceName.Contains("БУР-2"))
                    {
                        mode = "Т";
                    }
                    else if (_timDataService.CurrentInformationModule.DeviceInfo.DeviceName == "РЭД")
                    {
                        mode = "";
                        typeBlock = "БКП-8";
                    }
                    else
                    {
                        mode = "";
                        typeBlock = "БКЭП";
                    }
                    _typeInputSignal = mode;

                    switch((int)(_timDataService.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[1].DValue))
                    {
                        case 1:
                            mode2 = "01-01.";
                        break;
                        case 2: case 3: case 4: case 5: case 6:
                            mode2 = "04-1.";
                        break;
                        case 7: case 8:  case 9: case 10:
                            mode2 = "4-10.";
                        break;
                    }

                    if (_timDataService.CurrentInformationModule.DeviceInfo.DeviceName.Contains("БУР-2"))
                    {
                        switch((int)_timDataService.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[1].DValue)
                        {
                            case 1:
                                mode2 = "10Д10.";
                            break;
                            case 2: case 3:
                                mode2 = "15-20.";
                            break;
                            default: mode2 = ""; break;
                        }   
                    }

                    typeBlock = "Наименование блока:  " + typeBlock + mode2 + mode;
                }

                else if (_timDataServiceM.CurrentInformationModule != null)
                {
                    if (_timDataServiceM.CurrentInformationModule.DeviceInfo.DeviceName == "БУР-М" || _timDataServiceM.CurrentInformationModule.DeviceInfo.DeviceName == "БУР-M")
                    {
                        mode = "М";
                    }
                    else if (_timDataServiceM.CurrentInformationModule.DeviceInfo.DeviceName == "БУР-Т" || _timDataServiceM.CurrentInformationModule.DeviceInfo.DeviceName == "БУР-T" || _timDataServiceM.CurrentInformationModule.DeviceInfo.DeviceName.Contains("БУР-2"))
                    {
                        mode = "Т";
                    }
                    else if (_timDataServiceM.CurrentInformationModule.DeviceInfo.DeviceName == "РЭД")
                    {
                        mode = "";
                        typeBlock = "БКП-8";
                    }
                    else
                    {
                        mode = "";
                        typeBlock = "БКЭП";
                    }
                    _typeInputSignal = mode;

                    switch ((int)_timDataServiceM.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[1].DValue)
                    {
                        case 1:
                            mode2 = "01-01.";
                            break;
                        case 2: case 3:case 4:case 5:case 6:
                            mode2 = "04-1.";
                            break;
                        case 7:case 8:case 9:case 10:
                            mode2 = "4-10.";
                            break;
                    }

                    if (_timDataServiceM.CurrentInformationModule.DeviceInfo.DeviceName.Contains("БУР-2"))
                    {
                        switch ((int)_timDataServiceM.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[1].DValue)
                        {
                            case 1:
                                mode2 = "10Д10.";
                                break;
                            case 2: case 3:
                                mode2 = "15-20.";
                                break;
                            default: mode2 = ""; break;
                        }
                    }

                    typeBlock = "Наименование блока:  " + typeBlock + mode2 + mode;
                }
                else return String.Empty;

                return typeBlock;
            }
        }
        
    }
}

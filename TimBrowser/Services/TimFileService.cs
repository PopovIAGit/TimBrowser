using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        }

        #region Fields

        private readonly TimDataService _timDataService;
        private readonly TimDataServiceM _timDataServiceM;
        private readonly IFileOperation _fileOperation;
        private IFileItem _currentFileItem;


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

            IFileItem fileItem = new DesktopFileItem(fileName, appVer, _timDataService.CurrentFuncDownloadData);
            bool isSaveSuccess = _fileOperation.SaveFile(fileItem, savePath);
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

                if (_timDataService.CurrentInformationModule != null)
                {
                    int typeBlockValue = (int)(_timDataService.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[1].Value);
                    string typeBlockString = _timDataService.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[1].ValueDescription.Fields[typeBlockValue].Description;
                    string typeTU = "M220";
                    foreach (var item in _timDataService.CurrentInformationModule.CurrentParameters.Groups[1].Parameters)
                    {
                        if (item.Name.Contains("Тип входного сигнала"))
                        {
                            typeTU = _typeInputSignal + item.ValueDescription.Fields[(int)item.Value].Description;
                        }
                    }

                    typeDrive = "Электропривод:             " + typeBlockString + "." + typeTU;
                }

                else if (_timDataServiceM.CurrentInformationModule != null)
                {
                    int typeBlockValue = (int)(_timDataServiceM.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[1].Value);
                    string typeBlockString = _timDataServiceM.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[1].ValueDescription.Fields[typeBlockValue].Description;
                    string typeTU = "M220";
                    foreach (var item in _timDataService.CurrentInformationModule.CurrentParameters.Groups[2].Parameters)
                    {
                        if (item.Name.Contains("Тип входного сигнала"))
                        {
                            typeTU = "T" + item.ValueDescription.Fields[(int)item.Value].Description;
                        }
                    }

                    typeDrive = "Электропривод:             " + typeBlockString + "." + typeTU;
                }
                else return String.Empty;

                return typeDrive;
            }
        }

        public string CurrentVersion
        {
            get
            {
                string version;

                if (_timDataService.CurrentInformationModule != null)
                {
                    if (_timDataService.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[57].Name.Contains("Подверсия ПО"))
                    {
                        version = "Версия ПО:                      v_" + (_timDataService.CurrentInformationModule.DeviceInfo.FirmwareVersion / 1000).ToString() + "," + 
                            _timDataService.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[57].Value.ToString();
                    }
                    else version = "Версия ПО:                      v_" + (_timDataService.CurrentInformationModule.DeviceInfo.FirmwareVersion / 1000).ToString() + "," + 0;
                }

                else if (_timDataServiceM.CurrentInformationModule != null)
                {
                    if (_timDataServiceM.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[57].Name.Contains("Подверсия ПО"))
                    {
                        version = "Версия ПО:                      v_" + (_timDataServiceM.CurrentInformationModule.DeviceInfo.FirmwareVersion / 1000).ToString() + "," +
                            _timDataServiceM.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[57].Value.ToString();
                    }
                    else version = "Версия ПО:                      v_" + (_timDataServiceM.CurrentInformationModule.DeviceInfo.FirmwareVersion / 1000).ToString() + "," + 0;
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
                    else
                    {
                        mode = "R";
                        typeBlock = "БКД-";
                    }
                    _typeInputSignal = mode;

                    switch((int)(_timDataService.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[1].Value))
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
                        switch((int)(_timDataService.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[1].Value))
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
                    if (_timDataService.CurrentInformationModule.DeviceInfo.DeviceName == "БУР-М" || _timDataService.CurrentInformationModule.DeviceInfo.DeviceName == "БУР-M")
                    {
                        mode = "М";
                    }
                    else if (_timDataService.CurrentInformationModule.DeviceInfo.DeviceName == "БУР-Т" || _timDataService.CurrentInformationModule.DeviceInfo.DeviceName == "БУР-T" || _timDataService.CurrentInformationModule.DeviceInfo.DeviceName.Contains("БУР-2"))
                    {
                        mode = "Т";
                    }
                    else
                    {
                        mode = "R";
                        typeBlock = "БКД-";
                    }
                    _typeInputSignal = mode;

                    switch ((int)(_timDataServiceM.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[1].Value))
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
                        switch ((int)(_timDataServiceM.CurrentInformationModule.CurrentParameters.Groups[2].Parameters[1].Value))
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

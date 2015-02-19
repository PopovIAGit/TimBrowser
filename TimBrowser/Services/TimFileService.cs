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
            _currentFileItem = _fileOperation.LoadFile(loadPath);

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

            if (!savePath.Contains(Helper.Constants.FILE_EXT))
                savePath += Helper.Constants.FILE_EXT;

            IFileItem fileItem = new DesktopFileItem(fileName, appVer, _timDataService.CurrentFuncDownloadData);
            bool isSaveSuccess = _fileOperation.SaveFile(fileItem, savePath);
        }

        #endregion

        public string CurrentFileName
        {
            get 
            {
                string fileName;

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
                else return String.Empty;

                return fileName;
            }
        }

    }
}

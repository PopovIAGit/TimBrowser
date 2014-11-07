using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.File;

namespace TimBrowser.Services
{
    public class TimFileService
    {
        public TimFileService(TimDataService timDataService)
        {
            _timDataService = timDataService;
            _timDataService.OnTimDataChanged += TimDataChanged;

            // по хорошему нужно как-то передавать реализацию, а не создавать новую в этом классе
            _fileOperation = new DesktopFileOperation();
        }

        #region Fields

        private readonly TimDataService _timDataService;
        private readonly IFileOperation _fileOperation;
        private IFileItem _currentFileItem;

        private bool _canSaveFile;

        public Action<bool> CanSaveFileAction;
        
        #endregion

        #region Methods

        private void TimDataChanged(object sender, EventArgs e)
        {
            _canSaveFile = (_timDataService.CurrentInformationModule != null);

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
                if (_timDataService.CurrentInformationModule == null)
                    return String.Empty;

                string fileName = _timDataService.CurrentInformationModule.DeviceInfo.DeviceName +
                "-" + _timDataService.CurrentInformationModule.DeviceInfo.DeviceNumberString +
                " (" + _timDataService.CurrentFuncDownloadData.DateTimeDownload
                .ToString(Helper.Constants.DATE_TIME_FILE_FORMAT_STRING) + ")";

                return fileName;
            }
        }

    }
}

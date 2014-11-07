using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Communication;
using TimBrowser.DataCore.Download;
using TimBrowser.DataCore.Download.Model;
using System.Diagnostics;
using TimBrowser.DataCore.Services;
using TimBrowser.DataCore.Model;
using TimBrowser.DataCore.File;

namespace TimBrowser.Services
{
    /*
    public class DataManager
    {
        public DataManager()
        {

        }

        private FuncDownloadData _funcDownloadData;
        private InformationModuleData _informationModuleData;
        private bool _isBusy;

        public void TransformDbg()
        {
            IFileOperation desktopFileOperation = new DesktopFileOperation();
            IFileItem desktopFile = desktopFileOperation.LoadFile(@"c:\timtest\dbgFileItem.tim");

            DataService dataService = new DataService();
            var im = dataService.TransformFuncDownload(desktopFile.FuncDownloadData);

            TimBrowser.DataCore.Storage.Data.Instance.SetInformationModuleData(im);

            var logEv = Mapper.LogMapper.MapLogEv(im.DeviceLogs.EventLog);
            var logCmd = Mapper.LogMapper.MapLogCmd(im.DeviceLogs.CommandLog);
            var logParam = Mapper.LogMapper.MapLogParameter(im.DeviceLogs.ChangeParameterLog);

            var groupsOfParams = Mapper.ParameterMapper.MapGroupsOfParameters(im.CurrentParameters.Groups);
        }

        /// <summary>
        /// Запускает асинхронный процесс считывания
        /// </summary>
        /// <param name="timCommunication"></param>
        public void DownloadAsync(ITimCommunication timCommunication)
        {
            _isBusy = true;

            DataService downloadTransformService = new DataService();
            downloadTransformService.DownloadAsync(timCommunication,
                (result) =>
                {
                    SetFuncDownloadData(result);
                    _isBusy = false;
                });
        }

        private void TransormAsync(FuncDownloadData funcDownloadData)
        {
            _isBusy = true;

            DataService dataService = new DataService();
            dataService.TransformFuncDownloadAsync(funcDownloadData,
                (result) =>
                {
                    _informationModuleData = result;
                });

            _isBusy = false;
        }

        private void SetFuncDownloadData(FuncDownloadData funcDownloadData)
        {
            _funcDownloadData = funcDownloadData;

            if (_funcDownloadData != null)
            {
                //!!! DEBUG
                /*
                 * 
                 * 
                IFileItem desktopFile = new DesktopFileItem(
                    "dbgFileItem", 2.0, funcDownloadData);

                IFileOperation desktopFileOperation = new
                    DesktopFileOperation();

                desktopFileOperation.SaveFile(desktopFile, @"c:\timtest\" + desktopFile.Name + ".tpe");
                */

    /*
                TransormAsync(funcDownloadData);
            }
        }

        private void SetInformationModuleData(InformationModuleData informationModuleData)
        {
            _informationModuleData = informationModuleData;
        }


        public bool IsBusy
        {
            get { return _isBusy; }
        }

    }
    */
}

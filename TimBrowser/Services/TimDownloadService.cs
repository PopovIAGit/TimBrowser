using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Communication;
using TimBrowser.DataCore.Download.Model;
using TimBrowser.DataCore.DownloadM.Model;
using TimBrowser.DataCore.DownloadBLE.Model;
using TimBrowser.DataCore.Model;
using TimBrowser.DataCore.Services;
//using TimBrowser.ApiImplementation.ILow;

namespace TimBrowser.Services
{
    public class TimDownloadService
    {
        public TimDownloadService(TimDataService timDataService, TimDataServiceM timDataServiceM, TimErrorService timErrorService, TimDataServiceBLE timDataServiceBLE)
        {
            _coreDownloadService = new DataService();
            _coreDownloadService.ProgressChangedAction = ProgressChangedAction;

            _coreDownloadServiceM = new DataServiceM();
            _coreDownloadServiceM.ProgressChangedAction = ProgressChangedAction;

            _coreDownloadServiceBLE = new DataServiceBLE();
            _coreDownloadServiceBLE.ProgressChangedAction = ProgressChangedAction;

            _timDataService = timDataService;
            _timDataServiceM = timDataServiceM;
            _timDataServiceBLE = timDataServiceBLE;
            _timErrorService = timErrorService;
        }

        #region Fields

        private readonly DataService _coreDownloadService;
        private readonly DataServiceM _coreDownloadServiceM;
        private readonly DataServiceBLE _coreDownloadServiceBLE;
        private TimDataService _timDataService;
        private TimDataServiceM _timDataServiceM;
        private TimDataServiceBLE _timDataServiceBLE;
        private TimErrorService _timErrorService;
        private FuncDownloadData _funcDownloadData;
        private FuncDownloadDataM _funcDownloadDataM;
        private FuncDownloadDataBLE _funcDownloadDataBLE;
        private bool _isBusy;

        private Action<string> _progressChangedAction;
        
        #endregion

        public void DownloadAsync(ITimCommunication timCommunication,
            Action onComplete = null)
        {
            _isBusy = true;

            _coreDownloadService.DownloadAsync(timCommunication,
                (result) =>
                {
                    _funcDownloadData = result;
                    _isBusy = true;

                    if (result != null)
                        _timDataService.PutFuncDownloadData(ServiceSources.DownloadService, _funcDownloadData);
                    else
                        _timErrorService.SendErrorMessage(ServiceSources.DownloadService, ErrorMessageTypes.DownloadImError);

                    if (onComplete != null)
                        onComplete();
                });
        }

        public void DownloadAsyncM(ICommunicationSource timCommunicationM,
            Action onComplete = null)
        {
            _isBusy = true;

            _coreDownloadServiceM.DownloadAsyncM(timCommunicationM,
                (result) =>
                {
                    _funcDownloadDataM = result;
                    _isBusy = true;

                    if (result != null)
                        _timDataServiceM.PutFuncDownloadDataM(ServiceSources.DownloadService, _funcDownloadDataM);
                    else
                        _timErrorService.SendErrorMessage(ServiceSources.DownloadService, ErrorMessageTypes.DownloadImError);
                    
                    if (onComplete != null)
                        onComplete();
                });
        }

        public void DownloadAsyncBLE(IBLECommunication timCommunicationBLE,
            Action onComplete = null)
        {
            _isBusy = true;

            _coreDownloadServiceBLE.DownloadAsyncBLE(timCommunicationBLE,
                (result) =>
                {
                    _funcDownloadDataBLE = result;
                    _isBusy = true;

                    if (result != null)
                        _timDataServiceM.PutFuncDownloadDataM(ServiceSources.DownloadService, _funcDownloadDataM);
                    else
                        _timErrorService.SendErrorMessage(ServiceSources.DownloadService, ErrorMessageTypes.DownloadImError);

                    if (onComplete != null)
                        onComplete();
                });
        }



        public Action<string> ProgressChangedAction
        {
            get { return _progressChangedAction; }
            set 
            { 
                _progressChangedAction = value;
                _coreDownloadService.ProgressChangedAction = _progressChangedAction;
                _coreDownloadServiceM.ProgressChangedAction = _progressChangedAction;
            }
        }

        

    }
}

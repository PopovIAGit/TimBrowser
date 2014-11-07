using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Communication;
using TimBrowser.DataCore.Download.Model;
using TimBrowser.DataCore.Model;
using TimBrowser.DataCore.Services;

namespace TimBrowser.Services
{
    public class TimDownloadService
    {
        public TimDownloadService(TimDataService timDataService, TimErrorService timErrorService)
        {
            _coreDownloadService = new DataService();
            _coreDownloadService.ProgressChangedAction = ProgressChangedAction;

            _timDataService = timDataService;
            _timErrorService = timErrorService;
        }

        #region Fields

        private readonly DataService _coreDownloadService;
        private TimDataService _timDataService;
        private TimErrorService _timErrorService;
        private FuncDownloadData _funcDownloadData;
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



        public Action<string> ProgressChangedAction
        {
            get { return _progressChangedAction; }
            set 
            { 
                _progressChangedAction = value;
                _coreDownloadService.ProgressChangedAction = _progressChangedAction;
            }
        }

        

    }
}

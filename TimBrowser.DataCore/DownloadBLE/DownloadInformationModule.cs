using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Communication;
using System.ComponentModel;
using TimBrowser.DataCore.DownloadBLE.Model;
using TimBrowser.DataCore.DownloadBLE.Events;

namespace TimBrowser.DataCore.DownloadBLE
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DownloadInformationModule
    {

        public DownloadInformationModule(IBLECommunication communication)
        {
            _communication = communication;
        }

        #region Fields

        private IBLECommunication _communication;
        private FuncDownloadDataBLE _funcDownloadData;
        private int _maxIndex;

        public EventHandler<DownloadProgressChangedEventArgs> DownloadPropgressChanged;

        #endregion

        #region Methods

        public void DownloadAsync(Action<FuncDownloadDataBLE> onComplete)
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (s, e) =>
                {
                    e.Result = Download();
                };

            worker.RunWorkerCompleted += (s, e) =>
                {
                    if (e.Result != null)
                        _funcDownloadData = (FuncDownloadDataBLE)e.Result;

                    if (onComplete != null)
                        onComplete(_funcDownloadData);
                };

            worker.RunWorkerAsync();
        }

        public FuncDownloadDataBLE Download()
        {
            if (!_communication.CanCommunicate)
                return null;

            FuncDownloadDataBLE funcDownloadData = null;

            try
            {
                /*  FuncOneData funcOneData     = FuncOneDownload();
                  FuncTwoData funcTwoData     = FuncTwoDownload(funcOneData);
                  FuncThreeData funcThreeData = FuncThreeDownload(funcOneData);
                  FuncFourData funcFourData   = FuncFourDownload(funcOneData, funcThreeData);
                  FuncFiveData funcFiveData   = FuncFiveDownload(funcOneData, funcTwoData, funcThreeData);
                  FuncSixData funcSixData     = FuncSixDownload(funcOneData);*/

                FuncOneData funcOneData = null;
                FuncTwoData funcTwoData = null;
                FuncThreeData funcThreeData = null;
                FuncFourData funcFourData = null;
                FuncFiveData funcFiveData = null;
                FuncSixData funcSixData = null;

                if (funcOneData == null || funcTwoData == null || funcThreeData == null ||
                    funcFourData == null || funcFiveData == null || funcSixData == null)
                return null;

                funcDownloadData = new FuncDownloadDataBLE(funcOneData, funcTwoData, funcThreeData,
                    funcFourData, funcFiveData, funcSixData, DateTime.Now);
            }
            catch (CommunicationException)
            {

            }
            catch (DownloadException)
            {

            }
            catch (Exception)
            {

            }

            return funcDownloadData;
        }

        private void RaiseDownloadProgressChanged(int currentIndex, int progress)
        {
            if (DownloadPropgressChanged != null)
                DownloadPropgressChanged(this, new DownloadProgressChangedEventArgs(currentIndex,
                    _maxIndex, progress));
        }

        #endregion


        #region Properties

        public FuncDownloadDataBLE FuncDownloadData
        {
            get { return _funcDownloadData; }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Communication;
using System.ComponentModel;
using TimBrowser.DataCore.Download.Model;
using TimBrowser.DataCore.Download.Events;

namespace TimBrowser.DataCore.Download
{
    // 
    public partial class DownloadInformationModule
    {

        public DownloadInformationModule(ITimCommunication communication)
        {
            _communication = communication;
        }

        #region Fields

        private ITimCommunication _communication;
        private FuncDownloadData _funcDownloadData;
        private int _maxIndex;

        public EventHandler<DownloadProgressChangedEventArgs> DownloadPropgressChanged;

        #endregion

        #region Methods

        public void DownloadAsync(Action<FuncDownloadData> onComplete)
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (s, e) =>
                {
                    e.Result = Download();
                };

            worker.RunWorkerCompleted += (s, e) =>
                {
                    if (e.Result != null)
                        _funcDownloadData = (FuncDownloadData)e.Result;

                    if (onComplete != null)
                        onComplete(_funcDownloadData);
                };

            worker.RunWorkerAsync();
        }

        public FuncDownloadData Download()
        {
            if (!_communication.CanCommunicate)
                return null;

            FuncDownloadData funcDownloadData = null;

            try
            {
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

        public FuncDownloadData FuncDownloadData
        {
            get { return _funcDownloadData; }
        }

        #endregion
    }
}

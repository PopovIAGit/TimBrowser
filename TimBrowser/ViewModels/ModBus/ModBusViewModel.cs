using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Communication;

namespace TimBrowser.ViewModels
{
    public class ModBusViewModel : DownloadCommBase
    {
        public ModBusViewModel() : base()
        {

            _communication = null;

        }

        public override void DownloadBehaviour(bool isDownloading)
        {

        }

        public override bool IsBusy
        {
            get;
            set;
        }

        private ITimCommunication _communication;
        public override ITimCommunication Communication
        {
            get { return _communication; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using TimBrowser.DataCore.Communication;

namespace TimBrowser.ViewModels
{
    public abstract class DownloadCommBase : Screen
    {
        public DownloadCommBase()
        {

        }

        protected virtual void RaiseConnectAction(bool canDownload)
        {
            if (ConnectAction != null)
            {
                ConnectAction(canDownload);
            }
        }

        public abstract void DownloadBehaviour(bool isDownloading);


        public virtual void Activate() { }
        public virtual void Deactivate() { }


        public abstract ITimCommunication Communication
        {
            get;
        }

        public abstract bool IsBusy { get; set; }

        public Action<bool> ConnectAction { get; set; }
    }
}

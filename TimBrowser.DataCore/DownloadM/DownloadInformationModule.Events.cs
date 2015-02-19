using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.DownloadM.Events;

namespace TimBrowser.DataCore.DownloadM
{
    // Events
    public partial class DownloadInformationModule
    {
        private void ThrowDownloadException(DownloadErrorCode errorCode)
        {
            throw new DownloadException(errorCode);
        }
    }
}

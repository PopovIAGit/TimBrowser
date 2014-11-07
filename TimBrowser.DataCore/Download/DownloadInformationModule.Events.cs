using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Download.Events;

namespace TimBrowser.DataCore.Download
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

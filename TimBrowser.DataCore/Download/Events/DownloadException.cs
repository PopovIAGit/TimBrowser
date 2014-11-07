using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.Download.Events
{
    public class DownloadException : ApplicationException
    {
        public DownloadException(DownloadErrorCode errorCode)
        {
            _errorCode = errorCode;
        }

        private DownloadErrorCode _errorCode;

        public DownloadErrorCode ErrorCode
        {
            get { return _errorCode; }
        }
    }
}

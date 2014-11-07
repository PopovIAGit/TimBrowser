using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Bluetooth.Codes;

namespace TimBrowser.Bluetooth.Events
{
    public class DriverErrorEventArgs : EventArgs
    {
        private DriverErrCode _errorCode;

        public DriverErrorEventArgs(DriverErrCode errorCode)
        {
            _errorCode = errorCode;
        }

        public DriverErrCode ErrorCode
        {
            get { return _errorCode; }
        }
    }
}

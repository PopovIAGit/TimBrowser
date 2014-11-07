using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.Bluetooth.Codes
{
    public enum DriverErrCode
    {
        NoErr = 0,
        CannotCreateClient = 10,
        DiscoveringInProgress = 20,
        DiscoveringError,
        ConnectError = 30
    }
}

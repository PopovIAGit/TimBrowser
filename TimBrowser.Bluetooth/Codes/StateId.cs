using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.Bluetooth.Codes
{
    public enum StateId
    {
        Initial = 1,
        Discovering,
        Discovered,
        Authorization,
        Authorized,
        Connecting,
        Connected,
        Disconnecting,
        Disconnected
    }
}

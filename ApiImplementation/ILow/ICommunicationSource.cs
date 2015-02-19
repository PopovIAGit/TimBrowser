using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.ApiImplementation.Communication;

namespace TimBrowser.ApiImplementation.ILow
{
    public interface ICommunicationSource
    {
        ILowCommunicationFunc LowCommunicationFunc { get; set; }
        IProtocolCommunicationFunc ProtocolCommunicationFunc { get; set; }

        int DeviceAddress { get; set; }

        bool IsUpdating { get; set; }
        bool IsBusy { get; }
        bool CanCommunicate { get; }

        EventHandler<CommErrorEventArgs> CommErrorEvent { get; set; }
    }
}

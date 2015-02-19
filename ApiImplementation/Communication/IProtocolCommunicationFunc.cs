using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.ApiImplementation.Communication
{
    public interface IProtocolCommunicationFunc
    {
        ushort[] ReadBytes(int deviceAddr, int startAddr, int count);
        void WriteBytes(int deviceAddr, int startAddr, ushort[] buffer);

        int MaxCount { get; }

        bool IsBusy { get; }
        bool IsError { get; }

        Action<string> CommErrorAction { get; set; }
    }
}

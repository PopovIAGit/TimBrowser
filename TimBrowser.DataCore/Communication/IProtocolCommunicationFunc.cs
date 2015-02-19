using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.Communication
{
    public interface IProtocolCommunicationFunc
    {
        //ushort[] ReadByte(int deviceAddr, int startAddr, int count);
        ushort[] ReadBytes(int deviceAddr, int startAddr, int count);
        //void WriteByte(int deviceAddr, int startAddr, ushort[] buffer);
        void WriteBytes(int deviceAddr, int startAddr, ushort[] buffer);

        int MaxCount { get; }

        bool IsBusy { get; }
        bool IsError { get; }

        Action<string> CommErrorAction { get; set; }
    }
}

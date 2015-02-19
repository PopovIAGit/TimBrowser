using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.ApiImplementation.ILow
{
    public interface ILowCommunicationFunc
    {
        byte ReadByte();
        byte[] ReadBytes(int count);
        void WriteByte(byte data);
        void WriteBytes(byte[] data);

        bool IsBusy { get; }

        Action<CommunicationErrors> CommErrorAction { get; set; }
     }
}

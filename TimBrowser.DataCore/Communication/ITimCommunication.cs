using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.Communication
{
    public interface ITimCommunication
    {
        event EventHandler<CommErrorEventArgs> OnCommError;

        byte ReceiveByte();                         // Прием байт
        byte[] ReceiveBytes(int count);
        void TransmitByte(byte data);               // Передача байт
        void TransmitBytes(byte[] data);

        bool CanCommunicate { get; }

        void SetConnected(bool connected);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimBrowser.DataCore.Communication
{
    public interface IBLECommunication
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

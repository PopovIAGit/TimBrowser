using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TimBrowser.DataCore.Download
{
    // Communication
    public partial class DownloadInformationModule
	{
        private byte ReceiveByte()
        {
            return _communication.ReceiveByte();
        }

        private byte[] ReceiveBytes(int count)
        {
            return _communication.ReceiveBytes(count);
        }

        private void TransmitByte(byte data)
        {
            _communication.TransmitByte(data);
        }

        private void TransmitBytes(byte[] data)
        {
            _communication.TransmitBytes(data);
        }
	}
}



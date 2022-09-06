using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TimBrowser.DataCore.DownloadM
{
    // Communication
    public partial class DownloadInformationModule
	{
        private ushort ReceiveByte(int deviceAddr, int startAddr, int count)
        {
            ushort[] Data = _communication.ProtocolCommunicationFunc.ReadBytes(deviceAddr, startAddr, 1);
            return Data[0];// _communication.ProtocolCommunicationFunc.ReadBytes(deviceAddr, startAddr, 1);    // ReceiveByte();
        }

        private ushort[] ReceiveBytes(int deviceAddr, int startAddr, int count)
        {
            //ushort[] Data = { 1, 2 };
            /*ushort[] Data = _communication.ProtocolCommunicationFunc.ReadBytes(deviceAddr, startAddr, count);
            byte[] dat;
            return dat;// _communication.ProtocolCommunicationFunc.ReadBytes();    // ReceiveBytes(count);
            */
            return _communication.ProtocolCommunicationFunc.ReadBytes(deviceAddr, startAddr, count); 
        }

        private void TransmitByte(int deviceAddr, int startAddr, ushort[] buffer)
        {
            ushort[] buff = new ushort[1];
            buff[0] = buffer[0];
            _communication.ProtocolCommunicationFunc.WriteBytes(deviceAddr, startAddr, buff);          // TransmitByte(data);
        }

        private void TransmitBytes(int deviceAddr, int startAddr, ushort[] buffer)
        {
            _communication.ProtocolCommunicationFunc.WriteBytes(deviceAddr, startAddr, buffer);          //TransmitBytes(data);
        }
	}
}



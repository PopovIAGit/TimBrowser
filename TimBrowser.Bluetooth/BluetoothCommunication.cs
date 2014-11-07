using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TimBrowser.DataCore.Communication;



namespace TimBrowser.Bluetooth
{
    public class BluetoothCommunication : ITimCommunication
    {
        public BluetoothCommunication(Stream commStream)
        {
            _commStream = commStream;
        }

        private Stream _commStream;
        private bool  _canCommunicate;
        public UInt16 _repeat;

        public event EventHandler<CommErrorEventArgs> OnCommError;

        public byte ReceiveByte()
        {
            byte data = 0;

            try
            {
                _commStream.ReadTimeout = 700;
                data = Convert.ToByte(_commStream.ReadByte());
            }
            catch (Exception)
            {
                /*if (_repeat < 6)
                {
                    _repeat++;
                    return 0xff;
                } 
                else */
                RaiseCommErrEvent(CommunicationError.ReceiveError);
                
            }
            //_repeat = 0;
            return data;
        }

        public byte[] ReceiveBytes(int count)
        {
            if (count == 0)
                return null;

            byte[] dataArray = new byte[count];

            for (int i = 0; i < count; i++)
            {
                dataArray[i] = ReceiveByte();
            }
            
            return dataArray;
        }

        public void TransmitByte(byte data)
        {
            try
            {
                _commStream.WriteByte(data);
            }
            catch (Exception)
            {
                RaiseCommErrEvent(CommunicationError.TransmitError);
            }
        }

        public void TransmitBytes(byte[] data)
        {
            int count = data.Length;

            if (count == 0)
                return;

            for (int i = 0; i < count; i++)
            {
                TransmitByte(data[i]);
            }
        }

        public void SetConnected(bool connected)
        {
            _canCommunicate = connected;
        }

        public bool CanCommunicate
        {
            get { return _canCommunicate; }
        }

        private void RaiseCommErrEvent(CommunicationError errCode)
        {
            if (OnCommError != null)
            {
                OnCommError(this, new CommErrorEventArgs(errCode));
            }
        }
    }
}

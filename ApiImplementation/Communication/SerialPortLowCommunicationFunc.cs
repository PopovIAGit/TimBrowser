using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using TimBrowser.ApiImplementation.ILow;

namespace TimBrowser.ApiImplementation.Communication
{
    public class SerialPortLowCommunicationFunc : ILowCommunicationFunc
    {
        public SerialPortLowCommunicationFunc(SerialPort serialPort)
        {
            _serialPort = serialPort;
        }

        private SerialPort _serialPort;
        private bool _isBusy;

        public byte ReadByte()
        {
            _isBusy = true;

            byte data = (byte)_serialPort.ReadByte();

            _isBusy = false;

            return data;
        }

        public byte[] ReadBytes(int count)
        {
            if (count == 0)
                return null;

            _isBusy = true;

            byte[] dataArray = new byte[count];

            for (int i = 0; i < count; i++)
            {
                dataArray[i] = ReadByte();
            }

            _isBusy = false;

            return dataArray;
        }

        public void WriteByte(byte data)
        {
            _isBusy = true;

            byte[] dataArray = { data };

            _serialPort.Write(dataArray, 0, 1);

            _isBusy = false;
        }

        public void WriteBytes(byte[] data)
        {
            int count = data.Length;

            if (count == 0)
                return;

            _isBusy = true;

            for (int i = 0; i < count; i++)
            {
                WriteByte(data[i]);
            }

            _isBusy = false;
        }

        public bool IsBusy
        {
            get { return _isBusy; }
        }

        public Action<CommunicationErrors> CommErrorAction { get; set; }

    }
}

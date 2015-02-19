using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.Model
{
    public static class SerialPortData
    {
        static SerialPortData()
        {
            _comPorts = new List<string>();

            for (int i = 1; i <= 35; i++)
            {
                _comPorts.Add("COM" + i.ToString());
            }

            _baudRates = new List<int>()
            {
                2400, 4800, 9600, 19200, 38400, 57600, 115200
            };
        }

        private static List<string> _comPorts;
        private static List<int> _baudRates;

        public static List<string> ComPorts  { get { return _comPorts; } }
        public static List<int>    BaudRates { get { return _baudRates; } }
    }

    public enum MbParity
    {
        None = (int)System.IO.Ports.Parity.None,
        Odd = (int)System.IO.Ports.Parity.Odd,
        Even = (int)System.IO.Ports.Parity.Even
    }

    public enum MbStopBits
    {
        One = (int)System.IO.Ports.StopBits.One,
        Two = (int)System.IO.Ports.StopBits.Two
    }

}

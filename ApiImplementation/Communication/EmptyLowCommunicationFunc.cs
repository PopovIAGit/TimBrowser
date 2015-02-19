using TimBrowser.ApiImplementation.ILow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.ApiImplementation.Communication
{
    public class EmptyLowCommunicationFunc : ILowCommunicationFunc
    {
        public byte ReadByte()
        {
            return 0;
        }

        public byte[] ReadBytes(int count)
        {
            byte[] data = { 0, 0 };

            return data;
        }

        public void WriteByte(byte data)
        {

        }

        public void WriteBytes(byte[] data)
        {

        }

        public Action<CommunicationErrors> CommErrorAction { get; set; }



        public bool IsBusy
        {
            get { return false; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.ApiImplementation.ILow
{
    public class CommErrorEventArgs : EventArgs
    {
        public CommErrorEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}

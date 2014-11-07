using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.Communication
{
    public class CommErrorEventArgs : EventArgs
    {
        public CommErrorEventArgs(CommunicationError commErrorCode)
        {
            _commErrorCode = commErrorCode;
        }

        private CommunicationError _commErrorCode;

        public CommunicationError CommErrorCode
        {
            get { return _commErrorCode; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.Communication
{
    public class CommunicationException : ApplicationException
    {
        private CommunicationError _communicationErrorCode;

        public CommunicationException(CommunicationError communicationErrorCode)
        {
            _communicationErrorCode = communicationErrorCode;
        }

        public CommunicationError CommunicationErrorCode
        {
            get { return _communicationErrorCode; }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Services;

namespace TimBrowser.Messages
{
    public class TimErrorEventArgs : EventArgs
    {
        public TimErrorEventArgs(ServiceSources source, ErrorMessageTypes messageType, string addString = "")
        {
            _source = source;
            _messageType = messageType;
            _addString = addString;
        }

        private ServiceSources _source;
        private ErrorMessageTypes _messageType;
        private string _addString;

        public ServiceSources Source
        {
            get { return _source; }
        }

        public ErrorMessageTypes MessageType
        {
            get { return _messageType; }
        }

        public string AddString
        {
            get { return _addString; }
        }
    }
}

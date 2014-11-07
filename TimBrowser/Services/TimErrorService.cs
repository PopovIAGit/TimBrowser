using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Messages;

namespace TimBrowser.Services
{
    public class TimErrorService
    {
        public TimErrorService()
        {

        }

        public EventHandler<TimErrorEventArgs> TimErrorEvent;

        public void SendErrorMessage(ServiceSources source, ErrorMessageTypes messageType, string addString = "")
        {
            if (messageType == ErrorMessageTypes.None)
                return;

            if (TimErrorEvent != null)
                TimErrorEvent(this, new TimErrorEventArgs(source, messageType, addString));
        }

    }

    public enum ServiceSources
    {
        None = 0,
        DownloadService = 1,
        FileService,
        DataService
    }

    public enum ErrorMessageTypes
    {
        None = 0,
        DownloadImError = 1,
        TransformImError
    }

}

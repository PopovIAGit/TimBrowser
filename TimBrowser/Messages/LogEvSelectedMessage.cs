using System;
using TimBrowser.Model;

namespace TimBrowser.Messages
{
    public class LogEvSelectedMessage
    {
        public LogEvSelectedMessage(TimLogEvRecItem timLogEvRecSelectedItem)
        {
            TimLogEvRecSelectedItem = timLogEvRecSelectedItem;
        }

        public TimLogEvRecItem TimLogEvRecSelectedItem
        {
            get;
            private set;
        }



    }
}

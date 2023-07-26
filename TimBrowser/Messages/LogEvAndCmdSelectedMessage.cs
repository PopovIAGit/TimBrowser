using System;
using TimBrowser.Model;

namespace TimBrowser.Messages
{
    public class LogEvAndCmdSelectedMessage
    {
        public LogEvAndCmdSelectedMessage(TimLogEvAndCmdRecItem timLogEvRecSelectedItem)
        {
            TimLogEvRecSelectedItem = timLogEvRecSelectedItem;
        }

        public TimLogEvAndCmdRecItem TimLogEvRecSelectedItem
        {
            get;
            private set;
        }



    }
}

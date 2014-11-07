using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Model;
using TimBrowser.DataCore.Model.Logs;
using TimBrowser.Helper;
using TpeParameters.Model;

namespace TimBrowser.Mapper
{
    public static class LogMapper
    {
        public List<TimLogEvRecItem> MapLogEv(List<LogEventRecordItem> logEventRecords)
        {
            if (logEventRecords == null)
                return null;

            List<TimLogEvRecItem> timLogEvRecords = new List<TimLogEvRecItem>();


            foreach (var ler in logEventRecords)
            {
                string dateTimeString = ler.LogEventMainCell.DateAndTime.
                    ToString(Constants.DATE_TIME_FORMAT_STRING);

                string name = ler.LogEventMainCell.Id.ToString();
                bool isSet = ler.LogEventMainCell.Set;
            }

            /*
            _parameters = parameters;
             */


            return null;
        }

    }
}

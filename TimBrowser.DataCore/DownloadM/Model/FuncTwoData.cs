using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.DownloadM.Model
{
    [Serializable]
    public class FuncTwoData
    {
        public FuncTwoData(List<FuncTwoItem> logsData)
        {
            _logsData = logsData;
        }

        private List<FuncTwoItem> _logsData;

        public List<FuncTwoItem> LogsData
        {
            get { return _logsData; }
        }
    }

    [Serializable]
    public class FuncTwoItem
    {
        private int _logCurrentAddress;
        private int _logRecordsNumber;

        public FuncTwoItem(int logCurrentAddress, int logRecordsNumber)
        {
            _logCurrentAddress = logCurrentAddress;
            _logRecordsNumber = logRecordsNumber;
        }

        /// <summary>
        /// Текущий адрес журнала
        /// </summary>
        public int LogCurrentAddress
        {
            get { return _logCurrentAddress; }
        }

        /// <summary>
        /// Текущее число записей журнала
        /// </summary>
        public int LogRecordsNumber
        {
            get { return _logRecordsNumber; }
        }
    }
}

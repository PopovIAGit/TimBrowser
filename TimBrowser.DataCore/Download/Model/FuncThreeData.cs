using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.Download.Model
{
    [Serializable]
    public class FuncThreeData
    {
        public FuncThreeData(List<FuncThreeItem> logsData)
        {
            _logsData = logsData;
        }

        private List<FuncThreeItem> _logsData;

        public List<FuncThreeItem> LogsData
        {
            get { return _logsData; }
        }

    }

    [Serializable]
    public class FuncThreeItem
    {
        public FuncThreeItem(byte logCellNumber, List<int> logCellFieldNumber)
        {
            _logCellNumber = logCellNumber;
            _logCellFieldNumber = logCellFieldNumber;
        }

        private byte _logCellNumber;
        private List<int> _logCellFieldNumber;


        /// <summary>
        /// Количество ячеек журнала
        /// </summary>
        public byte LogCellNumber
        {
            get { return _logCellNumber; }
        }

        /// <summary>
        /// Количество полей журнала
        /// </summary>
        public List<int> LogCellFieldNumber
        {
            get { return _logCellFieldNumber; }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.Download.Model
{
    [Serializable]
    public class FuncFourData
    {
        public FuncFourData(List<FuncFourCells> logsData)
        {
            _logsData = logsData;
        }

        private List<FuncFourCells> _logsData;

        public List<FuncFourCells> LogsData
        {
            get { return _logsData; }
        }
    }

    [Serializable]
    public class FuncFourCells
    {
        public FuncFourCells(List<FuncFourFieldsAddress> logsCells)
        {
            _logsCells = logsCells;
        }

        private List<FuncFourFieldsAddress> _logsCells;

        /// <summary>
        /// Ячейки
        /// </summary>
        public List<FuncFourFieldsAddress> LogsCells
        {
            get { return _logsCells; }
        }

    }

    [Serializable]
    public class FuncFourFieldsAddress
    {
        public FuncFourFieldsAddress(List<int> logFieldsAddress)
        {
            _logFieldsAddress = logFieldsAddress;
        }

        private List<int> _logFieldsAddress;

        /// <summary>
        /// Адресс текущего поля
        /// </summary>
        public List<int> LogFieldsAddress
        {
            get { return _logFieldsAddress; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.DownloadM.Model
{
    [Serializable]
    public class FuncFiveData
    {
        public FuncFiveData(List<FuncFiveRecords> logsData)
        {
            _logsData = logsData;
        }

        private List<FuncFiveRecords> _logsData;

        public List<FuncFiveRecords> LogsData
        {
            get { return _logsData; }
        }
    }

    [Serializable]
    public class FuncFiveRecords
    {
        public FuncFiveRecords(List<FuncFiveCells> logFieldRecords)
        {
            _logFieldRecords = logFieldRecords;
        }

        private List<FuncFiveCells> _logFieldRecords;

        public List<FuncFiveCells> LogFieldRecords
        {
            get { return _logFieldRecords; }
        }
    }

    [Serializable]
    public class FuncFiveCells
    {
        public FuncFiveCells(List<FuncFiveValues> logFieldCells)
        {
            _logFieldCells = logFieldCells;
        }

        private List<FuncFiveValues> _logFieldCells;

        /// <summary>
        /// Ячейки
        /// </summary>
        public List<FuncFiveValues> LogFieldCells
        {
            get { return _logFieldCells; }
        }
    }

    [Serializable]
    public class FuncFiveValues
    {
        public FuncFiveValues(List<int> logFieldValues)
        {
            _logFieldValues = logFieldValues;
        }

        private List<int> _logFieldValues;

        /// <summary>
        /// Значения полей
        /// </summary>
        public List<int> LogFieldValues
        {
            get { return _logFieldValues; }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeParameters.Model;

namespace TimBrowser.DataCore.Model.Logs
{
    public class LogEventRecordItem
    {
        public LogEventRecordItem(LogEventMainCellItem logEventMainCell,
            List<LogEventBufferCellItem> logEventBufferCells)
        {
            _logEventMainCell = logEventMainCell;
            _logEventBufferCells = logEventBufferCells;
        }

        private LogEventMainCellItem _logEventMainCell;
        private List<LogEventBufferCellItem> _logEventBufferCells;

        public LogEventMainCellItem LogEventMainCell
        {
            get { return _logEventMainCell; }
        }

        public List<LogEventBufferCellItem> LogEventBufferCells
        {
            get { return _logEventBufferCells; }
        }

    }



    public class LogEventMainCellItem
    {
        public LogEventMainCellItem(DateTime dateAndTime, string code, string id, int recordIndex, bool set,
            List<ParameterItem> parameters)
        {
            _dateAndTime = dateAndTime;
            _code = code;
            _id = id;
            _set = set;
            _parameters = parameters;
            _recordIndex = recordIndex;
        }

    #region Fields

    private DateTime _dateAndTime;
    private string _code;
    private string _id;
    private bool _set;
    private List<ParameterItem> _parameters;
    private int _recordIndex;

    #endregion

    #region Properties

    /// <summary>
    /// Дата и время текущий ячейки
    /// </summary>
    public DateTime DateAndTime
    {
        get { return _dateAndTime; }
    }

    /// <summary>
    /// Код события
    /// </summary>
    public string Code
    {
        get { return _code; }
    }

    /// <summary>
    /// Идентификатор события
    /// </summary>
    public string Id
    {
        get { return _id; }
    }

    /// <summary>
    /// Флаг "выставлено"
    /// </summary>
    public bool Set
    {
        get { return _set; }
    }

    /// <summary>
    /// Список параметров текущего события
    /// </summary>
    public List<ParameterItem> Parameters
    {
        get { return _parameters; }
    }

    /// <summary>
    /// Номер текущий записи
    /// </summary>
    public int RecordIndex
    {
        get { return _recordIndex; }
    }


    #endregion

    }

    public class LogEventBufferCellItem
    {
        /// <summary>
        /// Буферная ячейка в записи журнала событий
        /// </summary>
        /// <param name="dateAndTime_">Дата и время записи ячейки</param>
        /// <param name="parameters_">Список параметров</param>
        public LogEventBufferCellItem(DateTime dateAndTime, List<ParameterItem> parameters)
        {
            _dateAndTime = dateAndTime;
            _parameters = parameters;
        }

        #region Properties

        private DateTime _dateAndTime;
        private List<ParameterItem> _parameters;

        /// <summary>
        /// Дата и время текущей ячейки
        /// </summary>
        public DateTime DateAndTime
        {
            get { return _dateAndTime; }
        }

        /// <summary>
        /// Список параметров текущей ячейки
        /// </summary>
        public List<ParameterItem> Parameters
        {
            get { return _parameters; }
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeParameters.Model;
using TimBrowser.DataCore.Helpers;

namespace TimBrowser.DataCore.Model.Logs
{
    public class LogEventAndCmdRecordItem
    {
        public LogEventAndCmdRecordItem(LogEventAndCmdMainCellItem logEventAndCmdMainCell,
            List<LogEventAndCmdBufferCellItem> logEventAndCmdBufferCells)
        {
            _logEventAndCmdMainCell = logEventAndCmdMainCell;
            _logEventAndCmdBufferCells = logEventAndCmdBufferCells;
        }

        private LogEventAndCmdMainCellItem _logEventAndCmdMainCell;
        private List<LogEventAndCmdBufferCellItem> _logEventAndCmdBufferCells;

        public LogEventAndCmdMainCellItem LogEventAndCmdMainCell
        {
            get { return _logEventAndCmdMainCell; }
        }

        public List<LogEventAndCmdBufferCellItem> LogEventAndCmdBufferCells
        {
            get { return _logEventAndCmdBufferCells; }
        }

    }


    public class LogEventAndCmdMainCellItem
    {
        public LogEventAndCmdMainCellItem(DateTime dateAndTime, string code, string id, int recordIndex, bool set, ParameterItem command, ParameterItem status, ParameterItem statusDig,
            CommandSource commandSource, List<ParameterItem> parameters)
        {
            _dateAndTime = dateAndTime;
            _code = code;
            _id = id;
            _set = set;
            _parameters = parameters;
            _recordIndex = recordIndex;

            _command = command;
            _status = status;
            _statusDig = statusDig;
            _commandSource = commandSource;
        }


        public LogEventAndCmdMainCellItem(DateTime dateAndTime, string code, string id, int recordIndex, bool set, ParameterItem command, ParameterItem status, 
            CommandSource commandSource, List<ParameterItem> parameters)
        {
            _dateAndTime = dateAndTime;
            _code = code;
            _id = id;
            _set = set;
            _parameters = parameters;
            _recordIndex = recordIndex;

            _command = command;
            _status = status;
            _statusDig = new ParameterItem();
            _commandSource = commandSource;
        }


    #region Fields

    private DateTime _dateAndTime;
    private string _code;
    private string _id;
    private bool _set;
    private List<ParameterItem> _parameters;
    private int _recordIndex;

        private ParameterItem _command;
        private ParameterItem _status;
        private ParameterItem _statusDig;
        private CommandSource _commandSource;

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

    public ParameterItem Command
    {
        get { return _command; }
    }

    public ParameterItem Status
    {
        get { return _status; }
    }

    public ParameterItem StatusDig
    {
        get { return _statusDig; }
    }

    public CommandSource commandSource
    {
        get { return _commandSource; }
    }



    #endregion

    }

    public class LogEventAndCmdBufferCellItem
    {
        /// <summary>
        /// Буферная ячейка в записи журнала событий
        /// </summary>
        /// <param name="dateAndTime_">Дата и время записи ячейки</param>
        /// <param name="parameters_">Список параметров</param>
        public LogEventAndCmdBufferCellItem(DateTime dateAndTime, List<ParameterItem> parameters)
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

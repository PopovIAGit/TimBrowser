using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.Model
{
    public class TimLogCmdRecItem
    {
        /// <summary>
        /// Одна запись журнала команд
        /// </summary>
        /// <param name="dateTimeString">Строковое значение даты и времени команды</param>
        /// <param name="cmdName">Наименование команды</param>
        /// /// <param name="srcCmdName">Наименование источника команды</param>
        /// <param name="statusParameter">Статусный регистр устройства во время команды</param>
        public TimLogCmdRecItem(int number, string dateTimeString, string cmdName, string srcCmdName, string moveDateTimeString,
            TimParameterItem statusParameter, string drivePosition)
        {
            _number = number;
            _drivePosition = drivePosition;
            //_dateTimeString = dateTimeString;
            DateTime.TryParse(dateTimeString, out _dateTimeString);
            _cmdName = cmdName;
            _srcCmdName = srcCmdName;
            _moveDateTimeString = moveDateTimeString;
            _statusParameter = statusParameter;
            _statusDigitalOut = null;
        }

        public TimLogCmdRecItem(int number, string dateTimeString, string cmdName, string srcCmdName, string moveDateTimeString,
            TimParameterItem statusParameter, TimParameterItem statusDigitalOut, string drivePosition)
        {
            _number = number;
            _drivePosition = drivePosition;
            //_dateTimeString = dateTimeString;
            DateTime.TryParse(dateTimeString, out _dateTimeString);
            _cmdName = cmdName;
            _srcCmdName = srcCmdName;
            _moveDateTimeString = moveDateTimeString;
            _statusParameter = statusParameter;
            _statusDigitalOut = statusDigitalOut;
        }

        #region Fields

        private int _number;
        private DateTime _dateTimeString;
        private string _cmdName;
        private string _srcCmdName;
        private string _moveDateTimeString;
        private string _drivePosition;
        private TimParameterItem _statusParameter;
        private TimParameterItem _statusDigitalOut;
        
        #endregion

        #region Properties

        public int Number
        {
            get { return _number; }
        }

        /// <summary>
        /// Дата и время команды
        /// </summary>
        public DateTime DateTimeString
        {
            get { return _dateTimeString; }
        }

        /// <summary>
        /// Наименование команды
        /// </summary>
        public string CmdName
        {
            get { return _cmdName; }
        }

        /// <summary>
        /// Наименование источника команды
        /// </summary>
        public string SrcCmdName
        {
            get { return _srcCmdName; }
        }

        /// <summary>
        /// Наименование источника команды
        /// </summary>
        public string MoveDateTimeString
        {
            get { return _moveDateTimeString; }
        }

        /// <summary>
        /// Положение привода
        /// </summary>
        public string DrivePosition
        {
            get { return _drivePosition; }
        }

        /// <summary>
        /// Статусный регистр устройства, записанный во время команды
        /// </summary>
        public TimParameterItem StatusParameter
        {
            get { return _statusParameter; }
        }

        /// <summary>
        /// Регистр состояния дискретных выходов, записанный во время команды
        /// </summary>
        public TimParameterItem StatusDigitalOut
        {
            get { return _statusDigitalOut; }
        }

        #endregion

    }
}

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
        /// <param name="statusParameter">Статусный регистр устройства во время команды</param>
        public TimLogCmdRecItem(int number, string dateTimeString, string cmdName, 
            TimParameterItem statusParameter)
        {
            _number = number;
            _dateTimeString = dateTimeString;
            _cmdName = cmdName;
            _statusParameter = statusParameter;
        }

        #region Fields

        private int _number;
        private string _dateTimeString;
        private string _cmdName;
        private TimParameterItem _statusParameter;
        
        #endregion

        #region Properties

        public int Number
        {
            get { return _number; }
        }

        /// <summary>
        /// Дата и время команды
        /// </summary>
        public string DateTimeString
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
        /// Статусный регистр устройства, записанный во время команды
        /// </summary>
        public TimParameterItem StatusParameter
        {
            get { return _statusParameter; }
        }

        #endregion

    }
}

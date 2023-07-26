using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.Model
{
    public class TimLogSimRecItem
    {
        /// <summary>
        /// Одна запись журнала команд
        /// </summary>
        /// <param name="dateTimeString">Строковое значение даты и времени команды</param>
        /// <param name="simId">номер сим карты</param>
        
        public TimLogSimRecItem(int number, string dateTimeString, string simId)
        {
            _number = number;
            DateTime.TryParse(dateTimeString, out _dateTimeString);
            _simId = simId;
         
        }

        #region Fields

        private int _number;
        private DateTime _dateTimeString;
        private string _simId;
        
        
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
        public string SimId
        {
            get { return _simId; }
        }

        #endregion

    }
}

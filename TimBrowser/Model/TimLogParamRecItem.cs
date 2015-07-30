using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.Model
{
    public class TimLogParamRecItem
    {
        /// <summary>
        /// Одна запись журнала изменения параметров
        /// </summary>
        /// <param name="dateTimeString">Строка даты и времени</param>
        /// <param name="name">Наименование параметра</param>
        /// <param name="valueString">Строковое значение параметра</param>
        /// <param name="timFields">Битовые поля или enum значения параметра</param>
        public TimLogParamRecItem(int number, string dateTimeString, string name, string valueString,
            List<TimParameterFieldItem> timFields)
        {
            _number = number;
            //_dateTimeString = dateTimeString;
            DateTime.TryParse(dateTimeString, out _dateTimeString);
            _name = name;
            _valueString = valueString;

            _timFields = timFields;
        }

        #region Fields

        private int _number;
        private DateTime _dateTimeString;
        private string _name;
        private string _valueString;

        private List<TimParameterFieldItem> _timFields;

        #endregion

        #region Properties

        public int Number
        {
            get { return _number; }
        }

        /// <summary>
        /// Строковое значение даты и времени
        /// </summary>
        public DateTime DateTimeString
        {
            get { return _dateTimeString; }
        }

        /// <summary>
        /// Наименование параметра
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Строковое значение параметра
        /// </summary>
        public string ValueString
        {
            get { return _valueString; }
        }

        /// <summary>
        /// Битовые поля или enum значение параметра
        /// </summary>
        public List<TimParameterFieldItem> TimFields
        {
            get { return _timFields; }
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.Model
{
    public class TimParameterItem
    {
        /// <summary>
        /// Параметр
        /// </summary>
        /// <param name="index">Индекс параметра. Например: "A1", "B10" и т.д. </param>
        /// <param name="dateTimeString">Строковое значение даты и времени</param>
        /// <param name="name">Наименование параметра</param>
        /// <param name="valueString">Строковое значение параметра. Например: 220В или 10Н*м</param>
        /// <param name="timFields">Битовые поля или enum значения параметра</param>
        public TimParameterItem(int number, string index, string dateTimeString, string name,
            string valueString, List<TimParameterFieldItem> timFields)
        {
            _number = number;
            _index = index;
            _dateTimeString = dateTimeString;
            _name = name;
            _valueString = valueString;
            _timFields = timFields;
        }

        #region Fields

        private int _number;
        private string _index;
        private string _dateTimeString;
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
        /// Индекс параметра
        /// </summary>
        public string Index
        {
            get { return _index; }
        }

        /// <summary>
        /// Строковое значение даты и времени
        /// </summary>
        public string DateTimeString
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
        /// Битовые поля или enum значения параметра
        /// </summary>
        public List<TimParameterFieldItem> TimFields
        {
            get { return _timFields; }
        }

        #endregion

    }


    public class TimParameterFieldItem
    {
        /// <summary>
        /// Одно битовое поле или одно enum значение параметра
        /// </summary>
        /// <param name="bitValue">Описываемый бит или число</param>
        /// <param name="description">Описание бита или числа</param>
        public TimParameterFieldItem(int bitValue, bool isSet, string description)
        {
            _bitValue = bitValue;
            _isSet = isSet;
            _description = description;     
        }

        private int _bitValue;
        private bool _isSet;
        private string _description;

        /// <summary>
        /// Описываемое значение
        /// </summary>
        public int BitValue
        {
            get { return _bitValue; }
        }

        /// <summary>
        /// Флаг: выставлено поле или нет
        /// </summary>
        public bool IsSet
        {
            get { return _isSet; }
        }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description
        {
            get { return _description; }
        }
    }
}

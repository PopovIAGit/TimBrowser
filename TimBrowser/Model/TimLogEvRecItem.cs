using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;


namespace TimBrowser.Model
{
    public class TimLogEvRecItem
    {
        /// <summary>
        /// Одна запись журнали события
        /// </summary>
        /// <param name="dateTimeString">Строковое значение даты и времени</param>
        /// <param name="name">Наименование события</param>
        /// <param name="isSet">Флаг: выставлено или снято событие</param>
        /// <param name="parameters">Основной список параметров</param>
        /// <param name="chosenParameters">Список избранных параметров</param>
        public TimLogEvRecItem(int number, string dateTimeString, string name, bool isSet,
            List<TimParameterItem> parameters, ObservableCollection<TimChosenParameterItem> chosenParameters)
        {
            _number = number;
            _dateTimeString = dateTimeString;
            _name = name;
            _isSet = isSet;
            _parameters = parameters;
            _chosenParameters = chosenParameters;
        }

        #region Fields

        private int _number;
        private string _dateTimeString;
        private string _name;
        private bool _isSet;
        private List<TimParameterItem> _parameters;
        //private ObservableCollection<TimParameterItem> _chosenParameters;
        private ObservableCollection<TimChosenParameterItem> _chosenParameters;

        #endregion

        #region Properties

        public int Number
        {
            get { return _number; }
        }

        /// <summary>
        /// Строкое значение даты и времени события
        /// </summary>
        public string DateTimeString
        {
            get { return _dateTimeString; }
        }

        /// <summary>
        /// Наименование события
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Флаг: выставлено событие или снято
        /// </summary>
        public bool IsSet
        {
            get { return _isSet; }
        }

        /// <summary>
        /// Основной список параметров события
        /// </summary>
        public List<TimParameterItem> Parameters
        {
            get { return _parameters; }
        }

        /// <summary>
        /// Список избранных параметров события
        /// </summary>
        public ObservableCollection<TimChosenParameterItem> ChosenParameters
        {
            get { return _chosenParameters; }
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;


namespace TimBrowser.Model
{
    public class TimLogEvAndCmdRecItem
    {
        //TODO здесь добавит параетры для вставки журнала сомманд
        /// <summary>
        /// Одна запись журнали события
        /// </summary>
        /// <param name="dateTimeString">Строковое значение даты и времени</param>
        /// <param name="name">Наименование события</param>
        /// <param name="isSet">Флаг: выставлено или снято событие</param>
        /// <param name="parameters">Основной список параметров</param>
        /// <param name="chosenParameters">Список избранных параметров</param>
        public TimLogEvAndCmdRecItem(int number, string dateTimeString, string name, int isSet, 
            List<TimParameterItem> parameters, ObservableCollection<TimChosenParameterItem> chosenParameters,
            string srcCmdName, string moveDateTimeString, TimParameterItem statusParameter)
        {
            _number = number;
            //_dateTimeString = dateTimeString;
            DateTime.TryParse(dateTimeString, out _dateTimeString);
            _name = name;
            _isSet = isSet;
            _parameters = parameters;
            _chosenParameters = chosenParameters;
             _srcCmdName = srcCmdName;
            _moveDateTimeString = moveDateTimeString;
            _statusParameter = statusParameter;
            _statusDigitalOut = null;
        }

        public TimLogEvAndCmdRecItem(int number, string dateTimeString, string name, int isSet, 
            List<TimParameterItem> parameters, ObservableCollection<TimChosenParameterItem> chosenParameters,
            string srcCmdName, string moveDateTimeString, TimParameterItem statusParameter, TimParameterItem statusDigitalOut)
        {
            _number = number;
            //_dateTimeString = dateTimeString;
            DateTime.TryParse(dateTimeString, out _dateTimeString);
            _name = name;
            _isSet = isSet;
            _parameters = parameters;
            _chosenParameters = chosenParameters;
             _srcCmdName = srcCmdName;
            _moveDateTimeString = moveDateTimeString;
            _statusParameter = statusParameter;
            _statusDigitalOut = statusDigitalOut;
        }
        
        #region Fields

        //TODO здесь добавит параетры для вставки журнала сомманд
        private int _number;
        private DateTime _dateTimeString;
        private string _name;
        private int _isSet;
        private List<TimParameterItem> _parameters;
        private ObservableCollection<TimChosenParameterItem> _chosenParameters;


        private string _srcCmdName;
        private string _moveDateTimeString;
        private TimParameterItem _statusParameter;
        private TimParameterItem _statusDigitalOut;

        #endregion

        #region Properties

        public int Number
        {
            get { return _number; }
        }

        /// <summary>
        /// Строкое значение даты и времени события
        /// </summary>
        public DateTime DateTimeString
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
        public int IsSet
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

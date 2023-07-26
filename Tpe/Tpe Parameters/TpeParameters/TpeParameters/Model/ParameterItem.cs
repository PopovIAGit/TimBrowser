using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeParameters.Helpers;

namespace TpeParameters.Model
{
    public class ParameterItem
    {
        public ParameterItem(int id, string index, string name, int address,
            string variableName, double DValue, String value, ParameterConfiguration configuration,
            ParameterValueDescription valueDescription, ParameterInfo info)
        {
            
            _id = id;
            _index = index;
            _name = name;
            _address = address;
            _variableName = variableName;

            _configuration = configuration;
            _valueDescription = valueDescription;
            _info = info;

            SetTextValue(value, DValue);
            SetUnsValue(DValue);
        }
      


        public ParameterItem(ParameterItem sourceParameter)
        {
            this._id = sourceParameter.Id;
            this._index = sourceParameter.Index;
            this._name = sourceParameter.Name;
            this._address = sourceParameter.Address;
            this._variableName = sourceParameter.VariableName;

            this._configuration = sourceParameter.Configuration;
            this._valueDescription = sourceParameter.ValueDescription;
            this._info = sourceParameter.Info;

            this.SetTextValue(sourceParameter.sValue.ToString(), sourceParameter.DValue);
            this.SetUnsValue(sourceParameter.DValue);
        }

        public ParameterItem()
        {
            // TODO: Complete member initialization
        }

        #region Fields

        private int _id;
        private string _index;
        private string _name;
        private int _address;
        private string _variableName;
        private double _value;
        private String _valueString;
        private ParameterConfiguration _configuration;
        private ParameterValueDescription _valueDescription;
        private ParameterInfo _info;

        #endregion

        private double TranformValue(double value)
        {
            if (_valueDescription != null)
            {
                return value * _valueDescription.Coefficient;
            }

            return value;
        }

        public void SetUnsValue(double unsValue)
        {
            // делаем преобразования знакового числа
            if (_valueDescription.ValueType == ParamValueTypes.Int)
            {
                if (unsValue > 32767)
                {
                    Int16 iv = (Int16)unsValue;
                    unsValue = (double)iv;
                }
            }

            // проверяем значение прежде чем присвоить его
            if (unsValue >= _valueDescription.Minimum && unsValue <= _valueDescription.Maximum)
                _value = TranformValue(unsValue);
        }

       
        public void SetTextValue(String unsValue,  double dValue)
        {
            // делаем преобразования знакового числа

            // тут необходимо прикрутить преобразование к строковому знаковому числу сос знаком
            if (_valueDescription.ValueType == ParamValueTypes.Int)
            {
                if (dValue > 32767)
                {
                    Int16 iv = (Int16)dValue;
                    dValue = (double)iv;
                }
            }

            // проверяем значение прежде чем присвоить его
            if (dValue >= _valueDescription.Minimum && dValue <= _valueDescription.Maximum)
                _value = TranformValue(dValue);

            // формирование по типам 
            //unsValue = "" ;
            if (_valueDescription.ValueType == ParamValueTypes.Time)
            {
                Int16 Date = (Int16)dValue;
                Int16 H = (Int16)((Date & 0xFFC0)>>6);
                Int16 m = (Int16)((Date & 0x003F));

                if (m > 9) unsValue = H.ToString() + ":" + m.ToString();
                else unsValue = H.ToString() + ":0" + m.ToString();
            }
            else if (_valueDescription.ValueType == ParamValueTypes.Date)
            {
                Int16 Date = (Int16)dValue;
                Int16 dd = (Int16)(Date&0x001F);
                Int16 mm = (Int16)((Date&0x01F0)>>5);
                Int16 yy = (Int16)(((Date&0xFE00)>>9)+2000);
                if (dd == 0) dd = 1;
                if (yy == 0) yy = 2008;

                unsValue = dd.ToString() + "/" + mm.ToString() + "/" + yy.ToString();
            } else if (_valueDescription.ValueType == ParamValueTypes.Union)
            {
                unsValue = "";
                Int16 NoFaultsNoStatus = 0;

                for (int i = 0; i < 16; i++)
                {
                    Int16 v = (Int16)dValue;
                    if (((v >> i) & 0x01) == 1)
                    {
                        NoFaultsNoStatus = 1;
                        unsValue += _valueDescription.Fields[i+1].Description + "\r\n";
                    }
                }
                if (NoFaultsNoStatus == 0) unsValue = _valueDescription.Fields[0].Description;

            }
            else if (_valueDescription.ValueType == ParamValueTypes.Enum)
            {
                unsValue = "";
                UInt16 v = (UInt16)dValue;
                if (v > 16) v = 0;
                try
                {
                    if (v <= _valueDescription.Fields.Count) unsValue = _valueDescription.Fields[v].Description;
                }
                catch (Exception e)
                {

                }

            }
            else
            {
                unsValue = _value.ToString();
            }

            //unsValue = _value.ToString() +"\r\n" + "123_testc" + "\r\n" + "test2";
            //unsValue.Add("test1");
            //unsValue.Add("test2");
            _valueString = unsValue;
        }

        public void SetDoubleValue(double doubleValue)
        {
            // проверяем значение прежде чем присвоить его
            if (doubleValue >= _valueDescription.Minimum && doubleValue <= _valueDescription.Maximum)
                _value = doubleValue;
        }

        public bool CheckRange(double doubleValue)
        {
            // проверяем значение прежде чем присвоить его
            if (doubleValue < _valueDescription.Minimum || doubleValue > _valueDescription.Maximum)
            {
                return false;
            }
            return true;
        }


        #region Properties

        public int Id
        {
            get { return _id; }
        }

        public string Index
        {
            get { return _index; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Address
        {
            set { _address = Address; }
            get { return _address; }
        }

        public string VariableName
        {
            get { return _variableName; }
        }

        // нужен пересчет значения в зависимости от конфигурации
        public String sValue
        {
            get { return _valueString; }
        }
        
        public double DValue
        {
            get { return _value; }
        }

        public ParameterConfiguration Configuration
        {
            get { return _configuration; }
        }

        public ParameterValueDescription ValueDescription
        {
            get { return _valueDescription; }
        }

        public ParameterInfo Info
        {
            get { return _info; }
        }

        #endregion

    }

    public class ParameterConfiguration
    {
        public ParameterConfiguration(ParamTypes paramType, ParamAppointments appointment, bool canEdit, bool isChosen)
        {
            _paramType = paramType;
            _appointment = appointment;
            _canEdit = canEdit;
            _isChosen = isChosen;
        }

        private ParamTypes _paramType;
        private ParamAppointments _appointment;
        private bool _canEdit;
        private bool _isChosen;

        public ParamTypes ParamType
        {
            get { return _paramType; }
        }

        public ParamAppointments Appointment
        {
            get { return _appointment; }
        }

        /// <summary>
        /// Указывает, что параметр может быть редактирован
        /// </summary>
        public bool CanEdit
        {
            get { return _canEdit; }
        }

        /// <summary>
        /// Параметры с этим флагом отображаются в панели "избранных параметров"
        /// </summary>
        public bool IsChosen
        {
            get { return _isChosen; }
        }
    }

    public class ParameterValueDescription
    {
        public ParameterValueDescription(double minimum, double maximum,
            double defValue, string unit, double coefficient, ParamValueTypes valueType,
            List<ParameterFieldItem> fields)
        {
            _coefficient = coefficient;
            _minimum = minimum;
            _maximum = maximum;
            _default = defValue;
            _unit = unit;            
            _valueType = valueType;
            _fields = fields;
        }

        #region Fields

        private double _minimum;
        private double _maximum;
        private double _default;
        private string _unit;

        private double _coefficient;

        private ParamValueTypes _valueType;
        private List<ParameterFieldItem> _fields;

        #endregion

        #region Properties

        /// <summary>
        /// Минимальное значение параметра
        /// </summary>
        public double Minimum
        {
            get { return _minimum; }
        }

        /// <summary>
        /// Максимальное значение параметра
        /// </summary>
        public double Maximum
        {
            get { return _maximum; }
        }

        /// <summary>
        /// Значение параметра по умолчанию
        /// </summary>
        public double Default
        {
            get { return _default; }
        }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Unit
        {
            get { return _unit; }
        }

        /// <summary>
        /// Подстроечный коэфициент
        /// </summary>
        public double Coefficient
        {
            get { return _coefficient; }
        }

        public ParamValueTypes ValueType
        {
            get { return _valueType; }
        }

        public List<ParameterFieldItem> Fields
        {
            get { return _fields; }
        }

        #endregion

    }

    public class ParameterFieldItem
    {
        public ParameterFieldItem(int bitValue, string description, string specialDescription)
        {
            _bitValue = bitValue;
            _description = description;
            _specialDescription = specialDescription;
        }

        private int _bitValue;
        private string _description;
        private string _specialDescription;

        public int BitValue
        {
            get { return _bitValue; }
        }

        public string Description
        {
            get { return _description; }
        }

        public string SpecialDescription
        {
            get { return _specialDescription; }
        }

    }

    public class ParameterInfo
    {
        public ParameterInfo(string description, string comment)
        {
            _description = description;
            _comment = comment;
        }

        private string _description;
        private string _comment;

        public string Description
        {
            get { return _description; }
        }

        public string Comment
        {
            get { return _comment; }
        }
    }
}

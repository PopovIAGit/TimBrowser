using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeParameters.Helpers;
using TpeParameters.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace TpeBuilder.Model
{

    public class TpeParameter : ModelBase
    {
        public TpeParameter(int id) : base()
        {
            _id = id;
            _index = "";
            _name = "Новый параметр";
            _address = 0;
            _variableName = "";
            _paramType = ParamTypes.None;
            _appointment = ParamAppointments.Regular;
            _canEdit = false;
            _isChosen = false;
            _minimum = 0;
            _maximum = 65535;
            _default = 0;
            _unit = "";
            _coefficient = 1;
            _valueType = ParamValueTypes.Uns;
            _fields = null;
            _description = "";
            _comment = "";

            GenerateFullName();

            //ParameterItem fd = new ParameterItem();
            //fd.Info.
        }


        #region Fields

        private string _fullName;

        private int _id;
        private string _index;
        private string _name;
        public int _address;
        private string _variableName;

        private ParamTypes _paramType;
        private ParamAppointments _appointment;
        private bool _canEdit;
        private bool _isChosen;

        private double _minimum;
        private double _maximum;
        private double _default;
        private string _unit;
        private double _coefficient;
        private ParamValueTypes _valueType;
        private ObservableCollection<TpeParameterFieldItem> _fields;

        private string _description;
        private string _comment;

        public Action OnValueTypeChanged;

        #endregion

        private void GenerateFullName()
        {
            FullName = _index + " " + _name + " (" + _address + ")";
        }


       #region Properties

        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                NotifyPropertyChanged("FullName");
            }
        }

        #region Root properties

        public int Id
        {
            get { return _id; }
        }

        public string Index
        {
            get { return _index; }
            set
            {
                _index = value;
                NotifyPropertyChanged("Index");

                GenerateFullName();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");

                GenerateFullName();
            }
        }

        public int Address
        {
            get { return _address; }
            set
            {
                _address = value;
                NotifyPropertyChanged("Address");

                GenerateFullName();
            }
        }

        public string VariableName
        {
            get { return _variableName; }
            set
            {
                _variableName = value;
                NotifyPropertyChanged("VariableName");
            }
        }

        #endregion

        #region Configuration properties

        public ParamTypes ParamType
        {
            get { return _paramType; }
            set
            {
                _paramType = value;
                NotifyPropertyChanged("ParamType");
            }
        }

        public ParamAppointments Appointment
        {
            get { return _appointment; }
            set
            {
                _appointment = value;
                NotifyPropertyChanged("Appointment");
            }
        }

        /// <summary>
        /// Признак, что можно редактировать параметр
        /// </summary>
        public bool CanEdit
        {
            get { return _canEdit; }
            set
            {
                _canEdit = value;
                NotifyPropertyChanged("CanEdit");
            }
        }

        /// <summary>
        /// Параметры с этим флагом отображаются в панели "избранных параметров"
        /// </summary>
        public bool IsChosen
        {
            get { return _isChosen; }
            set
            {
                _isChosen = value;
                NotifyPropertyChanged("IsChosen");
            }
        }

        #endregion

        #region Value Description

        /// <summary>
        /// Минимальное значение параметра
        /// </summary>
        public double Minimum
        {
            get { return _minimum; }
            set
            {
                _minimum = value;
                NotifyPropertyChanged("Minimum");
            }
        }

        /// <summary>
        /// Максимальное значение параметра
        /// </summary>
        public double Maximum
        {
            get { return _maximum; }
            set
            {
                _maximum = value;
                NotifyPropertyChanged("Maximum");
            }
        }

        /// <summary>
        /// Значение параметра по умолчанию
        /// </summary>
        public double Default
        {
            get { return _default; }
            set
            {
                _default = value;
                NotifyPropertyChanged("Default");
            }
        }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Unit
        {
            get { return _unit; }
            set
            {
                _unit = value;
                NotifyPropertyChanged("Unit");
            }
        }

        /// <summary>
        /// Подстроечный коэфициент
        /// </summary>
        public double Coefficient
        {
            get { return _coefficient; }
            set
            {
                _coefficient = value;
                NotifyPropertyChanged("Coefficient");
            }
        }

        public ParamValueTypes ValueType
        {
            get { return _valueType; }
            set
            {
                _valueType = value;
                NotifyPropertyChanged("ValueType");

                if (OnValueTypeChanged != null)
                    OnValueTypeChanged();
            }
        }

        public ObservableCollection<TpeParameterFieldItem> Fields
        {
            get { return _fields; }
            set
            {
                _fields = value;
                NotifyPropertyChanged("Fields");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }

        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                NotifyPropertyChanged("Comment");
            }
        }

        #endregion

        #endregion

    }

    public class TpeParameterFieldItem : ModelBase
    {
        public TpeParameterFieldItem(int bitValue, string description, string specialDescription) : base()
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
            set
            {
                _bitValue = value;
                NotifyPropertyChanged("BitValue");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }

        public string SpecialDescription
        {
            get { return _specialDescription; }
            set
            {
                _specialDescription = value;
                NotifyPropertyChanged("SpecialDescription");
            }
        }

    }
}

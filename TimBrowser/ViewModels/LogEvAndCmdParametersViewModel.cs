using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using TimBrowser.Model;
using Caliburn.Micro;
using System.Dynamic;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;

namespace TimBrowser.ViewModels
{
    public class LogEvAndCmdParametersViewModel : Screen
    {

        public LogEvAndCmdParametersViewModel(TimLogEvAndCmdRecItem logEvRec)
        {
            if (logEvRec != null)
            {
                IsEventSet = logEvRec.IsSet;
                EventName = logEvRec.Name;
                LogParameters = logEvRec.Parameters;
                ParameterFilterList = Helper.ViewModelUtils
                    .CreateParameterSortItemAndCmd(_logParameters, OnSortParamCheckedChanged);

                _logParametersView = CollectionViewSource.GetDefaultView(_logParameters);
                _logParametersView.Filter = ParameterFilter;
                _logParametersView.Refresh();
            }
        }

        #region Fields

        private int _isEventSet;
        private string _eventName;
        private ICollectionView _logParametersView;
        
        private List<TimParameterItem> _logParameters;
        private TimParameterItem _selectedParameter;
        private List<TimParameterFieldItem> _parameterFields;
        private string _parameterFieldsName;
        private List<ParameterSortItemAndCmd> _parameterFilterList;
        private bool _ignoreCheckedChanged;
        
        #endregion

        // обработка изменения состояния CheckBox'ов, т.е. сортировка таблицы параметров
        private void OnSortParamCheckedChanged(ParameterSortItemAndCmd sortItem)
        {
            // игнорировать сортировку, если она еще не завершена
            if (_ignoreCheckedChanged)
                return;

            // выделить все
            if (sortItem.Index == Helper.Constants.CHOOSE_ALL_INDEX)
            {
                sortItem.Name = (!sortItem.IsChecked) ? "Выделить все" : "Снять выделение";

                _ignoreCheckedChanged = true;

                foreach (var sp in _parameterFilterList)
                {
                    sp.IsChecked = sortItem.IsChecked;
                }

                _ignoreCheckedChanged = false;
            }

            // вызываем обновление CollectionViewSource, чтобы он "подхватил" изменения в сортировке
            _logParametersView.Refresh();
        }

        private bool ParameterFilter(object item)
        {
            TimParameterItem par = item as TimParameterItem;

            // фильтрация CollectionViewSource
            bool isFilter = _parameterFilterList.First(sp => sp.Index == par.Index).IsChecked;

            return isFilter;
        }

        #region Properties

        public ICollectionView LogParametersView
        {
            get { return _logParametersView; }
        }

        public int IsEventSet
        {
            get { return _isEventSet; }
            set
            {
                _isEventSet = value;
                NotifyOfPropertyChange("IsEventSet");
            }
        }

        public string EventName
        {
            get { return _eventName; }
            set
            {
                _eventName = value;
                NotifyOfPropertyChange("EventName");
            }
        }

        public List<TimParameterItem> LogParameters
        {
            get { return _logParameters; }
            private set
            {
                _logParameters = value;
                NotifyOfPropertyChange("LogParameters");
            }
        }
        
        public TimParameterItem SelectedParameter
        {
            get { return _selectedParameter; }
            set
            {
                _selectedParameter = value;
                NotifyOfPropertyChange("SelectedParameter");

                // отображение значение параметра типа Enum и List
                if (_selectedParameter != null)
                {
                    if (_selectedParameter.TimFields != null)
                    {
                        ParameterFields = _selectedParameter.TimFields;
                        ParameterFieldsName = _selectedParameter.Name;
                    }
                    else
                    {
                        ParameterFields = null;
                        ParameterFieldsName = String.Empty;
                    }
                }
            }
        }



        public string ParameterFieldsName
        {
            get { return _parameterFieldsName; }
            set
            {
                _parameterFieldsName = value;
                NotifyOfPropertyChange("ParameterFieldsName");
            }
        }



        public List<TimParameterFieldItem> ParameterFields
        {
            get { return _parameterFields; }
            set
            {
                _parameterFields = value;
                NotifyOfPropertyChange("ParameterFields");
            }
        }

        public List<ParameterSortItemAndCmd> ParameterFilterList
        {
            get { return _parameterFilterList; }
            set
            {
                _parameterFilterList = value;
                NotifyOfPropertyChange("ParameterSortList");
            }
        }


        #endregion

    }

    public class ParameterSortItemAndCmd : PropertyChangedBase
    {
        public ParameterSortItemAndCmd(bool isChecked, string name, string index, 
            System.Action<ParameterSortItemAndCmd> checkedChangedAction)
        {
            _isChecked = isChecked;
            _name = name;
            _index = index;
            _checkedChangedAction = checkedChangedAction;
        }

        private bool _isChecked;
        private string _name;
        private string _index;

        private System.Action<ParameterSortItemAndCmd> _checkedChangedAction;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                NotifyOfPropertyChange("IsChecked");

                if (_checkedChangedAction != null)
                    _checkedChangedAction(this);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange("Name");
            }
        }

        public string Index
        {
            get { return _index; }
        }

    }
}

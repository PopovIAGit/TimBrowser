using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TimBrowser.Model;
using TimBrowser.Services;

namespace TimBrowser.ViewModels
{
    public class ParametersTableViewModel : Screen
    {

        public ParametersTableViewModel(TimDataService timDataService, TimDataServiceM timDataServiceM)
        {
            _timDataService = timDataService;
            _timDataService.OnTimDataChanged += TimDataChanged;
            _timDataServiceM = timDataServiceM;
            _timDataServiceM.OnTimDataChangedM += TimDataChanged;


            _groupSelectedIndex = -1;
        }

        #region Fields

        private readonly TimDataService _timDataService;
        private readonly TimDataServiceM _timDataServiceM;

        private ObservableCollection<TimParametersGroup> _groups;
        private TimParametersGroup _selectedGroup;
        private List<TimParameterItem> _currentParameters;
        private TimParameterItem _selectedParameter;
        private string _parameterFieldsName;
        private List<TimParameterFieldItem> _parameterFields;
        private int _groupSelectedIndex;

        #endregion

        private void TimDataChanged(object sender, EventArgs e)
        {
            // добавляем новые данные
            Groups = _timDataService.GroupOfParameters;
            if (Groups == null) Groups = _timDataServiceM.GroupOfParameters;

            if (_groups != null)
                GroupSelectedIndex = 0;
        }

        #region Properties

        public ObservableCollection<TimParametersGroup> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                NotifyOfPropertyChange("Groups");
            }
        }

        public TimParametersGroup SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;
                NotifyOfPropertyChange("SelectedGroup");

                if (_selectedGroup != null)
                    CurrentParameters = _selectedGroup.Parameters;
            }
        }
       
        public List<TimParameterItem> CurrentParameters
        {
            get { return _currentParameters; }
            set
            {
                _currentParameters = value;
                NotifyOfPropertyChange("CurrentParameters");     
            }
        }

        public TimParameterItem SelectedParameter
        {
            get { return _selectedParameter; }
            set
            {
                _selectedParameter = value;
                NotifyOfPropertyChange("SelectedParameter");

                // отображение параметров типа Enum и List
                if (_selectedParameter != null)
                {
                    if (_selectedParameter.TimFields != null)
                    {
                        ParameterFieldsName = _selectedParameter.Name;
                        ParameterFields = _selectedParameter.TimFields;
                    }
                    else
                    {
                        ParameterFieldsName = String.Empty;
                        ParameterFields = null;
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

        public int GroupSelectedIndex
        {
            get { return _groupSelectedIndex; }
            set
            {
                _groupSelectedIndex = value;
                NotifyOfPropertyChange("GroupSelectedIndex");
            }
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Services;
using System.Collections.ObjectModel;
using TimBrowser.Model;
using Caliburn.Micro;

namespace TimBrowser.ViewModels
{
    public class LogParamViewModel : Screen
    {
        public LogParamViewModel(TimDataService timDataService)
        {
            _timDataService = timDataService;
            _timDataService.OnTimDataChanged += TimDataChanged;
        }

        #region Properties

        private readonly TimDataService _timDataService;
        private ObservableCollection<TimLogParamRecItem> _logParam;
        private TimLogParamRecItem _selectedLogParam;
        private string _parameterTitle;
        private List<TimParameterFieldItem> _parameterFields;

        #endregion

        private void TimDataChanged(object sender, EventArgs e)
        {
            // добавляем новые данные
            LogParam = _timDataService.LogParameter;
        }

        private void OnSelectedLogParamChanged()
        {
            if (_selectedLogParam != null)
            {
                if (_selectedLogParam.TimFields != null)
                {
                    ParameterTitle = _selectedLogParam.Name;
                    ParameterFields = _selectedLogParam.TimFields;
                }
                else
                {
                    ParameterTitle = String.Empty;
                    ParameterFields = null;
                }
            }
        }

        #region Properties

        public ObservableCollection<TimLogParamRecItem> LogParam
        {
            get { return _logParam; }
            set
            {
                _logParam = value;
                NotifyOfPropertyChange("LogParam");
            }
        }

        public TimLogParamRecItem SelectedLogParam
        {
            get { return _selectedLogParam; }
            set
            {
                _selectedLogParam = value;
                NotifyOfPropertyChange("SelectedLogParam");

                OnSelectedLogParamChanged();

            }
        }

        public string ParameterTitle
        {
            get { return _parameterTitle; }
            set
            {
                _parameterTitle = value;
                NotifyOfPropertyChange("ParameterTitle");
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

        #endregion



    }
}

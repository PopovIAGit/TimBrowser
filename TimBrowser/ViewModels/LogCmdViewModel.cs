using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using TimBrowser.Model;
using Caliburn.Micro;
using TimBrowser.Services;

namespace TimBrowser.ViewModels
{
    public class LogCmdViewModel : Screen
    {
        public LogCmdViewModel(TimDataService timDataService, TimDataServiceM timDataServiceM)
        {
            _timDataService = timDataService;
            _timDataService.OnTimDataChanged += TimDataChanged;
            _timDataServiceM = timDataServiceM;
            _timDataServiceM.OnTimDataChangedM += TimDataChanged;
            StatusRegName = "статусный регистр устройства";
        }

        #region Fields
        
        private readonly TimDataService _timDataService;
        private readonly TimDataServiceM _timDataServiceM;
        private ObservableCollection<TimLogCmdRecItem> _logCmd;
        private TimLogCmdRecItem _selectedLogCmdItem;
        private List<TimParameterFieldItem> _statusReg;
        private string _moveTimeString;

        #endregion

        private void TimDataChanged(object sender, EventArgs e)
        {
            // добавляем новые данные
            LogCmd = _timDataService.LogCmd;
            if (LogCmd == null)
            {
                LogCmd = _timDataServiceM.LogCmd;
                MoveTimeString = _timDataServiceM.MoveTimeString;
            }
            else MoveTimeString = _timDataService.MoveTimeString;
        }

        private void SelectedItemChanged()
        {
            if (_selectedLogCmdItem != null)
                StatusReg = _selectedLogCmdItem.StatusParameter.TimFields;
        }

        #region Properties
      
        public ObservableCollection<TimLogCmdRecItem> LogCmd
        {
            get { return _logCmd; }
            set
            {
                _logCmd = value;
                NotifyOfPropertyChange("LogCmd");
            }
        }

        public TimLogCmdRecItem SelectedLogCmdItem
        {
            get { return _selectedLogCmdItem; }
            set
            {
                _selectedLogCmdItem = value;
                NotifyOfPropertyChange("SelectedLogCmdItem");

                SelectedItemChanged();
            }
        }

        public string StatusRegName
        {
            get;
            private set;
        }

        public List<TimParameterFieldItem> StatusReg
        {
            get { return _statusReg; }
            set
            {
                _statusReg = value;
                NotifyOfPropertyChange("StatusReg");
            }
        }

        public string MoveTimeString
        {
            get { return _moveTimeString; }
            set
            {
                _moveTimeString = value;
                NotifyOfPropertyChange("MoveTimeString");
            }
        }

        #endregion

    }
}

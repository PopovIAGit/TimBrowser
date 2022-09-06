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
            StatusRegTSName = "состояние дискретных выходов блока";
        }

        #region Fields
        
        private readonly TimDataService _timDataService;
        private readonly TimDataServiceM _timDataServiceM;
        private ObservableCollection<TimLogCmdRecItem> _logCmd;
        private TimLogCmdRecItem _selectedLogCmdItem;
        private List<TimParameterFieldItem> _statusReg;
        private List<TimParameterFieldItem> _statusRegTS;
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
                StatusReg = _selectedLogCmdItem.StatusParameter.TimFields; //TODO про дискретные сигналы
            if (_selectedLogCmdItem.StatusDigitalOut!=null) StatusRegTS = _selectedLogCmdItem.StatusDigitalOut.TimFields;
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

        public string StatusRegTSName
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

        public List<TimParameterFieldItem> StatusRegTS
        {
            get { return _statusRegTS; }
            set
            {
                //if (value[1].BitValue == 1 && value[2].BitValue == 1)//в промежуточном положении
                _statusRegTS = value;
                NotifyOfPropertyChange("StatusRegTS");
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

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
    public class LogSimViewModel : Screen
    {
        public LogSimViewModel(TimDataService timDataService, TimDataServiceM timDataServiceM)
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
        private ObservableCollection<TimLogSimRecItem> _logSim;
        private TimLogSimRecItem _selectedLogSimItem;
        private List<TimParameterFieldItem> _statusReg;
        private string _moveTimeString;

        #endregion

        private void TimDataChanged(object sender, EventArgs e)
        {
            // добавляем новые данные
            LogSim = _timDataService.LogSim;
            if (LogSim == null)
            {
                LogSim = _timDataServiceM.LogSim;
                MoveTimeString = _timDataServiceM.MoveTimeString;
            }
            else MoveTimeString = _timDataService.MoveTimeString;
        }

        #region Properties
      
        public ObservableCollection<TimLogSimRecItem> LogSim
        {
            get { return _logSim; }
            set
            {
                _logSim = value;
                NotifyOfPropertyChange("LogSim");
            }
        }

        /*public TimLogSimRecItem SelectedLogSimItem
        {
            get { return _selectedLogSimItem; }
            set
            {
                _selectedLogSimItem = value;
                NotifyOfPropertyChange("SelectedLogSimItem");

                SelectedItemChanged();
            }
        }*/

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Services;
using System.Collections.ObjectModel;
using TimBrowser.Model;

namespace TimBrowser.ViewModels
{
    public class LogEvAndCmdMainViewModel
    {
        public LogEvAndCmdMainViewModel(TimDataService timDataService, TimDataServiceM timDataServiceM, LogEvAndCmdEventsViewModel logEvAndCmdEventsControl,
            LogEvAndCmdChosenParametersViewModel logEvAndCmdChosenParametersControl)
        {
            _timDataService = timDataService;
            _timDataServiceM = timDataServiceM;
            _timDataService.OnTimDataChanged += TimDataChanged;
            _timDataServiceM.OnTimDataChangedM += TimDataChanged;

            LogEvAndCmdEventsControl              = logEvAndCmdEventsControl;
            LogEvAndCmdChosenParametersControl = null;// logEvAndCmdChosenParametersControl;
        }

        #region Fields

        private readonly TimDataService _timDataService;
        private readonly TimDataServiceM _timDataServiceM;

        public System.Action DataLoadedAction;
        #endregion

        #region Methods

        private void TimDataChanged(object sender, EventArgs e)
        {
            // добавляем новые данные
            LogEvAndCmdEventsControl.LogEventsAndCmd = _timDataService.LogEventsAndCmd;
            if (LogEvAndCmdEventsControl.LogEventsAndCmd ==null) LogEvAndCmdEventsControl.LogEventsAndCmd = _timDataServiceM.LogEventsAndCmd;

            if (DataLoadedAction != null)
                DataLoadedAction();
        }

        #endregion

        

        #region Properties

        #region View Models

       
        public LogEvAndCmdEventsViewModel LogEvAndCmdEventsControl
        {
            get;
            private set;
        }

        public LogEvAndCmdChosenParametersViewModel LogEvAndCmdChosenParametersControl
        {
            get;
            private set;
        }

        #endregion

        public ObservableCollection<TimLogEvAndCmdRecItem> LogEventsAndCmd
        {
            get;
            private set;
        }

        #endregion
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.Services;
using System.Collections.ObjectModel;
using TimBrowser.Model;

namespace TimBrowser.ViewModels
{
    public class LogEvMainViewModel
    {
        public LogEvMainViewModel(TimDataService timDataService, TimDataServiceM timDataServiceM, LogEvEventsViewModel logEvEventsControl,
            LogEvChosenParametersViewModel logEvChosenParametersControl)
        {
            _timDataService = timDataService;
            _timDataServiceM = timDataServiceM;
            _timDataService.OnTimDataChanged += TimDataChanged;
            _timDataServiceM.OnTimDataChangedM += TimDataChanged;

            LogEvEventsControl              = logEvEventsControl;
            LogEvChosenParametersControl    = logEvChosenParametersControl;
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
            LogEvEventsControl.LogEvents = _timDataService.LogEvents;
            if (LogEvEventsControl.LogEvents ==null) LogEvEventsControl.LogEvents = _timDataServiceM.LogEvents;

            if (DataLoadedAction != null)
                DataLoadedAction();
        }

        #endregion

        

        #region Properties

        #region View Models

       
        public LogEvEventsViewModel LogEvEventsControl
        {
            get;
            private set;
        }

        public LogEvChosenParametersViewModel LogEvChosenParametersControl
        {
            get;
            private set;
        }

        #endregion

        public ObservableCollection<TimLogEvRecItem> LogEvents
        {
            get;
            private set;
        }

        #endregion
        
    }
}

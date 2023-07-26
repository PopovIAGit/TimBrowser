using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using TimBrowser.Model;
using TimBrowser.Messages;
using Caliburn.Micro;

namespace TimBrowser.ViewModels
{
    public class LogEvAndCmdChosenParametersViewModel : Screen,  IHandle<LogEvSelectedMessage>
    {
        public LogEvAndCmdChosenParametersViewModel(IEventAggregator evAggregator)
        {
            _evAggregator = evAggregator;
            _evAggregator.Subscribe(this);
        }

        private IEventAggregator _evAggregator;
        private ObservableCollection<TimChosenParameterItem> _logChosenParameters;

        public void Handle(LogEvSelectedMessage message)
        {
            LogChosenParameters = message.TimLogEvRecSelectedItem.ChosenParameters;
        }

        public ObservableCollection<TimChosenParameterItem> LogChosenParameters
        {
            get { return _logChosenParameters; }
            set
            {
                _logChosenParameters = value;
                NotifyOfPropertyChange("LogChosenParameters");
            }
        }
    }
}

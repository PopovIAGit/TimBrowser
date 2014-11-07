using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.Dynamic;
using System.Windows;
using TimBrowser.Services;
using TimBrowser.Model;


namespace TimBrowser.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        public MainWindowViewModel(IWindowManager windowManager, LogEvMainViewModel logEvMainViewControl,
            LogCmdViewModel logCmdViewControl, LogParamViewModel logParamViewControl, 
            ParametersTableViewModel parametersTableControl, MenuViewModel menuControl)
        {
            _windowManager = windowManager;
            LogEvMainViewControl = logEvMainViewControl;
            LogCmdViewControl = logCmdViewControl;
            LogParamViewControl = logParamViewControl;
            ParametersTableControl = parametersTableControl;
            MenuControl = menuControl;

            _applicationName = "Tim Browser v" + Helper.Constants.AppVersion;
            _deactivationRectangleVisibility = Visibility.Visible;

            LogEvMainViewControl.DataLoadedAction += () =>
                {
                    MainTabEnabled = true;
                    DeactivationRectangleVisibility = Visibility.Collapsed;
                };
        }

        public List<TimBrowser.Model.TimParameterFieldItem> BitFieldsProp
        {
            get;
            set;
        }

        private readonly IWindowManager _windowManager;
        private string _applicationName;
        private bool _mainTabEnabled;
        private Visibility _deactivationRectangleVisibility;

        #region Methods

        protected override void OnActivate()
        {
        }

        public void DownloadButton()
        {
            /*
            dynamic settings = new ExpandoObject();

            settings.WindowStartupLocation = WindowStartupLocation.Manual;
            settings.SizeToContent = SizeToContent.Manual;
            settings.ResizeMode = System.Windows.ResizeMode.NoResize;

            settings.Width = 400;
            settings.Height = 500;

            _windowManager.ShowDialog(DownloadViewModel, null, settings);
             */
        }

        #endregion



        #region Properties

        #region View Models

        public LogEvMainViewModel LogEvMainViewControl
        {
            get;
            private set;
        }

        public LogCmdViewModel LogCmdViewControl
        {
            get;
            private set;
        }

        public LogParamViewModel LogParamViewControl
        {
            get;
            private set;
        }

        public MenuViewModel MenuControl
        {
            get;
            private set;
        }

        public ParametersTableViewModel ParametersTableControl
        {
            get;
            private set;
        }

        #endregion

        public string ApplicationName
        {
            get { return _applicationName; }
        }

        public bool MainTabEnabled
        {
            get { return _mainTabEnabled; }
            set
            {
                _mainTabEnabled = value;
                NotifyOfPropertyChange("MainTabEnabled");
            }
        }

        public Visibility DeactivationRectangleVisibility
        {
            get { return _deactivationRectangleVisibility; }
            set
            {
                _deactivationRectangleVisibility = value;
                NotifyOfPropertyChange("DeactivationRectangleVisibility");
            }
        }

        
        #endregion
    }
}

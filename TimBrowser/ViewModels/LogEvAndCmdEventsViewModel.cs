using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using TimBrowser.Model;
using TimBrowser.Services;
using Caliburn.Micro;
using TimBrowser.Messages;
using System.ComponentModel;
using System.Dynamic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace TimBrowser.ViewModels
{
    public class LogEvAndCmdEventsViewModel : Screen
    {
        public LogEvAndCmdEventsViewModel(IEventAggregator evAggregator, IWindowManager windowManager)
        {
            _evAggregator = evAggregator;
            _windowManager = windowManager;

            _onSelectedItemChangedWorker = new BackgroundWorker();
            _onSelectedItemChangedWorker.DoWork += (s, e) => { OnSelectedItemChanged(); };
        }

        #region Fields

        private IEventAggregator _evAggregator;
        private IWindowManager _windowManager;

        private ObservableCollection<TimLogEvAndCmdRecItem> _logEventsAndCmd;
        private TimLogEvAndCmdRecItem _selectedLogEvAndCmdItem;

        private BackgroundWorker _onSelectedItemChangedWorker;

        #endregion

        public void MouseDoubleClick(System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                ShowParameters();

                e.Handled = true;
            }
        }


        public void ShowParameters()
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.Manual;

            settings.SizeToContent = SizeToContent.Manual;
            settings.ResizeMode = System.Windows.ResizeMode.CanResize;
            settings.Title = "Параметры события";
            settings.Icon = new BitmapImage(new Uri("pack://application:,,,/Assets/appicon.png"));

            // Устанавливать размеры окна только после SizeToContent, иначе не работает
            settings.Width = 800;
            settings.Height = 600;

            _windowManager.ShowDialog(new LogEvAndCmdParametersViewModel(_selectedLogEvAndCmdItem), null, settings);
            
        }

        private void OnSelectedItemChanged()
        {
            if (_selectedLogEvAndCmdItem != null)
                _evAggregator.Publish(new LogEvAndCmdSelectedMessage(_selectedLogEvAndCmdItem));
        }

        #region Properties

        public ObservableCollection<TimLogEvAndCmdRecItem> LogEventsAndCmd
        {
            get { return _logEventsAndCmd; }
            set
            {
                _logEventsAndCmd = value;
                NotifyOfPropertyChange("LogEventsAndCmd");                
            }
        }

        public TimLogEvAndCmdRecItem SelectedLogEvAndCmdItem
        {
            get { return _selectedLogEvAndCmdItem; }
            set
            {
                _selectedLogEvAndCmdItem = value;
                NotifyOfPropertyChange("SelectedLogEvItem");
     
                _onSelectedItemChangedWorker.RunWorkerAsync();
            }
        }

        #endregion
        
    }
}

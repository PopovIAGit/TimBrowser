using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimBrowser.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace TimBrowser.Views
{
    /// <summary>
    /// Interaction logic for LogChosenView.xaml
    /// </summary>
    public partial class LogChosenView : UserControl, INotifyPropertyChanged
    {
        public LogChosenView()
        {
            InitializeComponent();

            DataContext = this;

            this.Loaded += (s, e) =>
                {
                    DataContext = this;
                    MainGrid.DataContext = this;

                    ChosenList = new List<TimChosenParameterItem>()
                    {
                        new TimChosenParameterItem("Момент",
                            new List<TimParameterItem>()
                            {
                                new TimParameterItem(1, "A10", "01:00 01:01:05", "Момент", "10Н*м", null),
                                new TimParameterItem(2, "A10", "01:00 01:01:04", "Момент", "15Н*м", null),
                                new TimParameterItem(3, "A10", "01:00 01:01:03", "Момент", "20Н*м", null),
                                new TimParameterItem(4, "A10", "01:00 01:01:02", "Момент", "20Н*м", null),
                                new TimParameterItem(5, "A10", "01:00 01:01:01", "Момент", "20Н*м", null),
                                new TimParameterItem(6, "A10", "01:00 01:01:00", "Момент", "20Н*м", null)
                            }),

                        new TimChosenParameterItem("Напряжение",
                            new List<TimParameterItem>()
                            {
                                new TimParameterItem(1, "A14", "01:00 01:01:05", "Напряжение", "220В", null),
                                new TimParameterItem(2, "A14", "01:00 01:01:04", "Напряжение", "221В", null),
                                new TimParameterItem(3, "A14", "01:00 01:01:03", "Напряжение", "222В", null),
                                new TimParameterItem(4, "A14", "01:00 01:01:02", "Напряжение", "221В", null),
                                new TimParameterItem(5, "A14", "01:00 01:01:01", "Напряжение", "220В", null),
                                new TimParameterItem(6, "A14", "01:00 01:01:00", "Напряжение", "220В", null)
                            }),
                    };

                };

            
        }

        private List<TimChosenParameterItem> _chosenList;
        public List<TimChosenParameterItem> ChosenList 
        {
            get { return _chosenList; }
            set
            {
                _chosenList = value;
                NotifyPropertyChanged("ChosenList");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

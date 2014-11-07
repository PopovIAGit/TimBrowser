﻿using System;
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

namespace TimBrowser.Views
{
    /// <summary>
    /// Interaction logic for ListUserControl.xaml
    /// </summary>
    public partial class ListUserControl : UserControl
    {
        public ListUserControl()
        {
            InitializeComponent();

            DataContext = this;
            
            LogCmdList = new List<TimLogCmdRecItem>()
            {
                new TimLogCmdRecItem(1, "01.01.14", "Команда 1", null),
                new TimLogCmdRecItem(2, "01.01.14", "Команда 2", null),
                new TimLogCmdRecItem(3, "01.01.14", "Команда 3", null),
                new TimLogCmdRecItem(4, "01.01.14", "Команда 4", null),
                new TimLogCmdRecItem(5, "01.01.14", "Команда 5", null),
                new TimLogCmdRecItem(6, "01.01.14", "Команда 6", null),
                new TimLogCmdRecItem(7, "01.01.14", "Команда 7", null),
                new TimLogCmdRecItem(8, "01.01.14", "Команда 8", null),
            };
        }

        public List<TimLogCmdRecItem> LogCmdList
        {
            get;
            set;
        }

    }

}

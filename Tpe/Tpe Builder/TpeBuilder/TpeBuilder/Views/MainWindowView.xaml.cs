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
using System.Windows.Shapes;
using TpeBuilder.ViewModel;
using Microsoft.Win32;

namespace TpeBuilder.Views
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
                {
                    _dataContext = (MainWindowViewModel)DataContext;
                    _dataContext.ActivateModalWindow = ActivateModalWindow;
                };
        }

        private MainWindowViewModel _dataContext;

        public void ActivateModalWindow(Window window)
        {
            window.ShowDialog();
        }

        private void AddTableButton_Click(object sender, RoutedEventArgs e)
        {
            _dataContext.CreateNewTable();
        }

        private void AddGroupButton_Click(object sender, RoutedEventArgs e)
        {
            _dataContext.AddGroup();
        }

        private void AddParameterButton_Click(object sender, RoutedEventArgs e)
        {
            _dataContext.AddParameter();
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            _dataContext.UpSort();
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            _dataContext.DownSort();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveTPEButton_Click(object sender, RoutedEventArgs e)
        {
            _dataContext.SaveTPE();
        }

        private void SaveXMLButton_Click(object sender, RoutedEventArgs e)
        {
            _dataContext.SaveXML();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.DefaultExt = ".tpe";
            openDialog.Filter = "Шаблон параметров (.tpe)|*.tpe";

            if (openDialog.ShowDialog().Value)
            {
                _dataContext.LoadTpe(openDialog.FileName);
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _dataContext.DeleteGroup();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            _dataContext.DeleteParameter();
        }

    }
}

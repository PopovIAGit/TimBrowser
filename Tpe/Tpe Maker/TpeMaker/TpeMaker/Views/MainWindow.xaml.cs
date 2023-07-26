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
using TpeMaker.ViewModels;

namespace TpeMaker.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new MainWindowViewModel();
        }

        private MainWindowViewModel _viewModel;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenXmlFile();
        }
        
    }
}

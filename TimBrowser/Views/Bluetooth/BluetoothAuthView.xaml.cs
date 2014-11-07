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
using TimBrowser.ViewModels;

namespace TimBrowser.Views
{
    /// <summary>
    /// Interaction logic for BluetoothAuthView.xaml
    /// </summary>
    public partial class BluetoothAuthView : UserControl
    {
        public BluetoothAuthView()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
                {
                    PinCodeTextBox.Focus();
                };
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BluetoothAuthViewModel dc = (BluetoothAuthViewModel)this.DataContext;
                dc.AuthRequestButton();
            }
        }


    }
}

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
using System.ComponentModel;
using TimBrowser.Model;

namespace TimBrowser.Views.Controls
{
    /// <summary>
    /// Interaction logic for BitParameterControl.xaml
    /// </summary>
    public partial class BitParameterControl : UserControl 
    {
        public BitParameterControl()
        {
            InitializeComponent();
        }


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(BitParameterControl),
            new FrameworkPropertyMetadata(String.Empty, new PropertyChangedCallback(OnTitlePropertyChanged)));

        private static void OnTitlePropertyChanged(DependencyObject dependencyObject, 
               DependencyPropertyChangedEventArgs e) 
        {
            BitParameterControl control = dependencyObject as BitParameterControl;
            control.TitleTextBlock.Text = control.Title;
        }

        public List<TimParameterFieldItem> BitFields
        {
            get { return (List<TimParameterFieldItem>)GetValue(BitFieldsProperty); }
            set { SetValue(BitFieldsProperty, value); }
        }

        public static DependencyProperty BitFieldsProperty =
            DependencyProperty.Register("BitFields", typeof(List<TimParameterFieldItem>), typeof(BitParameterControl),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnBitFieldsPropertyChanged)));

        private static void OnBitFieldsPropertyChanged(DependencyObject dependencyObject, 
               DependencyPropertyChangedEventArgs e)
        {
            BitParameterControl control = dependencyObject as BitParameterControl;
            control.BitFieldsList.ItemsSource = control.BitFields;
        }
    }
}

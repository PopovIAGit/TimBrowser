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
using System.Collections.ObjectModel;
using TpeBuilder.Model;

namespace TpeBuilder.Views
{
    /// <summary>
    /// Interaction logic for BitFieldsEditView.xaml
    /// </summary>
    public partial class BitFieldsEditView : Window
    {
        public BitFieldsEditView()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                _dataContext = (BitFieldsEditViewModel)DataContext;
                _dataContext.FieldItems = _fieldItems;
            };
        }

        BitFieldsEditViewModel _dataContext;
        ObservableCollection<TpeParameterFieldItem> _fieldItems;

        public void Init(ObservableCollection<TpeParameterFieldItem> fieldItems)
        {
            _fieldItems = fieldItems;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _dataContext.AddBitField();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _dataContext.RemoveBitField();
        }
    }
}

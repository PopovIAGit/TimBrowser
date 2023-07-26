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
using TpeBuilder.ViewModel;
using TpeBuilder.Model;

namespace TpeBuilder.Views
{
    /// <summary>
    /// Interaction logic for ParameterEditView.xaml
    /// </summary>
    public partial class ParameterEditView : UserControl
    {
        public ParameterEditView()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                _dataContext = (ParameterEditViewModel)DataContext;
                _dataContext.CurrentParameterItem = _tpeParameter;
            };
        }

        ParameterEditViewModel _dataContext;
        TpeParameter _tpeParameter;

        public Action OnBitFieldsActivated;

        public void Init(TpeParameter tpeParameter)
        {
            _tpeParameter = tpeParameter;
        }

        private void FieldsButton_Click(object sender, RoutedEventArgs e)
        {
            _dataContext.ActivateFieldsList();

            if (OnBitFieldsActivated != null)
                OnBitFieldsActivated();
        }



    } 
}

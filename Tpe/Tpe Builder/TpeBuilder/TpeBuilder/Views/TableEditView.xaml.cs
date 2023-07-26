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
    /// Interaction logic for TableEditView.xaml
    /// </summary>
    public partial class TableEditView : UserControl
    {
        public TableEditView()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                _dataContext = (TableEditViewModel)DataContext;
                _dataContext.CurrentTpeTable = _tpeTable;
            };

        }


        TableEditViewModel _dataContext;
        TpeTable _tpeTable;

        public void Init(TpeTable tpeTable)
        {
            _tpeTable = tpeTable;
        }

    }
}

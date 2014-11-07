using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace TimBrowser.Model
{
    public class PrintTableItem
    {
        public PrintTableItem(string title, GridLength[] columnsWidths, List<string[]> columns)
        {
            _title = title;
            _columnsWidths = columnsWidths;
            _columns = columns;
        }

        private string _title;
        private GridLength[] _columnsWidths;
        private List<string[]> _columns;

        public string Title
        {
            get { return _title; }
        }

        public GridLength[] ColumnsWidths
        {
            get { return _columnsWidths; }
        }

        public List<string[]> Columns
        {
            get { return _columns; }
        }

    }
}

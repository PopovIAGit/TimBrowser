using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace TimBrowser.Converters
{
    public class RowToIndexConverter : MarkupExtension, IValueConverter
    {

        public RowToIndexConverter()
        {

        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataGridRow row = value as DataGridRow;

            if (row != null)
                return row.GetIndex() + 1;
            else
                return -1;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

    }
}

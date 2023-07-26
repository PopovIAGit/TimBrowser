using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace TimBrowser.Converters
{
    public class Row3EvToIndexConverter : MarkupExtension, IValueConverter
    {
        public SolidColorBrush SetColor;
        public SolidColorBrush UnsetColor;
        public SolidColorBrush CommandColor;

        public Row3EvToIndexConverter()
        {
            UnsetColor = (SolidColorBrush)App.Current.Resources["LogEvUnsetColor"];
            SetColor = (SolidColorBrush)App.Current.Resources["LogEvSetColor"];
            CommandColor = (SolidColorBrush)App.Current.Resources["LogEvCommandColor"];

        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if ((int)value == 0) return UnsetColor;
            if ((int)value == 1) return SetColor;
            if ((int)value == 2) return CommandColor;

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

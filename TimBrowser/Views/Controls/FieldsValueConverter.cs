using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;
using System.Windows;

namespace TimBrowser.Views.Controls
{
    public class FieldsValueConverter : MarkupExtension, IValueConverter
    {
        public FieldsValueConverter()
        {
            ResourceDictionary resource = new ResourceDictionary
            {
                Source = new Uri("/TimBrowser;component/Views/Controls/BitParameterResourceDictionary.xaml", UriKind.RelativeOrAbsolute)
            };

            _setColor = (SolidColorBrush)resource["BitParameterFieldValue"];
            _unsetColor = new SolidColorBrush(Colors.Transparent);

        }

        public SolidColorBrush _setColor;
        public SolidColorBrush _unsetColor;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value == true ? _setColor : _unsetColor;
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

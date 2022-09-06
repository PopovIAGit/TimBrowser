using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;
using System.Windows.Markup;

namespace TimBrowser.Converters
{
    public class BooleanLogEvColorValueExt : MarkupExtension, IValueConverter
    {
        public SolidColorBrush SetColor;
        public SolidColorBrush UnsetColor;

        public BooleanLogEvColorValueExt()
        {
            UnsetColor = (SolidColorBrush)App.Current.Resources["LogEvUnsetColor"];
            SetColor   = (SolidColorBrush)App.Current.Resources["LogEvSetColor"];
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //TODO выбор цвета строки
            //
            return (bool)value == true ? SetColor : UnsetColor;
            //return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            /*
            if (_converter == null)
                _converter = new BooleanLogEvColorValueExt();

            return _converter;
             */

            return this;
        }
    }
}

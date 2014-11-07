using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;

namespace TimBrowser.Converters
{
    public class AltIndexBackgroundConverter : MarkupExtension, IValueConverter
    {
        public AltIndexBackgroundConverter()
        {
            _oneColor = (SolidColorBrush)App.Current.Resources["TimListBackgroundBrush"];
            _zeroColor = (SolidColorBrush)App.Current.Resources["TimListAltBrush"];
        }

        private SolidColorBrush _zeroColor;
        private SolidColorBrush _oneColor;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double num = System.Convert.ToDouble(value);
            
            SolidColorBrush brush = null;

            brush = ((num % 2) == 0) ? _zeroColor : _oneColor;

            return brush;
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

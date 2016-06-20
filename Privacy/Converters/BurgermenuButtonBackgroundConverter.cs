using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Privacy.Converters
{
    public class BurgermenuButtonBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is bool) || targetType != typeof(Brush))
                return DependencyProperty.UnsetValue;
            var color = (bool)value ? Color.FromArgb(255, 69, 24, 24) : Color.FromArgb(255, 195, 179, 152);
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

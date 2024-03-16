using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Homework_7_2.Models.Converters
{
    class StatusToBackgroundColorConverter : IValueConverter
    {
        private const int DismissId = 4;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (System.Convert.ToInt32(value) == DismissId)
            {
                return new SolidColorBrush(Color.FromArgb(0xff, 0xcc, 0xaf, 0xaa));//#FFAAAAAA
            }

            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();

        }
    }
}

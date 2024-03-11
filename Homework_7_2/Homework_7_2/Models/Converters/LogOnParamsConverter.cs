using MahApps.Metro.Controls;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Homework_7_2.Models.Converters
{
    public class LogOnParamsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new LogOnParams { Window = values[0] as MetroWindow, PasswordBox = values[1] as PasswordBox };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Globalization;
using System.Windows.Data;

namespace TicTacToe.Helpers
{
    public class StringFormatConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string formatString = parameter as string;
            return string.Format(formatString, values);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

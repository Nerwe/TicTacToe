using FontAwesome.Sharp;
using System;
using System.Globalization;
using System.Windows.Data;
using TicTacToe.Model;

namespace TicTacToe.Helpers
{
    public class CellTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CellType cellType)
            {
                switch (cellType)
                {
                    case CellType.Empty:
                        return IconChar.None;
                    case CellType.Cross:
                        return IconChar.X;
                    case CellType.Circle:
                        return IconChar.Circle;
                    case CellType.Hint:
                        return IconChar.Lightbulb;
                    default:
                        return IconChar.None;
                }
            }
            return IconChar.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

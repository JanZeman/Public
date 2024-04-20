using System.Globalization;

namespace MauiTemplate2024.App.Converters;

public class DoubleToNegativeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return -(double)value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return -(double)value;
    }
}
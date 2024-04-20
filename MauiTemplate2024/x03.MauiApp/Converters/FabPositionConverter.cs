using System.Globalization;

namespace MauiTemplate2024.App.Converters;

public class FabPositionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var offset = (double)value;

        const int offsetThreshold = 400;
        const int hideMe = 100;
        const int difference = offsetThreshold - hideMe;

        return offset switch
        {
            < difference => hideMe,
            >= difference and < offsetThreshold => offsetThreshold - offset,
            _ => 0
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
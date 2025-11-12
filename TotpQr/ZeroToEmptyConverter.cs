using System.Globalization;
using System.Windows.Data;

namespace TotpQr;

public class ZeroToEmptyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is int intValue && intValue == 0 ? string.Empty : value?.ToString() ?? string.Empty;

    public object ConvertBack(
        object value, Type targetType, object parameter, CultureInfo culture) =>
        value is string stringValue && string.IsNullOrWhiteSpace(stringValue)
            ? 0 : int.TryParse(value?.ToString(), out int result) ? result : 0;
}   

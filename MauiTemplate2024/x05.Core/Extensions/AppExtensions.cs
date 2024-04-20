namespace MauiTemplate2024.Core.Extensions;

public static class AppExtensions
{
    public static Microsoft.Maui.Controls.Application MauiApplication => Microsoft.Maui.Controls.Application.Current;

    public static Microsoft.Maui.Graphics.Color GetAppColor(string key)
    {
        var fallbackColor = Colors.Gray;
        if (MauiApplication == null) return fallbackColor;
        var result = MauiApplication.Resources.TryGetValue(key, out var o);
        if (!result) return fallbackColor;
        return o as Microsoft.Maui.Graphics.Color ?? fallbackColor;
    }
}
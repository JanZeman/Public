using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using System.Reflection;

namespace MauiTemplate2024.Core.Platforms.Windows;

public static class Extensions
{
    public static void ChangeCursor(this UIElement uiElement, InputCursor cursor)
    {
        var type = typeof(UIElement);
        type.InvokeMember("ProtectedCursor", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, uiElement, new object[] { cursor });
    }
}
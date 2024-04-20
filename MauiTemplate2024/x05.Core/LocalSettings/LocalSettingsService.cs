namespace MauiTemplate2024.Core.LocalSettings;

public class LocalSettingsService : ILocalSettingsService
{
    public LocalSettingsService()
    {
        Debussy.WriteLine("");
    }

    public bool IsRecordingPaused => false;
    public bool HideWhenLosingFocus => true;
    public bool ShowInTaskbar => true;
}
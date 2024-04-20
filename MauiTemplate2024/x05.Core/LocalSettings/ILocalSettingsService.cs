namespace MauiTemplate2024.Core.LocalSettings;

public interface ILocalSettingsService
{
    bool IsRecordingPaused { get; }

    bool HideWhenLosingFocus { get; }

    bool ShowInTaskbar { get; }
}
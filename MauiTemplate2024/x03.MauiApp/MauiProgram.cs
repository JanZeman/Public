using MauiTemplate2024.Core.Services.Api;
using MauiTemplate2024.Core.Services.Caching;

namespace MauiTemplate2024.App;

public static class MauiProgram
{
    internal static IServiceProvider Services;

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddScoped(_ => new HttpClient());
        builder.Services.AddSingleton<ICacheService, CacheService>();
        builder.Services.AddSingleton<IApiService, ApiService>();
        builder.Services.AddSingleton<IStorageService, SqLiteStorageService>();
        builder.Services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
        builder.Services.AddSingleton<ILogicService, LogicService>();

        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddSingleton<HomePage>();

        builder.Services.AddTransient<DetailViewModel>();
        builder.Services.AddTransient<DetailPage>();

        var app = builder.Build();
        Services = app.Services;
        return app;
    }
}
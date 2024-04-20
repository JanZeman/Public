namespace MauiTemplate2024.Core.Services.Caching;

public interface ICacheService
{
    bool TryGetValue<T>(string key, out T value);
    void Set<T>(string key, T value);
}
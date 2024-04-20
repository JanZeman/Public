namespace MauiTemplate2024.Core.Services.Caching;

public class CacheService : ICacheService
{
    private readonly Dictionary<string, object> _cache = new();

    public bool TryGetValue<T>(string key, out T value)
    {
        if (_cache.TryGetValue(key, out object cachedValue))
        {
            if (cachedValue is T typedValue)
            {
                value = typedValue;
                return true;
            }
        }

        value = default;
        return false;
    }

    public void Set<T>(string key, T value)
    {
        _cache[key] = value;
    }
}

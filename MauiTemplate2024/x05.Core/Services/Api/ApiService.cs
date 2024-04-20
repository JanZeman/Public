using MauiTemplate2024.Core.Services.Caching;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MauiTemplate2024.Core.Services.Api;

public class ApiService(HttpClient httpClient, ICacheService cacheService) : IApiService
{
    private const string ApiUri = "https://raw.githubusercontent.com/JanZeman/Public/main/Backend/Potatoes/items.json";
    private const string CachedPotatoesKey = "potatoes";

    public async Task<List<Potato>> FetchItemsAsync(CancellationToken cancellationToken = default)
    {
        if (cacheService.TryGetValue(CachedPotatoesKey, out List<Potato> cachedPotatoes))
        {
            return cachedPotatoes;
        }

        return await FetchPotatoes(cancellationToken);
    }

    private async Task<List<Potato>> FetchPotatoes(CancellationToken cancellationToken)
    {
        var emptyPotatoesList = new List<Potato>();

        try
        {
            var responseString = await httpClient.GetStringAsync(ApiUri, cancellationToken);

            // Parse the JSON response directly into a List<Potato>
            var potatoes = JsonConvert.DeserializeObject<List<Potato>>(responseString) ?? emptyPotatoesList;

            // Optionally, you can handle any transformations or additional processing here
            // For example, updating a cache with the latest potato data
            cacheService.Set(CachedPotatoesKey, potatoes);

            return potatoes;
        }
        catch (Exception ex)
        {
            // Optionally, log the exception details for debugging
            Debug.WriteLine($"Error fetching potatoes: {ex}");
            return emptyPotatoesList;
        }
    }

}
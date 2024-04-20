namespace MauiTemplate2024.Core.Services.Api;

public interface IApiService
{
    Task<List<Potato>> FetchItemsAsync(CancellationToken cancellationToken = default);
}
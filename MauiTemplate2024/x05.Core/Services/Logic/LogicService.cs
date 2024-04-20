using MauiTemplate2024.Core.Services.Api;

namespace MauiTemplate2024.Core.Services.Logic;

// ReSharper disable once ClassNeverInstantiated.Global
public class LogicService(
    IApiService apiService,
    IStorageService storageService,
    ILocalSettingsService localSettingsService)
    : ILogicService
{
    private List<Potato> _allItems;
    private int _favoriteItemsCount;

    public ObservableCollectionEx<Potato> FilteredItems { get; private set; } = [];

    public string FilteredItemsDescription
    {
        get
        {
            string itemsCountDescription = FilteredItems.Count < _allItems.Count
                ? $"{FilteredItems.Count} of {_allItems.Count}"
                : $"{_allItems.Count}";

            return $"{itemsCountDescription} potato varieties listed; {_favoriteItemsCount} got marked as favorites.";
        }
    }

    public event EventHandler ItemsFilterChanged;

    public async Task Init()
    {
        // Fetch items
        _allItems = await apiService.FetchItemsAsync();

        FilteredItems = new ObservableCollectionEx<Potato>(_allItems);

        await UpdateDescription();
    }

    private async Task UpdateDescription()
    {
        var favoritePotatoes = await storageService.GetFavoritePotatos();
        _favoriteItemsCount = favoritePotatoes.Count;
        ItemsFilterChanged?.Invoke(this, EventArgs.Empty);
    }

    public async Task FilterByText(string textFilter)
    {
        if (string.IsNullOrWhiteSpace(textFilter))
            textFilter = string.Empty;

        textFilter = textFilter.ToLowerInvariant();

        await FilterByTextInternally(textFilter);
    }

    public async Task<Potato> ToggleStatus(Potato potato, bool accept)
    {
        potato.IsFavorite = accept;
        var numberOfChanges = await storageService.SavePotato(potato);
        if (numberOfChanges < 1) return potato;

        await UpdateDescription();
        return potato;
    }


    public async Task ResetAllItems()
    {
        await storageService.DeleteAllPotatos();

        // Re-fetch from the API as well?
        await Init();

        await UpdateDescription();
    }

    private async Task FilterByTextInternally(string textFilter)
    {
        IEnumerable<Potato> filteredItems = string.IsNullOrEmpty(textFilter) ? _allItems : _allItems.Where(item => item.Name.Contains(textFilter, StringComparison.OrdinalIgnoreCase));
        
        if (filteredItems == null) return;

        FilteredItems.Clear();
        foreach (var item in filteredItems)
        {
            FilteredItems.Add(item);
        }

        await UpdateDescription();
    }
}
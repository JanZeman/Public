namespace MauiTemplate2024.Core.Services.Logic;

public interface ILogicService
{
    event EventHandler ItemsFilterChanged;

    ObservableCollectionEx<Potato> FilteredItems { get; }

    string FilteredItemsDescription { get; }

    Task Init();

    Task FilterByText(string textFilter);
    
    Task<Potato> ToggleStatus(Potato potato, bool accept);

    Task ResetAllItems();
}
namespace MauiTemplate2024.App.ViewModel;

public partial class HomeViewModel : BaseViewModel, ICollectionCentricViewModel
{
    private readonly ILogicService _logicService;
    private bool _initialized;

    public HomeViewModel(ILogicService logicService)
    {
        _logicService = logicService;
        PageName = AppConstants.Name;
    }

    public ICollectionCentricView View { get; set; }

    [ObservableProperty]
    private ObservableCollectionEx<Potato> _potatoes;

    [ObservableProperty]
    private string _textFilter;

    [ObservableProperty]
    private string _itemsCountDescription;

    [ObservableProperty]
    private bool _scrollToTop;

    public override async void OnLoaded()
    {
        base.OnLoaded();

        TextFilter = string.Empty;

        if (_initialized) return;
        _initialized = true;

        IsBusy = true;
        await _logicService.Init();
        IsBusy = true;

        Potatoes = _logicService.FilteredItems;
        ItemsCountDescription = _logicService.FilteredItemsDescription;

        _logicService.ItemsFilterChanged += (_, _) =>
        {
            Potatoes = _logicService.FilteredItems;
            ItemsCountDescription = _logicService.FilteredItemsDescription;
        };
    }

    ////public override void OnAppearing()
    ////{
    ////    base.OnAppearing();
    ////}

    [RelayCommand]
    private void ScrollToTopItem()
    {
        //View.MainCollection.ScrollTo(0);
        ScrollToTop = true;
        ScrollToTop = false;
    }

    [RelayCommand]
    private async void Refresh()
    {
        IsBusy = true;
        await Task.Delay(500);
        IsBusy = false;
    }

    [RelayCommand]
    private async void FilterByText()
    {
        Potatoes = [];
        await _logicService.FilterByText(TextFilter);
    }

    [RelayCommand]
    private async void ResetAllItems()
    {
        await _logicService.ResetAllItems();
    }

    [RelayCommand]
    private async void Accept(Potato potato)
    {
        await _logicService.ToggleStatus(potato, true);
    }

    [RelayCommand]
    private async void Reject(Potato potato)
    {
        await _logicService.ToggleStatus(potato, false);
    }

    [RelayCommand]
    private async void Toggle(Potato potato)
    {
        await _logicService.ToggleStatus(potato, !potato.IsFavorite);
    }

    [RelayCommand]
    private async void NavigateToDetailPage(Potato potato)
    {
        if (potato == null) return;

        await Shell.Current.GoToAsync(nameof(DetailPage), true, new Dictionary<string, object>
        {
            { nameof(Potato), potato }
        });
    }
}
namespace MauiTemplate2024.App.ViewModel;

[QueryProperty(nameof(Potato), nameof(Potato))]
public partial class DetailViewModel : BaseViewModel
{
    private readonly ILogicService _logicService;

    public DetailViewModel(ILogicService logicService)
    {
        _logicService = logicService;
        PageName = "Potato Variety";
    }

    [ObservableProperty]
    private Potato _potato;

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private string _price;

    [ObservableProperty]
    private string _lastSold;

    [ObservableProperty]
    private string _imageUrl;

    [ObservableProperty]
    private string _isFavorite;

    [RelayCommand]
    private async void ToggleFavorite()
    {
        await _logicService.ToggleStatus(Potato, !Potato.IsFavorite);
        IsFavorite = Potato.IsFavoriteDisplay;
    }

    public override async void OnLoaded()
    {
        base.OnLoaded();

        //IsBusy = true;
        //await _logicService.Init();
        //IsBusy = true;

        Name = Potato.Name;
        Description = Potato.Description;
        Price = Potato.PriceDisplay;
        LastSold = Potato.LastSoldDisplay;
        ImageUrl = Potato.ImageUrl;
        IsFavorite = Potato.IsFavoriteDisplay;
    }
}
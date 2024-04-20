namespace MauiTemplate2024.App.View;

public partial class HomePage : ICollectionCentricView
{
    private readonly Color _activeColor, _inactiveColor;

	public HomePage()
	{
		InitializeComponent();
        ItemsCollectionView.ScrollTo(0);
        _activeColor = AppExtensions.GetAppColor("Primary");
        _inactiveColor = AppExtensions.GetAppColor("LightPrimaryGrayScale");
    }

    public CollectionView MainCollection => ItemsCollectionView;

    protected override void OnBindingContextChanged()
    {
        if (BindingContext is ICollectionCentricViewModel hasCollectionViewModel)
            hasCollectionViewModel.View = this;

        base.OnBindingContextChanged();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Window.Activated += OnWindowActivated;
        Window.Deactivated += OnWindowDeactivated;
    }

    private void OnWindowActivated(object sender, EventArgs e)
    {
        Debussy.WriteLine($"Activated: {Window}");
        LeftPane.BackgroundColor = _activeColor;
        LogoAwake.IsVisible = true;
        LogoAsleep.IsVisible = false;
    }

    private void OnWindowDeactivated(object sender, EventArgs e)
    {
        Debussy.WriteLine($"Deactivated: {Window}");
        LeftPane.BackgroundColor = _inactiveColor;
        LogoAwake.IsVisible = false;
        LogoAsleep.IsVisible = true;
    }
}
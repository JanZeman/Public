namespace MauiTemplate2024.App.View;

public class MauiPage : ContentPage
{
    protected BaseViewModel ViewModel { get; private set; }

    protected MauiPage()
    {
        Loaded += OnLoaded;
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (BindingContext is BaseViewModel viewModel)
            ViewModel = viewModel;
    }

    private void OnWindowActivated(object sender, EventArgs e)
    {
        Debussy.WriteLine("OnWindowActivated");
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        Debussy.WriteLine("OnLoaded");
        ViewModel?.OnLoaded();
    }
}
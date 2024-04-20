namespace MauiTemplate2024.App.Controls;

public partial class ItemCard
{
	public ItemCard()
	{
		InitializeComponent();
	}

    protected override void OnBindingContextChanged()
    {
        // Workaround for the 'not found' warning when BindingContext is temporarily set by the MAUI framework to ViewModel (visual inheritance) instead to the expected class
        if (BindingContext is not Potato)
            return;

        base.OnBindingContextChanged();

        // Log or debug the change
        ////Debug.WriteLine($"ItemCard BindingContext is: {BindingContext?.GetType().Name ?? "null"}");
    }
}
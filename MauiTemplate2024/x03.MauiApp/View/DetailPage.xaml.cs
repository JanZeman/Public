namespace MauiTemplate2024.App.View;

public partial class DetailPage
{
	public DetailPage(DetailViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}
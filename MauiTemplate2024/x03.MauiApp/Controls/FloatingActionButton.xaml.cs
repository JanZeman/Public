namespace MauiTemplate2024.App.Controls;

public partial class FloatingActionButton
{
	public FloatingActionButton()
	{
		InitializeComponent();
    }

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(FloatingActionButton), propertyChanged: OnCommandPropertyChanged);

    private static void OnCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((FloatingActionButton) bindable).Command = (ICommand) newValue;
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs e)
    {
        if (Command != null && Command.CanExecute(null))
            Command.Execute(null);
    }
}
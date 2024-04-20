namespace MauiTemplate2024.App.Controls;

public class CollectionViewEx : CollectionView
{
    public CollectionViewEx()
    {
        Scrolled += (_, args) => VerticalOffset = args.VerticalOffset;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static readonly BindableProperty ScrollToTopProperty = BindableProperty.Create(nameof(ScrollToTop), typeof(bool), typeof(CollectionViewEx), propertyChanged: OnScrollToTopPropertyChanged);

    public bool ScrollToTop
    {
        get => (bool)GetValue(ScrollToTopProperty);
        set => SetValue(ScrollToTopProperty, value);
    }

    public static void OnScrollToTopPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is not true) return;
        var autoScrollableCollectionView = (CollectionViewEx) bindable;
        autoScrollableCollectionView.ScrollTo(0);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static readonly BindableProperty VerticalOffsetProperty = BindableProperty.Create(nameof(VerticalOffset), typeof(double), typeof(CollectionViewEx));

    public double VerticalOffset
    {
        get => (double)GetValue(VerticalOffsetProperty);
        set => SetValue(VerticalOffsetProperty, value);
    }
}
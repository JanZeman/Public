namespace JanZeman.System.Collections.ObjectModel;

public class ObservableCollectionEx<T> : ObservableCollection<T>
{
    private bool _suppressNotification = false;

    public ObservableCollectionEx() : base() { }

    public ObservableCollectionEx(IEnumerable<T> collection) : base(collection) { }

    public ObservableCollectionEx(List<T> list) : base(list) { }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if (!_suppressNotification)
            base.OnCollectionChanged(e);
    }

    public void AddRange(IEnumerable<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);

        _suppressNotification = true;

        foreach (var item in collection)
            Add(item);

        _suppressNotification = false;
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
}
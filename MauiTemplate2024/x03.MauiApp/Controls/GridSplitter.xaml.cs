using System.Reflection;

#if WINDOWS
using Microsoft.Maui.Platform;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Input;
using MauiTemplate2024.Core.Platforms.Windows;
using PointerEventArgs = Microsoft.Maui.Controls.PointerEventArgs;
#endif

namespace MauiTemplate2024.App.Controls;

public partial class GridSplitter
{
    public static PropertyInfo ColumnDefinitionActualWidthProperty;

    private Grid _grid;
    private double _gridMousePositionX, _gridMousePositionY;
    private bool _isChangingWidths, _isMouseDown;
    private int _throttler;

    public static readonly BindableProperty ActiveColorProperty = BindableProperty.Create(nameof(ActiveColor), typeof(Color), typeof(GridSplitter));

    public Color ActiveColor
    {
        get => (Color)GetValue(ActiveColorProperty);
        set => SetValue(ActiveColorProperty, value);
    }

    public static readonly BindableProperty InactiveColorProperty = BindableProperty.Create(nameof(InactiveColor), typeof(Color), typeof(GridSplitter));

    public Color InactiveColor
    {
        get => (Color)GetValue(InactiveColorProperty);
        set => SetValue(InactiveColorProperty, value);
    }

    public GridSplitter()
	{
		InitializeComponent();
    }

    protected override void OnParentSet()
    {
        base.OnParentSet();
        _grid = (Grid)Parent;

        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += (sender, e) =>
        {
            var pos = e.GetPosition(_grid).Value;
            var param = e.Parameter;
            var a = (e.Buttons & ButtonsMask.Primary) != 0;

            //Debussy.WriteLine($"Tapped, x: {pos.X}");
        };
        _grid.GestureRecognizers.Add(tapGestureRecognizer);

        var pointerGestureRecognizer = new PointerGestureRecognizer();
        pointerGestureRecognizer.PointerMoved += (sender, e) =>
        {
            if (!_isMouseDown || _throttler++ % 10 != 0) return;

            var position = e.GetPosition((Grid)sender);
            if (!position.HasValue) return;

            if (Math.Abs(_gridMousePositionX - position.Value.X) > 1)
            {
                _gridMousePositionX = position.Value.X;
                //Debussy.WriteLine("XX: " + position.Value.X);
            }

            if (Math.Abs(_gridMousePositionY - position.Value.Y) > 1)
            {
                _gridMousePositionY = position.Value.Y;
                //Debussy.WriteLine("YY: " + position.Value.Y);
            }

            if (_isChangingWidths)
                UpdateGrid(_gridMousePositionX);

        };
        _grid.GestureRecognizers.Add(pointerGestureRecognizer);
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

#if WINDOWS
        if (_grid is not Grid { Handler.PlatformView: LayoutPanel layoutPanel }) return;
        layoutPanel.PointerMoved += OnLayoutPanelPointerMoved;
        
        layoutPanel.PointerPressed += (sender, args) =>
        {
            _isMouseDown = true;
        };

        layoutPanel.PointerReleased += (sender, args) =>
        {
            _isMouseDown = false;
        };

        layoutPanel.PointerMoved += (sender, args) =>
        {
            var props = args.GetCurrentPoint(layoutPanel).Properties;
            var isLeftButtonPressed = props.IsLeftButtonPressed;
            var isRightButtonPressed = props.IsRightButtonPressed;
            //Debussy.WriteLine($"layoutPanel.PointerMoved. Left {isLeftButtonPressed}, Right {isRightButtonPressed}");
        };
#endif
    }

    private void OnPointerEntered(object sender, PointerEventArgs e)
    {
        _isChangingWidths = true;
        SplitterGrid.Background = ActiveColor;

#if WINDOWS
        if (sender is not Grid { Handler.PlatformView: LayoutPanel layoutPanel }) return;
        layoutPanel.ChangeCursor(InputSystemCursor.Create(InputSystemCursorShape.SizeWestEast));
#endif
    }

    private void OnPointerExited(object sender, PointerEventArgs e)
    {
        SplitterGrid.Background = InactiveColor;
#if WINDOWS
        if (sender is not Grid { Handler.PlatformView: LayoutPanel layoutPanel }) return;
        layoutPanel.ChangeCursor(InputSystemCursor.Create(InputSystemCursorShape.Arrow));
#endif
    }

    private void OnPointerMoved(object sender, PointerEventArgs e)
    {

#if WINDOWS
        var position = e.GetPosition(_grid);
        //Debussy.WriteLine("X: " + position.Value.X);
        //Debussy.WriteLine("Y: " + position.Value.Y);

        //UpdateGrid(position.Value.X);

        //if (!_isChangingWidths) return;
        //var position = e.GetPosition(this);
        //if (position == null) return;
        //var leftGridColumnDefinition = MainGrid.ColumnDefinitions[0];
        //var leftGridWidth = leftGridColumnDefinition.Width;
        //leftGridColumnDefinition.Width = new GridLength(position.Value.X);
        //InvalidateMeasure();
#endif
    }

#if WINDOWS
    private void OnLayoutPanelPointerMoved(object sender, PointerRoutedEventArgs args)
    {
        if (sender is not LayoutPanel layoutPanel) return;
        //Debussy.WriteLine("PointerMoved");
        var props = args.GetCurrentPoint(layoutPanel).Properties;
        _isChangingWidths = props.IsLeftButtonPressed;
    }
#endif

    private void OnDragStarting(object sender, Microsoft.Maui.Controls.DragStartingEventArgs e)
    {
        Debussy.WriteLine("DragStarting");
    }


    private void OnDropCompleted(object sender, Microsoft.Maui.Controls.DropCompletedEventArgs e)
    {
        Debussy.WriteLine("DropCompleted");
    }

    private void OnDrop(object sender, DropEventArgs e)
    {
        Debussy.WriteLine("OnDrop");
    }

    private void UpdateGrid(double offsetX)
    {
        if (_grid == null) return;

        //var column = Grid.GetColumn(this);

        //var columnCount = _grid.ColumnDefinitions.Count;
        //if (columnCount <= 1 || column == 0 || column == columnCount - 1 || column + Grid.GetColumnSpan(this) >= columnCount) return;

        //var columnLeft = _grid.ColumnDefinitions[column - 1];

        //var actualWidth = GetColumnDefinitionActualWidth(columnLeft) + offsetX;
        //if (actualWidth < 0) actualWidth = 0;

        //_grid.ColumnDefinitions[column - 1] = new ColumnDefinition(new Microsoft.Maui.GridLength(actualWidth));

        _grid.ColumnDefinitions[0] = new ColumnDefinition(new Microsoft.Maui.GridLength(offsetX));
    }

    private static double GetColumnDefinitionActualWidth(ColumnDefinition column)
    {
        double actualWidth;

        if (column.Width.IsAbsolute)
        {
            actualWidth = column.Width.Value;
        }
        else
        {
            if (ColumnDefinitionActualWidthProperty == null)
                ColumnDefinitionActualWidthProperty = column.GetType().GetRuntimeProperties().First((p) => p.Name == "ActualWidth");

            actualWidth = (double)ColumnDefinitionActualWidthProperty.GetValue(column);
        }

        return actualWidth;
    }
}
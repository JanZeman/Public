// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using Microsoft.Maui.Platform;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Windows.Graphics;
using WinRT.Interop;
using Color = Windows.UI.Color;
////using Colors = Microsoft.Maui.Graphics.Colors;

namespace MauiTemplate2024.App.Platforms.Windows
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

            var window = Application.Windows[0];
            if (window.Handler == null) return;

            var mauiWindow = window.Handler.PlatformView as MauiWinUIWindow;
            if (mauiWindow == null) return;

            var windowHandle = WindowNative.GetWindowHandle(mauiWindow);
            var windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
            var appWindow = AppWindow.GetFromWindowId(windowId);

            mauiWindow.Activated += (sender, eventArgs) => { Debussy.WriteLine("!!!Activated"); };
            mauiWindow.VisibilityChanged += (sender, eventArgs) => { Debussy.WriteLine("!!!VisibilityChanged"); };

            // Attempt to hide the title bar completely
            ////var presenter = appWindow.Presenter as OverlappedPresenter;
            ////if (presenter != null)
            ////{
            ////    mauiWindow.ExtendsContentIntoTitleBar = false;
            ////    presenter.SetBorderAndTitleBar(false, false);
            ////}

            //var uiSettings = new UISettings();
            //var color = uiSettings.GetColorValue(UIColorType.Accent);
            var colorPrimary = Color.FromArgb(0, 80, 0, 115);
            var colorWhite = Color.FromArgb(0, 255, 255, 255);

            appWindow.Resize(new SizeInt32(480, 800));
            appWindow.TitleBar.BackgroundColor = null; //appWindow.TitleBar.ButtonBackgroundColor = colorPrimary;
            //appWindow.TitleBar.ButtonForegroundColor = colorWhite;
            appWindow.TitleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;
            appWindow.Title = AppConstants.Name;
        }

        private void TempMethodForFasterTyping()
        {
            var layoutPanel = new LayoutPanel();

            layoutPanel.PointerPressed += (sender, args) =>
            {

            };

            layoutPanel.PointerReleased += (sender, args) =>
            {

            };

            layoutPanel.PointerMoved += (sender, args) =>
            {
                var props = args.GetCurrentPoint(layoutPanel).Properties;
                var isChangingWidths = props.IsLeftButtonPressed;
            };
        }
    }
}
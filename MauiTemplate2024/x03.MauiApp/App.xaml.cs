﻿namespace MauiTemplate2024.App
{
    public partial class App : Application
    {
        public App()
        {
            MauiExceptions.UnhandledException += (sender, args) =>
            {
                Debussy.BreakIfAttached((Exception)args.ExceptionObject);
            };

            InitializeComponent();

            PageAppearing += OnPageAppearing;
            PageDisappearing += OnPageDisappearing;

            MainPage = new AppShell();
        }

        //protected override Window CreateWindow(IActivationState activationState)
        //{
        //    var window = base.CreateWindow(activationState);
        //    window.Title = "Custom Window Title";
        //    return window;
        //}

        private BaseViewModel ResolveViewModel(Page page)
        {
            if (page is not MauiPage) return null;

            var viewModelType = GetType().Assembly
                .GetTypes().FirstOrDefault(x => x.Name == page.GetType().Name.Replace(nameof(Page), "ViewModel"));

            if (viewModelType == null)
                return null;

            var viewModel = MauiProgram.Services.GetService(viewModelType) as BaseViewModel;
            return viewModel;
        }

        private void OnPageAppearing(object sender, Page page)
        {
            if (page.BindingContext is BaseViewModel viewModel)
            {
                viewModel.OnAppearing();
                return;
            }

            viewModel = ResolveViewModel(page);
            if (viewModel == null) return;

            page.BindingContext = viewModel;
            viewModel.OnAppearing();
        }

        private static void OnPageDisappearing(object sender, Page page)
        {
            if (page.BindingContext is BaseViewModel viewModel)
                viewModel.OnDisappearing();
        }

        public void Dispose()
        {
            PageAppearing -= OnPageAppearing;
            PageDisappearing -= OnPageDisappearing;
        }
    }
}
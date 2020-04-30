using System;
using System.Diagnostics;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using PrismSerilogXamarinDemoNetStandard20.ViewModels;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrismSerilogXamarinDemoNetStandard20
{
    public partial class App
    {
        /*
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor.
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null)
        {
        }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            //await NavigateToTabbedPage();
            await NavigationService.NavigateAsync("MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterSerilog(Log.Logger);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

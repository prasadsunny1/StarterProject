using MonkeyCache;
using MonkeyCache.LiteDB;
using Prism;
using Prism.Ioc;
using Refit;
using StarterProject.Interfaces;
using StarterProject.Services;
using StarterProject.ViewModels;
using StarterProject.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace StarterProject
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
            Barrel.ApplicationId = "com.companyname.appname";
            Barrel.EncryptionKey = "com.companyname.appname";

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterSingleton<IPermissionServices,PermissionServices>();
            containerRegistry.RegisterSingleton<IDialogsService,DialogsServices>();
            containerRegistry.RegisterSingleton<IMovieRepository,MovieRepository>();
            
            var api = RestService.For<IOmdbApi>(AppConstants.BaseUrl);
            containerRegistry.RegisterInstance<IOmdbApi>(api);
        }
    }
}

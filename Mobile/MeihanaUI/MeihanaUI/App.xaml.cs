using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeihanaUI
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTc2OTBAMzEzNzJlMzIyZTMwY3FSN1FsU3U5OTFhdSs2VkFqNzVBTDdFRkgwamtXUTk4cFhPcUNUcFBycz0=");

            InitializeComponent();
            MainPage = new NavigationPage(AppShell.Init());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App5.Views.Forms;

namespace App5
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTc2OTBAMzEzNzJlMzIyZTMwY3FSN1FsU3U5OTFhdSs2VkFqNzVBTDdFRkgwamtXUTk4cFhPcUNUcFBycz0=");
            InitializeComponent();

            MainPage = new NavigationPage(new SimpleLoginPage());
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

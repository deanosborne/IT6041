using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using App5.Views.Folder;
using App5.Views.Navigation;
using App5.Models;
using App5.Helpers;
using System.IO;
using App5.Views;

namespace App5
{
    public partial class App : Application
    {
        static ContributorDatabase database;

        public static ContributorDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ContributorDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Contributors.db3"));
                }
                return database;
            }
        }
        public static string ApiEndpoint = "https://meihana.b2clogin.com/meihana.onmicrosoft.com/oauth2/authresp";
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        public App()
        {

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTc2OTBAMzEzNzJlMzIyZTMwY3FSN1FsU3U5OTFhdSs2VkFqNzVBTDdFRkgwamtXUTk4cFhPcUNUcFBycz0=");
            InitializeComponent();

            DependencyService.Register<B2CAuthenticationService>();
            MainPage = new MainPage();



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

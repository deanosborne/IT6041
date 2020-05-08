using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App5.Views.ContactUs;
using App5.Views.AboutUs;
using App5.Views.Catalog;
using App5.Views.Navigation;
using App5.Views.Detail;
using App5.Views.Folder;
using App5.Models;

namespace App5.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {

        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Home, (NavigationPage)Detail);

        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Home:
                        MenuPages.Add(id, new NavigationPage(new ArticleListPage()));
                        break;
                    case (int)MenuItemType.Resources:
                        MenuPages.Add(id, new NavigationPage(new CategoryTilePage()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutMeihanaPage()));
                        break;
                    case (int)MenuItemType.Contributors:
                        MenuPages.Add(id, new NavigationPage(new NavigationListCardPage()));
                        break;
                    case (int)MenuItemType.Team:
                        MenuPages.Add(id, new NavigationPage(new AboutUsWithCardsPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}
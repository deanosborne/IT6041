using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Meihana.Models;

namespace Meihana.Views
{
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.About, Title="Home" },
                new HomeMenuItem {Id = MenuItemType.Placeholder, Title="Placeholder" },
                new HomeMenuItem {Id = MenuItemType.Diagnosis, Title="Diagnosis" },
                new HomeMenuItem {Id = MenuItemType.Resources, Title="Resources" },
                new HomeMenuItem {Id = MenuItemType.Contact, Title="Contact" },
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}
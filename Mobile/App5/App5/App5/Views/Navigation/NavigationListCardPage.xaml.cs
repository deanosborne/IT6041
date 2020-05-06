using App5.DataService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Identity.Client;
using Xamarin.Forms;
using App5.Views.Folder;
using App5.Models;
using App5.Helpers;

namespace App5.Views.Navigation
{
    public partial class NavigationListCardPage : ContentPage
    {
        public bool si { get; set; }
        public NavigationListCardPage()
        {
            InitializeComponent();
            this.BindingContext = NavigationDataService.Instance.NavigationViewModel;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetNotesAsync();
        }

        async void OnNoteAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewContributorPage
            {
                BindingContext = new Contributors()
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new NewContributorPage
                {
                    BindingContext = e.SelectedItem as Contributors
                });
            }
        }
    }
}
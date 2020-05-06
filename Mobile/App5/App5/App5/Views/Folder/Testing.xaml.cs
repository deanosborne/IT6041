using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App5.Views.Folder;

namespace App5.Views.Folder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Testing : ContentPage
    {
        public Testing()
        {
            InitializeComponent();
        }

        async void AddNewUser_Clicked(object sender, System.EventArgs e)
        {
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var users = await Users.ReadUser();
            userListView.ItemsSource = users;
        }

    }
}
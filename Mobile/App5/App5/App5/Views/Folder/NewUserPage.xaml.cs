using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App5.Views.Folder;
using App5.Helpers;

namespace App5.Views.Folder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewUserPage : ContentPage
    {
        public NewUserPage()
        {
            InitializeComponent();
        }

        async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            Users user = new Users
            {
                email = usersEmailEntry.Text,
                name = usersNameEntry.Text,
                usergroup = "user",
            };

            bool result = await user.SaveUser();
            if (result)
            {
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Error", "We had problems saving this book, please try again", "Ok");
            }
        }
    }
}
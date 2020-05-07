using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App5.Models;

namespace App5.Views.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewContributorPage : ContentPage
    {
        public NewContributorPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Contributors)BindingContext;
            note.Date = DateTime.UtcNow;
            await App.Database.SaveNoteAsync(note);
            await DisplayAlert("Alert", "Contributer added!", "OK");
        }
        async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var contributors = (Contributors)BindingContext;
            await App.Database.DeleteNoteAsync(contributors);
            await Navigation.PopAsync();
        }
    }
}
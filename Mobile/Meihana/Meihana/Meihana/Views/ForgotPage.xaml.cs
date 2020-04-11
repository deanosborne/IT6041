using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Meihana.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPage : ContentPage
    {
        public ForgotPage()
        {
            InitializeComponent();
        }

        async void SubmitButton_Clicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }
    }
}
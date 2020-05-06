using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Identity.Client;
using Xamarin.Forms;
using App5.Views.Forms;

namespace App5.Views.Folder
{
    public partial class TestLoginPage : ContentPage
    {
        public TestLoginPage()
        {
            InitializeComponent();
        }

                async void OnSignInSignOut(object sender, EventArgs e)
        {
            try
            {
                if (btnSignInSignOut.Text == "Add")
                {
                    var userContext = await B2CAuthenticationService.Instance.SignInAsync();
                    UpdateSignInState(userContext);
                    UpdateUserInfo(userContext);
                    await Application.Current.MainPage.Navigation.PushModalAsync(new SimpleForgotPasswordPage());
                }
                else
                {
                    var userContext = await B2CAuthenticationService.Instance.SignOutAsync();
                    UpdateSignInState(userContext);
                    UpdateUserInfo(userContext);
                    userContext.IsLoggedOn = false;
                }
            }
            catch (Exception ex)
            {
                // Checking the exception message 
                // should ONLY be done for B2C
                // reset and not any other error.
                if (ex.Message.Contains("AADB2C90118"))
                    OnPasswordReset();
                // Alert if any exception excluding user canceling sign-in dialog
                else if (((ex as MsalException)?.ErrorCode != "authentication_canceled"))
                    await DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
            }
        }
        async void OnCallApi(object sender, EventArgs e)
        {
            try
            {

                var userContext = await B2CAuthenticationService.Instance.SignInAsync();
                var token = userContext.AccessToken;

                // Get data from API
                HttpClient client = new HttpClient();
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, App.ApiEndpoint);
                message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.SendAsync(message);
                string responseString = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {

                }
            }
            catch (MsalUiRequiredException ex)
            {
                await DisplayAlert($"Session has expired, please sign out and back in.", ex.ToString(), "Dismiss");
            }
            catch (Exception ex)
            {
                await DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
            }
        }

        async void OnEditProfile(object sender, EventArgs e)
        {
            try
            {
                var userContext = await B2CAuthenticationService.Instance.EditProfileAsync();
                UpdateSignInState(userContext);
                UpdateUserInfo(userContext);
            }
            catch (Exception ex)
            {
                // Alert if any exception excluding user canceling sign-in dialog
                if (((ex as MsalException)?.ErrorCode != "authentication_canceled"))
                    await DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
            }
        }
        async void OnResetPassword(object sender, EventArgs e)
        {
            try
            {
                var userContext = await B2CAuthenticationService.Instance.ResetPasswordAsync();
                UpdateSignInState(userContext);
                UpdateUserInfo(userContext);
            }
            catch (Exception ex)
            {
                // Alert if any exception excluding user canceling sign-in dialog
                if (((ex as MsalException)?.ErrorCode != "authentication_canceled"))
                    await DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
            }
        }
        async void OnPasswordReset()
        {
            try
            {
                var userContext = await B2CAuthenticationService.Instance.ResetPasswordAsync();
                UpdateSignInState(userContext);
                UpdateUserInfo(userContext);
            }
            catch (Exception ex)
            {
                // Alert if any exception excluding user canceling sign-in dialog
                if (((ex as MsalException)?.ErrorCode != "authentication_canceled"))
                    await DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
            }
        }

        void UpdateSignInState(UserContext userContext)
        {
            var isSignedIn = userContext.IsLoggedOn;
            btnSignInSignOut.Text = isSignedIn ? "Sign out" : "Add";
        }

        public void UpdateUserInfo(UserContext userContext)
        {
        }

    }
}
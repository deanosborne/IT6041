using System;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Microsoft.Identity.Client;
using Xamarin.Forms;
using App5.Views.Folder;
using Plugin.CurrentActivity;

namespace App5.Droid
{
    [Activity(Label = "App5", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Xamarin.Essentials.Platform.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);

            base.OnCreate(bundle);

            Forms.Init(this, bundle);

            LoadApplication(new App());

            DependencyService.Register<IParentWindowLocatorService, AndroidParentWindowLocatorService>();
            CrossCurrentActivity.Current.Init(this, bundle);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }
    }
}
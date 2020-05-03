using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Application = Android.App.Application;

[assembly: ExportRenderer(typeof(App5.Controls.BorderlessEntry), typeof(App5.Droid.BorderlessEntryRenderer))]

namespace App5.Droid
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessEntryRenderer() : base(Application.Context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                this.Control.SetBackground(null);
                Control.Gravity = GravityFlags.CenterVertical;
                Control.SetPadding(0, 0, 0, 0);
            }
        }
    }
}
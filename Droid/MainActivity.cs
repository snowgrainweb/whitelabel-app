using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using FFImageLoading.Forms.Droid;
using CarouselView.FormsPlugin.Android;

namespace WhiteLabel.Droid
{
	[Activity (Label = "White Label", Icon = "@drawable/blank", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            CachedImageRenderer.Init(true);
			global::Xamarin.Forms.Forms.Init (this, bundle);
            ImageCircleRenderer.Init();
			LoadApplication(new WhiteLabel.Apps());
		}
	}
}


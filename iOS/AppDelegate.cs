using System;
using System.Collections.Generic;
using System.Linq;
using ImageCircle.Forms.Plugin.iOS;  
using Foundation;
using UIKit;
using CarouselView.FormsPlugin.iOS;
using FFImageLoading.Forms.Touch;

namespace WhiteLabel.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			
            ImageCircleRenderer.Init(); 
            CarouselViewRenderer.Init();
            CachedImageRenderer.Init();
			LoadApplication(new WhiteLabel.Apps());
			return base.FinishedLaunching (app, options);
		}
	}
}


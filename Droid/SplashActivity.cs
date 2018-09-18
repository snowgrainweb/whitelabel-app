
using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Android.OS;
using Android.Support.V7.App;
using ImageCircle.Forms.Plugin.Droid;
using FFImageLoading.Forms.Droid;
using CarouselView.FormsPlugin.Android;
namespace WhiteLabel.Droid
{
	[Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            
            await Task.Delay(2000); // Simulate a bit of startup work.
            
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}

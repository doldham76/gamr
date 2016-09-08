using System;
using System.Net.Http;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Diagnostics;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using nerdytinder;

namespace nerdytinder.Droid
{
	[Activity (Label = "GAMr", 
		Icon = "@drawable/icon", 
		MainLauncher = true, 
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		Theme = "@android:style/Theme.Holo.Light")]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		//private MobileServiceUser user;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            CurrentPlatform.Init();
			global::Xamarin.Forms.Forms.Init (this, bundle);
			//Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
			//App.Init();
			LoadApplication (new App ());
		}

        protected override void OnResume()
        {
            System.Diagnostics.Debug.WriteLine("RESUME");
            //IsRunning = true;

            //ProcessNotificationPayload();
            base.OnResume();
        }

        protected override void OnStop()
        {
            System.Diagnostics.Debug.WriteLine("STOP");
            base.OnStop();
        }

        protected override void OnStart()
        {
            System.Diagnostics.Debug.WriteLine("START");
            base.OnStart();
        }
    }
}


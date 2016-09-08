using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Connectivity;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace nerdytinder
{
	public class App : Application
	{
		static App _instance;

		public static App Instance
		{
			get
			{
				return _instance;
			}
		}

        
		public static void Init()
		{
			Authenticator = DependencyService.Get<IAuthenticator>();
			authenticated = false;
		}
		
        public static IAuthenticator Authenticator
        {
            get;
            private set;
        }

        public static bool authenticated
        {
            get;
            set;
        }

        public Person CurrentNerd
		{
			get;
			set;
		}

        public bool IsNetworkReachable
        {
            get;
            set;
        }

		public App ()
		{
			_instance = this;
            //InitializeComponent();
            Init();
			// The root page of your application
			if (Settings.GamrID == string.Empty || !Settings.RegistrationComplete)
			{
                MainPage = new NavigationPage(new WelcomePage());
            }
			else
			{
				MainPage = new NavigationPage(new HomePage());
			}
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}


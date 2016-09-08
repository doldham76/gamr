using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using System.Net.Http;
using System.Linq;
using System.Diagnostics;
using Xamarin.Forms;
using nerdytinder;

namespace nerdytinder
{
	public partial class WelcomePage : ContentPage
	{
        IAuthenticator _authenticator = DependencyService.Get<IAuthenticator>();
        AuthenticationViewModel viewModel;
        string _authenticationStatus;

        public string AuthenticationStatus
        {
            get
            {
                return _authenticationStatus;
            }
            set
            {
                _authenticationStatus = value;
            }
        }

        public WelcomePage()
		{
			InitializeComponent();
            viewModel = new AuthenticationViewModel();
            BindingContext = viewModel;
            //Image dd = new Image();
            //dd.Source = 
            //gbutton.Image = (FileImageSource)ImageSource.FromResource("btn_google_signin_light_normal_xhdpi.9.png");
          
            gbutton.Clicked += async (sender, e) => {
                gbutton.IsEnabled = false;
                fbutton.IsEnabled = false;
                await viewModel.AuthenticateCompletelyGoogle();

                if (viewModel.AuthUserProfile != null)
                {
                    await Navigation.PushAsync(new SetAliasPage(viewModel.AuthUserProfile),false);
                }
                else
                {
                    gbutton.IsEnabled = true;
                    fbutton.IsEnabled = true;
                }

                await viewModel.AuthenticateWithAzure();

                if(!Settings.RegistrationComplete)
                    Settings.RegistrationComplete = true ;
            };

            fbutton.Clicked += async (sender, e) => {
                await DisplayAlert("Alert", "GamrID=" + Settings.GamrID, "Ok");
                await DisplayAlert("Alert", "RegComp=" + Settings.RegistrationComplete, "Ok");
            };
		}
		async void GoogleLoginClicked(object sender, EventArgs args)
		{
            await viewModel.AuthenticateCompletelyGoogle();

			if (App.Instance.CurrentNerd != null)
				await DisplayAlert("Login", "Google Login Successful", "Ok");

		}

		async void FacebookLoginClicked(object sender, EventArgs args)
		{

			if (App.authenticated == true)
				await DisplayAlert("Login", "Facebook Login Successful", "Ok");
		}
    }
}


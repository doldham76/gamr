using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using nerdytinder;
using Xamarin.Forms;


namespace nerdytinder
{
    public class AuthenticationViewModel : BaseViewModel
    {
        #region Properties

        IAuthenticator _authenticator = App.Authenticator;
        string _authenticationStatus;
        string _authenticationType;

        public string AuthenticationType
        {
            get
            {
                return _authenticationType;
            }
            set
            {
                SetPropertyChanged(ref _authenticationType, value);
            }
        }


        public string AuthenticationStatus
        {
            get
            {
                return _authenticationStatus;
            }
            set
            {
                SetPropertyChanged(ref _authenticationStatus, value);
            }
        }

        internal GoogleUserProfile AuthUserProfile
        {
            get;
            set;
        }


        #endregion

        /// <summary>
        /// Performs a complete authentication pass
        /// </summary>
        public async Task<bool> AuthenticateCompletelyGoogle()
        {
            //using (new Busy(this))
            //{
                AuthenticationType = "google";
                await ShowGoogleAuthenticationView();

                if (Settings.GoogleAccessToken == null)
                    return false;

                if (AuthUserProfile == null)
                    await GetUserProfile();

                if (AuthUserProfile != null)
                    return App.Instance.CurrentNerd != null;
                //await AuthenticateWithAzure();

                return App.Instance.CurrentNerd != null;

            //}
        }

        public async Task<bool> AuthenticateCompletelyFacebook()
        {
            //using (new Busy(this))
            //{
                AuthenticationType = "facebook";
                await ShowFacebookAuthenticationView();

                if (Settings.FacebookAccessToken == null)
                    return false;

                if (AuthUserProfile == null)
                    await GetUserProfile();

                if (AuthUserProfile != null)
                {
                    
                    await AuthenticateWithAzure();
                }
                    

                return App.Instance.CurrentNerd != null;
            //}
        }

        /// <summary>
        /// Shows the Google authentication web view so the user can authenticate
        /// </summary>
        async Task ShowGoogleAuthenticationView()
        {
            if (Settings.GoogleAccessToken != "none" && Settings.GoogleUserId != "none")
            {
                Debug.WriteLine("AccessToken=" + Settings.GoogleAccessToken);
                Debug.WriteLine("UserId=" + Settings.GoogleUserId);
                var success = await GetUserProfile();
                System.Diagnostics.Debug.WriteLine("Exception=71");
                if (success)
                {
                    AzureService.Instance.Client.CurrentUser = new MobileServiceUser(Settings.AzureUserID)
                    {
                        MobileServiceAuthenticationToken = Settings.AzureAuthToken
                    };

                    return;
                }
            }

            try
            {
                AuthenticationStatus = "Loading...";
                MobileServiceUser user = await _authenticator.Authenticate();
#pragma warning disable CS1701 // Assuming assembly reference matches identity
                var identity = await AzureService.Instance.Client.InvokeApiAsync("getUserIdentity", null, HttpMethod.Get, null);
#pragma warning restore CS1701 // Assuming assembly reference matches identity

                Settings.GoogleAccessToken = identity.Value<string>("token");
                Settings.AzureUserID = user.UserId;
                Settings.AzureAuthToken = user.MobileServiceAuthenticationToken;
                Debug.WriteLine("**GAMR GOOGLE AUTHENTICATION Token**\n\n" + Settings.GoogleAccessToken);
            }
            catch (Exception e)
            {
                Debug.WriteLine("**GAMR GOOGLE AUTHENTICATION ERROR**\n\n" + e.GetBaseException());
                //InsightsManager.Report(e);
            }
        }

        async Task ShowFacebookAuthenticationView()
        {
            if (Settings.FacebookAccessToken != null && Settings.FacebookUserId != null)
            {
                var success = await GetUserProfile();

                if (success)
                {
                    AzureService.Instance.Client.CurrentUser = new MobileServiceUser(Settings.AzureUserID)
                    {
                        MobileServiceAuthenticationToken = Settings.AzureAuthToken
                    };

                    return;
                }
            }

            try
            {
                AuthenticationStatus = "Loading...";
                MobileServiceUser user = await _authenticator.Authenticate();
#pragma warning disable CS1701 // Assuming assembly reference matches identity
                var identity = await AzureService.Instance.Client.InvokeApiAsync("getUserIdentity", null, HttpMethod.Get, null);
#pragma warning restore CS1701 // Assuming assembly reference matches identity

                Settings.FacebookAccessToken = identity.Value<string>("token");
                Settings.AzureUserID = user.UserId;
                Settings.AzureAuthToken = user.MobileServiceAuthenticationToken;
            }
            catch (Exception e)
            {
                Debug.WriteLine("**GAMR FACEBOOK AUTHENTICATION ERROR**\n\n" + e.GetBaseException());
                //InsightsManager.Report(e);
            }
        }

        /// <summary>
        /// Authenticates the athlete against the Azure backend and loads all necessary data to begin the app experience
        /// </summary>
        public async Task<bool> AuthenticateWithAzure()
        {
            Person athlete;
            AuthenticationStatus = "Getting athlete's profile";
            athlete = await GetPersonProfile();

            if (athlete == null)
            {
                //Unable to get athlete - try registering as a new athlete
                //await Navigation
                athlete = await RegisterPerson(AuthUserProfile);
            }
            else
            {
                athlete.ProfileImageUrl = AuthUserProfile.Picture;

                if (athlete.IsDirty)
                {
                    await AzureService.Instance.PersonManager.UpsertAsync(athlete);
                }
            }

            Settings.GamrID = athlete?.Id;
            App.Instance.CurrentNerd = athlete;

            if (App.Instance.CurrentNerd != null)
            {
                //await GetAllLeaderboards();
                MessagingCenter.Send(this, Messages.UserAuthenticated);
            }

            AuthenticationStatus = "Done";
            return App.Instance.CurrentNerd != null;
        }

        /// <summary>
        /// Gets the athlete's profile from the Azure backend
        /// </summary>
        async Task<Person> GetPersonProfile()
        {
            Person athlete = null;

            //Let's try to load based on email address
            if (athlete == null && AuthUserProfile != null && !AuthUserProfile.Email.IsEmpty())
            {
                var task = AzureService.Instance.PersonManager.GetPersonByEmail(AuthUserProfile.Email);
                await RunSafe(task);
                /*
                await Task.Run(() => {
                    task.Start();
                    task.Wait();
                });
                */
                if (task.IsCompleted && !task.IsFaulted)
                    athlete = task.Result;
            }

            return athlete;
        }


        /// <summary>
        /// Registers an athlete with the backend and returns the new athlete profile
        /// </summary>
        async Task<Person> RegisterPerson(GoogleUserProfile profile)
        {
            AuthenticationStatus = "Registering person";
            var athlete = new Person(profile);

            await AzureService.Instance.PersonManager.UpsertAsync(athlete);

            //"You're now an officially registered athlete!".ToToast();
            Debug.WriteLine("You're now an officially registered GAMr!");
            Settings.RegistrationComplete = true;
            return athlete;
        }


        /// <summary>
        /// Attempts to get the user profile from Google. Will use the refresh token if the auth token has expired
        /// </summary>
        async public Task<bool> GetUserProfile()
        {
            //Can't get profile w/out a token
            if (Settings.GoogleAccessToken == null)
                return false;

            if (AuthUserProfile != null)
                return true;

            AuthenticationStatus = "Getting Google user profile";
            System.Diagnostics.Debug.WriteLine("Exception=B");
            var task = GoogleApiService.Instance.GetUserProfile();
            System.Diagnostics.Debug.WriteLine("Exception=A");
            await RunSafe(task, false);
            /*
            try
            {
                await Task.Run(() =>
                {
                    task.Start();
                    task.Wait();
                });
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception=" + e.GetBaseException());
            }
            */
            System.Diagnostics.Debug.WriteLine("Exception=AR");

            if (task.IsFaulted && task.IsCompleted)
            {
                //return false;
                //Need to get refresh token from Azure somehow
                //Likely our authtoken has expired
                AuthenticationStatus = "Refreshing token";
                MobileServiceUser user = await AzureService.Instance.Client.RefreshUserAsync();
                var identity = await AzureService.Instance.Client.InvokeApiAsync("getUserIdentity", null, HttpMethod.Get, null);

                Settings.GoogleAccessToken = identity.Value<string>("token");
                Settings.AzureUserID = user.UserId;
                Settings.AzureAuthToken = user.MobileServiceAuthenticationToken;
                return await GetUserProfile();
                //var refreshTask = GoogleApiService.Instance.GetNewAuthToken(Settings.Instance.RefreshToken);
                //await Task.Run(() => {
                //    refreshTask.Start();
                //    refreshTask.Wait();
                //});
                //await RunSafe(refreshTask);

                //if(refreshTask.IsCompleted && !refreshTask.IsFaulted)
                //{
                //Success in getting a new auth token - now lets attempt to get the profile again
                //if(!string.IsNullOrWhiteSpace(refreshTask.Result) && App.AuthToken != refreshTask.Result)
                //{
                //We have a valid token now, let's try this again
                //		App.AuthToken = refreshTask.Result;
                //		await Settings.Instance.Save();
                //		return await GetUserProfile();
                //	}
                //}
            }
            

            if (task.IsCompleted && !task.IsFaulted && task.Result != null)
            {
                AuthenticationStatus = "Authentication complete";
                AuthUserProfile = task.Result;

                //InsightsManager.Identify(AuthUserProfile.Email, new Dictionary<string, string> {
                //	{
                //		"Name",
                //		AuthUserProfile.Name
                //	}
                //});

                Settings.GoogleUserId = AuthUserProfile.Id;
            }
            else
            {
                AuthenticationStatus = "Unable to authenticate";
            }

            return AuthUserProfile != null;
        }

        public void LogOut(bool clearCookies)
        {
            //Utility.SetSecured("AuthToken", string.Empty, "xamarin.sport", "authentication");
            //AzureService.Instance.Client.Logout();

            AuthUserProfile = null;
            Settings.GoogleAccessToken = null;
            Settings.GamrID = null;
            Settings.GoogleUserId = null;

            if (clearCookies)
            {
                Settings.RegistrationComplete = false;
                _authenticator.ClearCookies();
            }
        }
    }
}
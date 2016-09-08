// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Microsoft.WindowsAzure.MobileServices;

namespace nerdytinder
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
  {
		private static ISettings AppSettings
    	{
      		get
      		{
        		return CrossSettings.Current;
      		}
    	}

        #region Setting Constants


        //private static readonly string SettingsDefault = string.Empty;
        private const string _azureAuthTokenKey = "auth_token";
        private const string _azureUserIdKey = "user_id";
		private const string _gamrIdKey = "gamr_id";
        private const string _authType = "auth_type";
        private const string _googleUserId = "google_user_id";
        private const string _googleAccessToken = "google_access_token";
        private const string _registrationComplete = "registration_complete";
        private const string _facebookUserId = "facebook_user_id";
        private const string _facebookAccessToken = "facebook_access_token";


    	#endregion


    	public static string AzureAuthToken
    	{
      		get
      		{
        		return AppSettings.GetValueOrDefault<string>(_azureAuthTokenKey);
      		}
      		set
      		{
        		AppSettings.AddOrUpdateValue<string>(_azureAuthTokenKey, value);
      		}
    	}

		public static string AzureUserID
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(_azureUserIdKey);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(_azureUserIdKey, value);
			}
		}

		public static string GamrID
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(_gamrIdKey, string.Empty);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(_gamrIdKey, value);
			}
		}

        public static bool RegistrationComplete
        {
            get
            {
                return AppSettings.GetValueOrDefault<bool>(_registrationComplete, false);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_registrationComplete, value);
            }
        }

        public static string GoogleAccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(_googleAccessToken, "none");
            }
            set
            {
                AppSettings.AddOrUpdateValue(_googleAccessToken, value);
            }
        }

        public static string GoogleUserId
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(_googleUserId, "none");
            }
            set
            {
                AppSettings.AddOrUpdateValue(_googleUserId, value);
            }
        }

        public static string AuthTokenAndType
        {
            get
            {
                return GoogleAccessToken == null ? null : string.Format("{0} {1}", "Bearer", GoogleAccessToken);
            }
        }

        public static string FacebookAccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(_facebookAccessToken, null);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_facebookAccessToken, value);
            }
        }

        public static string FacebookUserId
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(_facebookUserId, null);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_facebookUserId, value);
            }
        }

        public static string AuthType
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(_authType, null);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_authType, value);
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using nerdytinder;
using Xamarin.Forms;
using System.Diagnostics;

[assembly: Dependency(typeof(nerdytinder.Droid.Authentication))]

namespace nerdytinder.Droid
{
	public class Authentication: IAuthenticator
	{
		public async Task<MobileServiceUser> Authenticate()
		{
			try
			{
				return await AzureService.Instance.Client.LoginAsync(Forms.Context, MobileServiceAuthenticationProvider.Google);
			}
			catch (Exception e)
			{
                Debug.WriteLine(e.ToString());
			}

			return null;
		}

		public void ClearCookies()
		{
			global::Android.Webkit.CookieManager.Instance.RemoveAllCookies(null);
		}
	}
}


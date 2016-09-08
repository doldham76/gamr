using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(nerdytinder.iOS.Authenticator))]

namespace nerdytinder.iOS
{
    public class Authenticator : IAuthenticator
    {
        public async Task<MobileServiceUser> Authenticate()
        {
            try
            {
                var window = UIKit.UIApplication.SharedApplication.KeyWindow;
                var root = window.RootViewController;
                if (root != null)
                {
                    var current = root;
                    while (current.PresentedViewController != null)
                    {
                        current = current.PresentedViewController;
                    }

                    var user = await AzureService.Instance.Client.LoginAsync(window.RootViewController, MobileServiceAuthenticationProvider.Google);
                    return user;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //InsightsManager.Report(e);
            }

            return null;
        }

        public void ClearCookies()
        {
            //var store = NSHttpCookieStorage.SharedStorage;
            //var cookies = store.Cookies;

            //foreach(var c in cookies)
            //{
            //	store.DeleteCookie(c);
            //}
        }
    }
}
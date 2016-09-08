using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace nerdytinder
{
    public class GoogleApiService
    {
        static GoogleApiService _instance;

        public static GoogleApiService Instance
        {
            get
            {
                return _instance ?? (_instance = new GoogleApiService());
            }
        }

        public Task<GoogleUserProfile> GetUserProfile()
        {
            return new Task<GoogleUserProfile>(() =>
            {
                try
                {
                    using (var client = new HttpClient())
                    {

                        System.Diagnostics.Debug.WriteLine("Exception=1");
                        const string url = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json";
                        client.DefaultRequestHeaders.Add("Authorization", Settings.AuthTokenAndType);
                        var json = client.GetStringAsync(url).Result;
                        //var json = client.GetStringAsync(url).Result;
                        /*
                        System.Diagnostics.Debug.WriteLine("Exception=54");
                        if (json ==  null)
                        {
                            System.Diagnostics.Debug.WriteLine("Exception=8888884");
                            return null;
                        }
                        */
                        var profile = JsonConvert.DeserializeObject<GoogleUserProfile>(json);
                        return profile;
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Exception="+e.GetBaseException());
                    return null;
                }
            });
        }
    }
}


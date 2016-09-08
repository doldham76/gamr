using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using ModernHttpClient;
using System.Diagnostics;



namespace nerdytinder
{
	public class AzureService
	{
		public AzureService()
		{
			var url = new Uri(Constants.ApplicationURL);
			var store = new MobileServiceSQLiteStore($"{url.Host}.db");
			store.DefineTable<Person>();
			store.DefineTable<Groups>();
			//store.DefineTable<Membership>();
			Client.SyncContext.InitializeAsync(store);

			PersonManager = new PersonManager();
			//MembershipManager = new MembershipManager();
			//GroupsManager = new GroupsManager();
		}

		#region Properties
        /*
		public GroupsManager GroupsManager
		{
			get;
			private set;
		}
        */
		public PersonManager PersonManager
		{
			get;
			private set;
		}
        /*
		public MembershipManager MembershipManager
		{
			get;
			private set;
		}
        */


		static AzureService _instance;

		public static AzureService Instance
		{
			get
			{
				return _instance ?? (_instance = new AzureService());
			}
		}

		MobileServiceClient _client;

		public MobileServiceClient Client
		{
			get
			{
				if (_client == null)
				{
					var handler = new NativeMessageHandler();

#if __IOS__

					//Use ModernHttpClient for caching and to allow traffic to be routed through Charles/Fiddler/etc
					handler = new ModernHttpClient.NativeMessageHandler() {
						Proxy = CoreFoundation.CFNetwork.GetDefaultProxy(),
						UseProxy = true,
					};

#endif

					_client = new MobileServiceClient(Constants.ApplicationURL, new HttpMessageHandler[] {
						//new LeagueExpandHandler(),
						//new ChallengeExpandHandler(),
						handler,
					});

					_client.AlternateLoginHost = new Uri(Constants.ApplicationURL);
				}

				return _client;
			}
		}

		#endregion

		public async Task<bool> SyncAllAsync()
		{
			var list = new List<Task<bool>>();

			//list.Add(GroupsManager.SyncAsync());
			list.Add(PersonManager.SyncAsync());
			//list.Add(MembershipManager.SyncAsync());

			var successes = await Task.WhenAll(list).ConfigureAwait(false);
			//var count = MembershipManager.Table.ToListAsync().Result;
			return successes.Any(x => !x);
		}
	}
}


using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace nerdytinder
{
	public interface IAuthenticator
	{
		Task<MobileServiceUser> Authenticate();
		void ClearCookies();
	}
}


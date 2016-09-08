using System;

namespace nerdytinder
{
	public static class Constants
	{
		// Replace strings with your mobile services and gateway URLs.
		public static string ApplicationURL = @"https://nerdytinder.azurewebsites.net";
	}

	public static class Messages
	{
		public static readonly string IncomingPayloadReceived = "IncomingPayloadReceived";
		public static readonly string RegisteredForRemoteNotifications = "RegisteredForRemoteNotifications";
		public static readonly string ExceptionOccurred = "ExceptionOccurred";
		public static readonly string IncomingPayloadReceivedInternal = "IncomingPayloadReceivedInternal";
		public static readonly string AuthenticationComplete = "AuthenticationComplete";
		public static readonly string UserAuthenticated = "UserAuthenticated";
	}
}


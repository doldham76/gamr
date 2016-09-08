using System;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;

namespace nerdytinder
{
	public class Groups
	{
		string id;
		string name;
		string description;
		string address;
		string city;
		string zip;
		string state;
		string startDate;
		string endDate;

		[JsonProperty(PropertyName = "id")]
		public string Id
		{
			get { return id; }
			set { id = value; }
		}

		[JsonProperty(PropertyName = "name")]
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		[JsonProperty(PropertyName = "description")]
		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		[JsonProperty(PropertyName = "address")]
		public string Address
		{
			get { return address; }
			set { address = value; }
		}

		[JsonProperty(PropertyName = "city")]
		public string City
		{
			get { return city; }
			set { city = value; }
		}

		[JsonProperty(PropertyName = "zip")]
		public string Zip
		{
			get { return zip; }
			set { zip = value; }
		}

		[JsonProperty(PropertyName = "state")]
		public string State
		{
			get { return state; }
			set { state = value; }
		}

		[JsonProperty(PropertyName = "startDate")]
		public string StartDate
		{
			get { return startDate; }
			set { startDate = value; }
		}

		[JsonProperty(PropertyName = "endDate")]
		public string EndDate
		{
			get { return endDate; }
			set { endDate = value; }
		}

		[Version]
		public string Version { get; set; }
	}
}


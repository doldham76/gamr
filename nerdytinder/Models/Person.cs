using System;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;

namespace nerdytinder
{
	public class Person: BaseModel
	{
		public Person()
        {
            Initialize();
        }

        public Person(GoogleUserProfile gprofile = null, FacebookUserProfile fprofile = null)
        {
            Name = gprofile.Name;
            Email = gprofile.Email;
            AuthenticationId = gprofile.Id;
            ProfileImageUrl = gprofile.Picture;
            Initialize();
        }

        void Initialize()
        {
        }
        
        //string id;
		string name;
		string email;
		string fname;
		string lname;
		string personType;


		[JsonProperty(PropertyName = "name")]
		public string Name
		{
			get { return name; }
			set { SetPropertyChanged(ref name, value); }
		}

		[JsonProperty(PropertyName = "email")]
		public string Email
		{
			get { return email; }
			set { SetPropertyChanged(ref email, value); }
		}

		[JsonProperty(PropertyName = "fname")]
		public string First
		{
			get { return fname; }
			set { fname = value; }
		}
		[JsonProperty(PropertyName = "lname")]
		public string Last
		{
			get { return lname; }
			set { lname = value; }
		}

		[JsonProperty(PropertyName = "personType")]
		public string PersonType
		{
			get { return personType; }
			set { personType = value; }
		}

        string _profileImageUrl;

        public string ProfileImageUrl
        {
            get
            {
                return _profileImageUrl;
            }
            set
            {
                SetPropertyChanged(ref _profileImageUrl, value);
            }
        }

        string _authenticationId;

        public string AuthenticationId
        {
            get
            {
                return _authenticationId;
            }
            set
            {
                SetPropertyChanged(ref _authenticationId, value);
            }
        }

    }
}


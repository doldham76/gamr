using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nerdytinder;

using Xamarin.Forms;

namespace nerdytinder
{
    public partial class SetAliasPage : ContentPage
    {
        Person PersonProfile;
        public SetAliasPage(GoogleUserProfile Profile)
        {
            InitializeComponent();
            PersonProfile = new nerdytinder.Person()
            {
               Name = Profile.Name,
               Email = Profile.Email,
               ProfileImageUrl = Profile.Picture
            };

            labelName.Text = PersonProfile.Name;
            labelEmail.Text = PersonProfile.Email;
            labelUrl.Source = PersonProfile.ProfileImageUrl;
            state.Items.Add("CA");
            state.Items.Add("TX");
            state.Items.Add("NY");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.Animate("", s => Layout(new Rectangle(((-1 + s) * Width), Y, Width, Height)), 16, 250, Easing.Linear, null, null);
        }
    }
}

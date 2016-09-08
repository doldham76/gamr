using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace nerdytinder
{
    public partial class HomePage : ContentPage
    {
        PersonViewModel viewModel;
        public HomePage()
        {
            InitializeComponent();
            viewModel = new PersonViewModel();
            viewModel.PersonId = Settings.GamrID;
            this.BindingContext = viewModel;
            this.Title = "GAMr";
        }

    }
}

using Phanteon.Features.Profile.ViewModels;

namespace Phanteon.Features.Profile.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage(ProfileViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}

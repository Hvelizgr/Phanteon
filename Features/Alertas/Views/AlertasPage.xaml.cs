using Phanteon.Features.Alertas.ViewModels;

namespace Phanteon.Features.Alertas.Views
{
    public partial class AlertasPage : ContentPage
    {
        private readonly AlertasViewModel _viewModel;

        public AlertasPage(AlertasViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.InitializeAsync();
        }
    }
}

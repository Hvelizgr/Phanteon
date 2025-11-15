using Phanteon.Features.Dispositivos.ViewModels;

namespace Phanteon.Features.Dispositivos.Views
{
    public partial class DispositivoDetailPage : ContentPage
    {
        private readonly DispositivoDetailViewModel _viewModel;

        public DispositivoDetailPage(DispositivoDetailViewModel viewModel)
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

using Phanteon.Features.Dispositivos.ViewModels;

namespace Phanteon.Features.Dispositivos.Views
{
    public partial class DispositivosPage : ContentPage
    {
        private readonly DispositivosViewModel _viewModel;

        public DispositivosPage(DispositivosViewModel viewModel)
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

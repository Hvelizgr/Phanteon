using Phanteon.ViewModels;

namespace Phanteon.Views
{
    public partial class DispositivosPage : ContentPage
    {
        public DispositivosPage(DispositivosViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            // Cargar dispositivos al iniciar la página
            _ = viewModel.CargarDispositivosCommand.ExecuteAsync(null);
        }
    }
}

using Phanteon.ViewModels;

namespace Phanteon.Views
{
    public partial class DiagnosticoPage : ContentPage
    {
        private readonly DiagnosticoViewModel _viewModel;

        public DiagnosticoPage(DiagnosticoViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        private async void OnProbarConexionClicked(object sender, EventArgs e)
        {
            await _viewModel.ProbarConexionAsync();
            ActualizarEstadoUI();
        }

        private async void OnObtenerDispositivosClicked(object sender, EventArgs e)
        {
            await _viewModel.ObtenerDispositivosAsync();
            ActualizarEstadoUI();
        }

        private async void OnObtenerAlertasClicked(object sender, EventArgs e)
        {
            await _viewModel.ObtenerAlertasAsync();
            ActualizarEstadoUI();
        }

        private async void OnVolverClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void ActualizarEstadoUI()
        {
            // Cambiar color del frame según el estado
            if (_viewModel.EstadoConexion.Contains("Conectado") || _viewModel.EstadoConexion.Contains("Éxito"))
            {
                EstadoFrame.BackgroundColor = Color.FromArgb("#D4EDDA");
                EstadoLabel.TextColor = Color.FromArgb("#155724");
            }
            else if (_viewModel.EstadoConexion.Contains("Error") || _viewModel.EstadoConexion.Contains("Fallo"))
            {
                EstadoFrame.BackgroundColor = Color.FromArgb("#F8D7DA");
                EstadoLabel.TextColor = Color.FromArgb("#721C24");
            }
            else
            {
                EstadoFrame.BackgroundColor = Color.FromArgb("#FFF3CD");
                EstadoLabel.TextColor = Color.FromArgb("#856404");
            }
        }
    }
}

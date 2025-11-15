using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Phanteon.Services.Navigation;

namespace Phanteon.Features.Main.ViewModels
{
    /// <summary>
    /// ViewModel para la página principal de la aplicación
    /// </summary>
    public partial class MainViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        /// <summary>
        /// Navega a la página de dispositivos
        /// </summary>
        [RelayCommand]
        private async Task NavigateToDispositivosAsync()
        {
            await _navigationService.NavigateToAsync("Dispositivos");
        }

        /// <summary>
        /// Navega a la página de alertas
        /// </summary>
        [RelayCommand]
        private async Task NavigateToAlertasAsync()
        {
            await _navigationService.NavigateToAsync("Alertas");
        }

        /// <summary>
        /// Navega a la página de perfil
        /// </summary>
        [RelayCommand]
        private async Task NavigateToProfileAsync()
        {
            await _navigationService.NavigateToAsync("Profile");
        }
    }
}

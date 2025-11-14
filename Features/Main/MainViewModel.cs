using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Phanteon.Services.Navigation;

namespace Phanteon.Features.Main
{
    /// <summary>
    /// ViewModel para la página principal de la aplicación
    /// </summary>
    public partial class MainViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private int count;

        [ObservableProperty]
        private string counterText = "Click me";

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        /// <summary>
        /// Comando para incrementar el contador
        /// </summary>
        [RelayCommand]
        private void IncrementCounter()
        {
            Count++;
            CounterText = Count == 1
                ? $"Clicked {Count} time"
                : $"Clicked {Count} times";
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

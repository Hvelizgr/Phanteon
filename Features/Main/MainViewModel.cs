using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Phanteon.Features.Main
{
    /// <summary>
    /// ViewModel para la página principal de la aplicación
    /// </summary>
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private int count;

        [ObservableProperty]
        private string counterText = "Click me";

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
    }
}

namespace Phanteon.Services.Navigation
{
    /// <summary>
    /// Implementación del servicio de navegación usando Shell de MAUI
    /// </summary>
    public class NavigationService : INavigationService
    {
        public async Task NavigateToAsync(string route)
        {
            await Shell.Current.GoToAsync(route);
        }

        public async Task NavigateToAsync(string route, IDictionary<string, object> parameters)
        {
            await Shell.Current.GoToAsync(route, parameters);
        }

        public async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async Task NavigateToRootAsync()
        {
            await Shell.Current.GoToAsync("//");
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;

namespace Phanteon.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        // Aquí puedes extender con propiedades comunes como IsBusy, Title, etc.
        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }
    }
}
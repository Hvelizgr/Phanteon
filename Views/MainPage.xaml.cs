namespace Phanteon.Views
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OnDiagnosticoClicked(object? sender, EventArgs e)
        {
            // Obtener la página de diagnóstico desde el contenedor de DI
            var diagnosticoPage = Application.Current?.Handler?.MauiContext?.Services.GetService<DiagnosticoPage>();

            if (diagnosticoPage != null)
            {
                await Navigation.PushAsync(diagnosticoPage);
            }
        }
    }
}

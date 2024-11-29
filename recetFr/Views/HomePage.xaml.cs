using Microsoft.Maui.Controls;

namespace recetFr.Views
{
    public partial class HomePage : ContentPage
    {
        private bool isDarkTheme = false; // Variable para rastrear el estado del tema actual

        public HomePage()
        {
            InitializeComponent();
            ApplyTheme(); // Aplicar el tema inicial
        }

        private void OnThemeToggleClicked(object sender, EventArgs e)
        {
            isDarkTheme = !isDarkTheme; // Alternar entre claro y oscuro
            ApplyTheme(); // Aplicar el tema correspondiente
        }

        private void ApplyTheme()
        {
            // Cambiar el estilo de la página y del Label según el tema
            if (isDarkTheme)
            {
                this.Style = (Style)Resources["DarkTheme"];
                welcomeLabel.Style = (Style)Resources["DarkLabelStyle"];
            }
            else
            {
                this.Style = (Style)Resources["LightTheme"];
                welcomeLabel.Style = (Style)Resources["LightLabelStyle"];
            }
        }
    }
}

using recetFr.Models;
using recetFr.Services;

namespace recetFr.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly ApiService _apiService = new ApiService();

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text))
            {
                await DisplayAlert("Error", "Por favor, ingresa tu nombre de usuario y contraseña.", "OK");
                return;
            }

            var loginRequest = new LoginRequest
            {
                Username = UsernameEntry.Text,
                Password = PasswordEntry.Text
            };

            var result = await _apiService.Login(loginRequest);

            if (result.IsSuccess)
            {
                await DisplayAlert("Inicio de Sesión", result.Message, "OK");
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Error", result.Message, "OK");
            }
        }

        private async void OnNavigateToRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}

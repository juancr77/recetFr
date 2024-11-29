using System.Text.RegularExpressions;
using recetFr.Models;
using recetFr.Services;

namespace recetFr.Views
{
    public partial class RegisterPage : ContentPage
    {
        private readonly ApiService _apiService = new ApiService();

        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Validaciones de campos vacíos
            if (string.IsNullOrWhiteSpace(UsernameEntry.Text))
            {
                await DisplayAlert("Error", "El nombre de usuario es obligatorio.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(EmailEntry.Text) || !IsValidEmail(EmailEntry.Text))
            {
                await DisplayAlert("Error", "Por favor, ingresa un correo electrónico válido.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
            {
                await DisplayAlert("Error", "La contraseña es obligatoria.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(FullNameEntry.Text))
            {
                await DisplayAlert("Error", "El nombre completo es obligatorio.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(FavoriteFoodEntry.Text))
            {
                await DisplayAlert("Error", "La comida favorita es obligatoria.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(CityEntry.Text))
            {
                await DisplayAlert("Error", "La ciudad es obligatoria.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(CountryEntry.Text))
            {
                await DisplayAlert("Error", "El país es obligatorio.", "OK");
                return;
            }

            // Creación del objeto User
            var user = new User
            {
                Username = UsernameEntry.Text.Trim(),
                Email = EmailEntry.Text.Trim(),
                Password = PasswordEntry.Text.Trim(),
                FullName = FullNameEntry.Text.Trim(),
                FavoriteFood = FavoriteFoodEntry.Text.Trim(),
                City = CityEntry.Text.Trim(),
                Country = CountryEntry.Text.Trim()
            };

            // Llamada al servicio de registro
            var result = await _apiService.RegisterUser(user);

            if (result.IsSuccess)
            {
                // Si el registro es exitoso
                await DisplayAlert("Registro", result.Message, "OK");
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                // Si ocurre un error
                await DisplayAlert("Error", result.Message, "OK");
            }
        }

        // Método para validar formato de correo
        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}

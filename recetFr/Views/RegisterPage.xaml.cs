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
            if (string.IsNullOrWhiteSpace(UsernameEntry.Text) ||
                string.IsNullOrWhiteSpace(EmailEntry.Text) ||
                string.IsNullOrWhiteSpace(PasswordEntry.Text))
            {
                await DisplayAlert("Error", "Por favor, llena todos los campos obligatorios.", "OK");
                return;
            }

            var user = new User
            {
                Username = UsernameEntry.Text,
                Email = EmailEntry.Text,
                Password = PasswordEntry.Text,
                FullName = FullNameEntry.Text,
                FavoriteFood = FavoriteFoodEntry.Text,
                City = CityEntry.Text,
                Country = CountryEntry.Text
            };

            var result = await _apiService.RegisterUser(user);

            if (result.IsSuccess)
            {
                await DisplayAlert("Registro", result.Message, "OK");
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await DisplayAlert("Error", result.Message, "OK");
            }
        }
    }
}

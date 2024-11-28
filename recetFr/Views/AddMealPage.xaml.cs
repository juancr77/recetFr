using recetFr.Models;
using recetFr.Services;

namespace recetFr.Views
{
    public partial class AddMealPage : ContentPage
    {
        private readonly ApiService _apiService;

        public AddMealPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void OnSaveMealClicked(object sender, EventArgs e)
        {
            var newMeal = new Meal
            {
                Name = NameEntry.Text,
                Category = CategoryEntry.Text,
                Area = AreaEntry.Text,
                Ingredients = IngredientsEditor.Text,
                PreparationTime = int.TryParse(PreparationTimeEntry.Text, out var prepTime) ? prepTime : 0,
                CookingTime = int.TryParse(CookingTimeEntry.Text, out var cookTime) ? cookTime : 0,
                Instructions = InstructionsEditor.Text
            };

            var result = await _apiService.CreateMealAsync(newMeal);
            if (result.IsSuccess)
            {
                await DisplayAlert("Success", "Meal added successfully!", "OK");
                await Shell.Current.GoToAsync(".."); // Volver al listado de comidas
            }
            else
            {
                await DisplayAlert("Error", result.Message, "OK");
            }
        }
    }
}

using recetFr.Models;
using recetFr.Services;

namespace recetFr.Views
{
    [QueryProperty(nameof(MealId), "id")]
    public partial class EditMealPage : ContentPage
    {
        private readonly ApiService _apiService;
        public int MealId { get; set; }

        public EditMealPage()
        {
            InitializeComponent(); // Carga la interfaz de usuario
            _apiService = new ApiService();
        }

        // Este método se ejecuta cuando aparece la página
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var meal = await _apiService.GetMealByIdAsync(MealId);
            if (meal != null)
            {
                NameEntry.Text = meal.Name;
                CategoryEntry.Text = meal.Category;
                AreaEntry.Text = meal.Area;
                IngredientsEditor.Text = meal.Ingredients;
                PreparationTimeEntry.Text = meal.PreparationTime.ToString();
                CookingTimeEntry.Text = meal.CookingTime.ToString();
                InstructionsEditor.Text = meal.Instructions;
            }
        }

        // Guardar los cambios realizados a la comida
        private async void OnSaveChangesClicked(object sender, EventArgs e)
        {
            var updatedMeal = new Meal
            {
                MealId = MealId,
                Name = NameEntry.Text,
                Category = CategoryEntry.Text,
                Area = AreaEntry.Text,
                Ingredients = IngredientsEditor.Text,
                PreparationTime = int.TryParse(PreparationTimeEntry.Text, out var prepTime) ? prepTime : 0,
                CookingTime = int.TryParse(CookingTimeEntry.Text, out var cookTime) ? cookTime : 0,
                Instructions = InstructionsEditor.Text
            };

            var result = await _apiService.UpdateMealAsync(MealId, updatedMeal);
            if (result.IsSuccess)
            {
                await DisplayAlert("Success", "Meal updated successfully!", "OK");
                await Shell.Current.GoToAsync(".."); // Regresar a la página anterior
            }
            else
            {
                await DisplayAlert("Error", result.Message, "OK");
            }
        }
    }
}

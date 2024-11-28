using recetFr.Models;
using recetFr.Services;

namespace recetFr.Views
{
    public partial class MealsPage : ContentPage
    {
        private readonly ApiService _apiService;
        public List<Meal> Meals { get; set; }

        public MealsPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadMeals();
        }

        private async void LoadMeals()
        {
            Meals = await _apiService.GetAllMealsAsync();
            MealsList.ItemsSource = Meals;
        }

        private async void OnAddMealClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("AddMealPage");
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var mealId = (int)button.CommandParameter;

            await Shell.Current.GoToAsync($"{nameof(EditMealPage)}?id={mealId}");
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var mealId = (int)button.CommandParameter;

            var confirm = await DisplayAlert("Confirm", "Are you sure you want to delete this meal?", "Yes", "No");
            if (confirm)
            {
                var result = await _apiService.DeleteMealAsync(mealId);
                if (result.IsSuccess)
                {
                    await DisplayAlert("Success", "Meal deleted successfully!", "OK");
                    LoadMeals(); // Recargar lista
                }
                else
                {
                    await DisplayAlert("Error", result.Message, "OK");
                }
            }
        }

        private async void OnRefreshClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Refreshing", "Updating the meal list...", "OK");
            LoadMeals(); // Recargar la lista de comidas
        }
    }
}

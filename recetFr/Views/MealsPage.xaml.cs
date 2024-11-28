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

        private async void OnDetailsClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var mealId = (int)button.CommandParameter;
            var meal = await _apiService.GetMealByIdAsync(mealId);

            await DisplayAlert("Meal Details", $"Name: {meal.Name}\nCategory: {meal.Category}", "OK");
        }

        private async void OnAddMealClicked(object sender, EventArgs e)
        {
            // Navega a una página para crear una nueva comida
        }
    }
}

using recetFr.Services;
using recetFr.Models;

namespace recetFr.Views;

public partial class MealsSearchPage : ContentPage
{
    private readonly ApiService _apiService = new ApiService();

    public MealsSearchPage()
    {
        InitializeComponent();
    }

    private async void OnSearchButtonClicked(object sender, EventArgs e)
    {
        var query = SearchEntry.Text;
        if (!string.IsNullOrEmpty(query))
        {
            var meals = await _apiService.SearchMealByName(query);
            MealsListView.ItemsSource = meals;
        }
    }
}

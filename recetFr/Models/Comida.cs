namespace recetFr.Models;

public class ComidaResponse
{
    public List<Comida> Meals { get; set; }
}

public class Comida
{
    public string idMeal { get; set; }
    public string strMeal { get; set; }
    public string strCategory { get; set; }
    public string strInstructions { get; set; }
    public string strMealThumb { get; set; }
}

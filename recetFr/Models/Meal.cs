namespace recetFr.Models
{
    public class Meal
    {
        public int MealId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Area { get; set; }
        public string Ingredients { get; set; }
        public int PreparationTime { get; set; }
        public int CookingTime { get; set; }
        public string Instructions { get; set; }
    }
}

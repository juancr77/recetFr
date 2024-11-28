namespace recetFr.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; } // Agregar el campo Email
        public string Password { get; set; }
        public string FullName { get; set; }
        public string FavoriteFood { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}

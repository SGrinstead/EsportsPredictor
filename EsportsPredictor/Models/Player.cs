namespace EsportsPredictor.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public Team Current_team { get; set; }
        public Videogame Current_videogame { get; set; }
        public string Name { get; set; }
        public string? First_name { get; set; }
        public string? Last_name { get; set; }
        public string? Nationality { get; set; }
        public string? Role { get; set; }
        public Uri Image_url { get; set; }
    }
}

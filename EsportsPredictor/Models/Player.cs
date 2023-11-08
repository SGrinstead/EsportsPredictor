namespace EsportsPredictor.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public Team CurrentTeam { get; set; }
        public Videogame CurrentVideogame { get; set; }
        public string Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Nationality { get; set; }
        public string? Role { get; set; }
        public Uri ImageUrl { get; set; }
    }
}

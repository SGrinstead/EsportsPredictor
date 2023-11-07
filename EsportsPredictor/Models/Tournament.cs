namespace EsportsPredictor.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public DateTime BeginAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Tier { get; set; }
        public string Prizepool { get; set; }
        public Videogame Videogame { get; set; }
        public string WinnerType { get; set; }
        public int? WinnerId { get; set; }
        public List<Team> Teams { get; set; }
        public List<Match> Matches { get; set; }
    }
}

namespace EsportsPredictor.Models
{
    public class Match
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public DateTime BeginAt { get; set; }
        public DateTime EndAt { get; set; }
        public string MatchType { get; set; }
        public string Status { get; set; }
        public string WinnerType { get; set; }
        public int WinnerId { get; set; }
    }
}

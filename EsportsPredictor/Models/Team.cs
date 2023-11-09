namespace EsportsPredictor.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Acronym { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public Uri Image_url { get; set; }
    }
}

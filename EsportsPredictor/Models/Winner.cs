namespace EsportsPredictor.Models
{
	public class Winner
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }
        public string DetailsPageUrl { get; set; }

        public Winner() { }

        public Winner(Team team)
        {
            Id = team.Id;
            Name = team.Name;
            Slug = team.Slug;
            Type = "Team";
            DetailsPageUrl = $"/teams/{team.Slug}";
        }
    }
}

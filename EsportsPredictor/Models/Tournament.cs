namespace EsportsPredictor.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public League League { get; set; }
        public string Name { get; set; }
        public DateTime? Begin_at { get; set; }
        public DateTime? End_at { get; set; }
        public string Tier { get; set; }
        public string Prizepool { get; set; }
        public Videogame Videogame { get; set; }
        public string WinnerType { get; set; }
        public int? WinnerId { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();
        public List<Match> Matches { get; set; } = new List<Match>();

		public string GetLocalBeginAtString()
		{
			if (Begin_at.HasValue)
			{
				DateTime local = (DateTime)Begin_at;
				local = local.ToLocalTime();
				return local.ToShortDateString() + " " + local.ToShortTimeString();
			}
			else return "Unknown";
		}

		public string GetLocalEndAtString()
		{
			if (End_at.HasValue)
			{
				DateTime local = (DateTime)End_at;
				local = local.ToLocalTime();
				return local.ToShortDateString() + " " + local.ToShortTimeString();
			}
			else return "Unknown";
		}
	}
}

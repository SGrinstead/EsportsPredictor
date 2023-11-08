﻿namespace EsportsPredictor.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public League League { get; set; }
        public string Name { get; set; }
        public DateTime Begin_at { get; set; } = new DateTime();
        public DateTime End_at { get; set; } = new DateTime();
        public string Tier { get; set; }
        public string Prizepool { get; set; }
        public Videogame Videogame { get; set; }
        public string WinnerType { get; set; }
        public int? WinnerId { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();
        public List<Match> Matches { get; set; } = new List<Match>();

        public string GetLocalBeginAtString()
        {
            DateTime local = Begin_at.ToLocalTime();
            return local.ToShortDateString() + " " + local.ToShortTimeString();
        }

		public string GetLocalEndAtString()
		{
			DateTime local = End_at.ToLocalTime();
			return local.ToShortDateString() + " " + local.ToShortTimeString();
		}
	}
}

namespace EsportsPredictor.Models
{
    public class Match
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public DateTime? Begin_at { get; set; }
        public DateTime? End_at { get; set; }
        public string Match_type { get; set; }
        public string Status { get; set; }
        public string Winner_type { get; set; }
        public int? Winner_id { get; set; }

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

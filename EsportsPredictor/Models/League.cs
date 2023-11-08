namespace EsportsPredictor.Models
{
	public class League
	{
        public int Id { get; set; }
        public string Slug { get; set; }
		public string Name { get; set; }
		public Uri ImageUrl { get; set; }
        public Uri Url { get; set; }
    }
}

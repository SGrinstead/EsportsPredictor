namespace EsportsPredictor.Models
{
	public class Prediction
	{
        public int Id { get; set; }
        public string UserId { get; set; }
        public Match Match { get; set; }
        public bool IsCompleted { get; set; }
        public Winner PredictedWinner { get; set; }
        public Winner? ActualWinner { get; set; }
    }
}

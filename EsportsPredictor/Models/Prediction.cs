namespace EsportsPredictor.Models
{
	public class Prediction
	{
        public int Id { get; set; }
        public Match Match { get; set; }
        public bool IsCompleted { get; set; }
        public int PredictedWinnerId { get; set; }
    }
}

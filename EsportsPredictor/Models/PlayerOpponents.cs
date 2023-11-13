using EsportsPredictor.Interfaces;

namespace EsportsPredictor.Models
{
	public class PlayerOpponents : IOpponents
	{
		public string Opponent_type { get; set; }
		public List<Player> Opponents { get; set; }
	}
}

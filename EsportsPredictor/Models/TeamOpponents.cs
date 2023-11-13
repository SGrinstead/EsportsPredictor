using EsportsPredictor.Interfaces;

namespace EsportsPredictor.Models
{
	public class TeamOpponents : IOpponents
	{
        public string Opponent_type { get; set; }
        public List<Team> Opponents { get; set; } = new List<Team>();
    }
}

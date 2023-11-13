namespace EsportsPredictor.Interfaces
{
	public interface IOpponents
	{
		string Opponent_type { get; set; }

		// When the api returns opponents for a match, it could either be a list of Teams or Players
		// this string will have a vale of "Team" or "Player"
		// the TeamOpponents and PlayerOpponents classes implement this interface
		// this interface can be used to represent both classes until you need specific information i.e. the matches show view
	}
}

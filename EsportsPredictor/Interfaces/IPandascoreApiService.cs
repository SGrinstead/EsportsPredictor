using EsportsPredictor.Models;

namespace EsportsPredictor.Interfaces
{
    public interface IPandascoreApiService
    {
        Task<List<Tournament>> GetUpcomingTournamentsAsync();
        Task<Team> GetTeamAsync(string teamSlug);
        Task<Player> GetPlayerAsync(string playerSlug);
        Task<List<Match>> GetMatchesAsync(string tournamentSlug);
        Task<Match> GetMatchAsync(string matchSlug);
        Task<IOpponents> GetOpponentsAsync(string matchSlug);
	}
}

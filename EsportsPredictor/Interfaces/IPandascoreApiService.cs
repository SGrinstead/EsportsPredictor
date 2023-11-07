using EsportsPredictor.Models;

namespace EsportsPredictor.Interfaces
{
    public interface IPandascoreApiService
    {
        Task<List<Tournament>> GetUpcomingTournamentsAsync();
        Task<Team> GetTeamAsync(string teamSlug);
    }
}

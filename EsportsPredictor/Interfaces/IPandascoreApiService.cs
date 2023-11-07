using EsportsPredictor.Models;

namespace EsportsPredictor.Interfaces
{
    public interface IPandascoreApiService
    {
        Task<List<Tournament>> GetTournamentsAsync();
    }
}

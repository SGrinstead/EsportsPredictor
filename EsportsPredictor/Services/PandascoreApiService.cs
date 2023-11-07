using EsportsPredictor.Interfaces;
using EsportsPredictor.Models;
using System.Text.Json;

namespace EsportsPredictor.Services
{
    public class PandascoreApiService : IPandascoreApiService
    {
        private readonly HttpClient client;
        private readonly IConfiguration _config;

        public PandascoreApiService(IConfiguration configuration)
        {
            client = new HttpClient() { BaseAddress = new Uri("https://api.pandascore.co") };
            _config = configuration;
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _config["PandascoreToken"]);
        }

        public async Task<List<Tournament>> GetUpcomingTournamentsAsync()
        {
            string url = "/tournaments/upcoming";
            var result = new List<Tournament>();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<List<Tournament>>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else throw new HttpRequestException(response.ReasonPhrase);

            return result;
        }

        public async Task<Team> GetTeamAsync(string teamSlug)
        {
            string url = $"/teams/{teamSlug}";
            var result = new Team();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<Team>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else throw new HttpRequestException(response.ReasonPhrase);

            return result;
        } 
    }
}

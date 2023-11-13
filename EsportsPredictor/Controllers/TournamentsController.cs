using EsportsPredictor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using EsportsPredictor.Models;

namespace EsportsPredictor.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly IPandascoreApiService _pandascoreApiService;

        public TournamentsController(IPandascoreApiService pandascoreApiService)
        {
            _pandascoreApiService = pandascoreApiService;
        }

        public async Task<IActionResult> Index()
        {
            List<Videogame> games = await _pandascoreApiService.GetVideogamesAsync();

            return View(games);
        }

        [Route("/tournaments/upcoming")]
        public async Task<IActionResult> Upcoming(string? game)
        {
            List<Tournament> tournaments = await _pandascoreApiService.GetUpcomingTournamentsAsync();
            if(game is not null)
            {
                tournaments = tournaments.Where(t => t.Videogame.Slug == game).ToList();
            }

            return View(tournaments);
        }
    }
}

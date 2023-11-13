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

        // a page that lets you select a game to see tournaments for, or just view all upcoming tournaments
        public async Task<IActionResult> Index()
        {
            // gets a list of all offered games to choose from in a dropdown menu in the view
            List<Videogame> games = await _pandascoreApiService.GetVideogamesAsync();

            return View(games);
        }

        // displays upcoming tournaments with start dates and links to matches
        [Route("/tournaments/upcoming")]
        public async Task<IActionResult> Upcoming(string? game)
        {
            // gets upcoming tournaments from the api
            List<Tournament> tournaments = await _pandascoreApiService.GetUpcomingTournamentsAsync();

            // if a game is specified, only show tournaments for that game
            if(game is not null)
            {
                tournaments = tournaments.Where(t => t.Videogame.Slug == game).ToList();
            }

            return View(tournaments);
        }
    }
}

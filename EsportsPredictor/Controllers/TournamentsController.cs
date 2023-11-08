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

        public IActionResult Index()
        {
            return View();
        }

        [Route("/tournaments/upcoming")]
        public async Task<IActionResult> Upcoming()
        {
            List<Tournament> tournaments = await _pandascoreApiService.GetUpcomingTournamentsAsync();

            return View(tournaments);
        }
    }
}

using EsportsPredictor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using EsportsPredictor.Models;

namespace EsportsPredictor.Controllers
{
	public class MatchesController : Controller
	{
		private readonly IPandascoreApiService _pandascoreApiService;

        public MatchesController(IPandascoreApiService pandascoreApiService)
        {
			_pandascoreApiService = pandascoreApiService;
        }

        [Route("/tournaments/{tournamentSlug}/matches")]
		public async Task<IActionResult> Index(string tournamentSlug)
		{
			List<Match> matches = await _pandascoreApiService.GetMatchesAsync(tournamentSlug);

			return View(matches);
		}

		[Route("/matches/{matchSlug}")]
		public async Task<IActionResult> Show(string matchSlug)
		{
			Match match = await _pandascoreApiService.GetMatchAsync(matchSlug);

			return View(match);
		}
	}
}

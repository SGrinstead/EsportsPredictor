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

		// lists the matches for a tournament with start dates and links to more info about each match
        [Route("/tournaments/{tournamentSlug}/matches")]
		public async Task<IActionResult> Index(string tournamentSlug)
		{
			// gets a list of matches for a tournament
			List<Match> matches = await _pandascoreApiService.GetMatchesAsync(tournamentSlug);

			return View(matches);
		}

		// shows details about a single match
		[Route("/matches/{matchSlug}")]
		public async Task<IActionResult> Show(string matchSlug)
		{
			// gets info about a single match
			Match match = await _pandascoreApiService.GetMatchAsync(matchSlug);

			// list of opponents for the match, could be either teams or players so I use the IOpponents interface
			// the view implements partial views based on the type of opponent
			ViewData["Opponents"] = await _pandascoreApiService.GetOpponentsAsync(matchSlug);

			// cookie used to return to the match show page from multiple places (teams show, players show, predictions)
			Response.Cookies.Append("RecentlyViewedMatch", matchSlug);

			return View(match);
		}
	}
}

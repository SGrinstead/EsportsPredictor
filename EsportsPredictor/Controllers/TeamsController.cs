using EsportsPredictor.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EsportsPredictor.Controllers
{
	public class TeamsController : Controller
	{
		private readonly IPandascoreApiService _pandascoreApiService;

        public TeamsController(IPandascoreApiService pandascoreApiService)
        {
			_pandascoreApiService = pandascoreApiService;
        }

		// displays information about a single team
		[Route("/teams/{teamSlug}")]
        public async Task<IActionResult> Show(string teamSlug)
		{
			// gets info about a single team
			var team = await _pandascoreApiService.GetTeamAsync(teamSlug);

			return View(team);
		}
	}
}

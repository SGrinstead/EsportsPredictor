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

		[Route("/teams/{teamSlug}")]
        public async Task<IActionResult> Show(string teamSlug)
		{
			var team = await _pandascoreApiService.GetTeamAsync(teamSlug);

			return View(team);
		}
	}
}

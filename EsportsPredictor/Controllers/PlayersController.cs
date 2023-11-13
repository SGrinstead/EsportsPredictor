using EsportsPredictor.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EsportsPredictor.Controllers
{
	public class PlayersController : Controller
	{
		private readonly IPandascoreApiService _pandascoreApiService;

        public PlayersController(IPandascoreApiService pandascoreApiService)
        {
			_pandascoreApiService = pandascoreApiService;
        }

		// displays information about a single player
		[Route("/players/{playerSlug}")]
        public async Task<IActionResult> Show(string playerSlug)
		{
			// gets information about a single player
			var player = await _pandascoreApiService.GetPlayerAsync(playerSlug);

			return View(player);
		}
	}
}

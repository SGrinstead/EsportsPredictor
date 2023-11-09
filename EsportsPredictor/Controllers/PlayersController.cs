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

		[Route("/players/{playerSlug}")]
        public async Task<IActionResult> Show(string playerSlug)
		{
			var player = await _pandascoreApiService.GetPlayerAsync(playerSlug);

			return View(player);
		}
	}
}

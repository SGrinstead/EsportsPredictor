using EsportsPredictor.DataAccess;
using EsportsPredictor.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EsportsPredictor.Controllers
{
	public class PredictionsController : Controller
	{
		private readonly IPandascoreApiService _pandascoreApiService;
		private readonly EsportsPredictorContext _context;

        public PredictionsController(IPandascoreApiService pandascoreApiService, EsportsPredictorContext context)
        {
			_pandascoreApiService = pandascoreApiService;
			_context = context;
        }

        public IActionResult Index()
		{
			return View();
		}
	}
}

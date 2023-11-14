using EsportsPredictor.DataAccess;
using EsportsPredictor.Interfaces;
using EsportsPredictor.Models;
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
			var predictions = _context.Predictions.ToList();
			CheckPredictions(predictions);

			return View(predictions);
		}

		private async void CheckPredictions(List<Prediction> predictions)
		{
			foreach(var prediction in predictions)
			{
				if (!prediction.IsCompleted)
				{
					if(DateTime.UtcNow > prediction.Match.Begin_at)
					{
						prediction.Match = await _pandascoreApiService.GetMatchAsync(prediction.Match.Slug);

						if(prediction.Match.Status == "finished")
						{
							prediction.IsCompleted = true;
						}
					}
					_context.Predictions.Update(prediction);
				}
			}
			_context.SaveChanges();
		}
	}
}

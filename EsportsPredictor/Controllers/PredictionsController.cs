using EsportsPredictor.DataAccess;
using EsportsPredictor.Interfaces;
using EsportsPredictor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Index()
		{
			var predictions = await GetUpdatedPredictions();

			return View(predictions);
		}

		[Route("/predictions/new/{matchSlug}")]
		public async Task<IActionResult> New(string matchSlug)
		{
			ViewData["Opponents"] = await _pandascoreApiService.GetOpponentsAsync(matchSlug);

			return View("New", matchSlug);
		}

		[HttpPost]
		public async Task<IActionResult> Create(string winnerSlug, string matchSlug)
		{
			Match match = await _pandascoreApiService.GetMatchAsync(matchSlug);
			Team team = await _pandascoreApiService.GetTeamAsync(winnerSlug);
			
			Winner predictedWinner = new Winner(team);
			Prediction prediction = new Prediction { IsCompleted = false, Match = match, PredictedWinner = predictedWinner };

			_context.Predictions.Add(prediction);
			_context.SaveChanges();

			return Redirect("/predictions");
		}

		//[HttpPost]
		//[Route("/predictions/delete/{predictionId:int}")]
		//public IActionResult Delete(int predictionId)
		//{
		//	var predictionToDelete = _context.Predictions
		//		.Where(p => p.Id == predictionId)
		//		.Include(p => p.PredictedWinner)
		//		.Include(p => p.ActualWinner)
		//		.First();

		//	_context.Predictions.Remove(predictionToDelete);
		//	_context.SaveChanges();

		//	return Redirect("/predictions");
		//}

		private async Task<List<Prediction>> GetUpdatedPredictions()
		{
			List<Prediction> predictions = _context.Predictions.AsNoTracking()
				.Include(p => p.Match)
				.Include(p => p.PredictedWinner)
				.Include(p => p.ActualWinner)
				.ToList();

			foreach (var prediction in predictions)
			{
				if (!prediction.IsCompleted)
				{
					if(DateTime.UtcNow > prediction.Match.Begin_at)
					{
						prediction.Match = await _pandascoreApiService.GetMatchAsync(prediction.Match.Slug);
						_context.Predictions.Update(prediction);
				
						if(prediction.Match.Status == "finished")
						{
							prediction.IsCompleted = true;
						}
						_context.SaveChanges();
					}
				}
			}
			return predictions;
		}
	}
}

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
			await UpdatePredictions();
			var predictions = _context.Predictions
				.Include(p => p.Match)
				.Include(p => p.PredictedWinner)
				.Include(p => p.ActualWinner)
				.ToList();

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
			Match match;
			Winner predictedWinner;

			if (_context.Matches.Any(m => m.Slug == matchSlug))
			{
				match = _context.Matches.Where(m => m.Slug == matchSlug).First();
			}
			else
			{
				match = await _pandascoreApiService.GetMatchAsync(matchSlug);
			}

			if (_context.Winners.Any(w => w.Slug == winnerSlug))
			{
				predictedWinner = _context.Winners.Where(w => w.Slug == winnerSlug).First();
			}
			else
			{
				predictedWinner = new Winner(await _pandascoreApiService.GetTeamAsync(winnerSlug));
			}
			
			Prediction prediction = new Prediction { IsCompleted = false, Match = match, PredictedWinner = predictedWinner };

			_context.Predictions.Add(prediction);
			_context.SaveChanges();

			return Redirect("/predictions");
		}

		[HttpPost]
		[Route("/predictions/delete/{predictionId:int}")]
		public IActionResult Delete(int predictionId)
		{
			var predictionToDelete = _context.Predictions
				.Where(p => p.Id == predictionId)
				.First();

			_context.Predictions.Remove(predictionToDelete);
			_context.SaveChanges();

			return Redirect("/predictions");
		}

		private async Task UpdatePredictions()
		{
			await UpdateMatches();

			List<Prediction> predictions = _context.Predictions
				.Include(p => p.Match)
				.Include(p => p.PredictedWinner)
				.Include(p => p.ActualWinner)
				.ToList();

			foreach (var prediction in predictions)
			{
				if (prediction.Match.Winner_id is null)
				{
					prediction.IsCompleted = false;
				}
				if (!prediction.IsCompleted)
				{
					if (prediction.Match.Winner_id.HasValue)
					{
						int winnerId = (int)prediction.Match.Winner_id;
						if (_context.Winners.Any(w => w.Id == winnerId))
						{
							prediction.ActualWinner = _context.Winners.Find(winnerId);
						}
						else
						{
							Winner newWinner = await _pandascoreApiService.GetWinnerAsync(winnerId);
							_context.Winners.Add(newWinner);
							_context.SaveChanges();

							prediction.ActualWinner = _context.Winners.Find(newWinner.Id);
						}
						prediction.IsCompleted = true;
					}
				}
				_context.Predictions.Update(prediction);
				_context.SaveChanges();		
			}
		}

		private async Task UpdateMatches()
		{
			var matches = _context.Matches.AsNoTracking().ToList();
			foreach(var match in matches)
			{
				if(match.Winner_id is null && DateTime.UtcNow > match.Begin_at)
				{
					var updatedMatch = await _pandascoreApiService.GetMatchAsync(match.Slug);
					_context.Matches.Update(updatedMatch);
				}
			}
			_context.SaveChanges();
		}
	}
}

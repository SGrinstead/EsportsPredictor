using EsportsPredictor.DataAccess;
using EsportsPredictor.Interfaces;
using EsportsPredictor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EsportsPredictor.Controllers
{
	[Authorize]
	public class PredictionsController : Controller
	{
		private readonly IPandascoreApiService _pandascoreApiService;
		private readonly EsportsPredictorContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

        public PredictionsController(IPandascoreApiService pandascoreApiService, EsportsPredictorContext context, UserManager<ApplicationUser> userManager)
        {
			_pandascoreApiService = pandascoreApiService;
			_context = context;
			_userManager = userManager;
        }

		[Route("/{userId}/predictions")]
        public async Task<IActionResult> Index(string? userId)
		{
			await UpdatePredictions();

			var predictions = _context.Predictions
				.Where(p => p.UserId == userId)
				.Include(p => p.Match)
				.Include(p => p.PredictedWinner)
				.Include(p => p.ActualWinner)
				.ToList();

			return View(predictions);
		}

		public IActionResult RedirectToIndex()
		{
			string userId = _userManager.GetUserId(User);

			return Redirect($"/{userId}/predictions");
		}

		[Route("/predictions/new/{matchSlug}")]
		public async Task<IActionResult> New(string matchSlug)
		{
			ViewData["Opponents"] = await _pandascoreApiService.GetOpponentsAsync(matchSlug);

			return View("New", matchSlug);
		}

		[HttpPost]
		[Route("/{userId}/predictions/create")]
		public async Task<IActionResult> Create(string winnerSlug, string matchSlug, string userId)
		{
			if(_userManager.GetUserId(User) != userId)
			{
				return BadRequest();
			}

			Match match;
			Winner predictedWinner;

			var user = _context.Users
				.Where(user => user.Id == userId)
				.Include(user => user.Predictions)
				.First();

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
			
			Prediction prediction = new Prediction { IsCompleted = false, Match = match, PredictedWinner = predictedWinner, UserId = userId };
			
			user.Predictions.Add(prediction);
			_context.Predictions.Add(prediction);
			_context.Users.Update(user);
			_context.SaveChanges();

			return Redirect($"/{userId}/predictions");
		}

		[HttpPost]
		[Route("/{userId}/predictions/delete/{predictionId:int}")]
		public IActionResult Delete(string userId, int predictionId)
		{
            if (_userManager.GetUserId(User) != userId)
            {
                return BadRequest();
            }

            var predictionToDelete = _context.Predictions
				.Where(p => p.Id == predictionId)
				.First();

			if(_userManager.GetUserId(User) != predictionToDelete.UserId)
			{
				return BadRequest();
			}

			_context.Predictions.Remove(predictionToDelete);
			_context.SaveChanges();

			return Redirect($"/{userId}/predictions");
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

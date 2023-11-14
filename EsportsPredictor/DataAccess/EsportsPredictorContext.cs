using Microsoft.EntityFrameworkCore;
using EsportsPredictor.Models;

namespace EsportsPredictor.DataAccess
{
	public class EsportsPredictorContext : DbContext
	{
		public DbSet<Prediction> Predictions { get; set; }

		public EsportsPredictorContext(DbContextOptions<EsportsPredictorContext> options) 
			: base(options) { }
	}
}

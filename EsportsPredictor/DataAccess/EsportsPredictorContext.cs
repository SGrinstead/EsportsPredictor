using Microsoft.EntityFrameworkCore;
using EsportsPredictor.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EsportsPredictor.DataAccess
{
	public class EsportsPredictorContext : IdentityDbContext<ApplicationUser>
	{
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Prediction> Predictions { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Winner> Winners { get; set; }

		public EsportsPredictorContext(DbContextOptions<EsportsPredictorContext> options) 
			: base(options) { }
	}
}

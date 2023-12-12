using Microsoft.AspNetCore.Identity;

namespace EsportsPredictor.Models
{
	public class ApplicationUser : IdentityUser
	{
        public List<Prediction> Predictions { get; set; }
    }
}

using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IdentityProvider.Services
{
	public class BiProfileService : IProfileService
	{
		private readonly UserManager<IdentityUser> _userManager;

		public BiProfileService(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}

		public Task GetProfileDataAsync(ProfileDataRequestContext context)
		{
			context.IssuedClaims.AddRange(context.Subject.Claims);
			return Task.FromResult(0);
		}

		public Task IsActiveAsync(IsActiveContext context)
		{
			return Task.FromResult(0);
		}
	}
}

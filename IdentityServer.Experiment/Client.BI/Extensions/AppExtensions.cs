using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Client.BI.Extensions
{
	public static class AppExtensions
	{
		public static void AddIdentityServices(this IServiceCollection services, IConfiguration config)
		{
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = config["IdentityProvider:Authority"];

                options.ClientId = "BI_Client";
                options.ClientSecret = "Pass123??";
                options.ResponseType = "code";
                options.UsePkce = true;
                options.ResponseMode = "form_post";
                options.RequireHttpsMetadata = true;

                options.ClaimActions.MapUniqueJsonKey("Customer", "Customer");
                options.ClaimActions.MapUniqueJsonKey("BI_Role", "BI_Role");

                options.TokenValidationParameters.RoleClaimType = "BI_Role";

                options.CallbackPath = "/signin-oidc";
                options.GetClaimsFromUserInfoEndpoint = true;
                options.Scope.Add("api.legacy");
                options.Scope.Add("api.legacy.read");
                options.Scope.Add("api.legacy.write");
                options.Scope.Add("offline_access");
				options.Scope.Add("email");
				options.Scope.Add("profile");
				options.Scope.Add("Customer");
				options.Scope.Add("BI_Role");

				options.SaveTokens = true;
            });
        }
	}
}

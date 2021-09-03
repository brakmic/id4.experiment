using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

			// any API access must contain a valid auth token
			services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
			.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
						 {
							 options.Authority = Configuration["IdentityProvider:Authority"];
                             options.RequireHttpsMetadata = true;
							 options.TokenValidationParameters = new TokenValidationParameters
							 {
								 ValidateAudience = false,
                                 ValidTypes = new[] { "at+jwt" }
							 };
						 });

			// adds an authorization policy to make sure the token is for scope 'api'
			services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiLegacyPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Customer");
                    policy.RequireClaim("BI_role", new string[] { "Enterprise", "Essentials", "Reviewer", "Reader" });
                });
                options.AddPolicy("RemoteClientsPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute().RequireAuthorization());
        }
    }
}

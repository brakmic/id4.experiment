using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityProvider.Configuration
{
    internal class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "remoteClient",
                    ClientName = "Non-Interactive Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {
                        new Secret("Pass123??".Sha256())
                    },
                    AllowedScopes = new List<string> {
                        "api",
                        "api.read", 
                        "api.write"
                    },
                },
                new Client
                {
                    ClientId = "BI_Client",
                    ClientName = "BI Client",
                    ClientSecrets = new List<Secret> {
                        new Secret("Pass123??".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:5012/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5012/signout-callback-oidc" },
                    AllowedCorsOrigins = { "http://localhost:5012" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.Email,
						"Customer",
                        "BI_Role",
                        "api.legacy",
                        "api.legacy.read",
                        "api.legacy.write"
                    },
                    RequirePkce = true,
                    AllowPlainTextPkce = false,
                    AllowOfflineAccess = true,
                    RequireConsent = true
                }
            };
        }
    }

    internal class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "BI_Role",
                    UserClaims = new List<string> {"BI_Role"},
                    Required = false
                },
                new IdentityResource
                {
                    Name = "Customer",
                    UserClaims = new List<string> {"Customer"},
                    Required = false
                },
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource
                {
                    Name = "api",
                    DisplayName = "PatentSight API",
                    Description = "Allows accessing the PatentSight API on your behalf",
                    Scopes = new List<string> {
                        "api",
                        "api.read",
                        "api.write"
                    },
                    ApiSecrets = new List<Secret> {
                        new Secret("Pass123??".Sha256())
                    },
                },
                // Legacy API
                new ApiResource
                {
                    Name = "api.legacy",
                    DisplayName = "PatentSight API - Legacy",
                    Description = "Allows accessing the Legacy PatentSight API on your behalf",
                    Scopes = new List<string> {
                        "api.legacy",
                        "api.legacy.read",
                        "api.legacy.write"
                    },
                    ApiSecrets = new List<Secret> {
                        new Secret("Pass123??".Sha256())
                    },
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("api", "Full Access to the PatentSight API"),
                new ApiScope("api.read", "Read Access to the PatentSight API"),
                new ApiScope("api.write", "Write Access to the PatentSight API"),
                // legacy API (what we are calling 'webapi')
                new ApiScope("api.legacy", "Full Access to the Legacy PatentSight API"),
                new ApiScope("api.legacy.read", "Read Access to the Legacy PatentSight API"),
                new ApiScope("api.legacy.write", "Write Access to the Legacy PatentSight API")
            };
        }
    }

    internal class Users
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "92b37c24-8cae-473a-9228-9b1ec50915f6",
                    Username = "henkeladmin",
                    Password = "Pass123??",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "admin@henkel.com"),
                        new Claim(JwtClaimTypes.Role, "Enterprise"),
                        new Claim(JwtClaimTypes.Name, "Jane Doe"),
                        new Claim(JwtClaimTypes.Audience, "Henkel-Clients")
                    }
                },
                new TestUser
                {
                    SubjectId = "b8a0229b-052e-418b-8ecb-65436d24bd54",
                    Username = "henkeluser",
                    Password = "Pass123??",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "user@henkel.com"),
                        new Claim(JwtClaimTypes.Role, "user"),
                        new Claim(JwtClaimTypes.Name, "John Doe"),
                        new Claim(JwtClaimTypes.Audience, "Henkel-Clients")
                    }
                },
                // demo users representing various BI roles 
                new TestUser
                {
                    SubjectId = "490cd152-5e9c-4f8b-bcd4-cee16393e418",
                    Username = "user_enterprise",
                    Password = "Pass123??",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "max.mustermann@henkel.com"),
                        new Claim("Customer", "Henkel"),
                        new Claim("BI_Role", "Enterprise"),
                        new Claim(JwtClaimTypes.Name, "Max Mustermann"),
                        new Claim(JwtClaimTypes.Audience, "PatentSight-Customers")
                    }
                },
                new TestUser
                {
                    SubjectId = "c27f1dee-3bfb-4f22-a3db-abe74bc58ef8",
                    Username = "user_essentials",
                    Password = "Pass123??",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "j.jacobs@basf.com"),
                        new Claim("Customer", "BASF"),
                        new Claim("BI_Role", "Essentials"),
                        new Claim(JwtClaimTypes.Name, "Johnny Jacobs"),
                        new Claim(JwtClaimTypes.Audience, "PatentSight-Customers")
                    }
                },
                new TestUser
                {
                    SubjectId = "a6d3be2b-ec56-4671-a43e-f02e2f019316",
                    Username = "user_reviewer",
                    Password = "Pass123??",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "j.smith@bosch.com"),
                        new Claim("Customer", "Bosch"),
                        new Claim("BI_Role", "Reviewer"),
                        new Claim(JwtClaimTypes.Name, "Joanna Smith"),
                        new Claim(JwtClaimTypes.Audience, "PatentSight-Customers")
                    }
                },
                new TestUser
                {
                    SubjectId = "a5224b2f-bd26-4a25-bf89-d700723bd6c3",
                    Username = "user_reader",
                    Password = "Pass123??",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "b.jacobs@bmw.com"),
                        new Claim("Customer", "BMW"),
                        new Claim("BI_Role", "Reader"),
                        new Claim(JwtClaimTypes.Name, "Betty Jacobs"),
                        new Claim(JwtClaimTypes.Audience, "PatentSight-Customers")
                    }
                }
            };
        }
    }
}

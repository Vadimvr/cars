using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace identity_server.Services
{
    public class IS4
    {
        internal static void AddIS(IServiceCollection services)
        {
            services.AddIdentityServer()
              .AddAspNetIdentity<IdentityUser>()
              .AddInMemoryApiResources(Configuration.ApiResources)
              .AddInMemoryIdentityResources(Configuration.IdentityResources)
              .AddInMemoryApiScopes(Configuration.ApiScopes)
              .AddInMemoryClients(Configuration.Clients)
              .AddProfileService<ProfileService>()

              .AddDeveloperSigningCredential();
        }
    }

    internal class Configuration
    {
        public static List<TestUser> TestUser =>
            new List<TestUser> {
                new TestUser()
                {
                    Username = "user",
                    Password = "user",
                    Claims = new List<Claim>
                    {
                         new Claim(ClaimTypes.Role,"User")
                    }
                },
                new TestUser()
                {
                    Username = "admin",
                    Password = "admin",
                    Claims = new List<Claim>
                    {
                         new Claim(ClaimTypes.Role,"Admin")
                    }
                }
            };

        public static IEnumerable<ApiResource> ApiResources => 
            new List<ApiResource>()
            {
                new ApiResource("CarsAPI", "Cars Api + Swagger", new []{ JwtClaimTypes.Name})
                {
                    Scopes = { "CarsAPI" },
                }
            };


        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("CarsAPI"),
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>()
            {
                new Client()
                {
                    ClientId = "client_id_cars_api",
                    ClientSecrets = {new Secret("client_id_cars_api".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,


                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "CarsAPI",
                    },
                    AllowedCorsOrigins = { "https://localhost:5050" }                    
                },
            };
    }
}

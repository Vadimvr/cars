using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

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
                         new Claim(ClaimTypes.DateOfBirth,"01.01.2002"),
                         new Claim(ClaimTypes.Role,"User")
                    }
                },
                new TestUser()
                {
                    Username = "admin",
                    Password = "admin",
                    Claims = new List<Claim>
                    {
                         new Claim(ClaimTypes.DateOfBirth,"01.01.2005"),
                         new Claim(ClaimTypes.Role,"Admin")
                    }
                }
            };


        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>()
        {
            new ApiResource("apiServer", "Web API", new []{ JwtClaimTypes.Name})
            {
                Scopes = { "apiServer" },
            },
            new ApiResource("CarsAPI", "Cars Api Swagger", new []{ JwtClaimTypes.Name})
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
                new ApiScope("apiServer"),
                new ApiScope("CarsAPI"),
                //    new ApiScope(name: "mvc.read", displayName: "Read Access to mvc"),
                //    new ApiScope(name: "postman.read", displayName: "Read Access to postman"),
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
                    //RedirectUris =  {"https://localhost:5010/signin-oidc"},
                    //PostLogoutRedirectUris = {"https://localhost:5010/signout-callback-oidc" },
                    ////redirects to information with permissions
                    //RequireConsent = false,
                    //AllowOfflineAccess = true,
                    //// claim management
                    //// AlwaysIncludeUserClaimsInIdToken = true,
                    //AccessTokenLifetime =5,
                    ////AbsoluteRefreshTokenLifetime = 30,
                    
                },
                new Client()
                {
                    ClientId = "client_id_mvc",
                    ClientSecrets = {new Secret("client_id_mvc".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.Code,

                    AllowedScopes =
                     {
                         IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                        // IdentityServerConstants.StandardScopes.Email,
                        // IdentityServerConstants.StandardScopes.OfflineAccess,
                         "apiServer",
                        // "ClientMvc"
                     },
                    RedirectUris =  {"https://localhost:5010/signin-oidc"},
                    PostLogoutRedirectUris = {"https://localhost:5010/signout-callback-oidc" },
                    //redirects to information with permissions
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    // claim management
                    // AlwaysIncludeUserClaimsInIdToken = true,
                    AccessTokenLifetime =5,
                    //AbsoluteRefreshTokenLifetime = 30,
                    

                }
            };
    }
}

using cars_api.Db;
using IdentityServer4.AccessTokenValidation;
using Microsoft.OpenApi.Models;
using Npgsql;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Data;
using System.Reflection;

namespace cars_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddTransient<IDbConnection>((sp) => new NpgsqlConnection(builder.Configuration.GetConnectionString("NpgConnectionStrings")));
            builder.Services.AddSingleton<IDbInitializer, DbInitializer>();
            builder.Services.AddSingleton<ICarRepository, CarRepositoryDrapper>();


            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Description = "Cars API v1",
                    Version = "1.0.0",
                });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("https://localhost:5001/connect/token"),
                            Scopes = new Dictionary<string, string>()
                            {
                                {"CarsAPI", "Cars api" }
                            }
                        }
                    }
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,


                        },
                        new List<string>()
                    }
                });
            });




            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;

                })
                .AddIdentityServerAuthentication(options =>
                {
                    options.ApiName = "CarsAPI";
                    options.Authority = "https://localhost:5001";
                    options.RequireHttpsMetadata = false;

                });
            builder.Services.AddAuthorization();

            builder.Services.AddControllers();

            var app = builder.Build();

            app.Services.GetService<IDbInitializer>()?.Initialize();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Cars swagger UI");
                options.DocumentTitle = "Cars API";
                options.RoutePrefix = "";
                options.DocExpansion(DocExpansion.List);
                options.OAuthClientId("client_id_cars_api");
                options.OAuthScopeSeparator(" ");
                options.OAuthClientSecret("client_id_cars_api");
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
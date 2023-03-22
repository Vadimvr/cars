using identity_server.Data;
using identity_server.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace identity_server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();

            builder.Services.AddDbContext<ApplicationDbContext>(config =>
            {
                config.UseInMemoryDatabase("Memory");
            });

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireUppercase = false;
                config.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            IS4.AddIS(builder.Services);



            builder.Services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            var app = builder.Build();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "Styles")),
                RequestPath = "/styles"
            });

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();


            using (var scope = app.Services.CreateScope())
            {
                DatabaseInitializer.Init(scope.ServiceProvider);
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{controller=Site}/{action=Index}/{id?}");
            });


            app.Run();
        }
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using BlogApp.Data;
using BlogApp.Services;

namespace BlogApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure DbContext with connection string from appsettings.json
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            // Add the PostManager and CommentManager classes to the DI container
            services.AddScoped<PostManager>();
            services.AddScoped<CommentManager>();

            // Add other services as needed (e.g., authentication, authorization, etc.)
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Add production error handling middleware
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Configure static files, routing, and endpoints
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Additional endpoint for comment-related actions
                endpoints.MapControllerRoute(
                    name: "comment",
                    pattern: "Home/{action=Index}/{id?}");
            });
        }
    }
}

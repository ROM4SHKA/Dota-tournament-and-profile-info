using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using KursachV2.Services.TourService;
using KursachV2.Services.TeamsService;
using KursachV2.Services.PlayerService;
using KursachV2.Services.Validator;
using Microsoft.Extensions.Hosting;
using KursachV2.Models;

namespace KursachV2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)           
        {
            services.AddTransient<IUserValidator<User>, MyUserValidator>();
            services.AddDbContext<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>(op=> {
                op.User.RequireUniqueEmail = true;
                op.Password.RequiredLength = 4;  
                op.Password.RequireNonAlphanumeric = false;  
                op.Password.RequireLowercase = false; 
                op.Password.RequireUppercase = false; 
                op.Password.RequireDigit = true; 
            }).
                AddEntityFrameworkStores<MyContext>().AddDefaultTokenProviders();
            services.AddTransient<ITournament, GetTourInfo>();
            services.AddTransient<ITeam, GetTeamInfo>();
            services.AddTransient<IPlayer, DotaProfileInfo>();
            services.AddControllersWithViews();
          
        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}

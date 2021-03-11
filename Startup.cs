using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nile.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Nile
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<NileDbContext>(options =>
           {
               options.UseSqlite(Configuration["ConnectionStrings:NileConnection"]);
           });
            
            services.AddScoped<IBookRepository, EFBookRepository>();

            services.AddRazorPages();

            // Enable "sessions to be stored in cache: 
            services.AddDistributedMemoryCache();
            services.AddSession();
            
            // freeman ch. 9 -- storage aware cart
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // endpoint 1: type in category name & a page number
                endpoints.MapControllerRoute("catpage",
                    "{category}/{pageNum:int}",
                    new { Controller = "Home", action = "Index" });

                // endpoint 2: type in a page only
                endpoints.MapControllerRoute("page",
                    "{pageNum:int}",
                    new { Controller = "Home", action = "Index" });

                // endpoint 3: type in a category only
                endpoints.MapControllerRoute("category",
                    "{category}",
                    new { Controller = "Home", action = "Index", pageNum = 1 }); // page = 1 sets default to page 1


                endpoints.MapControllerRoute(
                "pagination",
                "P/{pageNum}",
                new { Controller = "Home", action = "Index" });

                endpoints.MapDefaultControllerRoute();
                //name: "default",
                //pattern: "{controller=Home}/{action=Index}/{id?}");

                // Razor Page endpoints
                endpoints.MapRazorPages();

            });

            

            // use the line below to make sure the database is populated w/ Seed Data:
            SeedData.EnsurePopulated(app);
        }
    }
}

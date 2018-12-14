﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Softuni.Community.Data;
using Softuni.Community.Data.Models;
using Softuni.Community.Services;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Common;
namespace Softuni.Community.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Authorization
            services.AddAuthorization();

            // Database
            services.AddDbContext<SuCDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration["Data:ConnectionString"]);
            });
            services.AddIdentity<CustomUser, IdentityRole>(
                    opts =>
                    {
                        opts.Password.RequiredLength = 4;
                        opts.Password.RequireNonAlphanumeric = false;
                        opts.Password.RequireLowercase = false;
                        opts.Password.RequireUppercase = false;
                        opts.Password.RequireDigit = false;

                        opts.User.RequireUniqueEmail = true;
                        opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    })
                .AddEntityFrameworkStores<SuCDbContext>()
                .AddDefaultTokenProviders();

            // Adding Services
            services.AddScoped<IDataService, DataService>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            // Disabled Scope Validation for easy role adding
            // Adding Admin Role
            CreateRole(app.ApplicationServices, Configuration, Role.Admin).Wait();
            // Adding User Role
            CreateRole(app.ApplicationServices, Configuration, Role.User).Wait();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public static async Task CreateRole(IServiceProvider serviceProvider,
            IConfiguration configuration, string roleName)
        {
            var roleManager =
                serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
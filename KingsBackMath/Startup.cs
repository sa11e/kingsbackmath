using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingsBackMath.Data;
using KingsBackMath.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using KingsBackMath.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace KingsBackMath
{
    public class Startup
    {
        private readonly IConfiguration config;
        private readonly IHostingEnvironment environment;

        public Startup(IConfiguration config, IHostingEnvironment environment)
        {
            this.config = config;
            this.environment = environment;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<GameUser, IdentityRole>(cfg =>
                {
                    cfg.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<KingsBackMathContext>();

            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = config["Tokens:Issuer"],
                        ValidAudience = config["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]))
                    };
                });

            services.AddDbContext<KingsBackMathContext>(cfg =>
            {
                cfg.UseSqlServer(config.GetConnectionString("KingsBackMathConnectionString"));
            });
            services.AddAutoMapper();
            services.AddTransient<IMailService, NullMailService>();
            services.AddTransient<KingsBackMathSeeder>();
            services.AddScoped<IKingsBackMathRepository, KingsBackMathRepository>();
            services.AddMvc(cfg =>
            {
                if (environment.IsProduction())
                {
                    cfg.Filters.Add(new RequireHttpsAttribute());
                }
            });
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
                app.UseExceptionHandler("/error");
            }

           app.UseStaticFiles();

            app.UseAuthentication();
           
            app.UseMvc(cfg =>
            {
                cfg.MapRoute("Default",
                    "{controller}/{action}/{id?}",
                    new {controller = "App", Action = "Index"});
            });

            if (env.IsDevelopment())
            {
                // Seed the database
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<KingsBackMathSeeder>();
                    seeder.Seed().Wait();
                }
            }
        }
    }
}

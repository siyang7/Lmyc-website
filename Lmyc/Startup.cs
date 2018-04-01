using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lmyc.Data;
using Lmyc.Models;
using Lmyc.Services;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using AspNet.Security.OpenIdConnect.Primitives;

namespace Lmyc
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
            services.AddMvc();

            // Online test database
            //services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("DevelopmentConnection"));

            //    // Register the entity sets needed by OpenIddict.
            //    // Note: use the generic overload if you need
            //    // to replace the default OpenIddict entities.
            //    options.UseOpenIddict();
            //});

            // Local Database
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Lmyc"));

                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities.
                options.UseOpenIddict();
            });


            // Docker Database
            //var host = Configuration["DBHOST"] ?? "localhost";
            //var port = Configuration["DBPORT"] ?? "3306";
            //var password = Configuration["DBPASSWORD"] ?? "secret";

            //services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseMySql($"server={host}; userid=root; pwd={password};"
            //        + $"port={port}; database=lmyc");
            //    options.UseOpenIddict();
            //});

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            // Register the OAuth2 validation handler.
            services.AddAuthentication()
                .AddOAuthValidation();

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            // Register the OpenIddict services.
            // Note: use the generic overload if you need
            // to replace the default OpenIddict entities.
            services.AddOpenIddict(options =>
            {
                // Register the Entity Framework stores.
                options.AddEntityFrameworkCoreStores<ApplicationDbContext>();

                // Register the ASP.NET Core MVC binder used by OpenIddict.
                // Note: if you don't call this method, you won't be able to
                // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                options.AddMvcBinders();

                // Enable the token endpoint (required to use the password flow).
                options.EnableTokenEndpoint("/connect/token");

                // Allow client applications to use the grant_type=password flow.
                options.AllowPasswordFlow();

                // During development, you can disable the HTTPS requirement.
                options.DisableHttpsRequirement();
            });

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "LYMC API",
                    Version = "v1.0",
                    Description = "An API for to provide necessary business functionality for the LMYC"
                });

                // Set the comments path for the Swagger JSON and UI.
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "LmycApi.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LMYC API V1.0");
            });
        }
    }
}

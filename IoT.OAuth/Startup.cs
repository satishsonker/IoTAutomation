using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.OAuth
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
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = "ClientCookie";
                config.DefaultSignInScheme = "ClientCookie";
                config.DefaultChallengeScheme = "OurServer";
            })
                .AddCookie("ClientCookie")
                .AddOAuth("OurServer", config =>
             {
                 config.CallbackPath = "/oauth/token";
                 config.ClientId = "client_Id";
                 config.ClientSecret = "client_secret";
                 config.AuthorizationEndpoint = "https://localhost:44342/oauth/authorize";
                 config.TokenEndpoint = "https://localhost:44342/oauth/token";
             }); 
            services.AddAuthentication("OAuth").AddJwtBearer("OAuth", config => {
                 config.TokenValidationParameters = new TokenValidationParameters()
                 {
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["OAuth.Secret"])),
                     ValidIssuer = Configuration["OAuth.Issuer"],
                     ValidAudience = Configuration["OAuth.Audiance"]
                 };
                 config.Events = new JwtBearerEvents()
                 {
                     OnMessageReceived = context =>
                     {
                         if (context.Request.Query.ContainsKey("access_token"))
                         {
                             context.Token = context.Request.Query["access_token"];
                         }
                         return Task.CompletedTask;
                     }
                 };
             });
            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

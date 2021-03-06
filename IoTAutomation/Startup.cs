using IoT.DataLayer;
using IoT.DataLayer.Interface;
using IoT.DataLayer.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using IoT.BusinessLayer;
using IoT.ModelLayer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;

namespace IoT.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

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
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IotDbConnectionString"));

            });
            services.AddCors();
            services.AddScoped<IUsers, UserRepository>();
            services.AddScoped<IRooms, RoomRepository>();
            services.AddScoped<IDevices, DeviceRepository>();
            services.AddScoped<IDashboard, DashBoardRepository>();
            services.AddScoped<IScenes, SceneRepository>();
            services.AddScoped<IActivityLogs, ActivityLogRepository>();
            services.AddScoped<IAdmin, AdminRepository>();
            services.AddScoped<IAlexaPayload, AlexaPayloadRepository>();
            services.AddScoped<IMasterData, MasterDataRepository>();
            services.AddScoped<IAlexaEventSource, AlexaEventSourceRepository>();
            services.AddScoped<IMqtt, MqttRepository>();
            services.AddScoped<IDeviceGroup, DeviceGroupRepository>();
            services.AddTransient<IEmailTemplate, EmailTemplateRepository>();
            services.AddTransient<IEmailSetting, EmailSettingRepository>();
            services.Configure<AppSettingConfig>(option => Configuration.GetSection("AppConfig").Bind(option));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UnitTest_Mock", Version = "v1" });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            // In production, the React files will be served from this directory

            var serviceProvider = services.BuildServiceProvider(); // Resolving Service
            var mqttRepository = serviceProvider.GetService<IMqtt>();
            var AlexaEventRepository = serviceProvider.GetService<IAlexaEventSource>();
            var iDevice = serviceProvider.GetService<IDevices>();
            var config = serviceProvider.GetService<IOptions<AppSettingConfig>>();
            var mqtt = new Mqtt(mqttRepository, AlexaEventRepository, config, iDevice);
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddFile("Logs/mylog-{Date}.txt");
            app.UseCors(builder => builder
                         .AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader());
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UnitTest_Mock v1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(
            endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=home}/{action=secret}");
            });
        }
    }
}

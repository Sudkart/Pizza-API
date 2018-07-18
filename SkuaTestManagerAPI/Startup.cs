using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.HttpOverrides;
using STM.Core.Repositories;
using STMAPI.Helper;
using STMAPI.JWT;
using System;
using System.IO;
using System.Text;

namespace SkuaTestManagerAPI
{
    public class Startup
    {
        private const string SecretKey = "needtogetthisfromenvironment";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
          .AddJsonFile("appSettings.json");
            Configuration = builder.Build();
            Console.WriteLine(Configuration.GetConnectionString("AppSettings"));

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			

			services.AddMvc();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IScenarioRepository, ScenarioRepository>();
            services.AddScoped<ISubScenarioRepository, SubScenarioRepository>();
            services.AddScoped<ITestStepRepository, TestStepRepository>();
            services.AddScoped<ISuiteRepository, SuiteRepository>();
            services.AddScoped<IObjectRepository, ObjectRepository>();
            services.AddScoped<INodeRepository, NodeRepository>();
            services.AddScoped<IUrlRepository, UrlRepository>();
            services.AddScoped<IProjKeyValueRepository, ProjKeyValueRepository>();
            services.AddScoped<IResultsRepository, ResultsRepository>();
            services.AddScoped<IPackResultsRepository, PackResultRepository>();
            services.AddScoped<IRunSuiteRepository, RunSuiteRepository>();
            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<IActionGroupRepository, ActionGroupRepository>();
            services.AddScoped<IActionRepository, ActionRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserService, UserService>();

            services.AddCors();
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAnyOrigin",
            //       builder => builder.AllowAnyOrigin()
            //                    .AllowAnyMethod()
            //                    .AllowAnyHeader()
            //                    .AllowCredentials());
            //});

            // Add framework services.
            // Make authentication compulsory across the board (i.e. shut
            // down EVERYTHING unless explicitly opened up).
            //services.AddMvc(config =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //                     .RequireAuthenticatedUser()
            //                     .Build();
            //    config.Filters.Add(new AuthorizeFilter(policy));
            //});

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                o.TokenValidationParameters = tokenValidationParameters;
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("RoleId", "1"));
                options.AddPolicy("UserOnly", policy => policy.RequireClaim("RoleId", "2"));
                options.AddPolicy("AllUsers", policy => policy.RequireClaim("RoleId", "1", "2"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseCors("AllowAnyOrigin");

            app.UseAuthentication();

            //app.UseJwtBearerAuthentication(new JwtBearerOptions
            //{
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true,
            //    TokenValidationParameters = tokenValidationParameters
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(
              options => options.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
            );
            app.Use(async (context, next) => { await next(); if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value) && !context.Request.Path.Value.StartsWith("/api")) { context.Request.Path = "/index.html"; context.Response.StatusCode = 200; await next(); } });
			app.UseMvc();
			app.UseDefaultFiles();
			app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

        }
    }
}

using System;
using System.Threading.Tasks;
using GoodsReseller.Api.Middlewares;
using GoodsReseller.AuthContext.Handlers;
using GoodsReseller.Configurations;
using GoodsReseller.DataCatalogContext.Handlers;
using GoodsReseller.Infrastructure;
using GoodsReseller.NotificationContext;
using GoodsReseller.OrderContext.Handlers;
using GoodsReseller.SupplyContext.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GoodsReseller.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;
        
        // TODO: extract to config
        private static readonly string SiteDNS = "http://happyboxy.ru";

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services
                .Configure<TelegramApiOptions>(_configuration.GetSection(nameof(TelegramApiOptions)));
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.HttpOnly = HttpOnlyPolicy.None;
                options.Secure = _environment.IsDevelopment()
                    ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
            });
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Events = new CookieAuthenticationEvents
                    {                          
                        OnRedirectToLogin = redirectContext =>
                        {
                            redirectContext.HttpContext.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        }
                    };  
                });

            var databaseOptions = _configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>();
            services.RegisterInfrastructure(databaseOptions);
            services.RegisterAuthContextHandlers();
            services.RegisterDataCatalogContextHandlers();
            services.RegisterOrderContextHandlers();
            services.RegisterSupplyContextHandlers();
            services.RegisterNotificationContextHandlers();

            if (!_environment.IsDevelopment())
            {
                services.AddCors(
                    options => options.AddPolicy("CorsPolicy", builder =>
                    {
                        builder.WithOrigins(SiteDNS)
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    }));
            }

            services.AddControllers();
            services.AddMvc();
            services.AddSwaggerGen();
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
                app.UseHsts();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseWhen(context =>
                    context.Request.Path.StartsWithSegments("/admin") ||
                    context.Request.Path.StartsWithSegments("/store"),
                appBuilder =>
                {
                    appBuilder.UseStatusCodePagesWithReExecute("/");
                });

            if (!env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Goods Reseller API V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append(
                        "Cache-Control",
                        $"public,max-age={TimeSpan.FromMinutes(10).TotalSeconds}");
                }
            });
            
            app.UseRouting();

            if (!_environment.IsDevelopment())
            {
                app.UseCors("CorsPolicy");
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
        }
    }
}

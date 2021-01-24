using System.Threading.Tasks;
using GoodsReseller.Api.Middlewares;
using GoodsReseller.AuthContext.Handlers;
using GoodsReseller.DataCatalogContext.Handlers;
using GoodsReseller.Infrastructure;
using GoodsReseller.Infrastructure.Configurations;
using GoodsReseller.OrderContext.Handlers;
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

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var goodsResellerDatabaseSection = _configuration.GetSection(nameof(GoodsResellerDatabaseOptions));
            services.Configure<GoodsResellerDatabaseOptions>(goodsResellerDatabaseSection);
            
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
            
            services.RegisterInfrastructure(goodsResellerDatabaseSection.Get<GoodsResellerDatabaseOptions>());
            services.RegisterAuthContextHandlers();
            services.RegisterDataCatalogContextHandlers();
            services.RegisterOrderContextHandlers();
            
            services.AddCors(options => options.AddPolicy("LandingCorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader();
            }));
            
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
            app.UseWhen(context => context.Request.Path.StartsWithSegments("/admin"),
                appBuilder =>
                {
                    appBuilder.UseStatusCodePagesWithReExecute("/");
                });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Goods Reseller API V1");
            });


            // app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            app.UseRouting();
            
            app.UseCors("LandingCorsPolicy");

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

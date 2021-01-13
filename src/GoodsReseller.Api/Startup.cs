using GoodsReseller.Api.Middlewares;
using GoodsReseller.AuthContext.Handlers;
using GoodsReseller.DataCatalogContext.Handlers;
using GoodsReseller.Infrastructure;
using GoodsReseller.Infrastructure.Configurations;
using GoodsReseller.OrderContext.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GoodsReseller.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var goodsResellerDatabaseSection = _configuration.GetSection(nameof(GoodsResellerDatabaseOptions));
            services.Configure<GoodsResellerDatabaseOptions>(goodsResellerDatabaseSection);
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Auth/Login");
                });
            
            services.RegisterInfrastructure(goodsResellerDatabaseSection.Get<GoodsResellerDatabaseOptions>());
            services.RegisterAuthContextHandlers();
            services.RegisterDataCatalogContextHandlers();
            services.RegisterOrderContextHandlers();
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
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Goods Reseller API V1");
            });


            // app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            app.UseRouting();

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

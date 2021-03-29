using System.Linq;
using System.Threading.Tasks;
using GoodsReseller.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GoodsReseller.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            // Environment.GetEnvironmentVariable("TESTABLE_DATABASE_CONNECTION_STRING")

            if (args.Contains("--updateDatabase"))
            {
                var context = host.Services.GetRequiredService<GoodsResellerDbContext>();
                await context.Database.MigrateAsync();
            }
            else
            {
                await host.RunAsync();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

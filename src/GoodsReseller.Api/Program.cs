using System;
using System.Linq;
using System.Threading.Tasks;
using GoodsReseller.AuthContext.Domain.Users.Entities;
using GoodsReseller.AuthContext.Domain.Users.ValueObjects;
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

            if (args.Contains("--updateDatabase"))
            {
                var context = host.Services.GetRequiredService<GoodsResellerDbContext>();
                await context.Database.MigrateAsync();

                if (args.Contains("--createAdmin"))
                {
                    var userEmail = "test@test";
                    var existing = await context.Users.FirstOrDefaultAsync(x => x.Email == userEmail && !x.IsRemoved);
                    if (existing == null)
                    {
                        await context.Users.AddAsync(
                            new User(
                                Guid.NewGuid(), 
                                1,
                                userEmail,
                                PasswordHash.Generate("qwe123"),
                                Role.Admin.ToString()));

                        await context.SaveChangesAsync();
                    }
                }
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

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GoodsReseller.AuthContext.Domain.Users.Entities;
using GoodsReseller.AuthContext.Domain.Users.ValueObjects;
using GoodsReseller.DataCatalogContext.Models.Products;
using GoodsReseller.Infrastructure;
using GoodsReseller.NotificationContext.Contracts;
using GoodsReseller.SeedWork.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

[assembly: InternalsVisibleTo("GoodsReseller.UnitTests")]

namespace GoodsReseller.Api
{
    internal static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            using var scope = host.Services.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(new SubscribeOnChatsRequest());

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

                if (args.Contains("--createProducts"))
                {
                    var productSet = "Product set";
                    var existing = await context.Products.FirstOrDefaultAsync(x => x.Name == productSet);
                    if (existing == null)
                    {
                        var firstProduct = new Product(
                            Guid.NewGuid(),
                            1,
                            "first-product",
                            "First product",
                            "",
                            new Money(100),
                            new Discount(0));
                        await context.Products.AddAsync(firstProduct);
                        
                        var secondProduct = new Product(
                            Guid.NewGuid(),
                            1,
                            "second-product",
                            "Second product",
                            "",
                            new Money(200),
                            new Discount(0));
                        await context.Products.AddAsync(secondProduct);
                        
                        var set = new Product(
                            Guid.NewGuid(),
                            1,
                            "set",
                            productSet,
                            productSet,
                            new Money(500),
                            new Discount(0),
                            productIds: new[] { firstProduct.Id, secondProduct.Id });
                        await context.Products.AddAsync(set);
                        
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

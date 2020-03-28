using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using WebPlayground.Domain.Management;

namespace WebPlayground.Infrastructure
{
    public static class DbInitializer
    {
        public static IHost InitializeAndSeed(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<BankContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return host;
        }

        public static void SeedSettings(BankContext context)
        {
            if (!context.Settings.Any())
            {
                var settings = new List<Settings>
                {
                    new Settings { }
                };

                context.AddRange(settings);
                context.SaveChanges();
            }
        }
    }
}

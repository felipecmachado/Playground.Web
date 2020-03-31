using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using Playground.Web.Domain.Branch;
using Playground.Web.Domain.CheckingAccount;
using Playground.Web.Domain.Management;

namespace Playground.Web.Infrastructure
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

                SeedSettings(context);
                SeedUsers(context);
                SeedBranches(context);
                SeedAccounts(context);
            }

            return host;
        }

        public static void SeedSettings(BankContext context)
        {
            if (!context.Settings.Any())
            {
                var settings = new List<Settings>
                {
                    new Settings { Key = "AnnualInterestRate", Value = "0.025" },
                    new Settings { Key = "ServiceFee", Value = "19.90" },
                    new Settings { Key = "BankCode", Value = "9999" }
                };

                context.AddRange(settings);
                context.SaveChanges();
            }
        }

        public static void SeedUsers(BankContext context)
        {
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User { Login = "admin", Password = "admin", FirstName = "Administrator", LastName = "", EmailAddress = "felipecmachado@outlook.com", PhoneNumber = "51 99999999" },
                    new User { Login = "FELIPE", Password = "F1E2L3", FirstName = "Felipe", LastName = "Machado", EmailAddress = "felipecmachado@outlook.com", PhoneNumber = "51 99999999" }
                };

                context.AddRange(users);
                context.SaveChanges();
            }
        }

        public static void SeedBranches(BankContext context)
        {
            if (!context.Branches.Any())
            {
                var branches = new List<Branch>
                {
                    new Branch { BranchCode = "A01" },
                    new Branch { BranchCode = "A02" }
                };

                context.AddRange(branches);
                context.SaveChanges();
            }
        }

        public static void SeedAccounts(BankContext context)
        {
            if (!context.CheckingAccounts.Any())
            {
                var accounts = new List<CheckingAccount>();

                var branch = context.Branches.FirstOrDefault();

                foreach(var user in context.Users.OrderBy(x => x.UserId).ToList())
                {
                    var acc = new CheckingAccount() { 
                        UserId = user.UserId,
                        AccountNumber = $"{user.FirstName.Substring(0,3).ToUpper()}001",
                        BranchId = branch.BranchId,
                        Balance = 300m,
                        Branch = branch };

                    acc.GenerateToken();
                    accounts.Add(acc);
                }

                context.AddRange(accounts);
                context.SaveChanges();
            }
        }
    }
}

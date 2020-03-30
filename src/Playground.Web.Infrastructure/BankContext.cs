using Microsoft.EntityFrameworkCore;
using Playground.Web.Domain.Branch;
using Playground.Web.Domain.CheckingAccount;
using Playground.Web.Domain.Management;

namespace Playground.Web.Infrastructure
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasIndex(b => b.Timestamp);

            modelBuilder.Entity<Transaction>()
                .HasOne(x => x.Deposit).WithOne(x => x.Transaction)
                .HasForeignKey<Deposit>(b => b.TransactionGuid);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public DbSet<Balance> Balances { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<CheckingAccount> CheckingAccounts { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Withdraw> Withdraws { get; set; }
    }
}


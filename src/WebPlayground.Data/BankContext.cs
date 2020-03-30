using Microsoft.EntityFrameworkCore;
using System;
using WebPlayground.Domain.Branch;
using WebPlayground.Domain.CheckingAccount;
using WebPlayground.Domain.Management;

namespace WebPlayground.Infrastructure
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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public DbSet<Balance> Balances { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<CheckingAccount> CheckingAccounts { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}


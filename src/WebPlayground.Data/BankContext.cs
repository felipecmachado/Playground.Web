using Microsoft.EntityFrameworkCore;
using System;
using WebPlayground.Domain.CheckingAccount;
using WebPlayground.Domain.Management;

namespace WebPlayground.Infrastructure
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}


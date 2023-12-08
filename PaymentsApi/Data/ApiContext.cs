
using Microsoft.EntityFrameworkCore;
using PaymentsApi.Models;

namespace PaymentsApi.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Account> Accounts { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().HasKey(t => t.transactionId);
            modelBuilder.Entity<Account>().HasKey(a => a.accountNumber);
            // Other configurations...
        }
    }
}

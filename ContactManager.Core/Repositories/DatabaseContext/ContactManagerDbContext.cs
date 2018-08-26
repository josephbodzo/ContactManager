using ContactManager.Core.Entities;
using ContactManager.Core.Repositories.DatabaseContext.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Core.Repositories.DatabaseContext
{
    public class ContactManagerDbContext: DbContext
    {
        public DbSet<PhoneBook> PhoneBooks { get; set; }
        public DbSet<PhoneEntry> PhoneEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO: Move to config
            optionsBuilder.UseSqlite("Data Source=bin\\ContactManager.db");
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PhoneBookConfiguration());
            modelBuilder.ApplyConfiguration(new PhoneEntryConfiguration());
            modelBuilder.ApplyConfiguration(new PhoneBookEntryConfiguration());
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MyBookAPI.Domain.Common;
using MyBookAPI.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Persistance
{
    public class MyBookDbContext : DbContext
    {
        public MyBookDbContext(DbContextOptions<MyBookDbContext> options): base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<PublishingHouse> PublishingHouses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().OwnsOne(p => p.AuthorName);
            modelBuilder.Entity<User>().OwnsOne(p => p.UserName);
            modelBuilder.Entity<User>().OwnsOne(p => p.Address);
            modelBuilder.Entity<PublishingHouse>().OwnsOne(p => p.Address);
        }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellatonToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = string.Empty;
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.StatusId = 1;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = string.Empty;
                        entry.Entity.Modified = DateTime.Now;
                        break;

                    case EntityState.Deleted:
                        entry.Entity.ModifiedBy = string.Empty;
                        entry.Entity.Modified = DateTime.Now;
                        entry.Entity.Inactivated = DateTime.Now;
                        entry.Entity.InactivatedBy = string.Empty;
                        entry.Entity.StatusId = 0;
                        entry.State = EntityState.Modified;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellatonToken);
        }
    }
}

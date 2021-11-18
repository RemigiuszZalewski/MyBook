using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Interfaces;
using MyBookAPI.Domain.Common;
using MyBookAPI.Domain.Entities;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Persistance
{
    public class MyBookDbContext : DbContext, IMyBookDbContext
    {
        private readonly IDateTime _dateTime;

        public MyBookDbContext(DbContextOptions<MyBookDbContext> options) : base(options)
        {

        }
        public MyBookDbContext(DbContextOptions<MyBookDbContext> options, IDateTime dateTime): base(options)
        {
            _dateTime = dateTime;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<PublishingHouse> PublishingHouses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.SeedData();
        }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellatonToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = string.Empty;
                        entry.Entity.Created = _dateTime.Now;
                        entry.Entity.StatusId = 1;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = string.Empty;
                        entry.Entity.Modified = _dateTime.Now;
                        break;

                    case EntityState.Deleted:
                        entry.Entity.ModifiedBy = string.Empty;
                        entry.Entity.Modified = _dateTime.Now;
                        entry.Entity.Inactivated = _dateTime.Now;
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

using Microsoft.EntityFrameworkCore;
using MyBookAPI.Domain.Common;
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

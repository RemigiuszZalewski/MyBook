using Microsoft.EntityFrameworkCore;
using MyBookAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Common.Interfaces
{
    public interface IMyBookDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<PublishingHouse> PublishingHouses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> Categories { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellatonToken = new CancellationToken());
    }
}

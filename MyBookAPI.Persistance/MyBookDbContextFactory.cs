using Microsoft.EntityFrameworkCore;

namespace MyBookAPI.Persistance
{
    public class MyBookDbContextFactory : DesignTimeDbContextFactoryBase<MyBookDbContext>
    {
        protected override MyBookDbContext CreateNewInstance(DbContextOptions<MyBookDbContext> options)
        {
            return new MyBookDbContext(options);
        }
    }
}

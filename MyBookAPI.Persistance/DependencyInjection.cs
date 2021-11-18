using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBookAPI.Application.Common.Interfaces;

namespace MyBookAPI.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyBookDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MyBookAPIDatabase")));
            services.AddScoped<IMyBookDbContext, MyBookDbContext>();
            return services;
        }
    }
}

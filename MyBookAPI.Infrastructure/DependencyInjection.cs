using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBookAPI.Application.Common.Interfaces;
using MyBookAPI.Infrastructure.FileStore;
using MyBookAPI.Infrastructure.Services;

namespace MyBookAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IFileStore, FileStore.FileStore>();
            services.AddTransient<IFileWrapper, FileWrapper>();
            services.AddTransient<IDirectoryWrapper, DirectoryWrapper>();
            return services;
        }
    }
}

using IdentityModel.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyBookAPI.Application.Common.Interfaces;
using MyBookAPI.Persistance;
using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyBookAPI.WebApi.IntegrationTests.Common
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            try
            {
                builder.ConfigureServices(services =>
                {
                    var provider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                    services.AddDbContext<MyBookDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDb");
                        options.UseInternalServiceProvider(provider);
                    });

                    services.AddScoped<IMyBookDbContext>(x => x.GetService<MyBookDbContext>());
                    var sp = services.BuildServiceProvider();
                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<MyBookDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    context.Database.EnsureCreated();

                    try
                    {
                        Utilities.InitializeDatabase(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            $"database. Error: {ex.Message}");
                    }
                })
               .UseSerilog()
               .UseEnvironment("Test");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<HttpClient> GetAuthenticatedClientAsync()
        {
            var client = CreateClient();
            var token = await GetAccessTokenAsync(client, "alice", "Pass123$");
            client.SetBearerToken(token);

            return client;
        }

        private async Task<string> GetAccessTokenAsync(HttpClient client, string username, string password)
        {
            var discoveryDoc = await client.GetDiscoveryDocumentAsync();

            if (discoveryDoc.IsError)
            {
                throw new Exception(discoveryDoc.Error);
            }

            var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discoveryDoc.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "openid profile MyBookAPIAPI mybook",
                UserName = username,
                Password = password
            });

            if (response.IsError)
            {
                throw new Exception(response.Error);
            }

            return response.AccessToken;
        }
    }
}

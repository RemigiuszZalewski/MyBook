using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyBookAPI.Persistance;
using MyBookAPI.Infrastructure;
using System;
using System.IO;
using MyBookAPI.Application;
using Serilog;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyBookAPI.Services;
using MyBookAPI.Application.Common.Interfaces;
using System.Collections.Generic;
using MyBookAPI.Infrastructure.IdentityServer;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;

namespace MyBookAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddPersistance(Configuration);
            services.AddInfrastructure(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin());
            });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));
            
            if (Environment.IsEnvironment("Test"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MyBookAPIDatabase")));

                services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<ApplicationDbContext>();

                services.AddIdentityServer()
                        .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                        {
                            options.ApiResources.Add(new ApiResource("mybook"));
                            options.ApiScopes.Add(new ApiScope("mybook"));
                            options.Clients.Add(new Client
                            {
                                ClientId = "client",
                                AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
                                ClientSecrets = { new IdentityServer4.Models.Secret("secret".Sha256()) },
                                AllowedScopes = { "openid", "profile", "MyBookAPIAPI", "mybook" }
                            });
                        }).AddTestUsers(new List<TestUser>
                        {
                                new TestUser
                                {
                                    SubjectId = "4B434A88-212D-4A4D-A17C-F35102D73CBB",
                                    Username = "alice",
                                    Password = "Pass123$",
                                    Claims = new List<Claim>
                                    {
                                        new Claim(JwtClaimTypes.Email, "alice@user.com"),
                                        new Claim(ClaimTypes.Name, "alice")
                                    }
                                }
                        });

                services.AddAuthentication("Bearer").AddIdentityServerJwt();
            }
            else
            {
                services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateAudience = false,
                    };
                });

                services.AddAuthorization(options =>
                {
                    options.AddPolicy("ApiScope", policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim("scope", "mybook");
                    });
                });
            }

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:5001/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "mybook", "Full access" },
                                { "user", "User information" },
                                { "openid", "OpenId scope" }
                            }
                        }
                    }
                });
                c.OperationFilter<AuthorizeCheckOperationFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "MyBookAPI",
                    Version = "v1",
                    Description = "Application used to exchange and trade books.",
                    Contact = new OpenApiContact
                    {
                        Name = "Remigiusz Zalewski",
                        Email = "remigiuszzalewski@yahoo.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://github.com/RemigiuszZalewski/MyBook/blob/main/LICENSE")
                    }
                });
                var filePath = Path.Combine(AppContext.BaseDirectory, "MyBookAPI.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBookAPI v1");
                c.OAuthClientId("swagger");
                c.OAuth2RedirectUrl("https://localhost:44389/swagger/oauth2-redirect.html");
                c.OAuthUsePkce();
            });
            

            app.UseHttpsRedirection();
            app.UseAuthentication();

            if (Environment.IsEnvironment("Test"))
            {
                app.UseIdentityServer();
            }

            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

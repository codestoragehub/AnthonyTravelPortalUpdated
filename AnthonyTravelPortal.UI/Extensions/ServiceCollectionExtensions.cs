using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Services;
using AnthonyTravelPortal.Infrastructure.Services;
using AnthonyTravelPortal.UI.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using AnthonyTravelPortal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using AnthonyTravelPortal.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace AnthonyTravelPortal.UI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddUiLayer(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
        {
            services.AddRouting();
            services.AddPersistenceContexts(configuration);
            services.AddNewPersistenceContexts(configuration);
            services.AddAuthenticationScheme(configuration);
            services.AddWebOptimizerBundler(environment);
            services.AddAuthServices(configuration);

            services
                .AddRazorPages()
                .AddRazorRuntimeCompilation();

            services.AddControllersWithViews();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
        }

        private static void AddWebOptimizerBundler(this IServiceCollection services, IHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                services.AddWebOptimizer(false, false);
                return;
            }

        }

        private static void AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        }
        private static void AddAuthenticationScheme(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMvc(o =>
                {
                    //Add Authentication to all Controllers by default.
                    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                    o.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddFluentValidation(fluentValidationConfiguration =>
                {
                    fluentValidationConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>();
                });
        }

        private static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AnthonyTravelPortalDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ApplicationConnection"), x =>
                {
                    x.MigrationsAssembly("AnthonyTravelPortal.Infrastructure");
                    x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
            })
                .AddEntityFrameworkStores<AnthonyTravelPortalDbContext>().AddDefaultUI()
                .AddDefaultTokenProviders();
        }
        
        private static void AddNewPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AnthonyTravelPortalNewDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ApplicationNewConnection"), x =>
                {
                    x.MigrationsAssembly("AnthonyTravelPortal.Infrastructure");
                    x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            });

            //services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            //{
            //    options.SignIn.RequireConfirmedAccount = true;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequiredLength = 8;
            //})
            //    .AddEntityFrameworkStores<AnthonyTravelPortalNewDbContext>().AddDefaultUI()
            //    .AddDefaultTokenProviders();
        }
    }
}

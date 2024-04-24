using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Contexts;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Services;
using AnthonyTravelPortal.ApplicationLayer.Common.Services;
using AnthonyTravelPortal.Infrastructure.Persistence;
using AnthonyTravelPortal.Infrastructure.Persistence.Repositories;
using AnthonyTravelPortal.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddPersistenceContexts()
                .AddRepositories()
                .AddSharedServices()
                .AddApplicationInsightsTelemetry()
                .AddCustomHealthChecks();
        }
        public static void AddNewInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddNewPersistenceContexts()
                .AddNewRepositories()
                .AddSharedServices()
                .AddApplicationInsightsTelemetry()
                .AddNewCustomHealthChecks();
        }
        private static IServiceCollection AddPersistenceContexts(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IAnthonyTravelPortalDbContext, AnthonyTravelPortalDbContext>();

            return services;
        }
        private static IServiceCollection AddNewPersistenceContexts(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IAnthonyTravelPortalNewDbContext, AnthonyTravelPortalNewDbContext>();
            return services;
        }

        private static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
        {
            services
                .AddHealthChecks()
                .AddDbContextCheck<AnthonyTravelPortalDbContext>();

            return services;
        }
        private static IServiceCollection AddNewCustomHealthChecks(this IServiceCollection services)
        {
            services
                .AddHealthChecks()
                .AddDbContextCheck<AnthonyTravelPortalNewDbContext>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            //services.RegisterAssemblyPublicNonGenericClasses()
            //    .Where(c => c.Name.EndsWith("Repository"))
            //    .AsPublicImplementedInterfaces();
            services.AddTransient<IInstitutionRepository, InstitutionRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            

            return services;
        }
        private static IServiceCollection AddNewRepositories(this IServiceCollection services)
        {
            services.AddTransient<IInstitutionNewRepository, InstitutionNewRepository>();
            services.AddTransient(typeof(IRepositoryAsyncNew<>), typeof(RepositoryAsyncNew<>));
            services.AddTransient<IUnitOfWorkNew, UnitOfWorkNew>();
            services.AddTransient<IAnthonyCloneServices, AnthonyCloneServices>();

            return services;
        }

        private static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
            return services;
        }

     }
}

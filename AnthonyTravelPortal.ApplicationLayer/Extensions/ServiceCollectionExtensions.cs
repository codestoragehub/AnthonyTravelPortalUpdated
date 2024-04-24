using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using AnthonyTravelPortal.ApplicationLayer.Authorization.Extensions.DependencyInjection;
using AnthonyTravelPortal.ApplicationLayer.Common.Behaviours;

namespace AnthonyTravelPortal.ApplicationLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddDomainAuthorization();
        }

        private static IServiceCollection AddDomainAuthorization(this IServiceCollection services)
        {
            services
                .AddMediatorAuthorization(Assembly.GetExecutingAssembly())
                .AddAuthorizersFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }

    }
}

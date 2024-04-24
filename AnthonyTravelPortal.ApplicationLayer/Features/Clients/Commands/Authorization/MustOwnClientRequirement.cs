using AnthonyTravelPortal.ApplicationLayer.Authorization.Interfaces;
using AnthonyTravelPortal.ApplicationLayer.Authorization.Models;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Contexts;
using AnthonyTravelPortal.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Clients.Commands.Authorization
{
    public class MustOwnClientRequirement : IAuthorizationRequirement
    {
        public int? ClientId { get; set; }
        public string? UserId { get; set; }

        private class MustOwnClientRequirementHandler : IAuthorizationHandler<MustOwnClientRequirement>
        {
            private readonly IAnthonyTravelPortalDbContext _dbContext;

            public MustOwnClientRequirementHandler(IAnthonyTravelPortalDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<AuthorizationResult> Handle(
                MustOwnClientRequirement request,
                CancellationToken cancellationToken)
            {
                var isCreateClientCommand = request.ClientId == null;
                if (isCreateClientCommand)
                {
                    return AuthorizationResult.Succeed();
                }    
                
                var isUserClient = await _dbContext.Clients
                    .AnyAsync(x =>
                        x.Client_ID == request.ClientId, cancellationToken);

                return isUserClient
                    ? AuthorizationResult.Succeed()
                    : AuthorizationResult.Fail("You don't own this Client to view.");
            }
        }
    }
}

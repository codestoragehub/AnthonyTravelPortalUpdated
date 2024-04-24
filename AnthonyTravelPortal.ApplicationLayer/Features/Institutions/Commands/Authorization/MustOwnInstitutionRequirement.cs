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

namespace AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Commands.Authorization
{
    public class MustOwnInstitutionRequirement : IAuthorizationRequirement
    {
        public int? InstitutionId { get; set; }
        public string? UserId { get; set; }

        private class MustOwnInstitutionRequirementHandler : IAuthorizationHandler<MustOwnInstitutionRequirement>
        {
            private readonly IAnthonyTravelPortalDbContext _dbContext;

            public MustOwnInstitutionRequirementHandler(IAnthonyTravelPortalDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<AuthorizationResult> Handle(
                MustOwnInstitutionRequirement request,
                CancellationToken cancellationToken)
            {
                var isCreateInstitutionCommand = request.InstitutionId == null;
                if (isCreateInstitutionCommand)
                {
                    return AuthorizationResult.Succeed();
                }    
                
                var isUserInstitution = await _dbContext.Institutions
                    .AnyAsync(x =>
                        x.Institution_ID == request.InstitutionId, cancellationToken);

                return isUserInstitution
                    ? AuthorizationResult.Succeed()
                    : AuthorizationResult.Fail("You don't own this Institution to view.");
            }
        }
    }
}

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

namespace AnthonyTravelPortal.ApplicationLayer.Features.Users.Commands.Authorization
{
    public class MustOwnUserRequirement : IAuthorizationRequirement
    {
        public string? UserId { get; set; }
        public string? user_ID { get; set; }

        private class MustOwnUserRequirementHandler : IAuthorizationHandler<MustOwnUserRequirement>
        {
            private readonly IAnthonyTravelPortalDbContext _dbContext;

            public MustOwnUserRequirementHandler(IAnthonyTravelPortalDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<AuthorizationResult> Handle(
                MustOwnUserRequirement request,
                CancellationToken cancellationToken)
            {
                var isCreateUserCommand = request.user_ID == null;
                if (isCreateUserCommand)
                {
                    return AuthorizationResult.Succeed();
                }    
                
                var isUserUser = await _dbContext.Users
                    .AnyAsync(x =>
                        x.User_ID == request.user_ID, cancellationToken);

                return isUserUser
                    ? AuthorizationResult.Succeed()
                    : AuthorizationResult.Fail("You don't own this User to view.");
            }
        }
    }
}

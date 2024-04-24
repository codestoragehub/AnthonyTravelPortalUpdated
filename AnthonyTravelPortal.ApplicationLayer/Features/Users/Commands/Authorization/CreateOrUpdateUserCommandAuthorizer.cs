using AnthonyTravelPortal.ApplicationLayer.Authorization.Models;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Services;
using AnthonyTravelPortal.ApplicationLayer.Features.Users.Commands.CreateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Users.Commands.Authorization
{
    
public class CreateOrUpdateUserCommandAuthorizer : AbstractRequestAuthorizer<CreateOrUpdateUserCommand>
{
    private readonly ICurrentUserService _currentUserService;

    public CreateOrUpdateUserCommandAuthorizer(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override void BuildPolicy(CreateOrUpdateUserCommand request)
    {
        UseRequirement(new MustOwnUserRequirement
        {
            UserId = _currentUserService.UserId
        });
    }
}
}

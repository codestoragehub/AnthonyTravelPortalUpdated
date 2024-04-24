using AnthonyTravelPortal.ApplicationLayer.Authorization.Models;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Services;
using AnthonyTravelPortal.ApplicationLayer.Features.Users.Commands.DeleteUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Users.Commands.Authorization
{
    public class DeleteUserCommandAuthorizer : AbstractRequestAuthorizer<DeleteUserCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public DeleteUserCommandAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override void BuildPolicy(DeleteUserCommand request)
        {
            UseRequirement(new MustOwnUserRequirement
            {
                user_ID = request.User_ID,
                UserId = _currentUserService.UserId
            });
        }
    }
}

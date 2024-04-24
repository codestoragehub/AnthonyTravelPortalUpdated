using AnthonyTravelPortal.ApplicationLayer.Authorization.Models;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Services;
using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Commands.DeleteClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Clients.Commands.Authorization
{
    public class DeleteClientCommandAuthorizer : AbstractRequestAuthorizer<DeleteClientCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public DeleteClientCommandAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override void BuildPolicy(DeleteClientCommand request)
        {
            UseRequirement(new MustOwnClientRequirement
            {
                ClientId = request.ClientId,
                UserId = _currentUserService.UserId
            });
        }
    }
}
